bg=zeros(7,1);league=cell(7,1);q=zeros(14,7,56);
figure;
for i=1:14
    if i<10
        num=strcat('0',num2str(i));
    else
        num=num2str(i);
    end
    if i==8
        continue;
    else
        for j=1:7
            switch j
                case 1
                    string=sprintf('%s-QSEC1-CoP-CoM.mat',num);
                case 2
                    string=sprintf('%s-QSEC2-CoP-CoM.mat',num);
                case 3
                    string=sprintf('%s-QSEO1-CoP-CoM.mat',num);
                case 4
                    string=sprintf('%s-QSEO2-CoP-CoM.mat',num);
                case 5
                    string=sprintf('%s-QSStr1-CoP-CoM.mat',num);
                case 6
                    string=sprintf('%s-QSStr2-CoP-CoM.mat',num);
                case 7
                    string=sprintf('%s-QSStrPost-CoP-CoM.mat',num);
            end
            disp(string);
            S=load(string,'GCoPx');
            T=load(string,'GCoPy');
            ml=S.GCoPx;
            ap=T.GCoPy;
 
            q(i,j,:)=copmeasures_Ray([ml,ap]);
         
        end
       
    end
end
