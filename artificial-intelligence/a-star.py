#Bu kod A* algoritmanın bir çevre programı içinde kullanımını gösteriyor.
#Ajan bir matrix dünyasında labirentler arasında yaşıyor. Ajana bir hedef konum vererek labirentler arasından hedefe ulaşmasını bekliyoruz  
#Bu kod ayrıca python da nesneye yonelik programla yeteneginizde geliştirecektir.

import copy as cp
class Problem():
    def __init__(self, h, hedef_testi): #formule edilmis problem
        self.ilk_konum = (1,3) 
        self.hedef_konum = (7,6)
        self.eylemler=[(0, -1), (0, 1), (-1, 0), (1, 0), (-1, -1), (-1, 1), (1, -1), (1, 1)]
        self.h=h
        self.g=1
        self.hedef_testi=hedef_testi
        
class Ajan():
    counter=0 # static(class) degiskeni; ajana ID vermek icin kullanilacak 
    def __init__(self, cevre):
        self.cevre=cevre #dis dunyaya bir referans (tum ajanlar icin ortak)
        self.problem=Problem(self.h, self.hedef_testi) #varsayilan baslangic problemi
        konum=self.problem.ilk_konum  #ajanin baslangic konumu        
        cevre[1]=konum;  # ajanın mevcut durumunu cevreye yansıtmak gerek.
        
        #ziyaret sayılarım ve dünya hakkında ön bilgim durum[0] da tutulacak
        x,y = konum
        ziyaret = cp.deepcopy(cevre[0]) #pointer to pointer olan yapilarida kopyala
        ziyaret[x][y] = 1
        
        #durum: [eylemlerimin etkisi, algilarimin etkisi]
        self.durum = [ziyaret, konum] 
        self.id =Ajan.counter+1 # her ajana tekil bir ID verelim
        Ajan.counter +=1
        
    # hedefe olan tahmini uzaklık h = mevcut konumun, hedef konuma olan oklid uzakligi    
    def h(self,durum):
        x1,y1=durum[1] #mevcut konum
        x2,y2=self.problem.hedef_konum #hedef konum
        return(x1 - x2) ** 2 + (y1 - y2) ** 2    

    def hedef_testi(self,durum):
        x1,y1=durum[1] #mevcut konum
        x2,y2=self.problem.hedef_konum #hedef konum
        return (x1,y1) == (x2,y2)
    
    def guncelle_durum(self, algi=None,eylem=None): #ic durum ya eylem yada algi neticesinde degisir 
        if eylem:
            x,y = eylem
            self.durum[1]=(x,y) #eylem neticesinde yeni konumum
            self.durum[0][x][y] += 1  #ziyaret sayisini bir arttir
        if algi:
            x,y = algi  #simdilik algi sadece mevcut konumu kapsiyor..gps ile mevcut konumu algilamak gibi
            self.durum[1]=(x,y) #mevcut konum


    def algila(self):   #cevre ile etkilesim var. tek sensorumuz gps: mevcut  pozisyonu algila
          (x,y) = self.cevre[1] #Ajanın konumunu al. burasi simule ediliyor. gercek hayatta sensorler yoluyla algilama olur
          return [x,y] #baska algilarda olsaydı daha uzun bir liste yada veriyapisi olacakti
    
    def karar_al(self, algi,derinlik=None): # bu algi ustune karar al: hedefe goturecek eylem dizisi planla
        self.guncelle_durum(algi=algi)       
        plan = self.arama_yap(self.durum,derinlik) # mevcut durumdan hedefe eylem dizisi planla
        return plan

    def icra_et(self, eylemler, n=None): # n: planin ilk n eylemini uygula, cevre ile etkilesim var
       if (n is None): n=len(eylemler)-1 # n verilmemisse tum planı uygula demektir
       for i in range(n) :
            x,y=eylemler[i+1]
            self.cevre[1]=(x,y)              # eylemimi cevreye yansit=ilerle
            algi=self.algila()               # yeni konumda algilama yap
            self.guncelle_durum(algi, eylem=[x,y])  # eylemimim ve algim neticesini ic durumuma yansit
            print(self.id,":",eylemler[i],"-->",eylemler[i+1]," : uygulandi")
            if self.hedef_testi(self.durum): return False          #eylemi durdur eger hedefe ulasilmissa
       return True

    def genislet(self, nod, kuyruk):
        dunya=nod.durum[0] # nodun temsil ettigi dunya: matrix: duvar ve o anki ziyaret durumları
        # child nodlari kuyruga ekle
        for eylem in self.problem.eylemler:# komsu kareler              
            # child nod pozisyonunu al
            x0, y0 = nod.durum[1]
            sonraki_pos = (x0 + eylem[0], y0 + eylem[1])
            x, y = sonraki_pos
        
            # dunyanin sinirlarini asma, sonraki durum her zaman uygun olmali
            if x > (len(dunya) - 1) or x < 0 or y > (len(dunya[0]) -1) or y < 0: continue

            if dunya[x][y] == 'd':  continue # nod pozisyonunda bir duvar var mi..                             

            # geldigin nodu (ata) yeniden kuyruga ekleme! basit tekrardan kacin!
            if (nod.parent is not None) and (nod.parent.durum[1] == sonraki_pos):   continue

            #sonraki dunyayının ziyaret durumunu guncelle 
            sonrakiDunya=cp.deepcopy(dunya) #kuyruktaki her nodun, ayrı bir durumu olmalı(pointer ataması olmamalı)
            sonrakiDunya[x][y] += 1
            child = Node(nod, durum=[sonrakiDunya, sonraki_pos]) #yeni pozisyona karsilik gelen child yarat 

            # f, g, ve h degerlerini hesapla  
            child.g = nod.g + self.problem.g
            child.h = self.problem.h(child.durum)
            #child.f = max(nod.f,child.g + child.h)
            child.f = child.g + child.h
            # childi kuyruga (agaca) ekle
            kuyruk.append(child)   

    def cozum(self, nod):
        path = []
        current = nod
        while current is not None:
           path.append(current.durum[1])
           current = current.parent
        return path[::-1] # Return reversed path

    def arama_yap(self,durum, derinlik):    # maliyet sinirli A* algoritma, ders notlarindaki algoritma
        """A* Algorithm: returns a list of tuples as a path from the current to the end"""
        ilk_nod = Node(None, durum) # mevcut dugumu yarat
        kuyruk=[]
        kuyruk.append(ilk_nod) # mevcut nodu kuyruga ekle

        while len(kuyruk) > 0:  # hedefe ulasana kadar agaci genislet
            mevcut_nod = kuyruk.pop(0) # ilk elemanini uzaklastir
            if self.problem.hedef_testi(mevcut_nod.durum) or (mevcut_nod.g == derinlik):
                return self.cozum(mevcut_nod)
            self.genislet(mevcut_nod, kuyruk) # nodu genislet ve kuyrugu f ye gore sirala
            kuyruk.sort(key=lambda x: x.f)
  
