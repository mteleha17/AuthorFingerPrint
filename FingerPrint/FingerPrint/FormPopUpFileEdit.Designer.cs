namespace FingerPrint
{
    partial class FormPopUpFileEdit
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
            this.newFileNameTextbox = new System.Windows.Forms.TextBox();
            this.newAuthorTextBox = new System.Windows.Forms.TextBox();
            this.quotesCheckbox = new System.Windows.Forms.CheckBox();
            this.saveChangesButton = new System.Windows.Forms.Button();
            this.nameOfTextLabel = new System.Windows.Forms.Label();
            this.nameOfAuthorLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // newFileNameTextbox
            // 
            this.newFileNameTextbox.Location = new System.Drawing.Point(125, 32);
            this.newFileNameTextbox.Name = "newFileNameTextbox";
            this.newFileNameTextbox.Size = new System.Drawing.Size(238, 20);
            this.newFileNameTextbox.TabIndex = 3;
            // 
            // newAuthorTextBox
            // 
            this.newAuthorTextBox.Location = new System.Drawing.Point(125, 73);
            this.newAuthorTextBox.Name = "newAuthorTextBox";
            this.newAuthorTextBox.Size = new System.Drawing.Size(238, 20);
            this.newAuthorTextBox.TabIndex = 4;
            // 
            // quotesCheckbox
            // 
            this.quotesCheckbox.AutoSize = true;
            this.quotesCheckbox.Location = new System.Drawing.Point(125, 114);
            this.quotesCheckbox.Name = "quotesCheckbox";
            this.quotesCheckbox.Size = new System.Drawing.Size(115, 17);
            this.quotesCheckbox.TabIndex = 5;
            this.quotesCheckbox.Text = "Include Quotations";
            this.quotesCheckbox.UseVisualStyleBackColor = true;
            // 
            // saveChangesButton
            // 
            this.saveChangesButton.Location = new System.Drawing.Point(197, 151);
            this.saveChangesButton.Name = "saveChangesButton";
            this.saveChangesButton.Size = new System.Drawing.Size(103, 23);
            this.saveChangesButton.TabIndex = 6;
            this.saveChangesButton.Text = "Save Changes";
            this.saveChangesButton.UseVisualStyleBackColor = true;
            this.saveChangesButton.Click += new System.EventHandler(this.saveChangesButton_Click);
            // 
            // nameOfTextLabel
            // 
            this.nameOfTextLabel.AutoSize = true;
            this.nameOfTextLabel.Location = new System.Drawing.Point(45, 35);
            this.nameOfTextLabel.Name = "nameOfTextLabel";
            this.nameOfTextLabel.Size = new System.Drawing.Size(74, 13);
            this.nameOfTextLabel.TabIndex = 7;
            this.nameOfTextLabel.Text = "Name of Text:";
            // 
            // nameOfAuthorLabel
            // 
            this.nameOfAuthorLabel.AutoSize = true;
            this.nameOfAuthorLabel.Location = new System.Drawing.Point(35, 76);
            this.nameOfAuthorLabel.Name = "nameOfAuthorLabel";
            this.nameOfAuthorLabel.Size = new System.Drawing.Size(84, 13);
            this.nameOfAuthorLabel.TabIndex = 8;
            this.nameOfAuthorLabel.Text = "Name of Author:";
            // 
            // FormPopUpFileEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 222);
            this.Controls.Add(this.nameOfAuthorLabel);
            this.Controls.Add(this.nameOfTextLabel);
            this.Controls.Add(this.saveChangesButton);
            this.Controls.Add(this.quotesCheckbox);
            this.Controls.Add(this.newAuthorTextBox);
            this.Controls.Add(this.newFileNameTextbox);
            this.Name = "FormPopUpFileEdit";
            this.Text = "Edit Text Details";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox newFileNameTextbox;
        private System.Windows.Forms.TextBox newAuthorTextBox;
        private System.Windows.Forms.CheckBox quotesCheckbox;
        private System.Windows.Forms.Button saveChangesButton;
        private System.Windows.Forms.Label nameOfTextLabel;
        private System.Windows.Forms.Label nameOfAuthorLabel;
    }
}