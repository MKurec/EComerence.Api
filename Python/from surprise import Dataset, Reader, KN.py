import pandas as pd
from sqlalchemy import create_engine
from sklearn.model_selection import train_test_split
from sklearn.preprocessing import LabelEncoder
from sklearn.ensemble import RandomForestClassifier
import numpy as np
import urllib

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

# Split data
X = data.drop(columns=['UserId', 'target'])
y = data['target']

# Convert column names to strings
X.columns = X.columns.astype(str)

# Check dimensions of X and y
print("X shape:", X.shape)
print("y shape:", y.shape)

X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=42)

# Check dimensions after split
print("X_train shape:", X_train.shape)
print("X_test shape:", X_test.shape)
print("y_train shape:", y_train.shape)
print("y_test shape:", y_test.shape)

# Model training
model = RandomForestClassifier(n_estimators=100, random_state=42)
model.fit(X_train, y_train)

# Check class distribution in the training set
print("Class distribution in training set:", np.bincount(y_train))

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
    missing_cols = set(X_train.columns) - set(user_data.columns)
    for c in missing_cols:
        user_data[c] = 0
    user_data = user_data[X_train.columns]

    proba = model.predict_proba(user_data)
    if proba.shape[1] == 1:
        return 0.0  # If the model predicts only one class, assume probability for the positive class is 0.
    return proba[0][1]

# Example usage
example_user_id = list(user_id_map.keys())[0]  # Just an example, use actual user_id

# Loop through all products and predict the probability for each
results = []
for product_id in product_id_map.keys():
    probability = predict_probability(example_user_id, product_id)
    results.append((product_id, probability))

# Print the results
for product_id, probability in results:
    print(f'The probability that user {example_user_id} will buy product {product_id} is {probability}')
  