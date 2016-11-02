namespace FingerPrint
{
    partial class NewGroupPopUp
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
            this.addNewGroupLabel = new System.Windows.Forms.Label();
            this.groupNameTextBox = new System.Windows.Forms.TextBox();
            this.saveNewGroupButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // addNewGroupLabel
            // 
            this.addNewGroupLabel.Location = new System.Drawing.Point(12, 28);
            this.addNewGroupLabel.Name = "addNewGroupLabel";
            this.addNewGroupLabel.Size = new System.Drawing.Size(344, 16);
            this.addNewGroupLabel.TabIndex = 0;
            this.addNewGroupLabel.Text = "Please enter a name for your new group";
            this.addNewGroupLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupNameTextBox
            // 
            this.groupNameTextBox.Location = new System.Drawing.Point(86, 60);
            this.groupNameTextBox.Name = "groupNameTextBox";
            this.groupNameTextBox.Size = new System.Drawing.Size(190, 20);
            this.groupNameTextBox.TabIndex = 1;
            // 
            // saveNewGroupButton
            // 
            this.saveNewGroupButton.Location = new System.Drawing.Point(146, 97);
            this.saveNewGroupButton.Name = "saveNewGroupButton";
            this.saveNewGroupButton.Size = new System.Drawing.Size(75, 23);
            this.saveNewGroupButton.TabIndex = 2;
            this.saveNewGroupButton.Text = "Add Group";
            this.saveNewGroupButton.UseVisualStyleBackColor = true;
            this.saveNewGroupButton.Click += new System.EventHandler(this.saveNewGroupButton_Click);
            // 
            // NewGroupPopUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 149);
            this.Controls.Add(this.saveNewGroupButton);
            this.Controls.Add(this.groupNameTextBox);
            this.Controls.Add(this.addNewGroupLabel);
            this.Name = "NewGroupPopUp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Group";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label addNewGroupLabel;
        private System.Windows.Forms.TextBox groupNameTextBox;
        private System.Windows.Forms.Button saveNewGroupButton;
    }
}