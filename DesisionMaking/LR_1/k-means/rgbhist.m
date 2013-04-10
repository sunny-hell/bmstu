function [hist] = rgbhist(I, nBins)
%RGBHIST   Histogram of RGB values.

if (size(I, 3) ~= 3)
    error('rgbhist:numberOfSamples', 'Input image must be RGB.')
end

%nBins = 256;  
hist = zeros(3, nBins);
hist(1, :) = imhist(I(:,:,1), nBins);  % r
hist(2, :) = imhist(I(:,:,2), nBins); % g
hist(3, :) = imhist(I(:,:,3), nBins); % b



 
