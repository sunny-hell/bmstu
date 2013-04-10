dirname = '../nature_training/';
files = dir(dirname)
nBins = 128;
N = size(files, 1);
h = zeros(3, nBins, N-2);
Nclust = 2;
for i = 3 : N
    fname = files(i).name;
    im = imread([dirname, fname], 'jpg');
    h(:,:,i-2) = normRGBHist(im, nBins);
    %drawHistRGB(h1);
end

dim = [256, 256, 256];
[centers, clusters] = k_means(h, N-2, Nclust, dim, 1000)
for i=1:Nclust
  sprintf('Cluster %d:\n', i)
  for j=1:N-2
      if (clusters(i,j) == 1)
        sprintf(files(j+2).name)
      end
  end
end

%end

