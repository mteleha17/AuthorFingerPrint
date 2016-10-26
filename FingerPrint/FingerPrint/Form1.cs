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
            
            ListViewItem item = new ListViewItem();
            item.Text = model.GetAuthor();
            item.SubItems.Add(model.GetName());
            item.SubItems.Add(includeQuotesl);
            fileListViewTab1.Items.Add(item);
            //fileGroupListViewTab2.Items.Add(item);
            //filesAndGroupsListviewTab3.Items.Add(item);



            
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

            
            ISingleWordCountModel withQuotes2 = _modelFactory.GetSingleCountModel(10);
            ISingleWordCountModel withoutQuotes2 = _modelFactory.GetSingleCountModel(10);
            withoutQuotes2.SetAt(0, 300);
            withoutQuotes2.SetAt(1, 450);
            withoutQuotes2.SetAt(2, 600);
            withoutQuotes2.SetAt(3, 750);
            withoutQuotes2.SetAt(4, 900);
            withoutQuotes2.SetAt(5, 700);
            withoutQuotes2.SetAt(6, 450);
            withoutQuotes2.SetAt(7, 225);
            withoutQuotes2.SetAt(8, 90);
            withoutQuotes2.SetAt(9, 45);
            IFlexibleWordCountModel wordCount2 = _modelFactory.GetFlexibleCountModel(withQuotes2, withoutQuotes2);

            TextModel model2 = new TextModel("test2", wordCount2);
            model2.SetAuthor("Paul");
            model2.SetName("Failures of Liberalism");
            model2.SetIncludeQuotes(false);
            string groupOrTitle2 = "SampleData2";
            ISingleWordCountModel wcount2 = model2.GetCounts();










            //graph
            analysisLineChart.Series.Add(groupOrTitle);
            analysisLineChart.Series.Add(groupOrTitle2);
            analysisLineChart.Series[groupOrTitle].ChartType = SeriesChartType.Line;
            analysisLineChart.Series[groupOrTitle2].ChartType = SeriesChartType.Line;

            for (int i = 1; i <= 10; i++)
            {
                
                analysisLineChart.Series[groupOrTitle].Points.AddXY(i, wcount.GetAt(i-1));
                analysisLineChart.Series[groupOrTitle2].Points.AddXY(i, wcount2.GetAt(i - 1));
            }
            analysisLineChart.Series[groupOrTitle].ChartArea = "ChartArea1";
            analysisLineChart.Series[groupOrTitle2].ChartArea = "ChartArea1";






            //table
            dataTable.Rows.Add();
            DataGridViewRow row = (DataGridViewRow)dataTable.Rows[0].Clone();
            DataGridViewRow row2 = (DataGridViewRow)dataTable.Rows[0].Clone();

            DataGridViewRow rowToAdd = row;
            DataGridViewRow rowToAdd2 = row2;
            dataTable.Rows.RemoveAt(0);
            
            rowToAdd.Cells[0].Value = groupOrTitle;
            rowToAdd2.Cells[0].Value = groupOrTitle2;
            for(int i =0; i<model.GetLength(); i++)
            {
                rowToAdd.Cells[i + 1].Value = wcount.GetAt(i);
                rowToAdd2.Cells[i + 1].Value = wcount2.GetAt(i);
            }
            dataTable.Rows.Add(rowToAdd);
            dataTable.Rows.Add(rowToAdd2);

            tabControl1.SelectTab(analysisTab);
            analysisTab.Visible = true;
        }



        private void selectFileButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var filePath = System.IO.Path.GetFullPath(openFileDialog1.FileName);
                fileLocationTextBox.Text = filePath;
               

            }
        }
        
      

        private void editButton_Click(object sender, EventArgs e)
        {
            if (fileListViewTab1.SelectedItems.Count > 0)
            {
                ListViewItem item = fileListViewTab1.SelectedItems[0];
                string authorName = item.SubItems[1].Text;
                string textName = item.SubItems[0].Text;
                string includeQuotes = item.SubItems[2].Text;
                var form = new FormPopUpFileEdit(authorName, textName, includeQuotes, this);
                form.Show(this);
            }

           
            
  
            
        }

        private void addButtonTab2_Click(object sender, EventArgs e)
        {
            if (fileGroupListViewTab2.SelectedItems.Count > 0)
            {

                ListViewItem itemToMove = fileGroupListViewTab2.SelectedItems[0];
                ListViewItem itemToAdd = (ListViewItem)itemToMove.Clone();
                groupListViewTab2.Items.Add(itemToAdd);
                fileGroupListViewTab2.Items.Remove(itemToMove);
            }
        }

        private void addButtonTab3_Click(object sender, EventArgs e)
        {
            if (filesAndGroupsListviewTab3.SelectedItems.Count > 0)
            {

                ListViewItem itemToMove = filesAndGroupsListviewTab3.SelectedItems[0];
                ListViewItem itemToAdd = (ListViewItem)itemToMove.Clone();
                analysisListView.Items.Add(itemToAdd);
                filesAndGroupsListviewTab3.Items.Remove(itemToMove);
            }
        }

        private void removeButtonTab3_Click(object sender, EventArgs e)
        {
            if (analysisListView.SelectedItems.Count > 0)
            {

                ListViewItem itemToMove = analysisListView.SelectedItems[0];
                ListViewItem itemToAdd = (ListViewItem)itemToMove.Clone();
                filesAndGroupsListviewTab3.Items.Add(itemToAdd);
                analysisListView.Items.Remove(itemToMove);
            }
        }

        private void removeButtonTab2_Click(object sender, EventArgs e)
        {
            if (fileGroupListViewTab2.SelectedItems.Count > 0)
            {

                ListViewItem itemToMove = groupListViewTab2.SelectedItems[0];
                ListViewItem itemToAdd = (ListViewItem)itemToMove.Clone();
                fileGroupListViewTab2.Items.Add(itemToAdd);
                groupListViewTab2.Items.Remove(itemToMove);
            }
        }

        public void updateListViews()
        {
          
        }



    }
}
