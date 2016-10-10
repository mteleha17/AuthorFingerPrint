using FingerPrint.Models;
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
            ISingleWordCountModel model = new SingleWordCountModel(10);
            int[] arrayPoints = new int[10];
            //TEST POINTS/*
            model.SetAt(0, 150);
            model.SetAt(1, 250);
            model.SetAt(2, 400);
            model.SetAt(3, 550);
            model.SetAt(4, 650);
            model.SetAt(5, 500);
            model.SetAt(6, 350);
            model.SetAt(7, 150);
            model.SetAt(8, 60);
            model.SetAt(9, 30);
            
            //*/
            AnalysisLineChart.Series.Add(groupOrTitle);
            AnalysisLineChart.Series[groupOrTitle].ChartType = SeriesChartType.Line;
            for(int i = 1; i <= 10; i++)
            {
                AnalysisLineChart.Series[groupOrTitle].Points.AddXY(i, model.GetAt(i-1));
            }
            AnalysisLineChart.Series[groupOrTitle].ChartArea = "ChartArea1";








        }

        private void uploadFileButton_Click(object sender, EventArgs e)
        {
          

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var onlyFileName = System.IO.Path.GetFileName(openFileDialog1.FileName);
                var listViewItem = new ListViewItem(onlyFileName);
                fileListViewTab1.Items.Add(listViewItem);

            }  
        }

        private void addButtonTab1_Click(object sender, EventArgs e)
        {
            ListViewItem itemToMove = fileListViewTab1.SelectedItems[0];
            ListViewItem itemToAdd = (ListViewItem)itemToMove.Clone();
            analysisListViewTab1.Items.Add(itemToAdd);
            fileListViewTab1.Items.Remove(itemToMove);


        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var filePath = System.IO.Path.GetFullPath(openFileDialog1.FileName);
                fileLocationTextBox.Text = filePath;
               

            }
        }

        private void addButtonTab3_Click(object sender, EventArgs e)
        {
            ListViewItem itemToMove = fileGroupListViewTab3.SelectedItems[0];
            ListViewItem itemToAdd = (ListViewItem)itemToMove.Clone();
            groupListView.Items.Add(itemToAdd);
            fileGroupListViewTab3.Items.Remove(itemToMove);
        }
    }
}
