using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FingerPrint
{
    public partial class FormPopUpFileEdit : Form
    {
        Form1 form1;
        public FormPopUpFileEdit(string authorName, string textName, string includeQuotes, Form1 _form1)
        {
            form1 = _form1;
            InitializeComponent();
            newAuthorTextBox.Text = authorName;
            newFileNameTextbox.Text = textName;
            if (includeQuotes.Equals("Yes"))
            {
                quotesCheckbox.Checked = true;
            }
            else
            {
                quotesCheckbox.Checked = false;
            }


        }

        private void saveChangesButton_Click(object sender, EventArgs e)
        {
            form1.updateListViews();
            this.Close();
        }
    }
}
