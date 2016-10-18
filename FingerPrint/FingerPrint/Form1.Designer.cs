namespace FingerPrint
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.filesTab = new System.Windows.Forms.TabPage();
            this.editModifyGroupBox = new System.Windows.Forms.GroupBox();
            this.deleteButtonTab2 = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.editModifyLabel = new System.Windows.Forms.Label();
            this.fileListViewTab1 = new System.Windows.Forms.ListView();
            this.authorHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textTitleHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.includeQuotesHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.addGroupBox = new System.Windows.Forms.GroupBox();
            this.addLabel = new System.Windows.Forms.Label();
            this.selectFileButtonTab2 = new System.Windows.Forms.Button();
            this.saveButtonTab2 = new System.Windows.Forms.Button();
            this.fileLocationTextBox = new System.Windows.Forms.TextBox();
            this.newFileNameTextbox = new System.Windows.Forms.TextBox();
            this.authorTextBox = new System.Windows.Forms.TextBox();
            this.groupsTab = new System.Windows.Forms.TabPage();
            this.groupsGroupBox = new System.Windows.Forms.GroupBox();
            this.newGroupButton = new System.Windows.Forms.Button();
            this.groupComboBox = new System.Windows.Forms.ComboBox();
            this.groupListView = new System.Windows.Forms.ListView();
            this.deleteButtonTab3 = new System.Windows.Forms.Button();
            this.removeButtonTab2 = new System.Windows.Forms.Button();
            this.addButtonTab2 = new System.Windows.Forms.Button();
            this.radiouttonPanel = new System.Windows.Forms.Panel();
            this.groupsRadioButton = new System.Windows.Forms.RadioButton();
            this.filesRadioButton = new System.Windows.Forms.RadioButton();
            this.fileGroupListViewTab3 = new System.Windows.Forms.ListView();
            this.groupTabLabel = new System.Windows.Forms.Label();
            this.fingerPrintTab = new System.Windows.Forms.TabPage();
            this.fingerprintAnalysisGroupBox = new System.Windows.Forms.GroupBox();
            this.radioButtonContainer = new System.Windows.Forms.Panel();
            this.groupsRadioButtonTab3 = new System.Windows.Forms.RadioButton();
            this.filesRadioButtonTab3 = new System.Windows.Forms.RadioButton();
            this.analysisListView = new System.Windows.Forms.ListView();
            this.filesAndGroupsListviewTab3 = new System.Windows.Forms.ListView();
            this.executeAnalysisButton = new System.Windows.Forms.Button();
            this.removeButtonTab3 = new System.Windows.Forms.Button();
            this.addButtonTab3 = new System.Windows.Forms.Button();
            this.fALabel = new System.Windows.Forms.Label();
            this.analysisTab = new System.Windows.Forms.TabPage();
            this.chartGroupBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.analysisLineChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.authorHeaderTab2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.titleHeaderTab2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.includeQuotesHeaderTab2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl1.SuspendLayout();
            this.filesTab.SuspendLayout();
            this.editModifyGroupBox.SuspendLayout();
            this.addGroupBox.SuspendLayout();
            this.groupsTab.SuspendLayout();
            this.groupsGroupBox.SuspendLayout();
            this.radiouttonPanel.SuspendLayout();
            this.fingerPrintTab.SuspendLayout();
            this.fingerprintAnalysisGroupBox.SuspendLayout();
            this.radioButtonContainer.SuspendLayout();
            this.analysisTab.SuspendLayout();
            this.chartGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.analysisLineChart)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.filesTab);
            this.tabControl1.Controls.Add(this.groupsTab);
            this.tabControl1.Controls.Add(this.fingerPrintTab);
            this.tabControl1.Controls.Add(this.analysisTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1157, 605);
            this.tabControl1.TabIndex = 3;
            // 
            // filesTab
            // 
            this.filesTab.Controls.Add(this.editModifyGroupBox);
            this.filesTab.Controls.Add(this.addGroupBox);
            this.filesTab.Location = new System.Drawing.Point(4, 22);
            this.filesTab.Name = "filesTab";
            this.filesTab.Padding = new System.Windows.Forms.Padding(3);
            this.filesTab.Size = new System.Drawing.Size(1149, 579);
            this.filesTab.TabIndex = 1;
            this.filesTab.Text = "Files";
            this.filesTab.UseVisualStyleBackColor = true;
            // 
            // editModifyGroupBox
            // 
            this.editModifyGroupBox.Controls.Add(this.deleteButtonTab2);
            this.editModifyGroupBox.Controls.Add(this.editButton);
            this.editModifyGroupBox.Controls.Add(this.editModifyLabel);
            this.editModifyGroupBox.Controls.Add(this.fileListViewTab1);
            this.editModifyGroupBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.editModifyGroupBox.Location = new System.Drawing.Point(534, 3);
            this.editModifyGroupBox.Name = "editModifyGroupBox";
            this.editModifyGroupBox.Size = new System.Drawing.Size(612, 573);
            this.editModifyGroupBox.TabIndex = 7;
            this.editModifyGroupBox.TabStop = false;
            // 
            // deleteButtonTab2
            // 
            this.deleteButtonTab2.Location = new System.Drawing.Point(361, 351);
            this.deleteButtonTab2.Name = "deleteButtonTab2";
            this.deleteButtonTab2.Size = new System.Drawing.Size(75, 23);
            this.deleteButtonTab2.TabIndex = 3;
            this.deleteButtonTab2.Text = "Delete";
            this.deleteButtonTab2.UseVisualStyleBackColor = true;
            // 
            // editButton
            // 
            this.editButton.Location = new System.Drawing.Point(280, 352);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(75, 23);
            this.editButton.TabIndex = 2;
            this.editButton.Text = "Edit";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // editModifyLabel
            // 
            this.editModifyLabel.AutoSize = true;
            this.editModifyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editModifyLabel.Location = new System.Drawing.Point(202, 29);
            this.editModifyLabel.Name = "editModifyLabel";
            this.editModifyLabel.Size = new System.Drawing.Size(87, 20);
            this.editModifyLabel.TabIndex = 1;
            this.editModifyLabel.Text = "Edit/Modify";
            // 
            // fileListViewTab1
            // 
            this.fileListViewTab1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.authorHeader,
            this.textTitleHeader,
            this.includeQuotesHeader});
            this.fileListViewTab1.Location = new System.Drawing.Point(52, 75);
            this.fileListViewTab1.Name = "fileListViewTab1";
            this.fileListViewTab1.Size = new System.Drawing.Size(456, 271);
            this.fileListViewTab1.TabIndex = 0;
            this.fileListViewTab1.UseCompatibleStateImageBehavior = false;
            this.fileListViewTab1.View = System.Windows.Forms.View.Details;
            // 
            // authorHeader
            // 
            this.authorHeader.Text = "Author";
            this.authorHeader.Width = 150;
            // 
            // textTitleHeader
            // 
            this.textTitleHeader.Text = "Text Title";
            this.textTitleHeader.Width = 200;
            // 
            // includeQuotesHeader
            // 
            this.includeQuotesHeader.Text = "Include Quotes";
            this.includeQuotesHeader.Width = 100;
            // 
            // addGroupBox
            // 
            this.addGroupBox.Controls.Add(this.addLabel);
            this.addGroupBox.Controls.Add(this.selectFileButtonTab2);
            this.addGroupBox.Controls.Add(this.saveButtonTab2);
            this.addGroupBox.Controls.Add(this.fileLocationTextBox);
            this.addGroupBox.Controls.Add(this.newFileNameTextbox);
            this.addGroupBox.Controls.Add(this.authorTextBox);
            this.addGroupBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.addGroupBox.Location = new System.Drawing.Point(3, 3);
            this.addGroupBox.Name = "addGroupBox";
            this.addGroupBox.Size = new System.Drawing.Size(525, 573);
            this.addGroupBox.TabIndex = 6;
            this.addGroupBox.TabStop = false;
            // 
            // addLabel
            // 
            this.addLabel.AutoSize = true;
            this.addLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addLabel.Location = new System.Drawing.Point(253, 30);
            this.addLabel.Name = "addLabel";
            this.addLabel.Size = new System.Drawing.Size(38, 20);
            this.addLabel.TabIndex = 0;
            this.addLabel.Text = "Add";
            // 
            // selectFileButtonTab2
            // 
            this.selectFileButtonTab2.Location = new System.Drawing.Point(233, 76);
            this.selectFileButtonTab2.Name = "selectFileButtonTab2";
            this.selectFileButtonTab2.Size = new System.Drawing.Size(75, 23);
            this.selectFileButtonTab2.TabIndex = 4;
            this.selectFileButtonTab2.Text = "Select File";
            this.selectFileButtonTab2.UseVisualStyleBackColor = true;
            this.selectFileButtonTab2.Click += new System.EventHandler(this.selectFileButton_Click);
            // 
            // saveButtonTab2
            // 
            this.saveButtonTab2.Location = new System.Drawing.Point(124, 196);
            this.saveButtonTab2.Name = "saveButtonTab2";
            this.saveButtonTab2.Size = new System.Drawing.Size(75, 23);
            this.saveButtonTab2.TabIndex = 5;
            this.saveButtonTab2.Text = "Save";
            this.saveButtonTab2.UseVisualStyleBackColor = true;
            this.saveButtonTab2.Click += new System.EventHandler(this.saveButtonTab2_Click);
            // 
            // fileLocationTextBox
            // 
            this.fileLocationTextBox.Location = new System.Drawing.Point(6, 79);
            this.fileLocationTextBox.Name = "fileLocationTextBox";
            this.fileLocationTextBox.Size = new System.Drawing.Size(193, 20);
            this.fileLocationTextBox.TabIndex = 1;
            this.fileLocationTextBox.Text = "File location";
            // 
            // newFileNameTextbox
            // 
            this.newFileNameTextbox.Location = new System.Drawing.Point(6, 114);
            this.newFileNameTextbox.Name = "newFileNameTextbox";
            this.newFileNameTextbox.Size = new System.Drawing.Size(193, 20);
            this.newFileNameTextbox.TabIndex = 2;
            this.newFileNameTextbox.Text = "New File Name";
            // 
            // authorTextBox
            // 
            this.authorTextBox.Location = new System.Drawing.Point(6, 152);
            this.authorTextBox.Name = "authorTextBox";
            this.authorTextBox.Size = new System.Drawing.Size(193, 20);
            this.authorTextBox.TabIndex = 3;
            this.authorTextBox.Text = "Name of Author";
            // 
            // groupsTab
            // 
            this.groupsTab.Controls.Add(this.groupsGroupBox);
            this.groupsTab.Location = new System.Drawing.Point(4, 22);
            this.groupsTab.Name = "groupsTab";
            this.groupsTab.Padding = new System.Windows.Forms.Padding(3);
            this.groupsTab.Size = new System.Drawing.Size(1149, 579);
            this.groupsTab.TabIndex = 2;
            this.groupsTab.Text = "Groups";
            this.groupsTab.UseVisualStyleBackColor = true;
            // 
            // groupsGroupBox
            // 
            this.groupsGroupBox.Controls.Add(this.newGroupButton);
            this.groupsGroupBox.Controls.Add(this.groupComboBox);
            this.groupsGroupBox.Controls.Add(this.groupListView);
            this.groupsGroupBox.Controls.Add(this.deleteButtonTab3);
            this.groupsGroupBox.Controls.Add(this.removeButtonTab2);
            this.groupsGroupBox.Controls.Add(this.addButtonTab2);
            this.groupsGroupBox.Controls.Add(this.radiouttonPanel);
            this.groupsGroupBox.Controls.Add(this.fileGroupListViewTab3);
            this.groupsGroupBox.Controls.Add(this.groupTabLabel);
            this.groupsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupsGroupBox.Location = new System.Drawing.Point(3, 3);
            this.groupsGroupBox.Name = "groupsGroupBox";
            this.groupsGroupBox.Size = new System.Drawing.Size(1143, 573);
            this.groupsGroupBox.TabIndex = 0;
            this.groupsGroupBox.TabStop = false;
            // 
            // newGroupButton
            // 
            this.newGroupButton.Location = new System.Drawing.Point(728, 63);
            this.newGroupButton.Name = "newGroupButton";
            this.newGroupButton.Size = new System.Drawing.Size(75, 23);
            this.newGroupButton.TabIndex = 8;
            this.newGroupButton.Text = "New Group";
            this.newGroupButton.UseVisualStyleBackColor = true;
            // 
            // groupComboBox
            // 
            this.groupComboBox.FormattingEnabled = true;
            this.groupComboBox.Location = new System.Drawing.Point(583, 63);
            this.groupComboBox.Name = "groupComboBox";
            this.groupComboBox.Size = new System.Drawing.Size(121, 21);
            this.groupComboBox.TabIndex = 7;
            // 
            // groupListView
            // 
            this.groupListView.Location = new System.Drawing.Point(506, 90);
            this.groupListView.Name = "groupListView";
            this.groupListView.Size = new System.Drawing.Size(401, 325);
            this.groupListView.TabIndex = 6;
            this.groupListView.UseCompatibleStateImageBehavior = false;
            // 
            // deleteButtonTab3
            // 
            this.deleteButtonTab3.Location = new System.Drawing.Point(64, 422);
            this.deleteButtonTab3.Name = "deleteButtonTab3";
            this.deleteButtonTab3.Size = new System.Drawing.Size(75, 23);
            this.deleteButtonTab3.TabIndex = 5;
            this.deleteButtonTab3.Text = "Delete";
            this.deleteButtonTab3.UseVisualStyleBackColor = true;
            // 
            // removeButtonTab2
            // 
            this.removeButtonTab2.Location = new System.Drawing.Point(425, 317);
            this.removeButtonTab2.Name = "removeButtonTab2";
            this.removeButtonTab2.Size = new System.Drawing.Size(75, 23);
            this.removeButtonTab2.TabIndex = 4;
            this.removeButtonTab2.Text = "<- Remove";
            this.removeButtonTab2.UseVisualStyleBackColor = true;
            // 
            // addButtonTab2
            // 
            this.addButtonTab2.Location = new System.Drawing.Point(425, 211);
            this.addButtonTab2.Name = "addButtonTab2";
            this.addButtonTab2.Size = new System.Drawing.Size(75, 23);
            this.addButtonTab2.TabIndex = 3;
            this.addButtonTab2.Text = "Add ->";
            this.addButtonTab2.UseVisualStyleBackColor = true;
            // 
            // radiouttonPanel
            // 
            this.radiouttonPanel.Controls.Add(this.groupsRadioButton);
            this.radiouttonPanel.Controls.Add(this.filesRadioButton);
            this.radiouttonPanel.Location = new System.Drawing.Point(114, 42);
            this.radiouttonPanel.Name = "radiouttonPanel";
            this.radiouttonPanel.Size = new System.Drawing.Size(200, 42);
            this.radiouttonPanel.TabIndex = 2;
            // 
            // groupsRadioButton
            // 
            this.groupsRadioButton.AutoSize = true;
            this.groupsRadioButton.Location = new System.Drawing.Point(106, 21);
            this.groupsRadioButton.Name = "groupsRadioButton";
            this.groupsRadioButton.Size = new System.Drawing.Size(59, 17);
            this.groupsRadioButton.TabIndex = 1;
            this.groupsRadioButton.TabStop = true;
            this.groupsRadioButton.Text = "Groups";
            this.groupsRadioButton.UseVisualStyleBackColor = true;
            // 
            // filesRadioButton
            // 
            this.filesRadioButton.AutoSize = true;
            this.filesRadioButton.Location = new System.Drawing.Point(4, 21);
            this.filesRadioButton.Name = "filesRadioButton";
            this.filesRadioButton.Size = new System.Drawing.Size(46, 17);
            this.filesRadioButton.TabIndex = 0;
            this.filesRadioButton.TabStop = true;
            this.filesRadioButton.Text = "Files";
            this.filesRadioButton.UseVisualStyleBackColor = true;
            // 
            // fileGroupListViewTab3
            // 
            this.fileGroupListViewTab3.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.authorHeaderTab2,
            this.titleHeaderTab2,
            this.includeQuotesHeaderTab2});
            this.fileGroupListViewTab3.Location = new System.Drawing.Point(23, 90);
            this.fileGroupListViewTab3.Name = "fileGroupListViewTab3";
            this.fileGroupListViewTab3.Size = new System.Drawing.Size(396, 325);
            this.fileGroupListViewTab3.TabIndex = 1;
            this.fileGroupListViewTab3.UseCompatibleStateImageBehavior = false;
            this.fileGroupListViewTab3.View = System.Windows.Forms.View.Details;
            // 
            // groupTabLabel
            // 
            this.groupTabLabel.AutoSize = true;
            this.groupTabLabel.Location = new System.Drawing.Point(413, 16);
            this.groupTabLabel.Name = "groupTabLabel";
            this.groupTabLabel.Size = new System.Drawing.Size(98, 13);
            this.groupTabLabel.TabIndex = 0;
            this.groupTabLabel.Text = "Text in Groups Tab";
            // 
            // fingerPrintTab
            // 
            this.fingerPrintTab.Controls.Add(this.fingerprintAnalysisGroupBox);
            this.fingerPrintTab.Location = new System.Drawing.Point(4, 22);
            this.fingerPrintTab.Name = "fingerPrintTab";
            this.fingerPrintTab.Padding = new System.Windows.Forms.Padding(3);
            this.fingerPrintTab.Size = new System.Drawing.Size(1149, 579);
            this.fingerPrintTab.TabIndex = 0;
            this.fingerPrintTab.Text = "Fingerprint Analysis";
            this.fingerPrintTab.UseVisualStyleBackColor = true;
            // 
            // fingerprintAnalysisGroupBox
            // 
            this.fingerprintAnalysisGroupBox.Controls.Add(this.radioButtonContainer);
            this.fingerprintAnalysisGroupBox.Controls.Add(this.analysisListView);
            this.fingerprintAnalysisGroupBox.Controls.Add(this.filesAndGroupsListviewTab3);
            this.fingerprintAnalysisGroupBox.Controls.Add(this.executeAnalysisButton);
            this.fingerprintAnalysisGroupBox.Controls.Add(this.removeButtonTab3);
            this.fingerprintAnalysisGroupBox.Controls.Add(this.addButtonTab3);
            this.fingerprintAnalysisGroupBox.Controls.Add(this.fALabel);
            this.fingerprintAnalysisGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fingerprintAnalysisGroupBox.Location = new System.Drawing.Point(3, 3);
            this.fingerprintAnalysisGroupBox.Name = "fingerprintAnalysisGroupBox";
            this.fingerprintAnalysisGroupBox.Size = new System.Drawing.Size(1143, 573);
            this.fingerprintAnalysisGroupBox.TabIndex = 0;
            this.fingerprintAnalysisGroupBox.TabStop = false;
            // 
            // radioButtonContainer
            // 
            this.radioButtonContainer.Controls.Add(this.groupsRadioButtonTab3);
            this.radioButtonContainer.Controls.Add(this.filesRadioButtonTab3);
            this.radioButtonContainer.Location = new System.Drawing.Point(137, 42);
            this.radioButtonContainer.Name = "radioButtonContainer";
            this.radioButtonContainer.Size = new System.Drawing.Size(200, 42);
            this.radioButtonContainer.TabIndex = 9;
            // 
            // groupsRadioButtonTab3
            // 
            this.groupsRadioButtonTab3.AutoSize = true;
            this.groupsRadioButtonTab3.Location = new System.Drawing.Point(106, 21);
            this.groupsRadioButtonTab3.Name = "groupsRadioButtonTab3";
            this.groupsRadioButtonTab3.Size = new System.Drawing.Size(59, 17);
            this.groupsRadioButtonTab3.TabIndex = 1;
            this.groupsRadioButtonTab3.TabStop = true;
            this.groupsRadioButtonTab3.Text = "Groups";
            this.groupsRadioButtonTab3.UseVisualStyleBackColor = true;
            // 
            // filesRadioButtonTab3
            // 
            this.filesRadioButtonTab3.AutoSize = true;
            this.filesRadioButtonTab3.Location = new System.Drawing.Point(4, 21);
            this.filesRadioButtonTab3.Name = "filesRadioButtonTab3";
            this.filesRadioButtonTab3.Size = new System.Drawing.Size(46, 17);
            this.filesRadioButtonTab3.TabIndex = 0;
            this.filesRadioButtonTab3.TabStop = true;
            this.filesRadioButtonTab3.Text = "Files";
            this.filesRadioButtonTab3.UseVisualStyleBackColor = true;
            // 
            // analysisListView
            // 
            this.analysisListView.Location = new System.Drawing.Point(585, 90);
            this.analysisListView.Name = "analysisListView";
            this.analysisListView.Size = new System.Drawing.Size(399, 325);
            this.analysisListView.TabIndex = 8;
            this.analysisListView.UseCompatibleStateImageBehavior = false;
            // 
            // filesAndGroupsListviewTab3
            // 
            this.filesAndGroupsListviewTab3.Location = new System.Drawing.Point(44, 90);
            this.filesAndGroupsListviewTab3.Name = "filesAndGroupsListviewTab3";
            this.filesAndGroupsListviewTab3.Size = new System.Drawing.Size(390, 325);
            this.filesAndGroupsListviewTab3.TabIndex = 7;
            this.filesAndGroupsListviewTab3.UseCompatibleStateImageBehavior = false;
            // 
            // executeAnalysisButton
            // 
            this.executeAnalysisButton.Location = new System.Drawing.Point(736, 421);
            this.executeAnalysisButton.Name = "executeAnalysisButton";
            this.executeAnalysisButton.Size = new System.Drawing.Size(96, 23);
            this.executeAnalysisButton.TabIndex = 6;
            this.executeAnalysisButton.Text = "Execute Analysis";
            this.executeAnalysisButton.UseVisualStyleBackColor = true;
            this.executeAnalysisButton.Click += new System.EventHandler(this.executeAnalysisButton_Click);
            // 
            // removeButtonTab3
            // 
            this.removeButtonTab3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.removeButtonTab3.Location = new System.Drawing.Point(464, 318);
            this.removeButtonTab3.Name = "removeButtonTab3";
            this.removeButtonTab3.Size = new System.Drawing.Size(75, 23);
            this.removeButtonTab3.TabIndex = 3;
            this.removeButtonTab3.Text = "<- Remove";
            this.removeButtonTab3.UseVisualStyleBackColor = true;
            // 
            // addButtonTab3
            // 
            this.addButtonTab3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.addButtonTab3.Location = new System.Drawing.Point(464, 211);
            this.addButtonTab3.Name = "addButtonTab3";
            this.addButtonTab3.Size = new System.Drawing.Size(75, 23);
            this.addButtonTab3.TabIndex = 2;
            this.addButtonTab3.Text = "Add ->";
            this.addButtonTab3.UseVisualStyleBackColor = true;
            this.addButtonTab3.Click += new System.EventHandler(this.addButtonTab3_Click);
            // 
            // fALabel
            // 
            this.fALabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.fALabel.AutoSize = true;
            this.fALabel.Location = new System.Drawing.Point(440, 42);
            this.fALabel.Name = "fALabel";
            this.fALabel.Size = new System.Drawing.Size(112, 13);
            this.fALabel.TabIndex = 0;
            this.fALabel.Text = "Text on Analysis Page";
            this.fALabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // analysisTab
            // 
            this.analysisTab.Controls.Add(this.chartGroupBox);
            this.analysisTab.Location = new System.Drawing.Point(4, 22);
            this.analysisTab.Name = "analysisTab";
            this.analysisTab.Padding = new System.Windows.Forms.Padding(3);
            this.analysisTab.Size = new System.Drawing.Size(1149, 579);
            this.analysisTab.TabIndex = 3;
            this.analysisTab.Text = "Analysis";
            this.analysisTab.UseVisualStyleBackColor = true;
            // 
            // chartGroupBox
            // 
            this.chartGroupBox.Controls.Add(this.tableLayoutPanel1);
            this.chartGroupBox.Controls.Add(this.analysisLineChart);
            this.chartGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartGroupBox.Location = new System.Drawing.Point(3, 3);
            this.chartGroupBox.Name = "chartGroupBox";
            this.chartGroupBox.Size = new System.Drawing.Size(1143, 573);
            this.chartGroupBox.TabIndex = 0;
            this.chartGroupBox.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 10;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(744, 57);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(364, 386);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // analysisLineChart
            // 
            chartArea2.Name = "ChartArea1";
            this.analysisLineChart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.analysisLineChart.Legends.Add(legend2);
            this.analysisLineChart.Location = new System.Drawing.Point(6, 19);
            this.analysisLineChart.Name = "analysisLineChart";
            this.analysisLineChart.Size = new System.Drawing.Size(715, 551);
            this.analysisLineChart.TabIndex = 0;
            title2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title2.Name = "authorAnalysis";
            title2.Text = "Author Analysis";
            this.analysisLineChart.Titles.Add(title2);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Text Files (.txt)|*.txt";
            // 
            // authorHeaderTab2
            // 
            this.authorHeaderTab2.Text = "Author";
            this.authorHeaderTab2.Width = 150;
            // 
            // titleHeaderTab2
            // 
            this.titleHeaderTab2.Text = "Text Title";
            this.titleHeaderTab2.Width = 200;
            // 
            // includeQuotesHeaderTab2
            // 
            this.includeQuotesHeaderTab2.Text = "Include Quotes";
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1157, 605);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Fingerprint Analysis";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.filesTab.ResumeLayout(false);
            this.editModifyGroupBox.ResumeLayout(false);
            this.editModifyGroupBox.PerformLayout();
            this.addGroupBox.ResumeLayout(false);
            this.addGroupBox.PerformLayout();
            this.groupsTab.ResumeLayout(false);
            this.groupsGroupBox.ResumeLayout(false);
            this.groupsGroupBox.PerformLayout();
            this.radiouttonPanel.ResumeLayout(false);
            this.radiouttonPanel.PerformLayout();
            this.fingerPrintTab.ResumeLayout(false);
            this.fingerprintAnalysisGroupBox.ResumeLayout(false);
            this.fingerprintAnalysisGroupBox.PerformLayout();
            this.radioButtonContainer.ResumeLayout(false);
            this.radioButtonContainer.PerformLayout();
            this.analysisTab.ResumeLayout(false);
            this.chartGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.analysisLineChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage fingerPrintTab;
        private System.Windows.Forms.TabPage filesTab;
        private System.Windows.Forms.TabPage groupsTab;
        private System.Windows.Forms.GroupBox fingerprintAnalysisGroupBox;
        private System.Windows.Forms.Button executeAnalysisButton;
        private System.Windows.Forms.Button removeButtonTab3;
        private System.Windows.Forms.Button addButtonTab3;
        private System.Windows.Forms.Label fALabel;
        private System.Windows.Forms.Button saveButtonTab2;
        private System.Windows.Forms.Button selectFileButtonTab2;
        private System.Windows.Forms.TextBox authorTextBox;
        private System.Windows.Forms.TextBox newFileNameTextbox;
        private System.Windows.Forms.TextBox fileLocationTextBox;
        private System.Windows.Forms.Label addLabel;
        private System.Windows.Forms.GroupBox editModifyGroupBox;
        private System.Windows.Forms.GroupBox addGroupBox;
        private System.Windows.Forms.Label editModifyLabel;
        private System.Windows.Forms.ListView fileListViewTab1;
        private System.Windows.Forms.Button deleteButtonTab2;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.GroupBox groupsGroupBox;
        private System.Windows.Forms.ListView groupListView;
        private System.Windows.Forms.Button deleteButtonTab3;
        private System.Windows.Forms.Button removeButtonTab2;
        private System.Windows.Forms.Button addButtonTab2;
        private System.Windows.Forms.Panel radiouttonPanel;
        private System.Windows.Forms.RadioButton groupsRadioButton;
        private System.Windows.Forms.RadioButton filesRadioButton;
        private System.Windows.Forms.ListView fileGroupListViewTab3;
        private System.Windows.Forms.Label groupTabLabel;
        private System.Windows.Forms.ComboBox groupComboBox;
        private System.Windows.Forms.TabPage analysisTab;
        private System.Windows.Forms.GroupBox chartGroupBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart analysisLineChart;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel radioButtonContainer;
        private System.Windows.Forms.RadioButton groupsRadioButtonTab3;
        private System.Windows.Forms.RadioButton filesRadioButtonTab3;
        private System.Windows.Forms.ListView analysisListView;
        private System.Windows.Forms.ListView filesAndGroupsListviewTab3;
        private System.Windows.Forms.Button newGroupButton;
        private System.Windows.Forms.ColumnHeader authorHeader;
        private System.Windows.Forms.ColumnHeader textTitleHeader;
        private System.Windows.Forms.ColumnHeader includeQuotesHeader;
        private System.Windows.Forms.ColumnHeader authorHeaderTab2;
        private System.Windows.Forms.ColumnHeader titleHeaderTab2;
        private System.Windows.Forms.ColumnHeader includeQuotesHeaderTab2;
    }
}

