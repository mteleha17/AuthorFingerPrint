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
    public partial class ErrorMessageDisplay : Form
    {
        public ErrorMessageDisplay(string errorMessage)
        {
            InitializeComponent();
            string errorMessageToShow = errorMessage;
            errorLabel.Text = errorMessage;
        }
        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
