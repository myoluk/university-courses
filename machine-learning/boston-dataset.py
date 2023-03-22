"""
Introduction to Machine Learning: Boston Dataset
"""

#libraries
import numpy as np
import matplotlib.pyplot as plt
import pandas as pd
from sklearn.datasets import load_boston
from sklearn.model_selection import train_test_split
from sklearn.linear_model import LinearRegression
from sklearn.metrics import mean_squared_error

# 1. load boston dataset and print
boston_dataset = load_boston()
print(boston_dataset.keys())

# 2. print descriptions
print(boston_dataset.DESCR)

# 3. print first few rows
boston = pd.DataFrame(boston_dataset.data)
print(boston.head())

# 4. correlations between input features and outputs
boston['MEDV'] = boston_dataset.target
correlation_matrix = boston.corr().round(2)
print(correlation_matrix)

# 5. length of X and y 
ind1 = 0;
ind2 = 12;
X = boston_dataset.data[:, [ind1, ind2]]
y = boston_dataset.target
print('length of X:', len(X), '\nlength of y:', len(y))

# 6. splitting data
X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.8, random_state=5)
print('X_train: ', X_train.shape)
print('X_test: ', X_test.shape)
print('y_train: ', y_train.shape)
print('y_test: ', y_test.shape)

# 7. train a linear regressor
model = LinearRegression()
model.fit(X_train, y_train)

# 8. predict test samples and get mse
fx_test = model.predict(X_test)
mse = mean_squared_error(y_test, fx_test)
print('RMSE is {}'.format(mse))

# 9. analyse the model performance
(minv, maxv) = (y_test.min(), y_test.max())
fig,ax = plt.subplots()
ax.scatter(y_test, fx_test, marker='o', s=5)       #point of size 5
ax.plot([minv, maxv], [minv, maxv])                  #y=f(x) ideal line
ax.set_xlabel('Real')
ax.set_ylabel('Predicted')
print('\nLeast-Square')
plt.show()
print('RMSE is {}\n'.format(mse))

# 10. cross validation
from sklearn.model_selection import cross_val_predict
fig,ax2 = plt.subplots()
fx = cross_val_predict(model, X, y, cv=3)
ax2.scatter(y, fx, marker='o', s=5)
ax2.plot([y.min(), y.max()], [y.min(), y.max()], 'r-', lw=1)
ax2.set_xlabel('Real')
ax2.set_ylabel('Predicted')
print('\nCross-Validation')
plt.show()

from sklearn.model_selection import cross_val_score
#linearregression
score = cross_val_score(model, X, y, cv=3, scoring='neg_mean_absolute_error')
print('\nLinearRegerssion MSE is {}'.format(mse))
print('LinearRegression scores : {}'.format(abs(score)))
print('LinearRegression scores avarage : {}'.format(np.mean(abs(score))))

# 11. other linear methods
#ridge
from sklearn.linear_model import Ridge
model = Ridge(alpha=1.0, tol=0.001)
model.fit(X,y)
fx_ridge = model.predict(X)
mse_ridge = mean_squared_error(y, fx_ridge)
score = cross_val_score(model, X, y, cv=3, scoring='neg_mean_absolute_error')
print('\nRidge MSE is {}'.format(mse_ridge))
print('Ridge scores : {}'.format(abs(score)))
print('Ridge scores avarage : {}'.format(np.mean(abs(score))))

#lasso
from sklearn.linear_model import Lasso
model = Lasso(alpha=1.0, tol=1e-4)
model.fit(X,y)
fx_lasso = model.predict(X)
mse_lasso = mean_squared_error(y, fx_lasso)
score = cross_val_score(model, X, y, cv=3, scoring='neg_mean_absolute_error')
print('\nLasso MSE is {}'.format(mse_lasso))
print('Lasso scores : {}'.format(abs(score)))
print('Lasso scores avarage : {}'.format(np.mean(abs(score))))

#elasticnet
from sklearn.linear_model import ElasticNet
model = ElasticNet(alpha=1.0, tol=1e-3, l1_ratio=0.6)
model.fit(X,y)
fx_elasticnet = model.predict(X)
mse_elasticnet = mean_squared_error(y, fx_elasticnet)
score = cross_val_score(model, X, y, cv=3, scoring='neg_mean_absolute_error')
print('\nElasticNet MSE is {}'.format(mse_elasticnet))
print('ElasticNet scores : {}'.format(abs(score)))
print('ElasticNet scores avarage : {}'.format(np.mean(abs(score))))
