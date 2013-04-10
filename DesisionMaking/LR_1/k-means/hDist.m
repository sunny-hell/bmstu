% Здесь d - массив из трех значений (r, g, b)
function [d] = hDist(h1, h2)
n = size(h1,1);
nBins = size(h1, 2);

sumh = zeros(1, n);
for i=1:1:nBins
    %h1(:, i)
    %h2(:, i)
    sq = sqrt(h1(:, i).*h2(:, i));
    sumh = sumh + sq';
end
d0 = 1.0-sumh
d = sum(d0(:))
end 
