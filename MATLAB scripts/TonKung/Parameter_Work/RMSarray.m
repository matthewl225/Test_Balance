for i = 1:14
    if i >= 10
        o = '';
    else
        o = 0;
    end
    if (i ~= 2) && (i ~= 8)
        for j = 1:4;
            switch j
                case 1
                    S = load(sprintf('%d%d-WalkArm-CoP-CoM.mat', o, i));
                case 2
                    S = load(sprintf('%d%d-WalkMet-CoP-CoM.mat', o, i));
                case 3
                    S = load(sprintf('%d%d-WalkNat-CoP-CoM.mat', o, i));
                case 4
                    S = load(sprintf('%d%d-WalkStr-CoP-CoM.mat', o, i));
            end

        DISrmsx(i,j) = rms(S.CoMx);
        DISrmsy(i,j) = rms(S.CoMy);
        DISrmsz(i,j) = rms(S.CoMz);
        
        VELrmsx(i,j) = rms(differentiate(S.CoMx));
        VELrmsy(i,j) = rms(differentiate(S.CoMy));
        VELrmsz(i,j) = rms(differentiate(S.CoMz));
        
        ACCrmsx(i,j) = rms(differentiate(differentiate(S.CoMx)));
        ACCrmsy(i,j) = rms(differentiate(differentiate(S.CoMy)));
        ACCrmsz(i,j) = rms(differentiate(differentiate(S.CoMz)));
        
        end
    end
end
