
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
                    if i==1
                        continue;
                    end
                    string=sprintf('%s-QSStrPost-CoP-CoM.mat',num);
            end
            
            disp(string);
            S=load(string,'CoMy');
            var=S.CoMy(1001:13000);
            dvar=differentiate(var);
            ddvar=differentiate(dvar);
            bg(j)=rms(ddvar);
            q(i,j,1)=rms(var);
            q(i,j,2)=rms(dvar);
            q(i,j,3)=rms(ddvar); 
          
        end
    end
end