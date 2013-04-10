function [nhist] = normRGBHist(I, nBins)
myhist = rgbhist(I, nBins);
sum = zeros(3,1);
nhist = zeros(3, nBins);
for i = 1:nBins
    sum = sum+myhist(:, i);
end
for j=1:3
    nhist(j,:) = myhist(j,:)/sum(j);
end
end

