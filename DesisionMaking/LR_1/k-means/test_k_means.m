N = 100
M=4
dim = [100,100]
obj = zeros(2, N)
hold on
for i = 1:N
    obj(:,i) = unidrnd(dim);
    line(obj(1,i), obj(2,i), 'Marker', 'o')
    
end

%plot(obj(1,:), obj(2,:))

[centers, clusters] = k_means(obj, N, 4, dim, 1000);
for j = 1:M
    line(centers(1,j), centers(2,j), 'Marker', 'o')
    for i = 1:N
        if clusters(j,i) == 1
            line([centers(1,j), obj(1,i)], [centers(2,j), obj(2,i)])
        end
    end
end
hold off
