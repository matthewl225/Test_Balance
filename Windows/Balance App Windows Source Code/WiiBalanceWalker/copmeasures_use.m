
function allparameters = copmeasures_use(signals, T) 
% signals = matrix of [COPx, COPy]
% T = period in seconds of trial

%frequencies description
Fs = 40;   % sampling frequency

fspel = 0.1; % frequency for spectrum lower band
fspeu = 5; % frequency for spectrum upper band

sampleNFFT = [8192, 4096, 2048, 1024, 512, 256, 128, 64, 32, 16, 8, 4, 2];

nfft_flag = 1;
for i = 1:(length(sampleNFFT))
    if i == 13 && nfft_flag == 1;
        nfft = 2;
        break;
    end
    if sampleNFFT(i) > 0.1*length(signals(:,1)) && sampleNFFT(i+1) < 0.1*length(signals(:,1))
        nfft = sampleNFFT(i);
        nfft_flag = 0;
    end
end

%*****************************************************

z05 = 1.645;        %constant for the z statistic at the 95% confidence level
F05 = 3;            %F statistic at 95% confidence level for a bivariate distribution for large sample size

%assigning data to variables
xCOP  = signals(:,1); %converting units
yCOP = signals(:,2); 


AP = yCOP - mean(yCOP);
ML = xCOP - mean(xCOP);
RD = sqrt(AP.^2+ML.^2);

meanCOPy=mean(yCOP);
meanCOPx=mean(xCOP);
%************************************************************
%Temporal Domain Measures
%Mean distance  
MDISTrd = mean(RD);
MDISTap = mean(abs(AP));
MDISTml = mean(abs(ML));

%RMS distance
sdrd = sqrt(mean(RD.^2));
sdap = sqrt(mean(AP.^2));     %also equal RDISTap
sdml = sqrt(mean(ML.^2));     %also equal RDISTml

%Total Excursion
TOTEXrd = sum(sqrt(diff(AP).^2 + diff(ML).^2));
TOTEXap = sum(abs(diff(AP)));
TOTEXml = sum(abs(diff(ML)));

%Mean velocity
MVELOrd = TOTEXrd/T;
MVELOap = TOTEXap/T;
MVELOml = TOTEXml/T;

%Range
RANGErd = max(RD)-min(RD);
RANGEap = max(AP)-min(AP);
RANGEml = max(ML)-min(ML);

%95% confidence circle AREA-CC
%assuming that distances are normally distributed; CC = %confidence circle
srd = sqrt(sdrd.^2 - MDISTrd.^2);
AREACC = pi * (MDISTrd + z05 * srd)^2;

%95% confidence ellipse - AREA-CE
sdapml = mean(AP.*ML);
AREACE = 2 * pi * F05 * sqrt(sdap^2 * sdml^2 - sdapml^2);

%Sway area AREA-SW
AREASWt = 0;
for i = 1:length(AP)-1
    AREASWt = AREASWt + (abs(AP(i+1)*ML(i) - AP(i)*ML(i+1) ));
end
AREASW = AREASWt/(2*T);

%Mean frequency
MFREQrd = MVELOrd / (2 * pi * MDISTrd);
MFREQap = MVELOap / (4 * sqrt(2) * MDISTap);
MFREQml = MVELOml / (4 * sqrt(2) * MDISTml);

%************************************************************
%Frequency Domain Measures

noverlap = nfft/2;

[Pap, f] = pwelch(AP, nfft, noverlap, nfft, Fs);
[Pml, f] = pwelch(ML, nfft, noverlap, nfft, Fs);
[Prd, f] = pwelch(RD, nfft, noverlap, nfft, Fs);

%find the index where fspel < f < fspeu
temp1 = find(f >= fspel);
temp2 = find(f >= fspeu);
ideltaf = linspace(temp1(1), temp2(1), temp2(1)-1);
% ideltaf = linspace(2, temp2(1), temp2(1)-1); % eliminate only zero Hz
% if Fs == 100
%     temp = find(f >= fc);
%     % In case Fs=100, line 4 = 0.15Hz
%     ideltaf = linspace(4, temp(1), temp(1)-1);
% elseif Fs == 1000
%     temp = find(f >= fc);
%     % In case Fs=1000, line 3 = 0.12Hz
%     ideltaf = linspace(3, temp(1), temp(1)-1);
% end
    
%define the frequency vector for calculating spectral moments
freq = f(ideltaf(1): ideltaf(end));

