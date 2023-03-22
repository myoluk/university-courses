"""
Introduction to Machine Learning: MNIST Dataset
"""

#libraries
import numpy as np
import matplotlib.pyplot as plt
import cv2
from sklearn.model_selection import train_test_split
from sklearn.neighbors import KNeighborsClassifier
from sklearn.metrics import confusion_matrix, classification_report, accuracy_score
from sklearn import datasets
from sklearn.svm import SVC

#load tje MNIST digits dataset
mnist = datasets.load_digits()

X = mnist.data
Y = mnist.target

#print the first digit as 8x8 matrix
print(X[0].reshape((8,8)))

#split dataset into training and test set
X_train, X_test, y_train, y_test = train_test_split(X, Y, test_size=0.1, random_state=1)

#show the sizef of each data split
print("\nNumber of train samples: {}".format(len(X_train)))
print("Number of test samples: {}".format(len(X_test)))

#model selection
model = SVC(kernel='linear')

#model training
model.fit(X_train, y_train)

#model testing and analysing
fx_test = model.predict(X_test)

print("\nEvaluation on testing data\n")
accuracy = accuracy_score(y_test, fx_test)
print("Accuracy is %.2f on test data\n"%accuracy)
print(classification_report(y_test, fx_test), end="\n\n")
print("Confusion matrix: \n{}\n".format(confusion_matrix(y_test, fx_test)))

#visualize the first 5 digits and annotate them
for i in range(5):
    #get data and classify it
    image = X_test[i]
    label = model.predict([image])[0]
    image_data = np.array(image, dtype='float')
    image_pixels = image_data.reshape((8,8))
    plt.imshow(image_pixels, cmap='gray')                                              #in case of error, try following line
    plt.annotate(label, (3,3), bbox={'facecolor':'white'}, fontsize=16)
    plt.show()
    print("I think the digit is: {}".format(label), end="\n___\n")
    cv2.waitKey(0)