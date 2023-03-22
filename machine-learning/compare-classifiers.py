"""
Introduction to Machine Learning: Comparing Classifiers
"""

#libraries
import numpy as np
from sklearn.datasets import load_iris
from sklearn.metrics import accuracy_score

#load iris dataset
iris = load_iris();

#split data for train and test
#use 20% for test
from sklearn.model_selection import train_test_split
x_train, x_test, y_train, y_test = train_test_split(np.array(iris.data),np.array(iris.target),test_size=0.2)

#SVC using linear kernel
from sklearn import svm
model_svm = svm.SVC(kernel='linear',C=1)
model_svm.fit(x_train,y_train)                                #model training
fx_svm = model_svm.predict(x_test)                      #model prediction
score_svm = accuracy_score(y_test, fx_svm)        #model score

#SVC using non-linear kernels(rbf, poly, sigmoid)
#models
svc_rbf = svm.SVC(kernel='rbf',C=1e4,gamma=0.1)
svc_poly = svm.SVC(kernel='poly',degree=3,coef0=0)
svc_sigmoid = svm.SVC(kernel='sigmoid',coef0=0.01)
#models training
svc_rbf.fit(x_train,y_train)
svc_poly.fit(x_train,y_train)
svc_sigmoid.fit(x_train,y_train)
#models prediction
fx_svc_rbf = svc_rbf.predict(x_test)
fx_svc_poly = svc_poly.predict(x_test)
fx_svc_sigmoid = svc_sigmoid.predict(x_test)
#models score
score_svc_rbf = accuracy_score(y_test, fx_svc_rbf)
score_svc_poly = accuracy_score(y_test, fx_svc_poly)
score_svc_sigmoid = accuracy_score(y_test, fx_svc_sigmoid)

#KNN Classifier
from sklearn.neighbors import KNeighborsClassifier
model_knn = KNeighborsClassifier(n_neighbors=1)
model_knn.fit(x_train,y_train)                                 #model training
fx_knn = model_knn.predict(x_test)                        #model prediction
score_knn = accuracy_score(y_test, fx_knn)          #model score

#Decision Tree
from sklearn.tree import DecisionTreeClassifier
model_tree = DecisionTreeClassifier(criterion='entropy',max_depth=(3))
model_tree.fit(x_train,y_train)                                #model training
fx_tree = model_tree.predict(x_test)                      #model prediction
score_tree = accuracy_score(y_test, fx_tree)        #model score

#Bagging Classifier, base learner = decision tree
from sklearn.ensemble import BaggingClassifier
from sklearn.tree import DecisionTreeClassifier
model_bag_tree = BaggingClassifier(base_estimator=DecisionTreeClassifier())
model_bag_tree.fit(x_train,y_train)                                    #model training
fx_bag_tree = model_bag_tree.predict(x_test)                  #model prediction
score_bag_tree = accuracy_score(y_test, fx_bag_tree)     #model score

#Bagging Classifier, base learner = KNN
from sklearn.ensemble import BaggingClassifier
from sklearn.neighbors import KNeighborsClassifier
model_bag_knn = BaggingClassifier(base_estimator=KNeighborsClassifier())
model_bag_knn.fit(x_train,y_train)                                     #model training
fx_bag_knn = model_bag_knn.predict(x_test)                   #model prediction
score_bag_knn = accuracy_score(y_test, fx_bag_knn)     #model score

#Random Forest Classifier
from sklearn.ensemble import RandomForestClassifier
model_rf = RandomForestClassifier(n_estimators=15,max_depth=3,criterion='entropy')
model_rf.fit(x_train,y_train)                                                #model training
fx_rf = model_rf.predict(x_test)                                          #model prediction
score_rf = accuracy_score(y_test, fx_rf)                            #model score
