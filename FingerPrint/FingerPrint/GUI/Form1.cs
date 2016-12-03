using FingerPrint;
using FingerPrint.AuxiliaryClasses;
using FingerPrint.Controllers;
using FingerPrint.Models;
using FingerPrint.Models.Implementations;
using FingerPrint.Models.Interfaces.TypeInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        /// 






        private void Form1_Load(object sender, EventArgs e)
        {

             updateListViews(); // Fill listviews
             fillGroupComboBox(); // Fill Group comboBox

        }




        private void selectFileButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var filePath = System.IO.Path.GetFullPath(openFileDialog1.FileName);
                fileLocationTextBox.Text = filePath;


            }
        }
        private void saveButtonTab1_Click(object sender, EventArgs e)
        {
            try
            {
                StreamReader input = new StreamReader(fileLocationTextBox.Text);
                ITextViewModel model = _textController.CreateText(initialFileNameTextbox.Text, input, UniversalCountSize.CountSize, initialAuthorTextBox.Text);
                //IGroupViewModel group = _groupController.CreateGroup("group" + model.GetName(), UniversalCountSize.CountSize);
                updateTextListView(fileListViewTab1);
            }
            catch(System.IO.FileNotFoundException)
            {
                string errorMessage = "You need to select a valid .txt file";
                var form2 = new ErrorMessageDisplay(errorMessage);
                form2.Show(this);
            }
            catch (ArgumentException)
            {

                string errorMessage = "You cannot add a file with the same name as another file";
                var form2 = new ErrorMessageDisplay(errorMessage);
                form2.Show(this);
            }
           
            

            if (filesRadioButton.Checked == true)
            {
                updateTextListView(fileGroupListViewTab2);

            }
            if (filesRadioButtonTab3.Checked == true)
            {
                updateTextListView(fileGroupListViewTab3);
            }




        }

        private void executeAnalysisButton_Click(object sender, EventArgs e)
        {

            //Fake Data, created fake text, added data
            _modelFactory = new ModelFactory();
            ModelFactory modelFactory = new ModelFactory();
            StreamReader input = new StreamReader(fileLocationTextBox.Text);
            IFlexibleWordCountModel wordCountModel = _modelFactory.GetFlexibleCountModel(10);
            modelFactory.GenerateCountsTestMethod(input, wordCountModel);
            TextModel model = new TextModel("Tales", wordCountModel);

            //table
            dataTable.Rows.Add();
            DataGridViewRow row = (DataGridViewRow)dataTable.Rows[0].Clone();
            DataGridViewRow row2 = (DataGridViewRow)dataTable.Rows[0].Clone();

            DataGridViewRow rowToAdd = row;
            DataGridViewRow rowToAdd2 = row2;
            dataTable.Rows.RemoveAt(0);

            rowToAdd.Cells[0].Value = "Tail";

            for (int i = 0; i < model.GetLength(); i++)
            {
                rowToAdd.Cells[i + 1].Value = wordCountModel.GetAt(false, i);

            }
            dataTable.Rows.Add(rowToAdd);


            tabControl1.SelectTab(analysisTab);
            analysisTab.Visible = true;

            //graph
            analysisLineChart.Series.Add("Tails");

            analysisLineChart.Series["Tails"].ChartType = SeriesChartType.Line;


            for (int i = 1; i <= 10; i++)
            {

                analysisLineChart.Series["Tails"].Points.AddXY(i, wordCountModel.GetAt(false, i - 1));

            }
            analysisLineChart.Series["Tails"].ChartArea = "ChartArea1";

            //ACTUAL PROCESS

            //table
            DataGridViewRow rowTemp = (DataGridViewRow)dataTable.Rows[0].Clone();
            dataTable.Rows.RemoveAt(0);
            ITextViewModel textToAdd;



            foreach (ListViewItem item in analysisListView.Items)
            {
                dataTable.Rows.Add();
                DataGridViewRow rowItem = rowTemp;
                textToAdd = _textController.GetTextByName(item.Text);
                rowToAdd.Cells[0].Value = textToAdd.GetName();

                for (int i = 0; i < model.GetLength(); i++)
                {
                    rowToAdd.Cells[i + 1].Value = wordCountModel.GetAt(false, i);

                }
                dataTable.Rows.Add(rowItem);

            }




            tabControl1.SelectTab(analysisTab);
            analysisTab.Visible = true;

            //graph
            analysisLineChart.Series.Add("Tails");

            analysisLineChart.Series["Tails"].ChartType = SeriesChartType.Line;


            for (int i = 1; i <= 10; i++)
            {

                analysisLineChart.Series["Tails"].Points.AddXY(i, wordCountModel.GetAt(false, i - 1));

            }
            analysisLineChart.Series["Tails"].ChartArea = "ChartArea1";

        }



        private void editButton_Click(object sender, EventArgs e)
        {
            if (fileListViewTab1.SelectedItems.Count > 0)
            {
                ListViewItem item = fileListViewTab1.SelectedItems[0];
                string textName = item.SubItems[1].Text;
                ITextViewModel model = _textController.GetTextByName(textName);
                var form = new FormPopUpFileEdit(model, _textController, this);
                form.Show(this);
            }
            else
            {
                string errorMessage = "You need to select an item to edit if first!";
                var form2 = new ErrorMessageDisplay(errorMessage);
                form2.Show(this);
            }


        }

        private void addButtonTab2_Click(object sender, EventArgs e)
        {
            try
            {
                IGroupViewModel model = _groupController.GetGroupByName(groupComboBox.Text);
                ListViewItem itemToMove = fileGroupListViewTab2.SelectedItems[0];
               
                if (filesRadioButton.Checked)
                {
                    _groupController.AddItemToGroup(model, _textController.GetTextByName(itemToMove.SubItems[1].Text));
                }
                else
                {
                    _groupController.AddItemToGroup(model, _groupController.GetGroupByName(itemToMove.Text));

                }
                groupComboBox_SelectedIndexChanged(sender, e);
                fileGroupListViewTab2.Items.Remove(itemToMove);
            }
            catch(ArgumentException)
            {

                if (groupComboBox.Text == "")
                {
                    string errorMessage = "You need to select a group or create one first!";
                    var form2 = new ErrorMessageDisplay(errorMessage);
                    form2.Show(this);
                }
                else if (fileGroupListViewTab2.SelectedItems.Count < 0)
                {
                    string errorMessage = "You need to select an item to edit if first!";
                    var form2 = new ErrorMessageDisplay(errorMessage);
                    form2.Show(this);
                }
                else
                {
                    
                    string errorMessage = "You cannot add a group to itself, or a text to a group that already contains it!";
                    var form2 = new ErrorMessageDisplay(errorMessage);
                    form2.Show(this);
                }
             
             
                }

        }

        private void addButtonTab3_Click(object sender, EventArgs e)
        {
           // try
            //{
                
                ListViewItem itemToMove = fileGroupListViewTab3.SelectedItems[0];

                if (filesRadioButtonTab3.Checked)
                {
                    IGroupViewModel model = _groupController.CreateGroup(itemToMove.SubItems[1].Text+" group", UniversalCountSize.CountSize);
                    _groupController.AddItemToGroup(model, _textController.GetTextByName(itemToMove.SubItems[1].Text));
                    _analysisController.AddToActiveGroups(model);
                    
                }
                else
                {
                    _analysisController.AddToActiveGroups(_groupController.GetGroupByName(itemToMove.Text));

                }
                
                fileGroupListViewTab3.Items.Remove(itemToMove);
                fillGroupComboBox();
                analysisListView.Items.Clear();
                List<IGroupViewModel> groupList = _analysisController.GetActiveGroups();
                foreach (IGroupViewModel groupEntry in groupList)
                {
                    ListViewItem itemGroup = new ListViewItem();

                   itemGroup.Text = groupEntry.GetName();
                   analysisListView.Items.Add(itemGroup);

                }

            //}
            //catch (ArgumentException)
            //{

                 if (fileGroupListViewTab3.SelectedItems.Count < 0)
                {
                    string errorMessage = "You need to select an item to edit if first!";
                    var form2 = new ErrorMessageDisplay(errorMessage);
                    form2.Show(this);
                }
                
            //}

        }

        private void removeButtonTab3_Click(object sender, EventArgs e)
        {
            if (analysisListView.SelectedItems.Count > 0)
            {

                ListViewItem itemToMove = analysisListView.SelectedItems[0];
                _analysisController.RemoveFromActiveGroups(_groupController.GetGroupByName(itemToMove.Text));

                analysisListView.Items.Clear();
                List<IGroupViewModel> groupList = _analysisController.GetActiveGroups();
                foreach (IGroupViewModel groupEntry in groupList)
                {
                    ListViewItem itemGroup = new ListViewItem();

                    itemGroup.Text = groupEntry.GetName();
                    analysisListView.Items.Add(itemGroup);

                }
                updateListViews();
            }

        }

        private void removeButtonTab2_Click(object sender, EventArgs e)
        {
            if (fileGroupListViewTab2.SelectedItems.Count > 0)
            {
                ListViewItem itemToMove = groupListViewTab2.SelectedItems[0];
                IGroupViewModel model = _groupController.GetGroupByName(groupComboBox.Text);
                _groupController.RemoveItemFromGroup(model, _groupController.GetGroupByName(itemToMove.Text));
                groupListViewTab2.Items.Remove(itemToMove);
                fillGroupComboBox();
                updateListViews();
               
            }
        }

        public void updateListViews()
        {
            fileListViewTab1.Items.Clear();
            updateTextListView(fileListViewTab1);
            if (filesRadioButton.Checked == true)
            {
                fileGroupListViewTab2.Items.Clear();
               
                updateTextListView(fileGroupListViewTab2);

            }
            if (filesRadioButtonTab3.Checked == true)
            {
                fileGroupListViewTab3.Items.Clear();
               
                    updateTextListView(fileGroupListViewTab3);
            }

            if (groupsRadioButton.Checked == true)
            {
                fileGroupListViewTab2.Items.Clear();
                updateGroupListView(fileGroupListViewTab2);

            }
            if (groupsRadioButtonTab3.Checked == true)
            {
                fileGroupListViewTab3.Items.Clear();
                updateGroupListView(fileGroupListViewTab3);
            }

        }


        public void updateGroupListView(ListView listView)
        {
            listView.Items.Clear();
            List<IGroupViewModel> groupList = _groupController.GetAllGroups();
            foreach (IGroupViewModel groupEntry in groupList)
            {
                ListViewItem itemGroup = new ListViewItem();

                itemGroup.Text = groupEntry.GetName();
                itemGroup.SubItems.Add("");
                itemGroup.SubItems.Add("");
                listView.Items.Add(itemGroup);

            }
        }
        public void updateTextListView(ListView listView)
        {
            listView.Items.Clear();
            List<ITextViewModel> textList = _textController.GetAllTexts();
           
            foreach (ITextViewModel textEntry in textList)
            {
                 ListViewItem item = new ListViewItem();
                item.Text = textEntry.GetAuthor();
                item.SubItems.Add(textEntry.GetName());
                String includeQuotesl;
                if (textEntry.GetIncludeQuotes())
                {
                    includeQuotesl = "Yes";
                }

                else
                {
                    includeQuotesl = "No";
                }
                item.SubItems.Add(includeQuotesl);

                if (!listView.Items.Contains(item))
                {
                    listView.Items.Add(item);
                }
            
            }
    
        }

        

        public void addGroup(string groupName)
        {
            _groupController.CreateGroup(groupName, UniversalCountSize.CountSize);
            groupComboBox.Items.Add(groupName);

        }

        public void editGroupName(string groupNameOld, string groupNameNew)
        {
            IGroupViewModel group = _groupController.GetGroupByName(groupNameOld);
            _groupController.UpdateGroup(group, groupNameNew);
            groupComboBox.Items.Clear();
            fillGroupComboBox();
            groupComboBox.SelectedIndex = groupComboBox.Items.IndexOf(groupNameNew);
            
        }

        private void newGroupButton_Click(object sender, EventArgs e)
        {
            var form = new NewGroupPopUp(this);
            form.Show(this);
        }

        private void editGroupNameButton_Click(object sender, EventArgs e)
        {
            if (groupComboBox.SelectedItem != null)
            {
                var form = new EditGroupName(groupComboBox.SelectedItem.ToString(), this);
                form.Show(this);
            }
        }

       

        private void groupsRadioButtonTab3_CheckedChanged(object sender, EventArgs e)
        {
            if (groupsRadioButtonTab3.Checked)
            {
                fileGroupListViewTab3.Items.Clear();
                authorHeaderTab3.Text = "Group Name";
                textHeaderTab3.Text = "";
                includeQuotesHeaderTab3.Text = "";
                updateGroupListView(fileGroupListViewTab3);
            }
            if (filesRadioButtonTab3.Checked)
            {
                fileGroupListViewTab3.Items.Clear();
                authorHeaderTab3.Text = "Author";
                textHeaderTab3.Text = "Text Title";
                includeQuotesHeaderTab3.Text = "Include Quotes";
                updateTextListView(fileGroupListViewTab3);
            }
        }

        private void groupsRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (groupsRadioButton.Checked)
            {
                fileGroupListViewTab2.Items.Clear();
                authorHeaderTab2.Text = "Group Name";
                titleHeaderTab2.Text = "";
                includeQuotesHeaderTab2.Text = "";
                updateGroupListView(fileGroupListViewTab2);
                
            }
            if (filesRadioButton.Checked)
            {
                fileGroupListViewTab2.Items.Clear();
                authorHeaderTab2.Text = "Author";
                titleHeaderTab2.Text = "Text Title";
                includeQuotesHeaderTab2.Text = "Include Quotes";
                updateTextListView(fileGroupListViewTab2);
            }
        }


        private void fillGroupComboBox()
        {
            List<IGroupViewModel> groupList = _groupController.GetAllGroups();
            
            foreach (IGroupViewModel groupEntry in groupList)
            {
                groupComboBox.Items.Add(groupEntry.GetName());

            }
        }

        private void deleteButtonTab2_Click(object sender, EventArgs e)
        {
            if (fileListViewTab1.SelectedItems.Count > 0)
            {
                ListViewItem item = fileListViewTab1.SelectedItems[0];
                string textName = item.SubItems[1].Text;
                ITextViewModel model = _textController.GetTextByName(textName);
                _textController.DeleteText(model);
                updateListViews();
                
            }
            else
            {
                string errorMessage = "You need to select an item to delete it first!";
                var form2 = new ErrorMessageDisplay(errorMessage);
                form2.Show(this);
            }
            
        }

        private void groupComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            IGroupViewModel model = _groupController.GetGroupByName(groupComboBox.Text);
            groupListViewTab2.Items.Clear();
            List<ITextOrGroupViewModel> list = model.GetMembers();
            
            foreach(ITextOrGroupViewModel textOrGroup in list)
            {
                ListViewItem item = new ListViewItem();
                item.Text = textOrGroup.GetName();
                groupListViewTab2.Items.Add(item);
            }
            


        }
    }
}


/*
 
    //TestData
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

*/
