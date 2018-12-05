//----------------------------------------------------------------------------------------------------------------------+
// WiiBalanceWalker - Released by Richard Perry from GreyCube.com - Under the Microsoft Public License.
//
// Project platform set as x86 for the joystick option work as VJoy.DLL only available as native 32-bit.
//
// Uses the WiimoteLib DLL:           http://wiimotelib.codeplex.com/
// Uses the 32Feet.NET bluetooth DLL: http://32feet.codeplex.com/
// Used the VJoy joystick DLL:        http://headsoft.com.au/index.php?category=vjoy
//----------------------------------------------------------------------------------------------------------------------+

using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Timers;
using System.Windows.Forms;
using VJoyLibrary;
using WiimoteLib;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;

namespace WiiBalanceWalker
{
    public partial class FormMain : Form
    {
        //default 0.025 second interval 40Hz
        //usually a little longer than the given time
        //AT LEAST x amount of ms
        //40ms (should be 50ms) for 20 Hz seems to be ok

        //MAKE SURE TIMER GETS UPDATED EVERYTIME A DIFFERENT RADIO BUTTON IS CHECKED.

        System.Timers.Timer infoUpdateTimer = new System.Timers.Timer() { Interval = 40,     Enabled = false };

        ActionList actionList = new ActionList();
        Wiimote wiiDevice     = new Wiimote();
        DateTime jumpTime     = DateTime.UtcNow;

        bool setCenterOffset = false;

        // Used to zero out the WiiBoardz
        float blOffset = 0f;
        float brOffset = 0f;
        float tlOffset = 0f;
        float trOffset = 0f;
        float weightOffset = 0f;

        float naCorners     = 0f;
        float oaTopLeft     = 0f;
        float oaTopRight    = 0f;
        float oaBottomLeft  = 0f;
        float oaBottomRight = 0f;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // Setup a timer which controls the rate at which updates are processed.
            
            infoUpdateTimer.Elapsed += new ElapsedEventHandler(infoUpdateTimer_Elapsed);

           

            Globals.TimerOn = false;

            // Load trigger settings.

            //numericUpDown_TLR.Value  = Properties.Settings.Default.TriggerLeftRight;
            //numericUpDown_TFB.Value  = Properties.Settings.Default.TriggerForwardBackward;
            //numericUpDown_TMLR.Value = Properties.Settings.Default.TriggerModifierLeftRight;
            //numericUpDown_TMFB.Value = Properties.Settings.Default.TriggerModifierForwardBackward;

            // Link up form controls with action settings.

            //actionList.Left          = new ActionItem("Left",          comboBox_AL,  numericUpDown_AL);
            //actionList.Right         = new ActionItem("Right",         comboBox_AR,  numericUpDown_AR);
            //actionList.Forward       = new ActionItem("Forward",       comboBox_AF,  numericUpDown_AF);
            //actionList.Backward      = new ActionItem("Backward",      comboBox_AB,  numericUpDown_AB);
            //actionList.Modifier      = new ActionItem("Modifier",      comboBox_AM,  numericUpDown_AM);
            //actionList.Jump          = new ActionItem("Jump",          comboBox_AJ,  numericUpDown_AJ);
            //actionList.DiagonalLeft  = new ActionItem("DiagonalLeft",  comboBox_ADL, numericUpDown_ADL);
            //actionList.DiagonalRight = new ActionItem("DiagonalRight", comboBox_ADR, numericUpDown_ADR);

            //initialize all the arrays to 0
            clear();

            Globals.TraceOn = false;
            Globals.TimeLeft = 0;

            //checkBox_EnableJoystick.Checked = Properties.Settings.Default.EnableJoystick;

            //for matlab code?
            /*MLApp.MLApp matlab = new MLApp.MLApp();

            // Change to the directory where the function is located 
            matlab.Execute(@"cd C:\Users\MatthewLee\Desktop\Standing-Walking-Balance-App\Windows\Balance App Windows Source Code\WiiBalanceWalker");

            // Define the output 
            object result = null;

            // Call the MATLAB function myfunc
            matlab.Feval("copmeasures_use", 1, out result, Globals.COGxArray, Globals.COGyArray, Globals.Time);*/
        }


