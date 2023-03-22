% Transfer Functions

g1 = tf([0 1],[1 0]);
g2 = 2;
h2 = -2;
ss1 = parallel(g1,1);
ss2 = series(ss1,g2);
sys = parallel(ss2,1);
zero(sys)
pole(sys)
pzmap(sys);figure;step(sys);figure;stepplot(sys);