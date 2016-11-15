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
    public partial class NewGroupPopUp : Form
    {
        Form1 form1;

        public NewGroupPopUp(Form1 _form1)
        {
            form1 = _form1;
            InitializeComponent();
        }

        private void saveNewGroupButton_Click(object sender, EventArgs e)
        {
            form1.addGroup(groupNameTextBox.Text);
            this.Close();
        }
    }
}
