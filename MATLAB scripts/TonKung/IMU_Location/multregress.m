X = [ones(size(ZMeanNat(:,2))) ZMeanNat(:,2) ZMeanNat(:,3) ZMeanNat(:,2).*ZMeanNat(:,3)];
b = regress(ZMeanNat(:,1),X)    % Removes NaN data

scatter3(ZMeanNat(:,2),ZMeanNat(:,3),ZMeanNat(:,1),'filled')
hold on
x1fit = min(ZMeanNat(:,2)):100:max(ZMeanNat(:,2));
x2fit = min(ZMeanNat(:,3)):10:max(ZMeanNat(:,3));
[X1FIT,X2FIT] = meshgrid(x1fit,x2fit);
YFIT = b(1) + b(2)*X1FIT + b(3)*X2FIT + b(4)*X1FIT.*X2FIT;
mesh(X1FIT,X2FIT,YFIT)
xlabel('Shoulder')
ylabel('ASIS')
zlabel('CoMz')
view(50,10)