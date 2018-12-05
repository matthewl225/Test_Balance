Tony Kung, Summer 2017, Development of a Clinically Usable Assessment Tool for Walking Balance

3 Objectives:

(CoM = Centre of Mass)
1. Obtain the optimal CoM parameters to use in assessing standing balance
	-> Assess most statistically relevant indicator of differences between the four conditions
2. Obtain the optimal location for the placement of a single IMU to approximate body CoM
3. Develop a cost-effective assessment tool to assess walking balance
	-> (Combined with standing balance, in this case, using Wii Board, Shimmer IMU and Matlab)

Folders:

USRP_Deliverables
- Elevator Pitch, Abstract and Final poster for USRP

Data_Sets
- Walking data for 14 subjects from the university of Tokyo
- Nat(Natural Walking), Met(Metronome), Arm(Arms restricted), Str(Stroop Test)
- Output Legend: explains the terms in the data files (from Angela)
- Centre of Mass (COM) data from ‘CoMx’, ‘CoMy’, ‘CoMz’ (filtered by Angela, cutoff freq of 6 hz)
- Markers data from ‘mdata’
- Motion capture sampling rate = 200 Hz


Paramater_Work	(Objective 1)
- RMSarray.m
	~ Creates the array of RMS values for displacement, velocity and acceleration under 4 conditions
	~ NOTE: no COM data for the metronome of subjects 2 and 8, so values were taken out

- Anova_Compare.m
	~ Compares the Arrays made from the above function
	~ Compares and computes the p values among the four conditions for each parameter
		-> A p value gives the probability that the data are from the same set (lower p value than 0.05 mean they are statistically different)
	~ Multcompare used to compare between two conditions after anova test.

- Parameters.xlsx
	~ All the rms values of the parameters, as well as p value summary


IMU_Location	(Objective 2)
- multregress.m
	~ ZMeanNat is the array of Mean Z values of COMz, Shoulderz, ASISz for each subject
	~ Used to find the coefficients for multiple regress of COMz in relation to Shoulder and ASIS height
	~ array b gives (Constant + coeff of variable 1 + coeff of variable 2 + coeff of variable 1 * variable 2)

- mean3D.m
	~ Used to calculate and make the array of projected 3D CoM values
	~ Uses ASIS (Anterior Superior Iliac Spine) marker as reference point (zero)
	~ Projects the 3D CoM onto the Shoulder - ASIS line down the torso

- IMULocatioinCalculations.xlsx
	~ Calculations used to compute different methods to find Virtual CoM
		(Refer to poster for picture)
	~ Method One, calculated using the multiple regress from multregress.m
		-> The coefficients were used to estimate the CoMz shown in sheet 1
		-> Column 'Estimate' shows the estimated height from multiple regression
	~ Method Two, in sheet 1. CoMz were compared to height of Shoulder and Asis markers
		-> rms of CoMz was averaged between the participants and compared to the two markers
		-> Gives a rough estimate of 20 mm above ASIS
	~ Method Three (Sheet 2)
		-> Using the 3D Centre ASIS as a reference point and projecting the 3D CoM onto the ASIS-Shoulder Line
		-> Uses the values from the array calculated in mean3D.m
		-> shown in the third table on the right
		-> The ratio as a of the projected 3D CoM over the trunk was used as the virtual marker point
		-> The root mean square error was calculated
	~ Etc.
		-> Second table column on sheet 2 calculated the 3D CoM without the projection onto the ASIS-Shoulder Line
		-> First table column on sheet 2 calculated the 3D CoM without ASIS as a reference point (Just to look at it)

- June26th.pptx
	~ Summary of the virtual marker location (estimate using ASIS and Shoulder)

Balance_Assessment (Objective 3)
	~ See RayFang folder