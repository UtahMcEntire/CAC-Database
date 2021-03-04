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
    public partial class Form1 : Form
    {
        private string caseNum, childFirstName, childLastName, childDob, parentFirstName, parentLastName, parentDob;
        private DateTime interviewDate;
        public Form1()
        {
            InitializeComponent();
        }

        private void AddPerpBtn_Click(object sender, EventArgs e)
        {
            string perpFirst = perpFirstTxt.Text;
            string perpLast = perpLastTxt.Text;

            if (perpFirst.Trim() != "" || perpLast.Trim() != "")
                perpLstView.Items.Add((perpFirst.Trim() + ' ' + perpLast.Trim()).Trim());
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void AddPdfBtn_Click(object sender, EventArgs e)
        {
            addPDF.ShowDialog();
            childFirstNameTxt.Text = addPDF.FileName;

            foreach (string PDF in addPDF.FileNames)
            {
                pdfFilesListView.Items.Add(PDF);
            }

        }

        private void mp4Btn_Click(object sender, EventArgs e)
        {
            addVideo.ShowDialog();
            childFirstNameTxt.Text = addVideo.FileName;

            foreach (string Video in addVideo.FileNames)
            {
                pdfFilesListView.Items.Add(Video);
            }
        }

        private void addCaseBtn_Click(object sender, EventArgs e)
        {
            if (cNumTxt.TextLength == 0)
            {
                cNumTxt.BackColor = Color.Red;
                return;
            }
            else
                cNumTxt.BackColor = Color.White;
            caseNum = cNumTxt.Text;
            childFirstName = childFirstNameTxt.Text;
            childLastName = childLastNameTxt.Text;
            childDob = childDobTxt.Text;
            parentFirstName = parentFirstNameTxt.Text;
            parentLastName = parentLastNameTxt.Text;
            parentDob = parentDobTxt.Text;
            interviewDate = interviewDateTxt.Value;
            // TODO: Perp first name. Does it need to be array for multiple perps?
            // TODO: Perp last name. Does it need to be array for multiple perps?
            // TODO: Pdf files
            // TODO: Video files 

        }
    }
}
