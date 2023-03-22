/*Cinsiyet gercekleri*/
erkek(hasan).
erkek(kerem).
erkek(ahmet).
erkek(burak).
erkek(ali).
erkek(omer).
kadin(merve).
kadin(kubra).
kadin(kadriye).
kadin(busra).
kadin(elif).
kadin(zeynep).

/*Ata gercekleri*/
ata(hasan,kerem).
ata(hasan,kadriye).
ata(hasan,burak).
ata(merve,kerem).
ata(merve,kadriye).
ata(merve,burak).
ata(kerem,ali).
ata(kerem,omer).
ata(kerem,busra).
ata(kubra,ali).
ata(kubra,omer).
ata(kubra,busra).
ata(kadriye,elif).
ata(kadriye,zeynep).
ata(ahmet,elif).
ata(ahmet,zeynep).

/*anne, baba kurallari*/
anne(X,Y):-ata(X,Y), kadin(X).
baba(X,Y):-ata(X,Y), erkek(X).

/*kardes kurali*/
kardes(X,Y):-ata(Z,X), ata(Z,Y), not(X=Y), erkek(Z).

/*torun kurali*/
torun(X,Y):-ata(Z,X), ata(Y,Z).
