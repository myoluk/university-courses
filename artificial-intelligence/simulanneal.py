#Implementation of Travelling Salesman Problem
#Just change the input functions to implement any other problem.

import random, numpy as np, math, copy, matplotlib.pyplot as plt 
from collections import namedtuple

def tavlama(baslangicCozum,objektifFonk,sicaklikFonk,izleyenFonk, kabulFonk ):
 print("please wait..it will take time...")
 
 model=ModelYarat()  #model includes city coordinates and other data
 mevcut = baslangicCozum(model) #initial solution is random
 
 #keep track of best solution, not part of simul tavlama
 eniyiCozum=mevcut
 eniyiDeger=objektifFonk(mevcut,model)   #degerlendirme fonksiyonu
 for t in range(model.maxIter): 
      T=sicaklikFonk(t,model)
      if T<model.Tmin : break
      sonraki=izleyenFonk(mevcut,model)
      sonrakiDeger, mevcutDeger = objektifFonk(sonraki,model), objektifFonk(mevcut,model)
	
      delta=sonrakiDeger-mevcutDeger
	
      if kabulFonk(T,delta):
        mevcut=sonraki   
        if sonrakiDeger < eniyiDeger :
            eniyiDeger = sonrakiDeger
            eniyiCozum = sonraki

 print("best solution is ",np.array(eniyiCozum)+1) 
 #real solution is = [13,18,9,4,19,1,5,17,7,16,3,6,11,8,14,15,2,20,12,10]
## best solution is [ 1 18 13 10 12 20  2 15 14  8 11  6  3 16  7 17  5 19  4  9]
 
 eniyiCozum.append(eniyiCozum[0]) #to draw line from last city to first city, we append first city 
 x=np.array(model.x)
 y=np.array(model.y)
 
 plt.plot(x[eniyiCozum],y[eniyiCozum]);
 plt.plot(x,y,"ro");
 plt.show()
 return mevcut

def kabulFonk(T, delta):
    if delta<0 : return True    #always accept good solution
    #accept some bad solutions with acceptance probability
    return random.random() < math.exp(-delta / T )                
		
def izleyenFonk(tour,model): #swap two random cities
   sonraki=tour.copy();
   index1=random.randrange(len(tour))
   index2=random.randrange(len(tour))
   sonraki[index1], sonraki[index2] = sonraki[index2], sonraki[index1]
   return sonraki

def ModelYarat():
    model=namedtuple('model', 'n,x,y, Tmin,Tmax, coolRate, maxIter, dist');
    
    #create (x, y) coordinates of 20 cities manually
    model.x=[82,91,12,92, 63, 9, 28, 55, 96, 97, 15, 98, 96, 49, 80, 14, 42, 92, 80, 96]
    model.y=[66, 3, 85, 94, 68, 76, 75, 39, 66, 17, 71, 3, 27, 4, 9, 83, 70, 32, 95, 3]
    #best solution is  [19  4  9  1 18 13 10 12 20  2 15 14  8 11  6  3 16  7 17  5]	
    
    model.maxIter=300000
    model.coolRate = 0.0001
    model.Tmin = 0.0001  #final temperature
    model.Tmax = 10      #initial temperature
    model.n=len(model.x)
    x=model.x
    y=model.y
    n=model.n
    
    #dynamic programming, save all possible distances between pairs
    dist=np.zeros((n,n))
    for i in range(n-1):
        for j in range(i+1,n):
           dist[i,j]=math.sqrt((np.array(x[i])-np.array(x[j]))**2+(np.array(y[i])-np.array(y[j]))**2)
           dist[j,i]=dist[i,j]

    model.dist=dist
    
    return model

def objektifFonk(tour,model):
    '''the total length of the tour based on the distance matrix'''
    total=0
    num_cities=len(tour)
    for i in range(num_cities):
        j=(i+1)%num_cities
        city_i=tour[i]
        city_j=tour[j]
        total+=model.dist[city_i,city_j]
    return total   #minumum distance produce better energy
	
def baslangicCozum(model):
   tour=list(range(model.n))
   random.shuffle(tour)
   return tour
  
def sicaklikFonk(t,model):
   #return model.Tmax*(model.coolRate**t)
   return model.Tmax-model.coolRate*t
	
def main():
  tavlama(baslangicCozum,objektifFonk,sicaklikFonk,izleyenFonk, kabulFonk)

main()	
