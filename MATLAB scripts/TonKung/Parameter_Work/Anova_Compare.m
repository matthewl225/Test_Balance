parameter = ACCrmsy;        % Parameter Array

Condit = {'Arm' 'Met' 'Nat' 'Str'};
[p,anova,stats] = anova1(parameter, Condit);
figure;
[c,m,h,nms] = multcompare(stats);