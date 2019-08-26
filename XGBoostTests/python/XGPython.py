from sklearn import datasets
#from sklearn.cross_validation import train_test_split  
from sklearn.model_selection import train_test_split


import xgboost as xgb
iris = datasets.load_iris()
X = iris.data
Y = iris.target

X_train, X_test, y_train, y_test = train_test_split(X, Y, test_size=0.2, random_state=42)
dtrain = xgb.DMatrix(X_train, label=y_train)

print("end")



#
#https://www.kdnuggets.com/2017/03/simple-xgboost-tutorial-iris-dataset.html
#PIP COMMANDS
#py -m pip list
#py -m pip install xgboost
#