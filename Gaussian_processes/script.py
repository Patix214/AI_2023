import numpy as np
def RBF_kernel(xn, xm, l = 1):
    """
    Inputs:
        xn: row n of x
        xm: row m of x
        l:  kernel hyperparameter, set to 1 by default
    Outputs:
        K:  kernel matrix element: K[n, m] = k(xn, xm)
    """
    K = np.exp(-np.linalg.norm(xn - xm)**2 / (2 * l**2))
    return K
def make_RBF_kernel(X, l = 1, sigma = 0):
    """
    Inputs:
        X: set of φ rows of inputs
        l: kernel hyperparameter, set to 1 by default
        sigma: Gaussian noise std dev, set to 0 by default
    Outputs:
        K:  Covariance matrix 
    """
    K = np.zeros([len(X), len(X)])
    for i in range(len(X)):
        for j in range(len(X)):
            K[i, j] = RBF_kernel(X[i], X[j], l)
    return K + sigma * np.eye(len(K))

def gaussian_process_predict_mean(X, y, X_new):
    """
    Inputs:
        X: set of φ rows of inputs
        y: set of φ observations 
        X_new: new input 
    Outputs:
        y_new: predicted target corresponding to X_new
    """
    rbf_kernel = make_RBF_kernel(np.vstack([X, X_new]))
    K = rbf_kernel[:len(X), :len(X)]
    k = rbf_kernel[:len(X), -1]
    return  np.dot(np.dot(k, np.linalg.inv(K)), y)
def gaussian_process_predict_std(X, X_new):
    """
    Inputs:
        X: set of φ rows of inputs
        X_new: new input
    Outputs:
        y_std: std dev. corresponding to X_new
    """
    rbf_kernel = make_RBF_kernel(np.vstack([X, X_new]))
    K = rbf_kernel[:len(X), :len(X)]
    k = rbf_kernel[:len(X), -1]
    return rbf_kernel[-1,-1] - np.dot(np.dot(k,np.linalg.inv(K)),k)

################
import pandas as pd

data = pd.read_csv('.\employ.csv')
data = data[['week_end','value']]
data['week_end'] = pd.to_datetime(data['week_end'], dayfirst=True)
data = data.sort_values(by='week_end')
data = data[:100]

import matplotlib.pyplot as plt


trainingDays = 65
days = np.linspace(0, trainingDays, 100)

X = np.arange(0, trainingDays)
X = X.reshape(-1, 1)
y_pred = []
#y_std = []
for i in range(len(days)):
    X_new = np.array([days[i]])
    y_pred.append(gaussian_process_predict_mean(X, data['value'][:trainingDays], X_new))
y_pred = np.array(y_pred)

plt.figure(figsize = (15, 5))

plt.plot(X, data['value'][:trainingDays], "ko")
plt.plot(np.arange(trainingDays, data.shape[0]), data['value'][trainingDays:], "co")
plt.plot(days, y_pred, "r-")

plt.xlabel("$day$", fontsize = 14)
plt.ylabel("$salary$", fontsize = 14)
plt.legend(["observations", "real observations", "predictions"], fontsize = 10)
plt.grid(True)
plt.xticks(fontsize = 14)
plt.yticks(fontsize = 14)
plt.show()