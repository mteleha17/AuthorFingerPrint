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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.fingerPrintTab = new System.Windows.Forms.TabPage();
            this.fingerprintAnalysisGroupBox = new System.Windows.Forms.GroupBox();
            this.executeAnalysisButton = new System.Windows.Forms.Button();
            this.uploadFileButton = new System.Windows.Forms.Button();
            this.analysisListViewTab1 = new System.Windows.Forms.ListView();
            this.removeButtonTab2 = new System.Windows.Forms.Button();
            this.addButtonTab1 = new System.Windows.Forms.Button();
            this.fileListViewTab1 = new System.Windows.Forms.ListView();
            this.fALabel = new System.Windows.Forms.Label();
            this.filesTab = new System.Windows.Forms.TabPage();
            this.editModifyGroupBox = new System.Windows.Forms.GroupBox();
            this.deleterButtonTab2 = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.editModifyLabel = new System.Windows.Forms.Label();
            this.fileListViewTab2 = new System.Windows.Forms.ListView();
            this.addGroupBox = new System.Windows.Forms.GroupBox();
            this.addLabel = new System.Windows.Forms.Label();
            this.selectFileButtonTab2 = new System.Windows.Forms.Button();
            this.saveButtonTab2 = new System.Windows.Forms.Button();
            this.fileLocationTextBox = new System.Windows.Forms.TextBox();
            this.newFileNameTextbox = new System.Windows.Forms.TextBox();
            this.authorTextBox = new System.Windows.Forms.TextBox();
            this.groupsTab = new System.Windows.Forms.TabPage();
            this.groupsGroupBox = new System.Windows.Forms.GroupBox();
            this.groupComboBox = new System.Windows.Forms.ComboBox();
            this.groupListView = new System.Windows.Forms.ListView();
            this.deleteButtonTab3 = new System.Windows.Forms.Button();
            this.removeButtonTab3 = new System.Windows.Forms.Button();
            this.addButtonTab3 = new System.Windows.Forms.Button();
            this.radiouttonPanel = new System.Windows.Forms.Panel();
            this.groupsRadioButton = new System.Windows.Forms.RadioButton();
            this.filesRadioButton = new System.Windows.Forms.RadioButton();
            this.fileGroupListViewTab3 = new System.Windows.Forms.ListView();
            this.groupTabLabel = new System.Windows.Forms.Label();
            this.analysisTab = new System.Windows.Forms.TabPage();
            this.chartGroupBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.AnalysisLineChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1.SuspendLayout();
            this.fingerPrintTab.SuspendLayout();
            this.fingerprintAnalysisGroupBox.SuspendLayout();
            this.filesTab.SuspendLayout();
            this.editModifyGroupBox.SuspendLayout();
            this.addGroupBox.SuspendLayout();
            this.groupsTab.SuspendLayout();
            this.groupsGroupBox.SuspendLayout();
            this.radiouttonPanel.SuspendLayout();
            this.analysisTab.SuspendLayout();
            this.chartGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AnalysisLineChart)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.fingerPrintTab);
            this.tabControl1.Controls.Add(this.filesTab);
            this.tabControl1.Controls.Add(this.groupsTab);
            this.tabControl1.Controls.Add(this.analysisTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1355, 605);
            this.tabControl1.TabIndex = 0;
            // 
            // fingerPrintTab
            // 
            this.fingerPrintTab.Controls.Add(this.fingerprintAnalysisGroupBox);
            this.fingerPrintTab.Location = new System.Drawing.Point(4, 22);
            this.fingerPrintTab.Name = "fingerPrintTab";
            this.fingerPrintTab.Padding = new System.Windows.Forms.Padding(3);
            this.fingerPrintTab.Size = new System.Drawing.Size(1347, 579);
            this.fingerPrintTab.TabIndex = 0;
            this.fingerPrintTab.Text = "Fingerprint Analysis";
            this.fingerPrintTab.UseVisualStyleBackColor = true;
            // 
            // fingerprintAnalysisGroupBox
            // 
            this.fingerprintAnalysisGroupBox.Controls.Add(this.executeAnalysisButton);
            this.fingerprintAnalysisGroupBox.Controls.Add(this.uploadFileButton);
            this.fingerprintAnalysisGroupBox.Controls.Add(this.analysisListViewTab1);
            this.fingerprintAnalysisGroupBox.Controls.Add(this.removeButtonTab2);
            this.fingerprintAnalysisGroupBox.Controls.Add(this.addButtonTab1);
            this.fingerprintAnalysisGroupBox.Controls.Add(this.fileListViewTab1);
            this.fingerprintAnalysisGroupBox.Controls.Add(this.fALabel);
            this.fingerprintAnalysisGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fingerprintAnalysisGroupBox.Location = new System.Drawing.Point(3, 3);
            this.fingerprintAnalysisGroupBox.Name = "fingerprintAnalysisGroupBox";
            this.fingerprintAnalysisGroupBox.Size = new System.Drawing.Size(1341, 573);
            this.fingerprintAnalysisGroupBox.TabIndex = 0;
            this.fingerprintAnalysisGroupBox.TabStop = false;
            // 
            // executeAnalysisButton
            // 
            this.executeAnalysisButton.Location = new System.Drawing.Point(1049, 407);
            this.executeAnalysisButton.Name = "executeAnalysisButton";
            this.executeAnalysisButton.Size = new System.Drawing.Size(96, 23);
            this.executeAnalysisButton.TabIndex = 6;
            this.executeAnalysisButton.Text = "Execute Analysis";
            this.executeAnalysisButton.UseVisualStyleBackColor = true;
            this.executeAnalysisButton.Click += new System.EventHandler(this.executeAnalysisButton_Click);
            // 
            // uploadFileButton
            // 
            this.uploadFileButton.Location = new System.Drawing.Point(166, 407);
            this.uploadFileButton.Name = "uploadFileButton";
            this.uploadFileButton.Size = new System.Drawing.Size(75, 23);
            this.uploadFileButton.TabIndex = 5;
            this.uploadFileButton.Text = "Upload File";
            this.uploadFileButton.UseVisualStyleBackColor = true;
            this.uploadFileButton.Click += new System.EventHandler(this.uploadFileButton_Click);
            // 
            // analysisListViewTab1
            // 
            this.analysisListViewTab1.Location = new System.Drawing.Point(937, 78);
            this.analysisListViewTab1.Name = "analysisListViewTab1";
            this.analysisListViewTab1.Size = new System.Drawing.Size(301, 323);
            this.analysisListViewTab1.TabIndex = 4;
            this.analysisListViewTab1.UseCompatibleStateImageBehavior = false;
            this.analysisListViewTab1.View = System.Windows.Forms.View.List;
            // 
            // removeButtonTab2
            // 
            this.removeButtonTab2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.removeButtonTab2.Location = new System.Drawing.Point(623, 220);
            this.removeButtonTab2.Name = "removeButtonTab2";
            this.removeButtonTab2.Size = new System.Drawing.Size(75, 23);
            this.removeButtonTab2.TabIndex = 3;
            this.removeButtonTab2.Text = "<- Remove";
            this.removeButtonTab2.UseVisualStyleBackColor = true;
            // 
            // addButtonTab1
            // 
            this.addButtonTab1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.addButtonTab1.Location = new System.Drawing.Point(623, 131);
            this.addButtonTab1.Name = "addButtonTab1";
            this.addButtonTab1.Size = new System.Drawing.Size(75, 23);
            this.addButtonTab1.TabIndex = 2;
            this.addButtonTab1.Text = "Add ->";
            this.addButtonTab1.UseVisualStyleBackColor = true;
            this.addButtonTab1.Click += new System.EventHandler(this.addButtonTab1_Click);
            // 
            // fileListViewTab1
            // 
            this.fileListViewTab1.Location = new System.Drawing.Point(57, 78);
            this.fileListViewTab1.Name = "fileListViewTab1";
            this.fileListViewTab1.Size = new System.Drawing.Size(301, 323);
            this.fileListViewTab1.TabIndex = 1;
            this.fileListViewTab1.UseCompatibleStateImageBehavior = false;
            this.fileListViewTab1.View = System.Windows.Forms.View.List;
            // 
            // fALabel
            // 
            this.fALabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.fALabel.AutoSize = true;
            this.fALabel.Location = new System.Drawing.Point(602, 34);
            this.fALabel.Name = "fALabel";
            this.fALabel.Size = new System.Drawing.Size(112, 13);
            this.fALabel.TabIndex = 0;
            this.fALabel.Text = "Text on Analysis Page";
            this.fALabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // filesTab
            // 
            this.filesTab.Controls.Add(this.editModifyGroupBox);
            this.filesTab.Controls.Add(this.addGroupBox);
            this.filesTab.Location = new System.Drawing.Point(4, 22);
            this.filesTab.Name = "filesTab";
            this.filesTab.Padding = new System.Windows.Forms.Padding(3);
            this.filesTab.Size = new System.Drawing.Size(1347, 579);
            this.filesTab.TabIndex = 1;
            this.filesTab.Text = "Files";
            this.filesTab.UseVisualStyleBackColor = true;
            // 
            // editModifyGroupBox
            // 
            this.editModifyGroupBox.Controls.Add(this.deleterButtonTab2);
            this.editModifyGroupBox.Controls.Add(this.editButton);
            this.editModifyGroupBox.Controls.Add(this.editModifyLabel);
            this.editModifyGroupBox.Controls.Add(this.fileListViewTab2);
            this.editModifyGroupBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.editModifyGroupBox.Location = new System.Drawing.Point(688, 3);
            this.editModifyGroupBox.Name = "editModifyGroupBox";
            this.editModifyGroupBox.Size = new System.Drawing.Size(656, 573);
            this.editModifyGroupBox.TabIndex = 7;
            this.editModifyGroupBox.TabStop = false;
            // 
            // deleterButtonTab2
            // 
            this.deleterButtonTab2.Location = new System.Drawing.Point(361, 351);
            this.deleterButtonTab2.Name = "deleterButtonTab2";
            this.deleterButtonTab2.Size = new System.Drawing.Size(75, 23);
            this.deleterButtonTab2.TabIndex = 3;
            this.deleterButtonTab2.Text = "Delete";
            this.deleterButtonTab2.UseVisualStyleBackColor = true;
            // 
            // editButton
            // 
            this.editButton.Location = new System.Drawing.Point(280, 352);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(75, 23);
            this.editButton.TabIndex = 2;
            this.editButton.Text = "Edit";
            this.editButton.UseVisualStyleBackColor = true;
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
            // fileListViewTab2
            // 
            this.fileListViewTab2.Location = new System.Drawing.Point(52, 75);
            this.fileListViewTab2.Name = "fileListViewTab2";
            this.fileListViewTab2.Size = new System.Drawing.Size(385, 271);
            this.fileListViewTab2.TabIndex = 0;
            this.fileListViewTab2.UseCompatibleStateImageBehavior = false;
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
            this.addGroupBox.Size = new System.Drawing.Size(679, 573);
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
            this.groupsTab.Size = new System.Drawing.Size(1347, 579);
            this.groupsTab.TabIndex = 2;
            this.groupsTab.Text = "Groups";
            this.groupsTab.UseVisualStyleBackColor = true;
            // 
            // groupsGroupBox
            // 
            this.groupsGroupBox.Controls.Add(this.groupComboBox);
            this.groupsGroupBox.Controls.Add(this.groupListView);
            this.groupsGroupBox.Controls.Add(this.deleteButtonTab3);
            this.groupsGroupBox.Controls.Add(this.removeButtonTab3);
            this.groupsGroupBox.Controls.Add(this.addButtonTab3);
            this.groupsGroupBox.Controls.Add(this.radiouttonPanel);
            this.groupsGroupBox.Controls.Add(this.fileGroupListViewTab3);
            this.groupsGroupBox.Controls.Add(this.groupTabLabel);
            this.groupsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupsGroupBox.Location = new System.Drawing.Point(3, 3);
            this.groupsGroupBox.Name = "groupsGroupBox";
            this.groupsGroupBox.Size = new System.Drawing.Size(1341, 573);
            this.groupsGroupBox.TabIndex = 0;
            this.groupsGroupBox.TabStop = false;
            // 
            // groupComboBox
            // 
            this.groupComboBox.FormattingEnabled = true;
            this.groupComboBox.Location = new System.Drawing.Point(642, 63);
            this.groupComboBox.Name = "groupComboBox";
            this.groupComboBox.Size = new System.Drawing.Size(121, 21);
            this.groupComboBox.TabIndex = 7;
            // 
            // groupListView
            // 
            this.groupListView.Location = new System.Drawing.Point(559, 90);
            this.groupListView.Name = "groupListView";
            this.groupListView.Size = new System.Drawing.Size(296, 325);
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
            // removeButtonTab3
            // 
            this.removeButtonTab3.Location = new System.Drawing.Point(425, 317);
            this.removeButtonTab3.Name = "removeButtonTab3";
            this.removeButtonTab3.Size = new System.Drawing.Size(75, 23);
            this.removeButtonTab3.TabIndex = 4;
            this.removeButtonTab3.Text = "<- Remove";
            this.removeButtonTab3.UseVisualStyleBackColor = true;
            // 
            // addButtonTab3
            // 
            this.addButtonTab3.Location = new System.Drawing.Point(425, 211);
            this.addButtonTab3.Name = "addButtonTab3";
            this.addButtonTab3.Size = new System.Drawing.Size(75, 23);
            this.addButtonTab3.TabIndex = 3;
            this.addButtonTab3.Text = "Add ->";
            this.addButtonTab3.UseVisualStyleBackColor = true;
            this.addButtonTab3.Click += new System.EventHandler(this.addButtonTab3_Click);
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
            this.fileGroupListViewTab3.Location = new System.Drawing.Point(64, 90);
            this.fileGroupListViewTab3.Name = "fileGroupListViewTab3";
            this.fileGroupListViewTab3.Size = new System.Drawing.Size(299, 325);
            this.fileGroupListViewTab3.TabIndex = 1;
            this.fileGroupListViewTab3.UseCompatibleStateImageBehavior = false;
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
            // analysisTab
            // 
            this.analysisTab.Controls.Add(this.chartGroupBox);
            this.analysisTab.Location = new System.Drawing.Point(4, 22);
            this.analysisTab.Name = "analysisTab";
            this.analysisTab.Padding = new System.Windows.Forms.Padding(3);
            this.analysisTab.Size = new System.Drawing.Size(1347, 579);
            this.analysisTab.TabIndex = 3;
            this.analysisTab.Text = "Analysis";
            this.analysisTab.UseVisualStyleBackColor = true;
            // 
            // chartGroupBox
            // 
            this.chartGroupBox.Controls.Add(this.tableLayoutPanel1);
            this.chartGroupBox.Controls.Add(this.AnalysisLineChart);
            this.chartGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartGroupBox.Location = new System.Drawing.Point(3, 3);
            this.chartGroupBox.Name = "chartGroupBox";
            this.chartGroupBox.Size = new System.Drawing.Size(1341, 573);
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(744, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(592, 424);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // AnalysisLineChart
            // 
            chartArea1.Name = "ChartArea1";
            this.AnalysisLineChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.AnalysisLineChart.Legends.Add(legend1);
            this.AnalysisLineChart.Location = new System.Drawing.Point(6, 19);
            this.AnalysisLineChart.Name = "AnalysisLineChart";
            this.AnalysisLineChart.Size = new System.Drawing.Size(731, 551);
            this.AnalysisLineChart.TabIndex = 0;
            this.AnalysisLineChart.Text = "analysisChart";
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.Name = "Author Analysis";
            title1.Text = "Author Analysis";
            this.AnalysisLineChart.Titles.Add(title1);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Text Files (.txt)|*.txt";
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1355, 605);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Fingerprint Analysis";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.fingerPrintTab.ResumeLayout(false);
            this.fingerprintAnalysisGroupBox.ResumeLayout(false);
            this.fingerprintAnalysisGroupBox.PerformLayout();
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
            this.analysisTab.ResumeLayout(false);
            this.chartGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AnalysisLineChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage fingerPrintTab;
        private System.Windows.Forms.TabPage filesTab;
        private System.Windows.Forms.TabPage groupsTab;
        private System.Windows.Forms.GroupBox fingerprintAnalysisGroupBox;
        private System.Windows.Forms.Button executeAnalysisButton;
        private System.Windows.Forms.Button uploadFileButton;
        private System.Windows.Forms.ListView analysisListViewTab1;
        private System.Windows.Forms.Button removeButtonTab2;
        private System.Windows.Forms.Button addButtonTab1;
        private System.Windows.Forms.ListView fileListViewTab1;
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
        private System.Windows.Forms.ListView fileListViewTab2;
        private System.Windows.Forms.Button deleterButtonTab2;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.GroupBox groupsGroupBox;
        private System.Windows.Forms.ListView groupListView;
        private System.Windows.Forms.Button deleteButtonTab3;
        private System.Windows.Forms.Button removeButtonTab3;
        private System.Windows.Forms.Button addButtonTab3;
        private System.Windows.Forms.Panel radiouttonPanel;
        private System.Windows.Forms.RadioButton groupsRadioButton;
        private System.Windows.Forms.RadioButton filesRadioButton;
        private System.Windows.Forms.ListView fileGroupListViewTab3;
        private System.Windows.Forms.Label groupTabLabel;
        private System.Windows.Forms.ComboBox groupComboBox;
        private System.Windows.Forms.TabPage analysisTab;
        private System.Windows.Forms.GroupBox chartGroupBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart AnalysisLineChart;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}

