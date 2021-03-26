using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Child_Advocacy_Database
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            var AddCaseForm = new AddCaseForm(null); // Bring up add form onclick
            AddCaseForm.FormClosed += AddCaseForm_FormClosing;
            AddButton.Enabled = false;
            Hide();
            AddCaseForm.Show();
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void QueryButton_Click(object sender, EventArgs e)
        {
            var QueryForm = new Query(); // Bring up search form onclick
            QueryForm.FormClosed += Query_FormClosing;
            QueryButton.Enabled = false;
            Hide();
            QueryForm.Show();
        }

        private void AddCaseForm_FormClosing(object sender, EventArgs e)
        {
            AddButton.Enabled = true; // re-enables add button on dashboard
            this.Show();
        }

        private void Query_FormClosing(object sender, EventArgs e)
        {
            QueryButton.Enabled = true; // re-enables query button on dashboard
            this.Show();
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            bool AddCaseForm_Open = false;
            FormCollection fc = Application.OpenForms;
            foreach (Form frm in fc)
            {
                //iterate through forms to see if "AddCaseForm" form or "Query" form is open
                if (frm.Name == "AddCaseForm")
                {
                    AddCaseForm_Open = true;
                    MessageBox.Show("The case file editor is open. Please save any unsaved data and close that window before exiting the program to avoid possible data loss.", "Case File Editor Open", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                if (frm.Name == "Query")
                {
                    AddCaseForm_Open = true;
                    MessageBox.Show("The search window is open. Please close that window before exiting the program to avoid possible data loss.", "Case File Search Open", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }

            }
            if (!AddCaseForm_Open)
            {
                System.Windows.Forms.Application.Exit();
            }
        }
    }
}
