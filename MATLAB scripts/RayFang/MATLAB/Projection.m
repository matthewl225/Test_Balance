bg=zeros(14,4);
RShoulder=zeros(3,12000);
LShoulder=zeros(3,12000);
RASIS=zeros(3,12000);
LASIS=zeros(3,12000);
ASIS=zeros(3,12000);
Shoulder=zeros(3,12000);
COM=zeros(3,12000);
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
            Z=load(string,'CoMz');
            X=load(string,'CoMx');
            Y=load(string,'CoMy');
            
            RShoulder(3,:)=S.mdata.RShoulder(3,1001:13000);
            LShoulder(3,:)=S.mdata.LShoulder(3,1001:13000);
            
            RShoulder(2,:)=S.mdata.RShoulder(2,1001:13000);
            LShoulder(2,:)=S.mdata.LShoulder(2,1001:13000);
            
            RShoulder(1,:)=S.mdata.RShoulder(1,1001:13000);
            LShoulder(1,:)=S.mdata.LShoulder(1,1001:13000);
            
            RASIS(3,:)=S.mdata.RASIS(3,1001:13000);
            LASIS(3,:)=S.mdata.LASIS(3,1001:13000);
            
            RASIS(2,:)=S.mdata.RASIS(2,1001:13000);
            LASIS(2,:)=S.mdata.LASIS(2,1001:13000);
            
            RASIS(1,:)=S.mdata.RASIS(1,1001:13000);
            LASIS(1,:)=S.mdata.LASIS(1,1001:13000);
            
            CoMz=Z.CoMz(1001:13000);
            CoMx=X.CoMx(1001:13000);
            CoMy=Y.CoMy(1001:13000);
            
            COM(1,:)=CoMx;
            COM(2,:)=CoMy;
            COM(3,:)=CoMz;
            
            ASIS=(RASIS+LASIS)/2;
           
            
            Shoulder=(RShoulder+LShoulder)/2;
            
            
            comASIS=COM-ASIS;
            shoulderASIS=Shoulder-ASIS;
            
            proj3D=zeros(1,12000);
            for k=1:12000
                mag=norm(shoulderASIS(:,k));
                d=dot(shoulderASIS(:,k),comASIS(:,k));
                proj3D(1,k)=d/mag;
            end
            
            
            mshoulderASIS=((mean(shoulderASIS(1,:)))^2+(mean(shoulderASIS(2,:)))^2+(mean(shoulderASIS(3,:)))^2)^0.5;
            if j==1
        
                bg(i,1)=mean(proj3D);
                bg(i,2)=mshoulderASIS;
            else
                bg(i,3)=mean(proj3D);
                bg(i,4)=mshoulderASIS;
            end
        end
    end
end