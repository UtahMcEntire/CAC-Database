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
    public partial class AddCaseForm : Form
    {
        private string caseNum, childFirstName, childLastName, childDob, parentFirstName, parentLastName, parentDob;
        private DateTime interviewDate;


        private void removePdfListItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in pdfFilesListView.SelectedItems)
            {
                pdfFilesListView.Items.Remove(item);
            }
        }

        private void dashboardBtn_Click(object sender, EventArgs e)
        {
            Application.OpenForms["dashboard"].BringToFront();
            Close();
        }

        private void removeMp4ListItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in mp4FilesListView.SelectedItems)
            {
                mp4FilesListView.Items.Remove(item);
            }
        }

        public AddCaseForm()
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

        private void AddPdfBtn_Click(object sender, EventArgs e)
        {
            addPDF.ShowDialog();

            foreach (string PDF in addPDF.FileNames)
            {
                pdfFilesListView.Items.Add(PDF);
            }

        }

        private void mp4Btn_Click(object sender, EventArgs e)
        {
            addVideo.ShowDialog();

            foreach (string Video in addVideo.FileNames)
            {
                mp4FilesListView.Items.Add(Video);
            }
        }

        private void addCaseBtn_Click(object sender, EventArgs e)
        {
            string targetPath = @"C:\tempCACfiles";
            string sourcePath;
            string fileName;
            string destFile;
            
            System.IO.Directory.CreateDirectory(targetPath);

            foreach (ListViewItem item in pdfFilesListView.Items)
            {
                sourcePath =  item.Text;
                fileName = System.IO.Path.GetFileName(sourcePath);
                destFile = System.IO.Path.Combine(targetPath, fileName);
                System.IO.File.Copy(sourcePath, destFile, true);
            }

            foreach (ListViewItem item in mp4FilesListView.Items)
            {
                sourcePath = item.Text;
                fileName = System.IO.Path.GetFileName(sourcePath);
                destFile = System.IO.Path.Combine(targetPath, fileName);
                System.IO.File.Copy(sourcePath, destFile, true);
            }
            //Examples of move and get directory name -
            //System.IO.Directory.Move(@"C:\Users\Public\public\test\", @"C:\Users\Public\private");
            //string path = @"C:\My Folder\DumentContent\Code\Data\Source\Web\Project\Word\Final\test.doc";
            //string folder = System.IO.Path.GetDirectoryName(path);

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
            parentDob = g1DobTxt.Text;
            interviewDate = interviewDateTxt.Value;

            // TODO: Perp first name. Does it need to be array for multiple perps?
            // TODO: Perp last name. Does it need to be array for multiple perps?

        }
    }
}
