I1=imread('..\nature_training\water1.jpg');
I2=imread('..\nature_training\grass10.jpg');

h1 = normRGBHist(I1, 256);
h2 = normRGBHist(I2, 256);
hDist(h1, h2)
%end
