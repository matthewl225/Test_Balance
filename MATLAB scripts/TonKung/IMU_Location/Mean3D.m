for i = 1:14
    if i >= 10
        o = '';
    else
        o = 0;
    end
    
    if (i ~= 2) && (i ~= 8)
        
        S = load(sprintf('%d-WalkNat-CoP-CoM.mat', i), 'mdata');
        
        asismarker = (S.mdata.RASIS' + S.mdata.LASIS')/2;
        shmarker = (S.mdata.RShoulder' + S.mdata.LShoulder')/2;
        
        T = load(sprintf('%d-WalkNat-CoP-CoM.mat', i), 'CoMx', 'CoMy', 'CoMz');
        
        com3d = [T.CoMx-asismarker(:,1) T.CoMy-asismarker(:,2) T.CoMz-asismarker(:,3)];
        
        line = shmarker - asismarker;
        project = ((com3d(:,1).*line(:,1) + com3d(:,2).*line(:,2) + com3d(:,3).*line(:,3))./(line(:,1).^2 + line(:,2).^2 + line(:,3).^2));
        projectCOM = [project.*line(:,1) project.*line(:,2) project.*line(:,3)];
        
        
        MeanNat3D(i,1) = sqrt((mean(projectCOM(:,1))).^2 + (mean(projectCOM(:,2))).^2 + (mean(projectCOM(:,3))).^2);
        MeanNat3D(i,2) = sqrt((mean(line(:,1))).^2 + (mean(line(:,2))).^2 + (mean(line(:,3))).^2);
        MeanNat3D(i,3) = sqrt((mean(asismarker(:,1))).^2 + (mean(asismarker(:,2))).^2 + (mean(asismarker(:,3))).^2);
        
    end
end