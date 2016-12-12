using FingerPrint.Controllers;
using FingerPrint.Controllers.Implementations;
using FingerPrint.Models.Interfaces.TypeInterfaces;
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
        ITextViewModel model;
        private ITextController _textController;
        private IGroupController _groupController;
        Form1 form1;
        public FormPopUpFileEdit(ITextViewModel _model,   ITextController textController, IGroupController groupController, Form1 _form1)
        {
            _textController = textController;
            _groupController = groupController;
            form1 = _form1;
            model = _model;
            
            InitializeComponent();
            
            quotesCheckbox.Checked = model.GetIncludeQuotes();
            newAuthorTextBox.Text = model.GetAuthor();
            newFileNameTextbox.Text = model.GetName();
              

        }

        private void saveChangesButton_Click(object sender, EventArgs e)
        {
            try
            {
                if(newFileNameTextbox.Text == model.GetName())
                {
                    newFileNameTextbox.Text = "";
                }
                if(newAuthorTextBox.Text == model.GetAuthor())
                {
                    newAuthorTextBox.Text = "";
                }
                _textController.UpdateText(model, newFileNameTextbox.Text, newAuthorTextBox.Text, quotesCheckbox.Checked);
                form1.updateListViews();
                this.Close();
            }
            catch
            {
                string errorMessage = "You cannot change a text's name to one that already exists";
                var form2 = new ErrorMessageDisplay(errorMessage);
                form2.Show(this);
            }
        }
    }
}
