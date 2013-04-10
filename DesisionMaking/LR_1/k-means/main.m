function varargout = main(varargin)
% MAIN MATLAB code for main.fig
%      MAIN, by itself, creates a new MAIN or raises the existing
%      singleton*.
%
%      H = MAIN returns the handle to a new MAIN or the handle to
%      the existing singleton*.
%
%      MAIN('CALLBACK',hObject,eventData,handles,...) calls the local
%      function named CALLBACK in MAIN.M with the given input arguments.
%
%      MAIN('Property','Value',...) creates a new MAIN or raises the
%      existing singleton*.  Starting from the left, property value pairs are
%      applied to the GUI before main_OpeningFcn gets called.  An
%      unrecognized property name or invalid value makes property application
%      stop.  All inputs are passed to main_OpeningFcn via varargin.
%
%      *See GUI Options on GUIDE's Tools menu.  Choose "GUI allows only one
%      instance to run (singleton)".
%
% See also: GUIDE, GUIDATA, GUIHANDLES

% Edit the above text to modify the response to help main

% Last Modified by GUIDE v2.5 24-Feb-2013 00:47:39

% Begin initialization code - DO NOT EDIT
gui_Singleton = 1;
gui_State = struct('gui_Name',       mfilename, ...
    'gui_Singleton',  gui_Singleton, ...
    'gui_OpeningFcn', @main_OpeningFcn, ...
    'gui_OutputFcn',  @main_OutputFcn, ...
    'gui_LayoutFcn',  [] , ...
    'gui_Callback',   []);
if nargin && ischar(varargin{1})
    gui_State.gui_Callback = str2func(varargin{1});
end

if nargout
    [varargout{1:nargout}] = gui_mainfcn(gui_State, varargin{:});
else
    gui_mainfcn(gui_State, varargin{:});
end
% End initialization code - DO NOT EDIT


% --- Executes just before main is made visible.
function main_OpeningFcn(hObject, eventdata, handles, varargin)
handles.M=0;
handles.N = 0;
handles.TrainPath = get(handles.TrainPathObj, 'String');
handles.SaveTo = get(handles.SaveToObj, 'String');
handles.TestFolder = get(handles.TestFolderObj, 'String');
handles.TestFiles = dir(handles.TestFolder);
handles.ClusterNames = [];
nameList = [];
for i=3:size(handles.TestFiles,1);
    nameList = [nameList; sprintf('%-50s\n',handles.TestFiles(i).name)];
end
set(handles.TestImages, 'String', nameList);
% Choose default command line output for main
handles.output = hObject;
% Update handles structure
guidata(hObject, handles);

% UIWAIT makes main wait for user response (see UIRESUME)
% uiwait(handles.figure1);


% --- Outputs from this function are returned to the command line.
function varargout = main_OutputFcn(hObject, eventdata, handles)
% varargout  cell array for returning output args (see VARARGOUT);
% hObject    handle to figure
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Get default command line output from handles structure
varargout{1} = handles.output;


% --- Executes on selection change in lstImages.
function lstImages_Callback(hObject, eventdata, handles)
pos = get(hObject, 'Value')
ind = get(handles.ClusterList, 'Value')

cnt = 0;
fname = '';
for j=1:handles.N
    if (handles.clusters(ind, j) == 1)
        cnt = cnt+1;
        if (cnt == pos)
            fname = handles.files(j+2).name
            break
        end
    end
end
figure
img = imread([handles.TrainPath, fname], 'jpg');
image(img);



% --- Executes during object creation, after setting all properties.
function lstImages_CreateFcn(hObject, eventdata, handles)
handles.ImageList = hObject;
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end
guidata(hObject, handles);

% --- Executes on selection change in cmbClusters.
function cmbClusters_Callback(hObject, eventdata, handles)
ind = get(hObject, 'Value')
strList = get(hObject, 'String')
set(handles.NewNameObj, 'String', strList(ind,:));
nameList = [];
for i=1:handles.N
    if (handles.clusters(ind,i) == 1)
        nameList = [nameList; sprintf('%-50s\n',handles.files(i+2).name)];
    end
end
set(handles.ImageList, 'String', nameList);
guidata(hObject, handles);


% --- Executes during object creation, after setting all properties.
function cmbClusters_CreateFcn(hObject, eventdata, handles)
handles.ClusterList = hObject;
guidata(hObject, handles);

