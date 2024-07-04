import pandas as pd
from sqlalchemy import create_engine, Table, MetaData
from sqlalchemy.orm import sessionmaker
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy import Column, String, Float
from sklearn.model_selection import cross_val_score, StratifiedKFold
from sklearn.preprocessing import LabelEncoder
from sqlalchemy_utils import UUIDType
from sklearn.ensemble import RandomForestClassifier
import numpy as np
import urllib
import uuid


# Database connection using SQLAlchemy
params = urllib.parse.quote_plus('DRIVER={ODBC Driver 17 for SQL Server};SERVER=(localdb)\\mssqllocaldb;DATABASE=EComerence;Trusted_Connection=yes')
engine = create_engine(f'mssql+pyodbc:///?odbc_connect={params}')

query = "SELECT * FROM dbo.Orders"
orders_df = pd.read_sql(query, engine)

# Preprocess data
orders_df['BrandTag'] = orders_df['BrandTag'].map({0: 'premium', 1: 'standard', 2: 'budget'})

# Encode categorical variables
label_encoder = LabelEncoder()
orders_df['ProducerName'] = label_encoder.fit_transform(orders_df['ProducerName'])
orders_df['BrandTag'] = label_encoder.fit_transform(orders_df['BrandTag'])

# Create translation tables
user_id_map = {id: idx for idx, id in enumerate(orders_df['UserId'].unique())}
product_id_map = {id: idx for idx, id in enumerate(orders_df['ProductId'].unique())}
orders_df['UserId'] = orders_df['UserId'].map(user_id_map)
orders_df['ProductId'] = orders_df['ProductId'].map(product_id_map)

# Feature Engineering
user_brand_tag_freq = orders_df.groupby(['UserId', 'BrandTag']).size().unstack(fill_value=0)
user_producer_freq = orders_df.groupby(['UserId', 'ProducerName']).size().unstack(fill_value=0)

features = user_brand_tag_freq.join(user_producer_freq, how='inner', lsuffix='_brand', rsuffix='_producer').reset_index()
features = features.fillna(0)

# Generate negative samples
users = orders_df['UserId'].unique()
products = orders_df['ProductId'].unique()

negative_samples = []
for user in users:
    for product in products:
        if not ((orders_df['UserId'] == user) & (orders_df['ProductId'] == product)).any():
            negative_samples.append([user, product, 0])

negative_df = pd.DataFrame(negative_samples, columns=['UserId', 'ProductId', 'target'])

# Undersample negative samples to match the number of positive samples
negative_df = negative_df.sample(len(orders_df), random_state=42)


# Add BrandTag and ProducerName to negative samples by using average or mode values
avg_brand_tag = orders_df['BrandTag'].mode()[0]
avg_producer_name = orders_df['ProducerName'].mode()[0]
 
negative_df['BrandTag'] = avg_brand_tag
negative_df['ProducerName'] = avg_producer_name

# Add target column to the positive samples
orders_df['target'] = 1

# Combine positive and negative samples
data = pd.concat([orders_df[['UserId', 'ProductId', 'target', 'BrandTag', 'ProducerName']], negative_df], ignore_index=True)

# Prepare the data for model training
features = data[['UserId', 'BrandTag', 'ProducerName']].copy()
user_brand_tag_freq = features.groupby(['UserId', 'BrandTag']).size().unstack(fill_value=0)
user_producer_freq = features.groupby(['UserId', 'ProducerName']).size().unstack(fill_value=0)

combined_features = user_brand_tag_freq.join(user_producer_freq, how='inner', lsuffix='_brand', rsuffix='_producer').reset_index()
combined_features = combined_features.fillna(0)

# Add ProductId to features
combined_features = combined_features.merge(data[['UserId', 'ProductId']], on='UserId')

# Convert column names to strings
combined_features.columns = combined_features.columns.astype(str)


# Prepare the data for model training
X = data.drop(columns=['UserId', 'target'])
y = data['target']

# Convert column names to strings
X.columns = X.columns.astype(str)

# Model initialization
class_weights = {0: 1, 1: 100}
model = RandomForestClassifier(n_estimators=400, random_state=42,class_weight=class_weights,min_samples_split=2)

# Cross-validation
cv = StratifiedKFold(n_splits=2)
cv_scores = cross_val_score(model, X, y, cv=cv, scoring='accuracy')

print(f'Cross-validation scores: {cv_scores}')
print(f'Average cross-validation score: {np.mean(cv_scores)}')

# Train the model on the full dataset
model.fit(X, y)

# Check class distribution in the training set
print("Class distribution in the full dataset:", np.bincount(y))


# Prediction function
def predict_probability(user_id, product_id):
    if user_id not in user_id_map or product_id not in product_id_map:
        return 0.0  # If user_id or product_id is not in the map, return 0 probability

    user_id_num = user_id_map[user_id]
    product_id_num = product_id_map[product_id]

    user_data = features[features['UserId'] == user_id_num].drop(columns=['UserId'])
    if user_data.empty:
        return 0.0  # If no data for user, return 0 probability

    # Include ProductId in user_data
    user_data = user_data.assign(ProductId=product_id_num)
    
    # Ensure the column names match the training set
    missing_cols = set(X.columns) - set(user_data.columns)
    for c in missing_cols:
        user_data[c] = 0
    user_data = user_data[X.columns]

    proba = model.predict_proba(user_data)
    if proba.shape[1] == 1:
        return 0.0  # If the model predicts only one class, assume probability for the positive class is 0.
    return proba[0][1]

# Example usage
example_user_id = list(user_id_map.keys())[0]  # Just an example, use actual user_id

# Loop through all products and predict the probability for each
results = []
for product_id in product_id_map.keys():
    for user_id in user_id_map.keys():
        probability = predict_probability(user_id, product_id)
        results.append((user_id,product_id, probability))

# Print the results
for user_id, product_id, probability in results:
    print(f'The probability that user {user_id} will buy product {product_id} is {probability}')
  
# Create a SQLAlchemy table for storing results
Base = declarative_base()

class UserProductProbability(Base):
    __tablename__ = 'UserProductProbabilitys'
    UserId = Column(UUIDType, primary_key=True)
    ProductId = Column(UUIDType, primary_key=True)
    Probablity = Column(Float, nullable=False)

Base.metadata.create_all(engine)

# Insert results into the table
Session = sessionmaker(bind=engine)
session = Session()

# Delete all records from the UserProductProbabilitys table
session.query(UserProductProbability).delete()

# Commit the changes to the database
session.commit()

for result in results:
    if len(result) >= 3:  # Ensure there are at least 3 elements in the tuple
        user_product_probability = UserProductProbability(UserId=uuid.UUID(result[0]), ProductId=uuid.UUID(result[1]), Probablity=result[2])
        session.add(user_product_probability)

session.commit()
session.close()

print("Results saved to the database.")