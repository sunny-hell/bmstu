for i=1:10
    x =  randi(90,1,1)
    y = randi(90,1,1)
    svetofor(30, x,y)
end

clear all
x=-50
y = 0
figure
% Сохранение дескриптора выводимого объекта
lh = line(x,y);
% Задание цвета кривой и выбор границ
set(lh,'color','r');
set(lh,'MarkerSize',6);
set(lh, 'Marker', 'o')
axlim=[-50 50 -50 50]
axis(axlim);
% Выбор режима стирания
set(lh,'erasemode','xor');
% Начало цикла анимации
for i = 2:100
% Пересчет координат кривой
x = x+2  
% Самый главный момент - замена координат
  set(lh,'XData',x,'YData',y);
drawnow;
pause(0.05)
end