if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function edtNewName_Callback(hObject, eventdata, handles)
ind = get(handles.ClusterList, 'Value');
handles.ClusterNames(ind,:) = sprintf('%-22s', get(hObject, 'String'));
set(handles.ClusterList, 'String', handles.ClusterNames);
guidata(hObject, handles);

% --- Executes during object creation, after setting all properties.
function edtNewName_CreateFcn(hObject, eventdata, handles)
handles.NewNameObj = hObject;
guidata(hObject, handles);

if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end


% --- Executes on button press in btnSave.
function btnSave_Callback(hObject, eventdata, handles)
% fid = fopen(handles.SaveTo, 'w+');
% fprintf(fid, '%d %d\n', handles.N, handles.M);
% for i = 1:handles.M
%     fprintf('%s\n', handles.ClusterNames(i,:));
%     fprintf('%f %f %f\n', handles.centers(i,:));
%     for j=1:handles.N
%         if (handles.clusters(i,j) == 1)
%             fprintf('%-50s\n',handles.files(j+2).name);
%         end
%     end
%     
% end
% fclose(fid);



function edtSave_Callback(hObject, eventdata, handles)
handles.SaveTo = get(hObject, 'String');


% --- Executes during object creation, after setting all properties.
function edtSave_CreateFcn(hObject, eventdata, handles)
handles.SaveToObj = hObject;
guidata(hObject, handles);
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function edtClustNum_Callback(hObject, eventdata, handles)
M = str2num(get(hObject, 'String'));
if (isempty(M))
    errordlg('Некорретный ввод','Ошибка ввода','modal')
    uicontrol(hObject)
    return
end
handles.M = M;
guidata(hObject,handles);
%        str2double(get(hObject,'String')) returns contents of edtClustNum as a double


% --- Executes during object creation, after setting all properties.
function edtClustNum_CreateFcn(hObject, eventdata, handles)

if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end

function edtTrainPath_Callback(hObject, eventdata, handles)
handles.TrainPath = get(hObject, 'String');
guidata(hObject, handles);

% --- Executes during object creation, after setting all properties.
function edtTrainPath_CreateFcn(hObject, eventdata, handles)

handles.TrainPathObj = hObject;
guidata(hObject, handles);

if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end


% --- Executes on button press in btnStartClust.
function btnStartClust_Callback(hObject, eventdata, handles)
handles.ClusterNames = [];
handles.centers = [];
handles.clusters = [];
handles.files = dir(handles.TrainPath);
nBins = 128;
N = size(handles.files, 1);
h = zeros(3, nBins, N-2);

for i = 3 : N
    fname = handles.files(i).name
    %handles.TrainPath, fname
    im = imread([handles.TrainPath, fname], 'jpg');
    h(:,:,i-2) = normRGBHist(im, nBins);
end
dim = [256, 256, 256];
[centers, clusters] = k_means(h, N-2, handles.M, dim, 1000);
handles.N = N-2;
handles.centers = centers;
handles.clusters = clusters;
clustItems = [];%zeros(1, handles.M)
for i=1:handles.M
    % clustItems = [clustItems; sprintf('Cluster %-2d', i)];
    handles.ClusterNames = [handles.ClusterNames; sprintf('%-20s%-2d', 'Cluster', i)];
end
set(handles.ClusterList, 'String', handles.ClusterNames);
%set(handles.ClusterList, 'Value', 2);
guidata(hObject, handles);


% --- Executes on selection change in lstTestImages.
function lstTestImages_Callback(hObject, eventdata, handles)
pos = get(hObject, 'Value')

fname = handles.TestFiles(pos+2).name;
figure
img = imread([handles.TestFolder, fname], 'jpg');
image(img);

if size(handles.centers) == 0
    msgbox('Не заданы кластеры', 'Error');
else
    h = normRGBHist(img, 128);
    i = AssociateCluster(h, handles.centers);
    clustName = handles.ClusterNames(i,:);
    msgbox(sprintf('Кластер %s', clustName), 'Result');
end


% --- Executes during object creation, after setting all properties.
function lstTestImages_CreateFcn(hObject, eventdata, handles)
handles.TestImages = hObject;
guidata(hObject, handles);
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function edtTestFolder_Callback(hObject, eventdata, handles)
handles.TestFolder = get(hObject, 'String');
handles.TestFiles = dir(handles.TestFolder);


% --- Executes during object creation, after setting all properties.
function edtTestFolder_CreateFcn(hObject, eventdata, handles)
handles.TestFolderObj = hObject;
guidata(hObject, handles);
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end
