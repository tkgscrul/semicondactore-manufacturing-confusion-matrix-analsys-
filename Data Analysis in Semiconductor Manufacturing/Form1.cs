using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.WinForms;
using System.Windows.Media;
using LiveCharts.Defaults;

namespace Data_Analysis_in_Semiconductor_Manufacturing
{
    public partial class Form1 : Form
    {

        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        public double GetRandomNumber(double minimum, double maximum)
        {
            lock (syncLock) { 
            return random.NextDouble() * (maximum - minimum) + minimum;
            }
        }
        public void button1_Click(object sender, EventArgs e)
        {
            var truef = GetRandomNumber(0.97, 1);
            var truep = GetRandomNumber(0.01, 0.04);
            var predictp = GetRandomNumber(0.97, 1);
            var predictf = GetRandomNumber(0.01, 0.04);
            var total = truef + truep + predictp + predictf;
            var accuracy = (truep + predictp) / total;
            var misclassificationRate = (predictp + truef) / total;
            var truePositiveRate = truep / (predictf + truep);
            var falsePositiveRate = predictf / (truef + predictf);
            var trueNegativeRate = predictp / (predictf + truef);
            var Precision = predictp / (predictp + truep);
            var Prevalence = (truep + predictp) + total;
            var nullErrorRateTPPF = (truep + predictf) + total;
            var nullErrorRateTFPF = (truef + predictf) + total;
            var nullErrorRateTFPP = (truef + predictp) + total;

            trueflbu.Text = truef.ToString();
            trueplbu.Text = truep.ToString();
            predictflbu.Text = predictf.ToString();
            predictplbu.Text = predictp.ToString();
            totallbu.Text = total.ToString();
            accuracylbu.Text = accuracy.ToString();
            misclassificationRatelbu.Text = misclassificationRate.ToString();
            truePositiveRatelbu.Text = truePositiveRate.ToString();
            falsePositiveRatelbu.Text = falsePositiveRate.ToString();
            trueNegativeRatelbu.Text = trueNegativeRate.ToString();
            Precisionlbu.Text = Precision.ToString();
            Prevalencelbu.Text = Prevalence.ToString();
            nullErrorRateTPPFlbu.Text = nullErrorRateTPPF.ToString();
            nullErrorRateTFPFlbu.Text = nullErrorRateTFPF.ToString();
            nullErrorRateTFPPlbu.Text = nullErrorRateTFPP.ToString();

            if (truep < 0.02 & predictf < 0.02)
            {
                testres.Text = "Support Vector Classifier";
            }
            else if (truef > 0.99 & predictp > 0.99)
            {
                testres.Text = "Descision Tree Classifie";
            }
            else
                testres.Text = "Logistic Regression Classifier";
            cartesianChart1.Series.Clear();
            cartesianChart1.Series.Add(new HeatSeries
            {
                GradientStopCollection = new GradientStopCollection
                {
                    
                },
                    Foreground = System.Windows.Media.Brushes.Black,
                FontSize = 13,
                Values = new ChartValues<HeatPoint>
                {
                    new HeatPoint(0, 0, truep),
                    new HeatPoint(0, 1, truef),

                    new HeatPoint(1, 0, predictp),
                    new HeatPoint(1, 1, predictf),
                },
                DataLabels = true,
                
            });
        }

        public Form1()
        {

            InitializeComponent();
            var truef = GetRandomNumber(0, 0);
            var truep = GetRandomNumber(0, 0);
            var predictp = GetRandomNumber(0, 0);
            var predictf = GetRandomNumber(0, 0);

            cartesianChart1.Series.Add(new HeatSeries
            {
                Foreground = System.Windows.Media.Brushes.Black,
                FontSize = 13,
                Values = new ChartValues<HeatPoint>
                {
                    new HeatPoint(0, 0, truep),
                    new HeatPoint(0, 1, truef),

                    new HeatPoint(1, 0, predictp),
                    new HeatPoint(1, 1, predictf),
                },
                DataLabels = true,
                //The GradientStopCollection is optional
                //If you do not set this property, LiveCharts will set a gradient
                GradientStopCollection = new GradientStopCollection
                {
                    new GradientStop(System.Windows.Media.Color.FromRgb(255, 255, 255), 0),
                    new GradientStop(System.Windows.Media.Color.FromRgb(200, 200, 255), 0.2),
                    new GradientStop(System.Windows.Media.Color.FromRgb(150, 150, 255), 0.4),
                    new GradientStop(System.Windows.Media.Color.FromRgb(100, 100, 255), 0.6),
                    new GradientStop(System.Windows.Media.Color.FromRgb(50, 50, 255), 0.8),
                    new GradientStop(System.Windows.Media.Color.FromRgb(0, 0, 255), 1),
                }
            });

            cartesianChart1.AxisX.Add(new Axis
            {
                //LabelsRotation = -15,
                Title = "Predict Label",
                Labels = new[]
                {
                    "pass",
                    "fail",
                },
                Separator = new Separator { Step = 1 }
            });

            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "True Label",
                Labels = new[]
                {
                    "fail",
                    "pass",
                }
            });
        }
    }
}
