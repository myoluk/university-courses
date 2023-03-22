% Stability of System
% For each of the following LDEs 
% a) 0.3 Y[n] = 0.4 Y[n-1] + 0.5 δ[n−3]; Y[0]=0.5 
% b) 2 Y[n] = 3 Y[n-1] + 1.25 Y[n-2]; Y[0]=0, Y[1]=1 
% c) 2 Y[n] = -3 Y[n-1] - 1.25 Y[n-2]; Y[0]=0, Y[1]=1 

% Study the stability of the system represented by this LDE, 
% solve the LDE by writing it as a closed form and if it has complex natural frequencies (oscillating), 
% write it in a polar form and find the oscillation period. 
% Then, using MATLAB/OCTAVE draw the sequence of samples.

n = 1:20
y = 4*(0.79).^n.*(cos(18.*n + pi/2));
stem(y)
y = (-0.46)*(1.84).^n+(0.46)*(-0.34).^n;
stem(y)
y = (4.3).^n.*0.5;
stem(y)