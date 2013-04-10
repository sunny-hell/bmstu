function [centers, clusters] = k_means (objects, N, M, dim, maxiter)
dd = size(objects, 1);
nBins = size(objects, 2);
centers = zeros(dd, nBins, M);
eps = mod(N,M)
if (eps == 0)
    eps = 1
end
part = floor(N/M)
exflg = false;
% выбираем центры кластеров, равномерно распределенные в пространстве объектов
% то есть рисеум рандомные гистограммы
%for i = 1:M
%    sumh = zeros(3,1);
%    for j=1:nBins
%        centers(:,j, i) = unidrnd(dim);
%        sumh = sumh + centers(:,j,i);
%    end
%    for j = 1:3
%        centers(j,:,i) = centers(j,:,i)./sumh(j);
%    end
%end
while ~exflg
    for i=1:M
        k = randi(N, 1)
        centers(:,:,i) = objects(:,:,k);
    end
    clusters = ObjectToClusters(objects, centers);
    exflg = true;
    for i=1:M
        nObj = sum(clusters(i,:))
        if (nObj < part - eps) || (nObj > part+eps)
            exflg = false;
        end
    end
end

exitflg = false;
iter = 0;
while ~exitflg && iter<=maxiter
    clusters = ObjectToClusters(objects, centers);

    % считаем новые центры кластеров
    exitflg = true;
    for j=1:M
        oldcenter = centers(:,:,j);
        clusters(j,:)
        num = sum(clusters(j,:));
        newcenter = zeros(3, nBins);
        for i = 1:N
            if clusters(j,i) == 1
                newcenter = newcenter + objects(:,:,i);
            end
        end
        newcenter = newcenter./num;
        %if newcenter(1,:) == [0;0;0]
        %   newcenter = oldcenter;
        %end

        if (oldcenter ~= newcenter)
            exitflg = false;
        end
        centers(:,:,j) = newcenter;
    end
    iter = iter + 1
end

function [clusters] = ObjectToClusters(objects, centers)
M = size(centers, 3)
N = size(objects, 3)
clusters = zeros(M, N);
for i = 1:N
    mindist = realmax;
    for j = 1:M
        curdist = hDist(objects(:,:, i), centers(:,:,j)); %dist_evkl(objects(:,i), centers(:,j));
        if curdist < mindist
            mindist = curdist;
            minclust = j;
        end
        
    end
    clusters(minclust, i) = 1;%.objects = union(clusters(minclust).objects, objects(i));
end
