for i=1:10
    x =  randi(90,1,1)
    y = randi(90,1,1)
    svetofor(30, x,y)
end

clear all
x=-50
y = 0
figure
% ���������� ����������� ���������� �������
lh = line(x,y);
% ������� ����� ������ � ����� ������
set(lh,'color','r');
set(lh,'MarkerSize',6);
set(lh, 'Marker', 'o')
axlim=[-50 50 -50 50]
axis(axlim);
% ����� ������ ��������
set(lh,'erasemode','xor');
% ������ ����� ��������
for i = 2:100
% �������� ��������� ������
x = x+2  
% ����� ������� ������ - ������ ���������
  set(lh,'XData',x,'YData',y);
drawnow;
pause(0.05)
end