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
            var AddForm = new Form1(); // Bring up add form onclick
            AddForm.FormClosed += Form1_FormClosing;
            AddButton.Enabled = false;
            AddForm.Show();
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void QueryButton_Click(object sender, EventArgs e)
        {
            var QueryForm = new Query(); // Bring up search form onclick
            QueryForm.FormClosed += Form1_FormClosing;
            SearchButton.Enabled = false;
            QueryForm.Show();
        }

        private void Form1_FormClosing(object sender, EventArgs e)
        {
            AddButton.Enabled = true; // re-enables add button on dashboard
            this.Show();
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            bool Form1_Open = false;
            FormCollection fc = Application.OpenForms;
            foreach (Form frm in fc)
            {
                //iterate through forms to see if "form1" form or "Query" form is open
                if (frm.Name == "Form1")
                {
                    Form1_Open = true;
                    MessageBox.Show("The case file editor is open. Please save any unsaved data and close that window before exiting the program to avoid possible data loss.", "Case File Editor Open", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                if (frm.Name == "Query")
                {
                    Form1_Open = true;
                    MessageBox.Show("The search window is open. Please save close that window before exiting the program to avoid possible data loss.", "Case File Search Open", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }

            }
            if (!Form1_Open)
            {
                System.Windows.Forms.Application.Exit();
            }
        }
    }
}
