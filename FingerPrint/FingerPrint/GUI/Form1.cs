using FingerPrint.AuxiliaryClasses;
using FingerPrint.Controllers;
using FingerPrint.Controllers.Implementations;
using FingerPrint.Models;
using FingerPrint.Models.Interfaces.TypeInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
namespace FingerPrint




//fix duplicate entries in analysis 


{
    public partial class Form1 : Form
    {
        //  private IModelFactory _modelFactory;
        private List<ITextOrGroupViewModel> _activeItems;
        private ITextController _textController;
        private IGroupController _groupController;
        //private IAnalysisController _analysisController;
        public Form1(ITextController textController,
            IGroupController groupController)
        {
            _activeItems = new List<ITextOrGroupViewModel>();
            //_analysisController = analysisController;
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
             //((GroupController)_groupController).DangerousDeleteEverything();
        }
        private void selectFileButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) //opens dialogue box
            {
                var filePath = System.IO.Path.GetFullPath(openFileDialog1.FileName); //gets the filepath
                fileLocationTextBox.Text = filePath; //places the file path within the respective textbox
            }
        }
        private void saveButtonTab1_Click(object sender, EventArgs e)
        {
            try
            {
                StreamReader input = new StreamReader(fileLocationTextBox.Text);
                if (initialAuthorTextBox.Text == "Name of Author")
                {
                    initialAuthorTextBox.Text = "";
                }

                ITextViewModel model = _textController.CreateText(initialFileNameTextBox.Text, input, UniversalConstants.CountSize, initialAuthorTextBox.Text); //creates the text in the db
                updateTextListView(fileListViewTab1);
                initialFileNameTextBox.Text = "New File Name";
                initialAuthorTextBox.Text = "Name of Author";
                fileLocationTextBox.Text = "File location";
               
            }
            catch(System.IO.FileNotFoundException)
            {
                string errorMessage = "You need to select a valid .txt file";
                var form2 = new ErrorMessageDisplay(errorMessage);
                form2.Show(this);
            }
            catch 
            {
                if(initialFileNameTextBox.Text =="")
                {
                    string errorMessage = "You cannot add a text without a file name";
                    var form2 = new ErrorMessageDisplay(errorMessage);
                    form2.Show(this);
                }
                else if (_textController.GetTextByName(initialFileNameTextBox.Text) != null)
                {
                    string errorMessage = "You cannot add a file with the same name as another file";
                    var form2 = new ErrorMessageDisplay(errorMessage);
                    form2.Show(this);
                }
                
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
            //if (_analysisController.GetActiveItems().Count > 0)
            if (_activeItems.Count > 0)
            {
                try
                {
                    dataTable.Rows.Clear(); //clear any previous analysis
                    analysisLineChart.Series.Clear();
                    //List<ITextOrGroupViewModel> groupList = _analysisController.GetActiveItems(); //gets any texts/groups that have been added to the analysis group
                    List<ITextOrGroupViewModel> groupList = new List<ITextOrGroupViewModel>(_activeItems); //gets any texts/groups that have been added to the analysis group
                    foreach (ITextOrGroupViewModel groupEntry in groupList)
                    {
                        //table
                        int rowId = dataTable.Rows.Add(); //new row created to make sure formatting in present
                        DataGridViewRow row = dataTable.Rows[rowId]; //assigns the new row to be worked on
                        ISingleWordCountModel model = groupEntry.GetCounts();
                        row.Cells[0].Value = groupEntry.GetName();  //name of group placed in the first cell of the datatable
                        for (int i = 0; i < groupEntry.GetLength(); i++)
                        {
                            row.Cells[i + 1].Value = model.GetAt(i);    //assigning all of the counts to cells
                        }
                        //graph
                        analysisLineChart.Series.Add(groupEntry.GetName()); // add the name of the series
                        analysisLineChart.Series[groupEntry.GetName()].ChartType = SeriesChartType.Line; // assign chart type
                        for (int i = 1; i <= UniversalConstants.CountSize; i++)
                        {
                            analysisLineChart.Series[groupEntry.GetName()].Points.AddXY(i, model.GetAt(i - 1)); // assigning all of the xy points on the chart
                        }
                        analysisLineChart.Series[groupEntry.GetName()].ChartArea = "Analysis Chart"; //neccesary so that the chart has a location for the series
                        analysisLineChart.Series[groupEntry.GetName()].BorderWidth = 3;
                        analysisLineChart.Series[groupEntry.GetName()].BorderDashStyle = ChartDashStyle.Solid;
                    }
                    tabControl1.SelectTab(analysisTab); //move to analysis page
                    this.WindowState = FormWindowState.Maximized; //maximize page size so that whole chart can be seen
                }
                catch
                {
                    string errorMessage = "You need to add groups to be analyzed";
                    var form2 = new ErrorMessageDisplay(errorMessage);
                    form2.Show(this);
                }
            }
            else
            {
                string errorMessage = "There are no groups or texts being analyzed. Please add a text or group before executing.";
                var form2 = new ErrorMessageDisplay(errorMessage);
                form2.Show(this);
            }

        }
        private void editButton_Click(object sender, EventArgs e)
        {
            if (fileListViewTab1.SelectedItems.Count > 0)   // opens a new window so that the user can edit the details of the selected text
            {
                ListViewItem item = fileListViewTab1.SelectedItems[0];
                string textName = item.SubItems[1].Text;
                ITextViewModel model = _textController.GetTextByName(textName);
                var form = new FormPopUpFileEdit(model, _textController, _groupController, this);
                form.Show(this);
            }
            else
            {
                string errorMessage = "You need to select an item to edit it first";
                var form2 = new ErrorMessageDisplay(errorMessage);
                form2.Show(this);
            }
        }
        private void addButtonTab2_Click(object sender, EventArgs e)    //allows for texts and groups to be added to groups
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
                updateListViews();
            }
            catch(ArgumentException )
            {
                if (groupComboBox.Text == "")
                {
                    string errorMessage = "You need to select a group or create one first";
                    var form2 = new ErrorMessageDisplay(errorMessage);
                    form2.Show(this);
                }
                else if (fileGroupListViewTab2.SelectedItems.Count <= 0)
                {
                    string errorMessage = "You need to select an item to add it";
                    var form2 = new ErrorMessageDisplay(errorMessage);
                    form2.Show(this);
                }
                else
                {
                    string errorMessage = "You cannot add a group to itself, or a text to a group that already contains it";
                    var form2 = new ErrorMessageDisplay(errorMessage);
                    form2.Show(this);
                }
             }
        }
        private void addButtonTab3_Click(object sender, EventArgs e)    //allows groups and texts to be added to an analysis
        {
           ListViewItem itemToMove = new ListViewItem(); 
           try
            {
             itemToMove = fileGroupListViewTab3.SelectedItems[0];
            
                if (filesRadioButtonTab3.Checked)
                 {
                    //_analysisController.AddToActiveItems(_textController.GetTextByName(itemToMove.SubItems[1].Text));
                    _activeItems.Add(_textController.GetTextByName(itemToMove.SubItems[1].Text));           
                }   
                else
                {
                    //_analysisController.AddToActiveItems(_groupController.GetGroupByName(itemToMove.Text));
                    _activeItems.Add(_groupController.GetGroupByName(itemToMove.Text));

                }

                analysisListView.Items.Clear();
                updateAnalysisGroups();
                updateListViews();
            }
            catch (ArgumentException)
            {
                if (fileGroupListViewTab3.SelectedItems.Count <= 0)
                {
                    string errorMessage = "You need to select an item to edit if first";
                    var form2 = new ErrorMessageDisplay(errorMessage);
                    form2.Show(this);
                }
                //else if(_groupController.GroupIsEmpty(_groupController.GetGroupByName(itemToMove.Text)))
                //{
                //    string errorMessage = "You cannot analyze an empty group";
                //    var form2 = new ErrorMessageDisplay(errorMessage);
                //    form2.Show(this);
                //}
                else
                {
                    string errorMessage = "Group already exists within the analysis group";
                    var form2 = new ErrorMessageDisplay(errorMessage);
                    form2.Show(this);
                }
            }
        }
        private void removeButtonTab3_Click(object sender, EventArgs e) //removes texts from the analysis group
        {
            try
            {
                ListViewItem itemToMove = analysisListView.SelectedItems[0];
                //_analysisController.RemoveFromActiveItems(itemToMove.Text);
                ITextOrGroupViewModel modelToRemove = _activeItems.Find(x => x.GetName() == itemToMove.Text);
                _activeItems.Remove(modelToRemove);

                analysisListView.Items.Clear();
                updateAnalysisGroups();
                updateListViews();
                fillGroupComboBox();
            }
            catch (Exception)
            {
                if (analysisListView.SelectedItems.Count <= 0)
                {
                    string errorMessage = "You need to select an item to remove it";
                    var form2 = new ErrorMessageDisplay(errorMessage);
                    form2.Show(this);
                }
            }
        }
        private void removeButtonTab2_Click(object sender, EventArgs e) //removes texts and groups from selected group
        {
            try {
                ListViewItem itemToMove = groupListViewTab2.SelectedItems[0];
                IGroupViewModel model = _groupController.GetGroupByName(groupComboBox.Text);
                
                if(_groupController.GetGroupByName(itemToMove.Text) != null)
                {
                    _groupController.RemoveItemFromGroup(model, _groupController.GetGroupByName(itemToMove.Text));
                }
                else
                {
                    _groupController.RemoveItemFromGroup(model, _textController.GetTextByName(itemToMove.Text));
                }
                //For debugging:
                //_analysisController.ItemIsActive(model.GetName());

                updateListViews();
                groupComboBox_SelectedIndexChanged(sender, e);
            }
            catch(Exception)
            {
                if(groupListViewTab2.SelectedItems.Count <= 0)
                {
                    string errorMessage = "You need to select an item to remove it";
                    var form2 = new ErrorMessageDisplay(errorMessage);
                    form2.Show(this);
                }
            }
        }
        public void updateListViews()   //a general update to all listviews within the form, adds all texts and groups depending on selections
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
        public void updateGroupListView(ListView listView) //specific method for updating listviews to a group interface
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
        public void updateTextListView(ListView listView)   //specifically updates listviews to show texts
        {
            listView.Items.Clear();
            List<ITextViewModel> textList = _textController.GetAllTexts();
            foreach (ITextViewModel textEntry in textList)
            {

                 ListViewItem item = new ListViewItem();
                string author = textEntry.GetAuthor();
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
        public void addGroup(string groupName)  //add group method utilized by the new group popup
        {
            try
            {
                _groupController.CreateGroup(groupName, UniversalConstants.CountSize);
                fillGroupComboBox();
                groupComboBox.SelectedIndex = groupComboBox.FindStringExact(groupName);
                updateListViews(); 
                
                              
            }
            catch
            {
                if (_textController.GetTextByName(groupName) != null) {
                    string errorMessage = "You cannot name a group the same name as one of the saved texts";
                    var form2 = new ErrorMessageDisplay(errorMessage);
                    form2.Show(this);
                }
                else if(_groupController.GetGroupByName(groupName) != null)
                {
                    string errorMessage = "You have already created that group";
                    var form2 = new ErrorMessageDisplay(errorMessage);
                    form2.Show(this);
                }
                else
                {
                    string errorMessage = "You must not leave the group name blank";
                    var form2 = new ErrorMessageDisplay(errorMessage);
                    form2.Show(this);
                }
                
                    
            }
            
        }

        

        public void editGroupName(string groupNameOld, string groupNameNew) // edit method used by the editGroup popup
        {
            if (_groupController.GetGroupByName(groupNameNew) == null)
            {
                IGroupViewModel group = _groupController.GetGroupByName(groupNameOld);
                _groupController.UpdateGroup(group, groupNameNew);
                fillGroupComboBox();
                groupComboBox.SelectedIndex = groupComboBox.Items.IndexOf(groupNameNew);
                analysisListView.Items.Clear();
                fillGroupComboBox();
                updateAnalysisGroups();
                updateListViews();
            }
            else
            {
                string errorMessage = "You cannot change a group name to one that already exists";
                var form2 = new ErrorMessageDisplay(errorMessage);
                form2.Show(this);
            }
         }
        private void newGroupButton_Click(object sender, EventArgs e) //used to open the newGroup popup
        {
            var form = new NewGroupPopUp(this);
            form.Show(this);
        }
        private void editGroupNameButton_Click(object sender, EventArgs e) // used to open the editGroup name popup
        {
            if (groupComboBox.SelectedItem != null)
            {
                var form = new EditGroupName(groupComboBox.SelectedItem.ToString(), this);
                form.Show(this);
            }
        }
        private void groupsRadioButtonTab3_CheckedChanged(object sender, EventArgs e)   //makes changes to listviews when radiobuttons are selected
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
        public void groupsRadioButton_CheckedChanged(object sender, EventArgs e) //makes changes to listviews when radiobuttons are selected
        {
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
        }
        private void fillGroupComboBox()    //gets all groups to add to the combo box
        {
            groupListViewTab2.Items.Clear();
            groupComboBox.Items.Clear();
            List<IGroupViewModel> groupList = _groupController.GetAllGroups();
            foreach (IGroupViewModel groupEntry in groupList)
            {
                groupComboBox.Items.Add(groupEntry.GetName());
            }
        }
        private void deleteButtonTab_Click(object sender, EventArgs e) //deletes texts from the system in tab1
        {
            if (fileListViewTab1.SelectedItems.Count > 0)
            {
               
                ListViewItem item = fileListViewTab1.SelectedItems[0];
                string textName = item.SubItems[1].Text;
                if (_activeItems.Contains(_activeItems.Find(x => x.GetName() == textName)))
                {
                    _activeItems.Remove(_activeItems.Find(x => x.GetName() == textName));
                }
                ITextViewModel model = _textController.GetTextByName(textName);
                //List<IGroupViewModel> groupList = _groupController.GetAllGroups();
                //foreach (IGroupViewModel groupEntry in groupList)
                //{
                //    if (_groupController.Contains(groupEntry.GetName(), textName))
                //    {
                //        _groupController.RemoveItemFromGroup(groupEntry, _textController.GetTextByName(textName));
                //    }
                //}
                _textController.DeleteText(model);
                updateListViews();
                updateAnalysisGroups();
                                groupComboBox_SelectedIndexChanged(sender, e);

            }
            else
            {
                string errorMessage = "You need to select an item to delete it first";
                var form2 = new ErrorMessageDisplay(errorMessage);
                form2.Show(this);
            }
        }
        private void groupComboBox_SelectedIndexChanged(object sender, EventArgs e) //makes the selected group the target group
        {
            groupListViewTab2.Items.Clear();
            if (groupComboBox.Text != "")
            {
                IGroupViewModel model = _groupController.GetGroupByName(groupComboBox.Text);
                List<ITextOrGroupViewModel> list = model.GetMembers();
                foreach (ITextOrGroupViewModel textOrGroup in list)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = textOrGroup.GetName();
                    groupListViewTab2.Items.Add(item);
                }
               
            }
        }
        private void deleteButtonTab2_Click(object sender, EventArgs e)
        {
            List<IGroupViewModel> groupList = _groupController.GetAllGroups();
            if (fileGroupListViewTab2.SelectedItems.Count > 0)
            {
                if (filesRadioButton.Checked)
                {
                    ListViewItem item = fileGroupListViewTab2.SelectedItems[0];
                    string textName = item.SubItems[1].Text;
                    if (_activeItems.Contains(_activeItems.Find(x => x.GetName() == textName))){ 
                    _activeItems.Remove(_activeItems.Find(x => x.GetName() == textName));
                }
                    
                    //foreach (IGroupViewModel groupEntry in groupList)
                    //{
                    //    if (_groupController.Contains(groupEntry.GetName(), textName)){
                    //        _groupController.RemoveItemFromGroup(groupEntry, _textController.GetTextByName(textName));
                    //    }
                    //}
                    _textController.DeleteText(_textController.GetTextByName(textName));
                }
                else
                {
                    ListViewItem item = fileGroupListViewTab2.SelectedItems[0];
                    string textName = item.SubItems[0].Text;
                    if (_activeItems.Contains(_activeItems.Find(x => x.GetName() == textName)))
                    {
                        _activeItems.Remove(_activeItems.Find(x => x.GetName() == textName));
                    }
                    //if (_analysisController.ItemIsActive(textName)){
                    //if (_activeItems.Exists(x => x.GetName() == textName))
                    //{
                    //    //_analysisController.RemoveFromActiveItems(textName);
                    //    ITextOrGroupViewModel modelToRemove = _activeItems.Find(x => x.GetName() == textName);
                    //    _activeItems.Remove(modelToRemove);


                    //}


                    //<JG>

                    _groupController.Delete(_groupController.GetGroupByName(textName));

                    //if (!_groupController.IsChild(_groupController.GetGroupByName(textName))){
                    //    _groupController.Delete(_groupController.GetGroupByName(textName));
                    //}
                    //else
                    //{
                    //    string errorMessage = "You cannot delete a group that is within a group";
                    //    var form2 = new ErrorMessageDisplay(errorMessage);
                    //    form2.Show(this);
                    //}
                    
                    //</JG>
                  
                }
                fillGroupComboBox();
                updateListViews();
                updateAnalysisGroups();
                groupComboBox_SelectedIndexChanged(sender, e);
            }
            else
            {
                string errorMessage = "You need to select an item to delete it first";
                var form2 = new ErrorMessageDisplay(errorMessage);
                form2.Show(this);
            }
        }
        public void updateAnalysisGroups()
        {
            //List<ITextOrGroupViewModel> groupList = _analysisController.GetActiveItems();
            List<ITextOrGroupViewModel> groupList = new List<ITextOrGroupViewModel>(_activeItems);
            analysisListView.Items.Clear();
            foreach (ITextOrGroupViewModel groupEntry in groupList)
            {
                ListViewItem itemGroup = new ListViewItem();
                itemGroup.Text = groupEntry.GetName();
                analysisListView.Items.Add(itemGroup);
            }
        }

        
    }
}
