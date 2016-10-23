using FingerPrint;
using FingerPrint.Controllers;
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
        private IModelFactory _modelFactory;
        private ITextController _textController;
        private IGroupController _groupController;
        private IAnalysisController _analysisController;

        public Form1(IAnalysisController analysisController,
            ITextController textController,
            IGroupController groupController)
        {
            _analysisController = analysisController;
            _textController = textController;
            _groupController = groupController;
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            _modelFactory = new ModelFactory();
            
            IFlexibleWordCountModel wordCount = _modelFactory.GetFlexibleCountModel(10);
            TextModel model = new TextModel("test", wordCount);
            model.SetAuthor("Twain");
            model.SetName("Adventures of SON");
            model.SetIncludeQuotes(true);
            
            String includeQuotesl;
            if (model.GetIncludeQuotes())
            {
                includeQuotesl = "Yes";
            }

            else{
                includeQuotesl = "No";
            }
            String[] row = { model.GetAuthor(), model.GetName(), includeQuotesl };
            ListViewItem item = new ListViewItem(row);
            fileListViewTab1.Items.Add(item); 


            
        }

        private void executeAnalysisButton_Click(object sender, EventArgs e)
        {
            //Fake Data, created fake text, added data
            _modelFactory = new ModelFactory();
            ISingleWordCountModel withQuotes = _modelFactory.GetSingleCountModel(10);
            ISingleWordCountModel withoutQuotes = _modelFactory.GetSingleCountModel(10);
            withoutQuotes.SetAt(0, 150);
            withoutQuotes.SetAt(1, 250);
            withoutQuotes.SetAt(2, 400);
            withoutQuotes.SetAt(3, 550);
            withoutQuotes.SetAt(4, 650);
            withoutQuotes.SetAt(5, 500);
            withoutQuotes.SetAt(6, 350);
            withoutQuotes.SetAt(7, 150);
            withoutQuotes.SetAt(8, 60);
            withoutQuotes.SetAt(9, 30);
            IFlexibleWordCountModel wordCount = _modelFactory.GetFlexibleCountModel(withQuotes,withoutQuotes);

            TextModel model = new TextModel("test", wordCount);
            model.SetAuthor("Twain");
            model.SetName("Adventures of SON");
            model.SetIncludeQuotes(false);
            string groupOrTitle = "SampleData1";

            ISingleWordCountModel wcount = model.GetCounts();
            //graph
            analysisLineChart.Series.Add(groupOrTitle);
            analysisLineChart.Series[groupOrTitle].ChartType = SeriesChartType.Line;
            for(int i = 1; i <= 10; i++)
            {
                analysisLineChart.Series[groupOrTitle].Points.AddXY(i, wcount.GetAt(i-1));
            }
            analysisLineChart.Series[groupOrTitle].ChartArea = "ChartArea1";


            //table
            dataTable.Rows.Add();
            DataGridViewRow row = (DataGridViewRow)dataTable.Rows[0].Clone();
            DataGridViewRow rowToAdd = row;
            dataTable.Rows.RemoveAt(0);
            
            row.Cells[0].Value = groupOrTitle;
            for(int i =0; i<model.GetLength(); i++)
            {
                rowToAdd.Cells[i + 1].Value = wcount.GetAt(i);
            }
            dataTable.Rows.Add(rowToAdd);

            tabControl1.SelectTab(analysisTab);
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

    }
}
