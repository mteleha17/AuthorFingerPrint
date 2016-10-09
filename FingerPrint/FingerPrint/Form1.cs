using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FingerPrint
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void executeAnalysisButton_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(analysisTab);
            string groupOrTitle = "SampleData";
            int[] arrayPoints = new int[10];
            //TEST POINTS/*
            arrayPoints[0] = 150;
            arrayPoints[1] = 250;
            arrayPoints[2] = 400;
            arrayPoints[3] = 560;
            arrayPoints[4] = 435;
            arrayPoints[5] = 363;
            arrayPoints[6] = 320;
            arrayPoints[7] = 221;
            arrayPoints[8] = 190;
            arrayPoints[9] = 90;

            //*/
            AnalysisLineChart.Series.Add(groupOrTitle);
            AnalysisLineChart.Series[groupOrTitle].ChartType = SeriesChartType.Line;
            for(int i = 1; i <= 10; i++)
            {
                AnalysisLineChart.Series[groupOrTitle].Points.AddXY(i, arrayPoints[i-1]);
            }
            AnalysisLineChart.Series[groupOrTitle].ChartArea = "ChartArea1";








        }

        
    }
}
