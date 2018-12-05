using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using System.Windows.Media;

namespace WiiBalanceWalker
{
    /// <summary>
    /// Interaction logic for COPScatter.xaml
    /// </summary>
    public partial class COPScatter : UserControl, INotifyPropertyChanged
    {
        public COPScatter()
        {
            InitializeComponent();

            var r = new Random();
            ValuesA = new ChartValues<ObservablePoint>();
            ValuesB = new ChartValues<ScatterPoint>();
            //ValuesC = new ChartValues<ObservablePoint>();

            //MAKE THE WEIGHTS REALLY SMALL MAYBE ENOUGH DOTS WILL LOOK LIKE LINE
            
            ValuesA.Add(new ObservablePoint(Globals.COGx, Globals.COGy));
                
            
            
            ValuesB.Add(new ScatterPoint(Globals.COGx, Globals.COGy, 1));

            var lineSeries = new LineSeries
            {
                Values = ValuesA,
                StrokeThickness = 4,
                Fill = Brushes.Transparent,
                PointGeometrySize = 0,
                //DataLabels = true

            };

            SeriesCollection = new SeriesCollection { lineSeries };
            DataContext = this;

            XCoordinates = new[] { -200, -150, -100, -50, 0, 50, 100, 150, 200 };

            YCoordinates = new[] { -100, -50, 0, 50, 100 };

            IsReading = false;

            DataContext = this;

            

        }

        public ChartValues<ObservablePoint> ValuesA { get; set; }
        public ChartValues<ScatterPoint> ValuesB { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        public LineSeries lineSeries { get; set; }
        //public ChartValues<ScatterPoint> ValuesC { get; set; }

        public int[] XCoordinates { get; set; }
        public int[] YCoordinates { get; set; }

        const double boardXmin = -216;
        const double boardXmax = 216;
        const double boardYmin = -114;
        const double boardYmax = 114;

        public void Update()
        {
            /*for(var i = 0; i < 100; i++)
            {
                ValuesA[i].X = Globals.COGxArray[500 + i];
                ValuesA[i].Y = Globals.COGyArray[500 + i];
                ValuesA[i].Weight = 0.0001;
            }*/

            ValuesA.Add(new ObservablePoint(Globals.COGx, Globals.COGy));
            if (ValuesA.Count > 50) ValuesA.RemoveAt(0);
            //ValuesA[0].X = Globals.COPx;
            //ValuesA[0].Y = Globals.COPy;
            ValuesB[0].X = Globals.COGx;
            ValuesB[0].Y = Globals.COGy;
            ValuesB[0].Weight = 1;
        }

        public bool IsReading { get; set; }

        private void Read()
        {
            //var r = new Random();

            while (IsReading)
            {
                Thread.Sleep(150);
                var now = DateTime.Now;

                //_trend += r.Next(-8, 10);

                ValuesA.Add(new ObservablePoint(Globals.COGx, Globals.COGy));
                if (ValuesA.Count > 50) ValuesA.RemoveAt(0);
                //ValuesA[0].X = Globals.COPx;
                //ValuesA[0].Y = Globals.COPy;
                ValuesB[0].X = Globals.COGx;
                ValuesB[0].Y = Globals.COGy;
                ValuesB[0].Weight = 1;

                //lets only use the last 150 values
                //if (ChartValues.Count > 150) ChartValues.RemoveAt(0);
            }
        }

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
