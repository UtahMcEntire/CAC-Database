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
    public partial class Query : Form
    {
        public Query()
        {
            InitializeComponent();
        }
        private void addRemoveBtn_Click(object sender, EventArgs e)
        {
            var AddForm = new AddCaseForm(); 
            AddForm.Show();
            Close();
        }
    }
}