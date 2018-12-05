using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiiBalanceWalker
{
    public static class Globals
    {
        public static double COPx;
        public static double COPy;
        public static double COGx;
        public static double COGy;

        //keeps track of the last 600 seconds (600*100)
        //100 is the maximum sampling frequency
        public static double[] COGxArray = new double[60000];
        public static double[] COGyArray = new double[60000];

        public static double[] TLArray = new double[60000];
        public static double[] BLArray = new double[60000];
        public static double[] TRArray = new double[60000];
        public static double[] BRArray = new double[60000];

        public static double[] TimeArray = new double[60000];

        //time stays the same in seconds
        public static int Time;
        public static double TimeLeft;
        //period counts down
        public static int Period;
        public static double Freq;

        public static bool TimerOn;
        public static bool TraceOn;

        public static bool Offline;

    
    }
}
