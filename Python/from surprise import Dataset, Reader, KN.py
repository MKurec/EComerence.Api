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

# Add ProductId to features
features = features.merge(orders_df[['UserId', 'ProductId']], on='UserId')

# Convert column names to strings
features.columns = features.columns.astype(str)

# Target variable
orders_df['target'] = 1  # We assume the user bought the product
data = features.merge(orders_df[['UserId', 'ProductId', 'target']], on=['UserId', 'ProductId'], how='left').fillna(0)

# Check if there are enough samples for both classes
if data['target'].sum() == 0 or (data['target'] == 0).sum() == 0:
    print("Warning: The target variable does not have enough samples for both classes.")
    # Add synthetic data for the minority class
    synthetic_data = data.sample(frac=0.1, random_state=42)
    synthetic_data['target'] = 1 - synthetic_data['target']
    data = pd.concat([data, synthetic_data], ignore_index=True)

# Prepare the data for model training
X = data.drop(columns=['UserId', 'target'])
y = data['target']

# Convert column names to strings
X.columns = X.columns.astype(str)

# Model initialization
model = RandomForestClassifier(n_estimators=100, random_state=42,class_weight='balanced',min_samples_split=10)

# Cross-validation
cv = StratifiedKFold(n_splits=2, random_state=None, shuffle=True)
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

for result in results:
    if len(result) >= 3:  # Ensure there are at least 3 elements in the tuple
        user_product_probability = UserProductProbability(UserId=uuid.UUID(result[0]), ProductId=uuid.UUID(result[1]), Probablity=result[2])
        session.add(user_product_probability)

session.commit()

session.commit()
session.close()

print("Results saved to the database.")