bg=zeros(14,6);
for i=1:14
    if i<10
        num=strcat('0',num2str(i));
    else
        num=num2str(i);
    end
    if i==8
        continue;
    else
        for j=1:2
            switch j
                case 1
                    string=sprintf('%s-QSEO1-CoP-CoM.mat',num);
                case 2
                    string=sprintf('%s-QSEO2-CoP-CoM.mat',num);
            end
            disp(string);
            S=load(string,'mdata');
            T=load(string,'CoMz');
            RShoulder=S.mdata.RShoulder(3,1001:13000);
            LShoulder=S.mdata.LShoulder(3,1001:13000);
            RASIS=S.mdata.RASIS(3,1001:13000);
            LASIS=S.mdata.LASIS(3,1001:13000);
            shoulderMarker=(RShoulder+LShoulder)/2;
            asisMarker=(RASIS+LASIS)/2;
            
            meanShoulder=mean(shoulderMarker);
            meanASIS=mean(asisMarker);
            
            CoMz=T.CoMz(1001:13000);
            meanCOMZ=mean(CoMz);
            
            if j==1
                bg(i,1)=meanShoulder;
                bg(i,2)=meanASIS;
                bg(i,3)=meanCOMZ;
            else
                bg(i,4)=meanShoulder;
                bg(i,5)=meanASIS;
                bg(i,6)=meanCOMZ;
            end
        end
    end
end