class Node():
    def __init__(self, parent=None, durum=None):
        self.parent = parent
        self.durum = durum
        self.g = self.h = self.f = 0
    
#cevre programi
def main():
#cevre programi: ajanin algi ve eylem etkilesimini gerçekleştirir=simulasyon
#cevre, gercek dünyayı temsil etsin. arkaalan sabit nesneleri ("d" : duvar) matrix ile tutsun,  ajanın konumu ise ayrı yapıda tutalım (matrixte de tutabiliriz ama bu durumda ajanin konumunu herdefasinda aramak gerekir)
    arkaalan=[[0,  0,  0,  0, 'd',  0,  0,   0,   0,  0],
              [0,  0,  0,  0, 'd',  0,  0,   0,   0,  0],
              [0,  0,  0,  0, 'd',  0,  0,   0,   0,  0],
              [0,  0,  0,  0, 'd',  0,  0,   0,   0,  0],
              [0,  0,  0,  0, 'd',  0,  0,   0,   0,  0],
              [0,  0,  0,  0, 'd',  0,  0,   0,   0,  0],
              [0,  0,  0,  0,  0,  'd', 0,   0,   0,  0],
              [0,  0,  0,  0, 'd', 'd', 0,   0,   0,  0],
              [0,  0,  0,  0, 'd', 'd','d', 'd', 'd', 0],
              [0,  0,  0,  0,  0,   0,  0,   0,   0,  0]]

    #cevre: [arkaalanin durumu, ajanin konumu] baska varliklarda olsaydi eklemek gerekirdi 
    cevre = [arkaalan,None] # ajanin baslangic konumu problemden gelecek. o yuzden None verdik
    ajan1 = Ajan(cevre) #Ajan sinifinin bir instansi yaratiliyor 
    algi1=ajan1.algila()  
    plan1 = ajan1.karar_al(algi1,1) #g=1 maliyetlik bir dusunme yap
    print("plan1: ", plan1)
    ajan1.icra_et(plan1) #planin tamamini uygula, bunun icin 2.param kullanma    
## ---------------------------------------------
    algi2=ajan1.algila()  
    plan2 = ajan1.karar_al(algi2,3) #g=3 maliyetlik bir dusunme yap
    print("plan2: ", plan2)
    ajan1.icra_et(plan2,2) #planin ilk 2 eylemini uygula, non-deterministik dunyada sadece ilk eylemi uygulamak mantikli    
## ---------------------------------------------
    algi3=ajan1.algila()  
    plan3 = ajan1.karar_al(algi3) #hedefe kadar tam bir dusunme yap
    print("plan3: ", plan3)
    ajan1.icra_et(plan3) #planin tamamini uygula     
## ---------------------------------------------
    print("hedefe vardik mi:", ajan1.problem.hedef_testi(ajan1.durum))
# HEDEFE ULASTIK AJAN SIMDI NE YAPACAK, BELKI YENI BIR PROBLEM ILE DEVAM EDEBILIRIZ
    ajan1.problem.ilk_konum=ajan1.durum[1]  
    ajan1.problem.hedef_konum=(8,9) #ajan1 icin hedef durumu degistir, mevcut durumdan devam eder
    algi4=ajan1.algila()  
    plan4 = ajan1.karar_al(algi4) #hedefe kadar tam bir dusunme yap
    print("plan4: ", plan4)
    ajan1.icra_et(plan4) #planin tamamini uygula     

main()
