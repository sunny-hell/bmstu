function tzel=svetofor(vrz,y1,y2)
  % � �������� ������� ���������� ����� ���������: 
  % vrz - ����� �������� ����� ��������� �� ������� ����� ��� ������
  % y1 - ����� �����, ����������� � ����������� �� ����� �����-��
  % y2 - ����� �����, ����������� � ����������� �� ����� �����-������

r=newfis('svetofor');
  % �������� ����� �������� �������
  % ��� ��������� svetofor

r=addvar(r,'input','����� �������� �����',[10 50]);
  % �������� ����� ������� ���������� � �������� �������,
  % [10, 50] - �������� �������� ���������� � ��������.

r=addmf(r,'input',1,'�����','trapmf',[10 10 20 25]);
r=addmf(r,'input',1,'�������','trapmf',[20 25 35 40]);
r=addmf(r,'input',1,'�������','trapmf',[35 40 50 50]);
  % ������� ������������� (trapmf) ������� �������������� ���
  % ���������� "����� �������� ����� ����������".

r=addvar(r,'input','����� �����-��',[0 90]);
  % �������� ������ ������� ����������
  % ��� ��������� "����� �����-��"

r=addmf(r,'input',2,'����� �����','trapmf',[0 0 12 18]);
r=addmf(r,'input',2,'�����','trapmf',[15 21 31 37]);
r=addmf(r,'input',2,'�������','trapmf',[34 40 50 56]);
r=addmf(r,'input',2,'�������','trapmf',[53 59 69 75]);
r=addmf(r,'input',2,'����� �������','trapmf',[72 78 90 90]);

r=addvar(r,'input','����� �����-������',[0 90]);
  r=addmf(r,'input',3,'����� �����','trapmf',[0 0 12 18]);
  r=addmf(r,'input',3,'�����','trapmf',[15 21 31 37]);
  r=addmf(r,'input',3,'�������','trapmf',[34 40 50 56]);
  r=addmf(r,'input',3,'�������','trapmf',[53 59 69 75]);
  r=addmf(r,'input',3,'����� �������','trapmf',[72 78 90 90]);
r=addvar(r,'output','����� ���.����� �� ����� ��',[-20 20]);
  % ������� ������������ (� ����� ������)
  % �������� ����������, ������������ 
  % ��������� ������ ��������� "� ������� ������"
  % � ��������� �����.

r=addmf(r,'output',1,'���������','gaussmf',[7 20]);
r=addmf(r,'output',1,'�� ��������','gaussmf',[7 0]);
r=addmf(r,'output',1,'���������','gaussmf',[7 -20]);
  % ��� ���� ���������� �������� 
  % ������� �������������� ���� ������

list=[
  % ������� ������� ������
1 1 1 1 1 1
1 1 2 1 1 1
1 1 3 1 1 1
1 1 4 2 1 1
1 1 5 2 1 1
1 2 1 1 1 1
1 2 2 1 1 1
1 2 3 1 1 1
1 2 4 2 1 1
1 2 5 2 1 1
1 3 1 1 1 1
1 3 2 1 1 1
1 3 3 1 1 1
1 3 4 1 1 1
1 3 5 2 1 1
1 4 1 1 1 1
1 4 2 1 1 1
1 4 3 1 1 1
1 4 4 1 1 1
1 4 5 1 1 1
1 5 1 1 1 1
1 5 2 1 1 1
1 5 3 1 1 1
1 5 4 1 1 1
1 5 5 1 1 1
2 1 1 2 1 1
2 1 2 2 1 1
2 1 3 3 1 1
2 1 4 3 1 1
2 1 5 3 1 1
2 2 1 1 1 1
2 2 2 2 1 1
2 2 3 3 1 1
2 2 4 3 1 1
2 2 5 3 1 1
2 3 1 1 1 1
2 3 2 1 1 1
2 3 3 2 1 1
2 3 4 3 1 1
2 3 5 3 1 1
2 4 1 1 1 1
2 4 2 1 1 1
2 4 3 1 1 1
2 4 4 2 1 1
2 4 5 3 1 1
2 5 1 1 1 1
2 5 2 1 1 1
2 5 3 1 1 1
2 5 4 1 1 1
2 5 5 2 1 1
3 1 1 3 1 1
3 1 2 3 1 1
3 1 3 3 1 1
3 1 4 3 1 1
3 1 5 3 1 1
3 2 1 3 1 1
3 2 2 3 1 1
3 2 3 3 1 1
3 2 4 3 1 1
3 2 5 3 1 1
3 3 1 2 1 1
3 3 2 3 1 1
3 3 3 3 1 1
3 3 4 3 1 1
3 3 5 3 1 1
3 4 1 2 1 1
3 4 2 2 1 1
3 4 3 2 1 1
3 4 4 3 1 1
3 4 5 3 1 1
3 5 1 2 1 1
3 5 2 2 1 1
3 5 3 2 1 1
3 5 4 3 1 1
3 5 5 3 1 1];
r=addrule(r,list);
  % ���������� ������� ������ 
  % � ��������� ����� �������� ��������

tzel=evalfis([vrz y1 y2],r);
  % ������ ���������� �� ���������
  % ��������� � ����������� ��������
  % ���������� tzel - ��������� ������� 
  % �������� ����� ��������� � ��������� �����.