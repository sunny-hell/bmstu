function [] = drawHistRGB(hist)
nBins = size(hist, 2)
hFig = figure;
hold on
h(1) = stem(1:nBins, hist(1, :), '.r');
h(2) = stem(1:nBins, hist(2, :), '.g');
h(3) = stem(1:nBins, hist(3, :), '.b');

% set(h(1), 'color', [1 0 0])
% set(h(2), 'color', [0 1 0])
% set(h(3), 'color', [0 0 1])
end