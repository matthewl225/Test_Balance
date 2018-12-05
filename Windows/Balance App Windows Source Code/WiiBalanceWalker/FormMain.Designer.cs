namespace WiiBalanceWalker
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label_rwWT = new System.Windows.Forms.Label();
            this.button_Connect = new System.Windows.Forms.Button();
            this.groupBox_RawWeight = new System.Windows.Forms.GroupBox();
            this.label_rwBR = new System.Windows.Forms.Label();
            this.label_rwBL = new System.Windows.Forms.Label();
            this.label_rwTR = new System.Windows.Forms.Label();
            this.label_rwTL = new System.Windows.Forms.Label();
            this.groupBox_OffsetWeight = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.groupBox_General = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button_BluetoothAddDevice = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label_Status = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.copScatter1 = new WiiBalanceWalker.COPScatter();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.groupBox_RawWeight.SuspendLayout();
            this.groupBox_OffsetWeight.SuspendLayout();
            this.groupBox_General.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_rwWT
            // 
            this.label_rwWT.AutoSize = true;
            this.label_rwWT.Location = new System.Drawing.Point(73, 104);
            this.label_rwWT.Name = "label_rwWT";
            this.label_rwWT.Size = new System.Drawing.Size(23, 12);
            this.label_rwWT.TabIndex = 0;
            this.label_rwWT.Text = "WT";
            // 
            // button_Connect
            // 
            this.button_Connect.Location = new System.Drawing.Point(263, 58);
            this.button_Connect.Name = "button_Connect";
            this.button_Connect.Size = new System.Drawing.Size(213, 25);
            this.button_Connect.TabIndex = 0;
            this.button_Connect.Text = "Connect to Wii balance board";
            this.button_Connect.UseVisualStyleBackColor = true;
            this.button_Connect.Click += new System.EventHandler(this.button_Connect_Click);
            // 
            // groupBox_RawWeight
            // 
            this.groupBox_RawWeight.Controls.Add(this.label_rwBR);
            this.groupBox_RawWeight.Controls.Add(this.label_rwBL);
            this.groupBox_RawWeight.Controls.Add(this.label_rwTR);
            this.groupBox_RawWeight.Controls.Add(this.label_rwTL);
            this.groupBox_RawWeight.Controls.Add(this.label_rwWT);
            this.groupBox_RawWeight.Location = new System.Drawing.Point(14, 11);
            this.groupBox_RawWeight.Name = "groupBox_RawWeight";
            this.groupBox_RawWeight.Size = new System.Drawing.Size(175, 128);
            this.groupBox_RawWeight.TabIndex = 3;
            this.groupBox_RawWeight.TabStop = false;
            this.groupBox_RawWeight.Text = "Raw Weight (kgf)";
            // 
            // label_rwBR
            // 
            this.label_rwBR.AutoSize = true;
            this.label_rwBR.Location = new System.Drawing.Point(118, 70);
            this.label_rwBR.Name = "label_rwBR";
            this.label_rwBR.Size = new System.Drawing.Size(21, 12);
            this.label_rwBR.TabIndex = 0;
            this.label_rwBR.Text = "BR";
            // 
            // label_rwBL
            // 
            this.label_rwBL.AutoSize = true;
            this.label_rwBL.Location = new System.Drawing.Point(29, 70);
            this.label_rwBL.Name = "label_rwBL";
            this.label_rwBL.Size = new System.Drawing.Size(20, 12);
            this.label_rwBL.TabIndex = 0;
            this.label_rwBL.Text = "BL";
            // 
            // label_rwTR
            // 
            this.label_rwTR.AutoSize = true;
            this.label_rwTR.Location = new System.Drawing.Point(118, 30);
            this.label_rwTR.Name = "label_rwTR";
            this.label_rwTR.Size = new System.Drawing.Size(21, 12);
            this.label_rwTR.TabIndex = 0;
            this.label_rwTR.Text = "TR";
            // 
            // label_rwTL
            // 
            this.label_rwTL.AutoSize = true;
            this.label_rwTL.Location = new System.Drawing.Point(29, 30);
            this.label_rwTL.Name = "label_rwTL";
            this.label_rwTL.Size = new System.Drawing.Size(20, 12);
            this.label_rwTL.TabIndex = 0;
            this.label_rwTL.Text = "TL";
            // 
            // groupBox_OffsetWeight
            // 
            this.groupBox_OffsetWeight.Controls.Add(this.label2);
            this.groupBox_OffsetWeight.Controls.Add(this.label1);
            this.groupBox_OffsetWeight.Location = new System.Drawing.Point(196, 11);
            this.groupBox_OffsetWeight.Name = "groupBox_OffsetWeight";
            this.groupBox_OffsetWeight.Size = new System.Drawing.Size(124, 128);
            this.groupBox_OffsetWeight.TabIndex = 4;
            this.groupBox_OffsetWeight.TabStop = false;
            this.groupBox_OffsetWeight.Text = "COP Values (cm)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "COPy";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "COPx";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(17, 59);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(213, 24);
            this.button5.TabIndex = 7;
            this.button5.Text = "Trace";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // groupBox_General
            // 
            this.groupBox_General.Controls.Add(this.button4);
            this.groupBox_General.Controls.Add(this.textBox2);
            this.groupBox_General.Controls.Add(this.button1);
            this.groupBox_General.Controls.Add(this.button_BluetoothAddDevice);
            this.groupBox_General.Controls.Add(this.button5);
            this.groupBox_General.Controls.Add(this.button_Connect);
            this.groupBox_General.Location = new System.Drawing.Point(326, 11);
            this.groupBox_General.Name = "groupBox_General";
            this.groupBox_General.Size = new System.Drawing.Size(491, 128);
            this.groupBox_General.TabIndex = 0;
            this.groupBox_General.TabStop = false;
            this.groupBox_General.Text = "General";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(17, 92);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(118, 24);
            this.button4.TabIndex = 10;
            this.button4.Text = "Select Data Folder";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // textBox2
            // 
            this.textBox2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox2.Location = new System.Drawing.Point(141, 95);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(335, 21);
            this.textBox2.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(17, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(213, 25);
            this.button1.TabIndex = 4;
            this.button1.Text = "Zero";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_BluetoothAddDevice
            // 
            this.button_BluetoothAddDevice.Location = new System.Drawing.Point(263, 24);
            this.button_BluetoothAddDevice.Name = "button_BluetoothAddDevice";
            this.button_BluetoothAddDevice.Size = new System.Drawing.Size(213, 25);
            this.button_BluetoothAddDevice.TabIndex = 1;
            this.button_BluetoothAddDevice.Text = "Add bluetooth Wii device";
            this.button_BluetoothAddDevice.UseVisualStyleBackColor = true;
            this.button_BluetoothAddDevice.Click += new System.EventHandler(this.button_BluetoothAddDevice_Click);
            // 
            // textBox1
            // 
            this.textBox1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox1.Location = new System.Drawing.Point(6, 50);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(101, 21);
            this.textBox1.TabIndex = 8;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(6, 20);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(162, 24);
            this.button3.TabIndex = 6;
            this.button3.Text = "Start";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 77);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(162, 25);
            this.button2.TabIndex = 5;
            this.button2.Text = "Pause";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label_Status
            // 
            this.label_Status.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_Status.Location = new System.Drawing.Point(12, 142);
            this.label_Status.Name = "label_Status";
            this.label_Status.Size = new System.Drawing.Size(805, 22);
            this.label_Status.TabIndex = 4;
            this.label_Status.Text = "WiiBoard Status";
            this.label_Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Location = new System.Drawing.Point(14, 172);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(174, 118);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Controls";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(113, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "Seconds";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButton2);
            this.groupBox2.Controls.Add(this.radioButton1);
            this.groupBox2.Location = new System.Drawing.Point(14, 297);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(168, 67);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sampling Frequency";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(7, 43);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(54, 16);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "40 Hz";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(7, 21);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(54, 16);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "20 Hz";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButton6);
            this.groupBox3.Controls.Add(this.radioButton5);
            this.groupBox3.Controls.Add(this.radioButton3);
            this.groupBox3.Controls.Add(this.radioButton4);
            this.groupBox3.Location = new System.Drawing.Point(14, 370);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(168, 91);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Toggle MATLAB";
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(75, 42);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(29, 16);
            this.radioButton5.TabIndex = 2;
            this.radioButton5.Text = "2";
            this.radioButton5.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Checked = true;
            this.radioButton3.Location = new System.Drawing.Point(7, 20);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(38, 16);
            this.radioButton3.TabIndex = 1;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Off";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(78, 20);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(29, 16);
            this.radioButton4.TabIndex = 0;
            this.radioButton4.Text = "1";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // elementHost1
            // 
            this.elementHost1.Location = new System.Drawing.Point(196, 167);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(621, 372);
            this.elementHost1.TabIndex = 6;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = this.copScatter1;
            // 
            // textBox3
            // 
            this.textBox3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox3.Location = new System.Drawing.Point(14, 467);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(101, 21);
            this.textBox3.TabIndex = 10;
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Location = new System.Drawing.Point(7, 42);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(58, 16);
            this.radioButton6.TabIndex = 3;
            this.radioButton6.Text = "Offline";
            this.radioButton6.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 553);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.elementHost1);
            this.Controls.Add(this.label_Status);
            this.Controls.Add(this.groupBox_General);
            this.Controls.Add(this.groupBox_OffsetWeight);
            this.Controls.Add(this.groupBox_RawWeight);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Wii Balance Walker - Version 0.4";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.groupBox_RawWeight.ResumeLayout(false);
            this.groupBox_RawWeight.PerformLayout();
            this.groupBox_OffsetWeight.ResumeLayout(false);
            this.groupBox_OffsetWeight.PerformLayout();
            this.groupBox_General.ResumeLayout(false);
            this.groupBox_General.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_rwWT;
        private System.Windows.Forms.Button button_Connect;
        private System.Windows.Forms.GroupBox groupBox_RawWeight;
        private System.Windows.Forms.Label label_rwBR;
        private System.Windows.Forms.Label label_rwBL;
        private System.Windows.Forms.Label label_rwTR;
        private System.Windows.Forms.Label label_rwTL;
        private System.Windows.Forms.GroupBox groupBox_OffsetWeight;
        private System.Windows.Forms.GroupBox groupBox_General;
        private System.Windows.Forms.Label label_Status;
        private System.Windows.Forms.Button button_BluetoothAddDevice;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private COPScatter copScatter1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.RadioButton radioButton6;
    }
}

