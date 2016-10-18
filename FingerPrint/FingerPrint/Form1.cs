using FingerPrint.Models;
using FingerPrint.Models.Implementations;
using FingerPrint.Models.Interfaces.TypeInterfaces;
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
        IModelFactory<ISingleWordCountModel, IFlexibleWordCountModel<ISingleWordCountModel>> _modelFactory;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _modelFactory = new ModelFactory();
            
            IFlexibleWordCountModel<ISingleWordCountModel> wordCount = _modelFactory.GetFlexibleCountModel(10);
            TextModel model = new TextModel("test", wordCount);
            model.SetAuthor("Twain");
            model.SetName("Adventures of SON");
            model.SetIncludeQuotes(true);
            
            //String includeQuotesl;
            //if (model.GetIncludeQuotes)
            //{

            //}



            //ISingleWordCountModel model2 = new SingleWordCountModel(10);
            ISingleWordCountModel model2 = _modelFactory.GetSingleCountModel(10);
            //TEST POINTS/*
            model2.SetAt(0, 150);
            model2.SetAt(1, 250);
            model2.SetAt(2, 400);
            model2.SetAt(3, 550);
            model2.SetAt(4, 650);
            model2.SetAt(5, 500);
            model2.SetAt(6, 350);
            model2.SetAt(7, 150);
            model2.SetAt(8, 60);
            model2.SetAt(9, 30);
            
        }

        private void executeAnalysisButton_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(analysisTab);
            string groupOrTitle = "SampleData";
            //ISingleWordCountModel model = new SingleWordCountModel(10);
            ISingleWordCountModel model = _modelFactory.GetSingleCountModel(10);
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
            analysisLineChart.Series.Add(groupOrTitle);
            analysisLineChart.Series[groupOrTitle].ChartType = SeriesChartType.Line;
            for(int i = 1; i <= 10; i++)
            {
                analysisLineChart.Series[groupOrTitle].Points.AddXY(i, model.GetAt(i-1));
            }
            analysisLineChart.Series[groupOrTitle].ChartArea = "ChartArea1";


        }

       

        private void selectFileButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var filePath = System.IO.Path.GetFullPath(openFileDialog1.FileName);
                fileLocationTextBox.Text = filePath;
               

            }
        }

        private void addButtonTab3_Click(object sender, EventArgs e)
        {
            /* 
            ListViewItem itemToMove = fileListViewTab1.SelectedItems[0];
            ListViewItem itemToAdd = (ListViewItem)itemToMove.Clone();
            analysisListViewTab1.Items.Add(itemToAdd);
            fileListViewTab1.Items.Remove(itemToMove);
            */

        
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            var form = new FormPopUpFileEdit();
            form.Show(this);
        }

        private void saveButtonTab2_Click(object sender, EventArgs e)
        {

        }
    }
}
