% ���������� �����������
% fel = future events list - ������ ������� �������
% ��������� �������:
% 1 - ������ � �������
% 2 - ������ � ���
% 3 - ������ � ������
% 4 - ������ � ������
% 5 - ������� ���� ��� �����-������
% 6 - ������� ���� ��� �����-������
fel = [randi(10,1,1), randi(10,1,1), randi(10,1,1), randi(10,1,1), 30, 0]
[time, nextEvent] = min(fel)

eastCount = 0;

westCount = 0;

northCount = 0;

southCount = 0;
greenTime = 30;
redTime = 30;
figure
lh = line(x,y);
% ������� ����� ������ � ����� ������
set(lh,'color','r');
set(lh,'MarkerSize',6);
set(lh, 'Marker', 'o')
axlim=[-50 50 -50 50]
axis(axlim);
set(lh,'erasemode','xor');
while (true)
    switch nextEvent
        case 1: % ������ � �������
            eastCount = eastCount + 1
            fel(1) = time + randi(10,1,1);
            %eastCoords= zeros(2, eastCount);
            %for i=1:eastCount
            %  eastCoords(:,i) = [2*i; 0]
            %end
        case 2: % ������ � ���
            southCount = southCount + 1
            fel(2) = time + randi(10,1,1)
            % southCoords= zeros(2, southCount);
            %for i=1:southCount
            %   southCoords(:,i) = [0; 2*i]
            %end
        case 3: % ������ � ������
            westCount = westCount + 1
            fel(3) = time + randi(10,1,1);
            %westCoords= zeros(2, westCount);
            %for i=1:westCount
            %   westCoords(:,i) = [-2*i; 0]
            %end
        case 4: % ������ � ������
            northCount = northCount + 1
            fel(4) = time + randi(10,1,1);
            
        case 5: %������� ����
            newdelta = svetofor(greenTime, northCount+southCount, westCount+eastCount)
            westCount = westCount - floor(greenTime)
            eastCount = eastCount - floor(greenTime)
            greenTime = greenTime + delta
            fel(5) = time + greenTime
        case 6: % ������� ����
            redTime = 60-greenTime
            northCount = northCount -floor(redTime)
            southCount = southCount -floor(redTime)
            fel(6) = redTime*2
            
    end
    southCoords= zeros(2, southCount);
    for i=1:southCount
        southCoords(:,i) = [0; 2*i]
    end
    northCoords= zeros(2, northCount);
    for i=1:northCount
        northCoords(:,i) = [-2*i; 0]
    end
    westCoords= zeros(2, westCount);
    for i=1:westCount
        westCoords(:,i) = [-2*i; 0]
    end
    eastCoords= zeros(2, eastCount);
    for i=1:eastCount
        eastCoords(:,i) = [2*i; 0]
    end
    
%drawnow;
%pause(0.05)
end