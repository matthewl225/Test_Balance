S=load('_height1.mat'); %load height data
data=S.bg; 

x1=data(:,1); %column of shoulder heights in mm for all subjects
x2=data(:,2); %column of ASIS heights in mm for all subjects 
y=data(:,3); %column of COM heights in mm for all subjects

x1(8,:)=[]; %removing the zeros from the 8th subject
x2(8,:)=[];
y(8,:)=[];

b1=regress(y,[ones(size(x1)) x1 x2 x1.*x2]); %find the regression equation for first trial

x1=data(:,4);
x2=data(:,5);
y=data(:,6);

x1(8,:)=[];
x2(8,:)=[];
y(8,:)=[];

b2=regress(y,[ones(size(x1)) x1 x2 x1.*x2]); %find the regression equation for first trial

%{
%plotting the equation
scatter3(x1,x2,y,'filled')
hold on
x1fit = min(x1):100:max(x1);
x2fit = min(x2):10:max(x2);
[X1FIT,X2FIT] = meshgrid(x1fit,x2fit);
YFIT = b2(1) + b2(2)*X1FIT + b2(3)*X2FIT + b2(4)*X1FIT.*X2FIT;
mesh(X1FIT,X2FIT,YFIT)
xlabel('Shoulder')
ylabel('ASIS')
zlabel('COM')
view(50,10)
%}
b=[b1 b2];