        private void button_SetCenterOffset_Click(object sender, EventArgs e)
        {
            setCenterOffset = true;
        }

        private void button_ResetDefaults_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
            this.Close();
        }

        private void button_BluetoothAddDevice_Click(object sender, EventArgs e)
        {
            var form = new FormBluetooth();
            form.ShowDialog(this);
        }

        private void button_Connect_Click(object sender, EventArgs e)
        {
            try
            {
                // Find all connected Wii devices.

                var deviceCollection = new WiimoteCollection();
                deviceCollection.FindAllWiimotes();

                for (int i = 0; i < deviceCollection.Count; i++)
                {
                    wiiDevice = deviceCollection[i];

                    // Device type can only be found after connection, so prompt for multiple devices.

                    if (deviceCollection.Count > 1)
                    {
                        var devicePathId = new Regex("e_pid&.*?&(.*?)&").Match(wiiDevice.HIDDevicePath).Groups[1].Value.ToUpper();

                        var response = MessageBox.Show("Connect to HID " + devicePathId + " device " + (i + 1) + " of " + deviceCollection.Count + " ?", "Multiple Wii Devices Found", MessageBoxButtons.YesNoCancel);
                        if (response == DialogResult.Cancel) return;
                        if (response == DialogResult.No) continue;
                    }

                    // Setup update handlers.

                    wiiDevice.WiimoteChanged          += wiiDevice_WiimoteChanged;
                    wiiDevice.WiimoteExtensionChanged += wiiDevice_WiimoteExtensionChanged;

                    // Connect and send a request to verify it worked.

                    wiiDevice.Connect();
                    wiiDevice.SetReportType(InputReport.IRAccel, false); // FALSE = DEVICE ONLY SENDS UPDATES WHEN VALUES CHANGE!
                    wiiDevice.SetLEDs(true, false, false, false);

                    // Enable processing of updates.

                    infoUpdateTimer.Enabled = true;

                    // Prevent connect being pressed more than once.

                    button_Connect.Enabled = false;
                    button_BluetoothAddDevice.Enabled = false;
                    break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Function to zero out WiiBoard
        private void button1_Click(object sender, EventArgs e)
        {
            const float four = 4;

            weightOffset = wiiDevice.WiimoteState.BalanceBoardState.WeightKg;

            tlOffset = (wiiDevice.WiimoteState.BalanceBoardState.SensorValuesKg.TopLeft) / four;
            trOffset = (wiiDevice.WiimoteState.BalanceBoardState.SensorValuesKg.TopRight) / four;
            blOffset = (wiiDevice.WiimoteState.BalanceBoardState.SensorValuesKg.BottomLeft) / four;
            brOffset = (wiiDevice.WiimoteState.BalanceBoardState.SensorValuesKg.BottomRight) / four;

        }

        private void wiiDevice_WiimoteChanged(object sender, WiimoteChangedEventArgs e)
        {
            // Called every time there is a sensor update, values available using e.WiimoteState.
            // Use this for tracking and filtering rapid accelerometer and gyroscope sensor data.
            // The balance board values are basic, so can be accessed directly only when needed.
        }

        private void wiiDevice_WiimoteExtensionChanged(object sender, WiimoteExtensionChangedEventArgs e)
        {
            // This is not needed for balance boards.
        }

        void infoUpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // Pass event onto the form GUI thread.

            this.BeginInvoke(new Action(() => InfoUpdate()));
        }

        private void InfoUpdate()
        {

            Globals.Offline = false;

            double TimerInterval;
            if (radioButton1.Checked)
            {
                Globals.Freq = 20;
                TimerInterval = 40;
                //0.05ms

            }
            else if (radioButton2.Checked)
            {
                Globals.Freq = 40;
                TimerInterval = 16;
                //0.025ms
            }
            else
            {
                Globals.Freq = 20;
                TimerInterval = 40;
            }

            infoUpdateTimer.Interval = TimerInterval;

            //yes, its four
            const float four = 4;

            if (wiiDevice.WiimoteState.ExtensionType != ExtensionType.BalanceBoard)
            {
                label_Status.Text = "DEVICE IS NOT A BALANCE BOARD...";
                Globals.Offline = true;
                //return;
            }

            if (radioButton6.Checked == true)
            {
                Globals.Offline = true;
            }

            // Get the current raw sensor KG values.

            float rwWeight = 0;
            float rwTopLeft = 0;
            float rwTopRight = 0;
            float rwBottomLeft = 0;
            float rwBottomRight = 0;

            if (Globals.Offline == false)
            {

                rwWeight = wiiDevice.WiimoteState.BalanceBoardState.WeightKg - weightOffset;

                rwTopLeft = ((wiiDevice.WiimoteState.BalanceBoardState.SensorValuesKg.TopLeft) / four) - tlOffset;
                rwTopRight = ((wiiDevice.WiimoteState.BalanceBoardState.SensorValuesKg.TopRight) / four) - trOffset;
                rwBottomLeft = ((wiiDevice.WiimoteState.BalanceBoardState.SensorValuesKg.BottomLeft) / four) - blOffset;
                rwBottomRight = ((wiiDevice.WiimoteState.BalanceBoardState.SensorValuesKg.BottomRight) / four) - brOffset;

                //if want to use COP value calculated from meaured values instead of COG straight from the board
                /*Globals.COPx = ((boardX / 2.0) * ((rwTopRight + rwBottomRight) - (rwTopLeft + rwBottomLeft))
                    / (rwTopRight + rwBottomRight + rwTopLeft + rwBottomLeft))/10.0;
                Globals.COPy = ((boardY / 2.0) * ((rwTopRight + rwTopLeft) - (rwBottomLeft + rwBottomRight))
                    / (rwTopRight + rwBottomRight + rwTopLeft + rwBottomLeft))/10.0;*/

                Globals.COGx = wiiDevice.WiimoteState.BalanceBoardState.CenterOfGravity.X;
                Globals.COGy = (-1.0) * wiiDevice.WiimoteState.BalanceBoardState.CenterOfGravity.Y;

                

            } else {
                //**TODO
                //OFFLINE mode here
                //how to imitate noise
                //noise on the 4 corners, weight is the addition of each
                //check board dimensions

                rwTopLeft = 0;
                rwTopRight = 0;
                rwBottomLeft = 0;
                rwBottomRight = 0;

                rwWeight = rwTopLeft + rwTopRight + rwBottomLeft + rwBottomRight;

                float boardX = 400;
                float boardY = 280;

                Globals.COPx = ((boardX / 2.0) * ((rwTopRight + rwBottomRight) - (rwTopLeft + rwBottomLeft))
                    / (rwTopRight + rwBottomRight + rwTopLeft + rwBottomLeft)) / 10.0;
                Globals.COPy = ((boardY / 2.0) * ((rwTopRight + rwTopLeft) - (rwBottomLeft + rwBottomRight))
                    / (rwTopRight + rwBottomRight + rwTopLeft + rwBottomLeft)) / 10.0;

            }

            //start at the end COGxArray[59999] (COGxArray is all initialized to 0)
            //shift to left by one
            Array.Copy(Globals.COGxArray, 1, Globals.COGxArray, 0, 59999);
            Globals.COGxArray[59999] = Globals.COGx;

            Array.Copy(Globals.COGyArray, 1, Globals.COGyArray, 0, 59999);
            Globals.COGyArray[59999] = Globals.COGy;



            Array.Copy(Globals.TLArray, 1, Globals.TLArray, 0, 59999);
            Globals.TLArray[59999] = rwTopLeft;

            Array.Copy(Globals.BLArray, 1, Globals.BLArray, 0, 59999);
            Globals.BLArray[59999] = rwBottomLeft;

            Array.Copy(Globals.TRArray, 1, Globals.TRArray, 0, 59999);
            Globals.TRArray[59999] = rwTopRight;

            Array.Copy(Globals.BRArray, 1, Globals.BRArray, 0, 59999);
            Globals.BRArray[59999] = rwBottomRight;

            //decrement timer here
            if (Globals.TimerOn)
            {
                Globals.Period--;
                Globals.TimeLeft -= 1/(Globals.Freq);
                textBox1.Text = Globals.TimeLeft.ToString("0.##");
                if (Globals.Period == 0)
                {
                    print();
                    Globals.TimerOn = false;
                    button3.Text = "Start";
                }
            }

            //only trace the last 100 (599~500);

            //copScatter1.Update();
            if (Globals.TraceOn) {
                copScatter1.ValuesA.Add(new ObservablePoint(Globals.COGx, Globals.COGy));
                if (copScatter1.ValuesA.Count > 50) copScatter1.ValuesA.RemoveAt(0);
            }
            copScatter1.ValuesB[0].X = Globals.COGx;
            copScatter1.ValuesB[0].Y = Globals.COGy;
            copScatter1.ValuesB[0].Weight = 1;

            // The alternative .SensorValuesRaw is not adjusted with 17KG and 34KG calibration data, but does that make for better or worse control?
            //
            //var rwTopLeft     = wiiDevice.WiimoteState.BalanceBoardState.SensorValuesRaw.TopLeft     - wiiDevice.WiimoteState.BalanceBoardState.CalibrationInfo.Kg0.TopLeft;
            //var rwTopRight    = wiiDevice.WiimoteState.BalanceBoardState.SensorValuesRaw.TopRight    - wiiDevice.WiimoteState.BalanceBoardState.CalibrationInfo.Kg0.TopRight;
            //var rwBottomLeft  = wiiDevice.WiimoteState.BalanceBoardState.SensorValuesRaw.BottomLeft  - wiiDevice.WiimoteState.BalanceBoardState.CalibrationInfo.Kg0.BottomLeft;
            //var rwBottomRight = wiiDevice.WiimoteState.BalanceBoardState.SensorValuesRaw.BottomRight - wiiDevice.WiimoteState.BalanceBoardState.CalibrationInfo.Kg0.BottomRight;

            // Show the raw sensor values.

            label_rwWT.Text = rwWeight.ToString("0.000");
            label_rwTL.Text = rwTopLeft.ToString("0.000");
            label_rwTR.Text = rwTopRight.ToString("0.000");
            label_rwBL.Text = rwBottomLeft.ToString("0.000");
            label_rwBR.Text = rwBottomRight.ToString("0.000");

            label1.Text = Globals.COGx.ToString("0.000");
            label2.Text = Globals.COGy.ToString("0.000");
            

            // Prevent negative values by tracking lowest possible value and making it a zero based offset.

            if (rwTopLeft     < naCorners) naCorners = rwTopLeft;
            if (rwTopRight    < naCorners) naCorners = rwTopRight;
            if (rwBottomLeft  < naCorners) naCorners = rwBottomLeft;
            if (rwBottomRight < naCorners) naCorners = rwBottomRight;
            
            // Negative total weight is reset to zero as jumping or lifting the board causes negative spikes, which would break 'in use' checks.

            var owWeight      = rwWeight < 0f ? 0f : rwWeight;

            var owTopLeft     = rwTopLeft     -= naCorners;
            var owTopRight    = rwTopRight    -= naCorners;
            var owBottomLeft  = rwBottomLeft  -= naCorners;
            var owBottomRight = rwBottomRight -= naCorners;

            // Get offset that would make current values the center of mass.

            if (setCenterOffset)
            {
                setCenterOffset = false;

                var rwHighest = Math.Max(Math.Max(rwTopLeft, rwTopRight), Math.Max(rwBottomLeft, rwBottomRight));

                oaTopLeft     = rwHighest - rwTopLeft;
                oaTopRight    = rwHighest - rwTopRight;
                oaBottomLeft  = rwHighest - rwBottomLeft;
                oaBottomRight = rwHighest - rwBottomRight;
            }
            
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Stop updates.

            infoUpdateTimer.Enabled = false;
            wiiDevice.Disconnect();

        }

        //pause
        private void button2_Click(object sender, EventArgs e)
        {
            if (infoUpdateTimer.Enabled == true)
            {
                button2.Text = "Resume";
                infoUpdateTimer.Enabled = false;
            }
            else
            {
                button2.Text = "Pause";
                infoUpdateTimer.Enabled = true;
            }
            
        }

        //start button
        private void button3_Click(object sender, EventArgs e)
        {
            //this is to stop
            if(Globals.TimerOn == true)
            {
                print();
                Globals.TimerOn = false;
                button3.Text = "Start";
            }
            //this is to start
            else
            {

                infoUpdateTimer.Enabled = true;

                if (!string.IsNullOrWhiteSpace(textBox1.Text))
                {

                    //MAX 600 SECONDS
                    int parsedInt = 0;
                    if (int.TryParse(textBox1.Text, out parsedInt))
                    {
                        

                        Globals.TimerOn = true;
                        button3.Text = "Stop";
                        //infoUpdateTimer.Enabled = true;
                        Globals.Time = parsedInt;
                        Globals.TimeLeft = parsedInt;

                       

                        //period as in terms of how many cycles are left
                        Globals.Period = parsedInt * (int) Globals.Freq;
                        
                    }



                }
            }
            
        }

        private void print()
        {

            textBox3.Text = "A";

            string strFilePath;
            //string strFilePath = @"C:\testfile.csv";

            //get current time, this is name of the datafile
            var time = DateTime.Now;
            string formattedTime = time.ToString("yy-MM-dd-hh-mm-ss");

            textBox3.Text = "B";
            
            //CHECK IF WORKS IF FOLDER IS SELECTED
            //if folder selected
            if (!string.IsNullOrWhiteSpace(textBox2.Text))
            {
                strFilePath = textBox2.Text + "\\" + formattedTime + ".csv";
            }
            else
            {
                //location might be different in windows 10
                //@ ignores escape characters

                textBox2.Text = @"C:\";
                strFilePath = @"C:\" + formattedTime + ".csv";
                //textBox2.Text = (System.IO.Directory.GetCurrentDirectory());
                //strFilePath = System.IO.Directory.GetCurrentDirectory() + "\\" + formattedTime + ".csv";
            }

            textBox3.Text = "C";

            //need to append the file name to the end of the address

            string strSeperator = ",";
            StringBuilder sbOutput = new StringBuilder();

            String[] headers = { "Time (seconds)", "COPx (cm)", "COPy (cm)", "TL (kgf)", "BL (kgf)", "TR (kgf)", "BR (kgf)" };
            sbOutput.AppendLine(string.Join(strSeperator, headers));
            double[] OutputRow = { 0, 0, 0, 0, 0, 0, 0 };

            textBox3.Text = "D";

            //truncate this matrix after it gets sent to matlab script
            double[,] COPMatrix = new double[600, 2];

            //int ilength = inaOutput.GetLength(0);
            int i;
            int j = 0;
            for (i = 60000 - (Globals.Time * (int) Globals.Freq) + Globals.Period; i < 60000; i++){
                OutputRow[0] = (1/(Globals.Freq)) * j;
                OutputRow[1] = Globals.COGxArray[i];
                OutputRow[2] = Globals.COGyArray[i];
                OutputRow[3] = Globals.TLArray[i];
                OutputRow[4] = Globals.BLArray[i];
                OutputRow[5] = Globals.TRArray[i];
                OutputRow[6] = Globals.BRArray[i];

                //x is the first 
                COPMatrix[j, 0] = Globals.COGxArray[i];
                COPMatrix[j, 1] = Globals.COGyArray[i];

                sbOutput.AppendLine(string.Join(strSeperator, OutputRow));
                j++;
            }

            textBox3.Text = "E";

            String[] headers3 = { " ", " ", " ", " ", " ", " ", " " };

            sbOutput.AppendLine(string.Join(strSeperator, headers3));



            textBox3.Text = "F";



            //parameter calculations
            if (radioButton4.Checked || radioButton6.Checked)
            {
                textBox3.Text = "G";

                // Create the MATLAB instance 
                MLApp.MLApp matlab = new MLApp.MLApp();

                // Change to the directory where the function is located 
                matlab.Execute(@"cd 'C:\'");
                //matlab.Execute(@"a=h.Feval('copmeasures_use', 1, ");

                // Define the output 
                object result = null;
                object result2 = null;

                //THIS SHIT ONLY WORKS FOR 5 SECONDS
                //Clock doesnt stop at 0 anymore
                //if i dont say infoupdatetimer.enabled to true doesn't work
                // Call the MATLAB function myfunc
                //try the myfunc example
                matlab.Feval("copmeasures_use", 1, out result, COPMatrix, Globals.Time, Globals.Freq);

                
                // Display result 
                object[] res = result as object[];
                //System.Diagnostics.Debug.WriteLine(res[0]);
                //System.Diagnostics.Debug.WriteLine(res[1]);

                
                //object arr = res[0];
                //int[,] da = (int[,])arr;
               

                double output = Convert.ToDouble(res[0]);

                String[] headers2 = {"MDISTrd", "MDISTap", "MDISTml", "sdrd", "sdap", "sdml",
                "MVELOrd", "MVELOap", "MVELOml", "TOTEXrd", "TOTEXap", "TOTEXml",
                "RANGErd", "RANGEap", "RANGEml", "AREACC", "AREACE", "AREASW",
                "MFREQrd", "MFREQap", "MFREQml", "u0rd", "u0ap", "u0ml",
                "PF50rd", "PF50ap", "PF50ml", "PF95rd", "PF95ap", "PF95ml",
                "CFREQrd", "CFREQap", "CFREQml", "FREQDrd", "FREQDap", "FREQDml",
                "meanCOPy", "meanCOPx"};
                sbOutput.AppendLine(string.Join(strSeperator, headers2));
                //this needs to be an array of 38 zeroes
                double[] OutputRow2 = new double[38];
                //double[] OutputRow2 = { output };

                
                for (i = 0; i < 1; i++)
                {
                    //print new paramter results, divide each element by 10000
                    //OutputRow2[i] = Convert.ToDouble(res[i]);
                    OutputRow2[i] = Convert.ToDouble(res[i]);

                }
                

                sbOutput.AppendLine(string.Join(strSeperator, OutputRow2));

            }

            textBox3.Text = "H";

            // Create and write the csv file
            // PROBLEM IS HERE
            // what is returned from sbOuput.ToString()
            File.WriteAllText(strFilePath, sbOutput.ToString());

            textBox3.Text = "I";

            // To append more lines to the csv file
            //File.AppendAllText(strFilePath, sbOutput.ToString());

            textBox3.Text = "J";



        }

        private void clear()
        {
            Array.Clear(Globals.COGxArray, 0, 60000);
            Array.Clear(Globals.COGyArray, 0, 60000);
            Array.Clear(Globals.TLArray, 0, 60000);
            Array.Clear(Globals.BLArray, 0, 60000);
            Array.Clear(Globals.TRArray, 0, 60000);
            Array.Clear(Globals.BRArray, 0, 60000);
            Array.Clear(Globals.TimeArray, 0, 60000);
        }

        //trace button
        private void button5_Click(object sender, EventArgs e)
        {
            if (Globals.TraceOn)
            {
                Globals.TraceOn = false;
                while(copScatter1.ValuesA.Count > 0)
                {
                    copScatter1.ValuesA.RemoveAt(0);
                }

            } else
            {
                Globals.TraceOn = true;
            }
        }

        //data folder location
        private void button4_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.RootFolder = Environment.SpecialFolder.Desktop;
            fbd.Description = "Select Data Folder";
            fbd.ShowNewFolderButton = false;

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = fbd.SelectedPath + "\\";
            }
        }


    }
}
