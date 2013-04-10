function [nclust] = AssociateCluster(h, centers) 
  N = size(centers, 3)
  d = zeros(1,N);
  for i=1:N
      d(i) = hDist(h, centers(:,:,i));
  end
  [~, nclust] = min(d);
  
  
end
