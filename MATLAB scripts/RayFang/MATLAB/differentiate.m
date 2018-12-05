function [vel] = differentiate(x)
% original by Kay
% modified by Kei on 160906
% modified by Kei on 170523

    sizeOfP = length(x); % expecting sizeOfP > 3
    vel = zeros(sizeOfP,1);
    
    for i = 2:sizeOfP-1
        vel(i) = (x(i+1)-x(i-1))/2;
    end
    
    vel(1) = vel(2); % embedding the one beside to fit the length
    vel(sizeOfP) = vel(sizeOfP-1); % embedding the one beside to fit the length
end