%define the power spectral density vectors up to fc
Pap1 = Pap(ideltaf(1) : ideltaf(end));
Pml1 = Pml(ideltaf(1) : ideltaf(end));
Prd1 = Prd(ideltaf(1) : ideltaf(end));

%zero moment
u0ap = sum( Pap1 );
u0ml = sum( Pml1 );
u0rd = sum( Prd1 );

%first moment
u1ap = sum( freq.*Pap1 );
u1ml = sum( freq.*Pml1 );
u1rd = sum( freq.*Prd1 );

%second moment
u2ap = sum( (freq.^2).*Pap1 );
u2ml = sum( (freq.^2).*Pml1 );
u2rd = sum( (freq.^2).*Prd1 );

%     %zero moment
%     u0ap(k) = sum( Pap1 ) * f(2);
%     u0ml(k) = sum( Pml1 ) * f(2);
%     u0rd(k) = sum( Prd1 ) * f(2);
%  
%     %first moment
%     u1ap = sum( freq.*Pap1 ) * f(2);
%     u1ml = sum( freq.*Pml1 ) * f(2);
%     u1rd = sum( freq.*Prd1 ) * f(2);
%  
%     %second moment
%     u2ap = sum( (freq.^2).*Pap1 ) * f(2);
%     u2ml = sum( (freq.^2).*Pml1 ) * f(2);
%     u2rd = sum( (freq.^2).*Prd1 ) * f(2);
% 
%centroidal frequency
CFREQap = sqrt(u2ap/u0ap); 
CFREQml = sqrt(u2ml/u0ml); 
CFREQrd = sqrt(u2rd/u0rd); 

%frequency dispersion
FREQDap = sqrt( 1 - (u1ap^2 / (u0ap * u2ap)));
FREQDml = sqrt( 1 - (u1ml^2 / (u0ml * u2ml)));
FREQDrd = sqrt( 1 - (u1rd^2 / (u0rd * u2rd)));

%determining the 50% and 95% power
P50ap = 0.5*u0ap; 
P50ml = 0.5*u0ml;
P50rd = 0.5*u0rd;
P95ap = 0.95*u0ap;
P95ml = 0.95*u0ml;
P95rd = 0.95*u0rd;    

%temporary markers for storing the index at which the 50% and 95% power
%frequencies occur
marker50ap = 0;
marker50ml = 0;
marker50rd = 0;
marker95ap = 0;
marker95ml = 0;
marker95rd = 0;

for i = 1: length(freq)
    if marker50ap == 0
        if sum(Pap1(1:i)) >= P50ap
%             if sum(Pap1(1:i))*freq(1) >= P50ap
            marker50ap = i;
        end
    end
    if marker95ap == 0
        if sum(Pap1(1:i)) >= P95ap
%             if sum(Pap1(1:i))*freq(1) >= P95ap
            marker95ap = i;
        end
    end
    if marker50ml == 0
        if sum(Pml1(1:i)) >= P50ml
%             if sum(Pml1(1:i))*freq(1) >= P50ml
            marker50ml = i;
        end
    end
    if marker95ml == 0
        if sum(Pml1(1:i))>= P95ml
%             if sum(Pml1(1:i))*freq(1) >= P95ml
            marker95ml = i;
        end
    end
    if marker50rd == 0
        if sum(Prd1(1:i)) >= P50rd
%             if sum(Prd1(1:i))*freq(1) >= P50rd
            marker50rd = i;
        end
    end
    if marker95rd == 0
        if sum(Prd1(1:i)) >= P95rd
%             if sum(Prd1(1:i))*freq(1) >= P95rd
            marker95rd = i;
        end
    end        
end

%50% power frequency
PF50ap = freq(marker50ap);
PF50ml = freq(marker50ml);
PF50rd = freq(marker50rd);

%95% power frequency
PF95ap = freq(marker95ap);
PF95ml = freq(marker95ml);
PF95rd = freq(marker95rd);

%Output all parameters
allparameters=[ MDISTrd MDISTap MDISTml sdrd sdap sdml ...
                MVELOrd MVELOap MVELOml TOTEXrd TOTEXap TOTEXml ...
                RANGErd RANGEap RANGEml AREACC AREACE AREASW ...
                MFREQrd MFREQap MFREQml u0rd u0ap u0ml ...
                PF50rd PF50ap PF50ml PF95rd PF95ap PF95ml ...
                CFREQrd CFREQap CFREQml FREQDrd FREQDap FREQDml...
                meanCOPy meanCOPx];
