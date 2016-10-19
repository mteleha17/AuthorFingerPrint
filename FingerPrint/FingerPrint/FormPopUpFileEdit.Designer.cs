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
            this.authorTextBox = new System.Windows.Forms.TextBox();
            this.quotesCheckbox = new System.Windows.Forms.CheckBox();
            this.saveChangesButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // newFileNameTextbox
            // 
            this.newFileNameTextbox.Location = new System.Drawing.Point(23, 43);
            this.newFileNameTextbox.Name = "newFileNameTextbox";
            this.newFileNameTextbox.Size = new System.Drawing.Size(238, 20);
            this.newFileNameTextbox.TabIndex = 3;
            this.newFileNameTextbox.Text = "New File Name";
            // 
            // authorTextBox
            // 
            this.authorTextBox.Location = new System.Drawing.Point(23, 80);
            this.authorTextBox.Name = "authorTextBox";
            this.authorTextBox.Size = new System.Drawing.Size(238, 20);
            this.authorTextBox.TabIndex = 4;
            this.authorTextBox.Text = "Name of Author";
            // 
            // quotesCheckbox
            // 
            this.quotesCheckbox.AutoSize = true;
            this.quotesCheckbox.Location = new System.Drawing.Point(23, 121);
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
            // 
            // FormPopUpFileEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 222);
            this.Controls.Add(this.saveChangesButton);
            this.Controls.Add(this.quotesCheckbox);
            this.Controls.Add(this.authorTextBox);
            this.Controls.Add(this.newFileNameTextbox);
            this.Name = "FormPopUpFileEdit";
            this.Text = "Edit Text Details";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox newFileNameTextbox;
        private System.Windows.Forms.TextBox authorTextBox;
        private System.Windows.Forms.CheckBox quotesCheckbox;
        private System.Windows.Forms.Button saveChangesButton;
    }
}