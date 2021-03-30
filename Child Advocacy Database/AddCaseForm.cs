using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.IO.Compression;
using System.IO;
using System.Reflection;
using Child_Advocacy_Database.Properties;
using System.Resources;
using System.Collections;

namespace Child_Advocacy_Database
{
    public partial class AddCaseForm : Form
    {
        // TODO: Solidify the DatabaseItem class or make a class to store the data members below and link the class to the database
        // TODO: Remove testing code
        // TODO: Find out if there will always be files associated with a case (a new folder is only made if files are added right now)
        // TODO: Test functionality, colors, resets, tab index, status information, 
        // TODO: Add the case to the database (see addCaseBtn_Click function comments below)
        // TODO: Remove from database after deletion from hard drive (see removeCaseBtn_Click function comments below)
        // TODO: Reset tab order to 0 in function clearFormBtn_Click
        // TODO: ?? more


        Case addCase;
        

        //
        // Initialization of HDD list and form
        //
        public AddCaseForm(Case editCase)
        {
            InitializeComponent();

            addCase = new Case();

            try
            {
                DriveInfo[] myDrives = DriveInfo.GetDrives();

                foreach (DriveInfo drive in myDrives)
                {
                    selectHddListBox.Items.Add(drive.Name + " " + drive.VolumeLabel);
                }
            }
            catch (Exception e)
            {
                statusLbl.ForeColor = Color.Red;
                statusLbl.Text = "**Status: Could not read hard drive list.";
                MessageBox.Show("Exception message: " + e.Message);
            }

            if (editCase != null)
            {
                statusLbl.Text = "**Status: Entering edit.";
                addCase = editCase;
                EditCaseFunc();
            }
        }

        //
        // Edit a case, gets the editCase from the query form
        //
        private void EditCaseFunc()
        {
            int i;
            ncaNumTxt.Text = addCase.CaseNum;
            childFirstNameTxt.Text = addCase.ChildFirst;
            childLastNameTxt.Text = addCase.ChildLast;
            childDobTxt.Text = addCase.ChildDob;
            g1FirstNameTxt.Text = addCase.Guardian1First;
            g1LastNameTxt.Text = addCase.Guardian1Last;
            g2FirstNameTxt.Text = addCase.Guardian2First;
            g2LastNameTxt.Text = addCase.Guardian2Last;
            interviewTxt.Text = addCase.InterviewDate;

            for (i = 0; i < addCase.PerpFirstNames.Count; i++)
            {
                perpListBox.Items.Add(addCase.PerpFirstNames[i].Trim() + ' ' + 
                    addCase.PerpLastNames[i].Trim() + ' ' + addCase.PerpNicks[i].Trim());
            }

            for (i = 0; i < addCase.SiblingFirstNames.Count; i++)
            {
                siblingListBox.Items.Add(addCase.SiblingFirstNames[i].Trim() + ' ' +
                    addCase.SiblingLastNames[i].Trim());
            }

            for (i = 0; i < addCase.OtherVictimFirstNames.Count; i++)
            {
                otherVictimListBox.Items.Add(addCase.OtherVictimFirstNames[i].Trim() + ' ' +
                    addCase.OtherVictimLastNames[i].Trim());
            }
        }


        //
        // Select a hard drive to save the database 
        //
        private void selectHddListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tempHdd;
            string addHdd;
            selectHddListBox.BackColor = Color.White;
            //hdd = selectHddListBox.SelectedItem.ToString().Split();
            if (selectHddListBox.SelectedIndex != -1)
            {
                tempHdd = selectHddListBox.SelectedItem.ToString();
                addHdd = "";
                if (tempHdd.Length > 3)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        addHdd += tempHdd[j];
                    }
                    if (addCase.HddList.Count == 0)
                        addCase.HddList.Add(addHdd);
                    else
                        addCase.HddList[0] = addHdd;
                }


                statusLbl.ForeColor = Color.Green;
                statusLbl.Text = "**Status: Enter case information to add to database.";
            }
            // for testing purposes
            //MessageBox.Show("Testing HDD: " + addCase.HddList[0]);

        }

        //
        // Add a perpetrator to the perp list box
        //
        private void AddPerpBtn_Click(object sender, EventArgs e)
        {
            string perpFirst = perpFirstTxt.Text;
            string perpLast = perpLastTxt.Text;
            string perpNick = perpNickTxt.Text;

            // Adds "" (empty string) if nothing was added to the text box, this is important for removal
            addCase.PerpFirstNames.Add(perpFirst);
            addCase.PerpLastNames.Add(perpLast);
            addCase.PerpNicks.Add(perpNick);

            // New code using the Perp class
            if (perpFirst.Trim() != "" || perpLast.Trim() != "" || perpNick.Trim() != "")
                addCase.PerpList.Add(new Perp(perpFirst, perpLast, perpNick));


            if (perpFirst.Trim() != "" || perpLast.Trim() != "" || perpNick.Trim() != "")
                perpListBox.Items.Add((perpFirst.Trim() + ' ' + perpLast.Trim() + ' ' + perpNick.Trim()).Trim());

            perpFirstTxt.Clear();
            perpLastTxt.Clear();
            perpNickTxt.Clear();
        }

        //
        // Remove a perpetrator from the perp list box
        //
        private void removePerpBtn_Click(object sender, EventArgs e)
        {
            if(perpListBox.SelectedIndex != -1) { 
                addCase.PerpFirstNames.RemoveAt(perpListBox.SelectedIndex);
                addCase.PerpLastNames.RemoveAt(perpListBox.SelectedIndex);
                addCase.PerpNicks.RemoveAt(perpListBox.SelectedIndex);

            // New code using the Perp class
            addCase.PerpList.RemoveAt(perpListBox.SelectedIndex);

            /* Testing
            foreach(var x in addCase.PerpFirstNames)
            {
                MessageBox.Show("Testing names left in list perp first name: " + x);
            }
            foreach(var x in addCase.PerpLastNames)
            {
                MessageBox.Show("Testing names left in list last perp name: " + x);
            }
            foreach(var x in addCase.PerpNicks)
            {
                MessageBox.Show("Testing names left in list perp nick name: " + x);
            }
            */
            perpListBox.Items.Remove(perpListBox.SelectedItem);
            }
        }

        //
        // Add a sibling to the sibling list box
        //
        private void addSiblingBtn_Click(object sender, EventArgs e)
        {
            string siblingFirst = siblingFirstNameTxt.Text;
            string siblingLast = siblingLastNameTxt.Text;

            addCase.SiblingFirstNames.Add(siblingFirst);
            addCase.SiblingLastNames.Add(siblingLast);

            // New code using the Sibling class
            if (siblingFirst.Trim() != "" || siblingLast.Trim() != "")
                addCase.SiblingList.Add(new Sibling(siblingFirst, siblingLast));

            if (siblingFirst.Trim() != "" || siblingLast.Trim() != "")
            {
                siblingListBox.Items.Add((siblingFirst.Trim() + ' ' + siblingLast.Trim()).Trim());
            }
            siblingFirstNameTxt.Clear();
            siblingLastNameTxt.Clear();
        }

        //
        // Remove a sibling from the sibling list box
        //
        private void removeSiblingBtn_Click(object sender, EventArgs e)
        {
            addCase.SiblingFirstNames.RemoveAt(siblingListBox.SelectedIndex);
            addCase.SiblingLastNames.RemoveAt(siblingListBox.SelectedIndex);

            // New code using the Sibling class
            addCase.SiblingList.RemoveAt(siblingListBox.SelectedIndex);

            /* Testing
            foreach(var x in addCase.SiblingFirstNames)
            {
                MessageBox.Show("Testing names left in list sibling first name: " + x);
            }
            foreach(var x in addCase.SiblingLastNames)
            {
                MessageBox.Show("Testing names left in list sibling last name: " + x);
            }
            */
            siblingListBox.Items.Remove(siblingListBox.SelectedItem);
        }

        //
        // Add an 'other victim' to the victim list box
        //
        private void addVictimBtn_Click(object sender, EventArgs e)
        {
            string victimFirst = otherVictimFirstNameTxt.Text;
            string victimLast = otherVictimLastNameTxt.Text;

            addCase.OtherVictimFirstNames.Add(victimFirst);
            addCase.OtherVictimLastNames.Add(victimLast);

            // New code using the Perp class
            if (victimFirst.Trim() != "" || victimLast.Trim() != "")
                addCase.VictimList.Add(new Victim(victimFirst, victimLast));

            if (victimFirst.Trim() != "" || victimLast.Trim() != "")
            {
                otherVictimListBox.Items.Add((victimFirst.Trim() + ' ' + victimLast.Trim()).Trim());
            }
            otherVictimFirstNameTxt.Clear();
            otherVictimLastNameTxt.Clear();
        }

        //
        // Remove an 'other victim' from the victim list box
        //
        private void removeVictimBtn_Click(object sender, EventArgs e)
        {
            addCase.OtherVictimFirstNames.RemoveAt(otherVictimListBox.SelectedIndex);
            addCase.OtherVictimLastNames.RemoveAt(otherVictimListBox.SelectedIndex);

            // New code using the Victim class
            addCase.VictimList.RemoveAt(otherVictimListBox.SelectedIndex);

            /* Testing 
            foreach(var x in addCase.OtherVictimFirstNames)
            {
                MessageBox.Show("Testing names left in list other victim first name: " + x);
            }
            foreach(var x in addCase.OtherVictimLastNames)
            {
                MessageBox.Show("Testing names left in list other victim last name: " + x);
            }
            */
            otherVictimListBox.Items.Remove(otherVictimListBox.SelectedItem);
            
        }
        
        //
        // Add a mp4 file to the mp4 list view
        //
        private void mp4Btn_Click(object sender, EventArgs e)
        {
            addVideo.ShowDialog();

            foreach (string Video in addVideo.FileNames)
            {
                mp4FilesListView.Items.Add(Video);
            }

            pdfFilesListView.BackColor = Color.White;
            mp4FilesListView.BackColor = Color.White;
            statusLbl.ForeColor = Color.Green;
            statusLbl.Text = "**Status: Enter case information to add to database.";
        }

        //
        // Remove a mp4 file from the mp4 list view
        //
        private void removeMp4ListItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in mp4FilesListView.SelectedItems)
            {
                mp4FilesListView.Items.Remove(item);
            }

            pdfFilesListView.BackColor = Color.White;
            mp4FilesListView.BackColor = Color.White;
            statusLbl.ForeColor = Color.Green;
            statusLbl.Text = "**Status: Enter case information to add to database.";
        }

        //
        // Add a pdf file to the pdf list fiew
        //
        private void AddPdfBtn_Click(object sender, EventArgs e)
        {
            addPDF.ShowDialog();

            foreach (string PDF in addPDF.FileNames)
            {
                pdfFilesListView.Items.Add(PDF);
            }

            pdfFilesListView.BackColor = Color.White;
            mp4FilesListView.BackColor = Color.White;
            statusLbl.ForeColor = Color.Green;
            statusLbl.Text = "**Status: Enter case information to add to database.";

        }

        //
        // Remove a pdf file from the pdf list view
        //
        private void removePdfListItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in pdfFilesListView.SelectedItems)
            {
                pdfFilesListView.Items.Remove(item);
            }

            pdfFilesListView.BackColor = Color.White;
            mp4FilesListView.BackColor = Color.White;
            statusLbl.ForeColor = Color.Green;
            statusLbl.Text = "**Status: Enter case information to add to database.";
        }

        //
        // Change the color of the background for the NCA# to white
        //
        private void ncaNumTxt_TextChanged(object sender, EventArgs e)
        {
            ncaNumTxt.BackColor = Color.White;
            statusLbl.ForeColor = Color.Green;
            statusLbl.Text = "**Status: Enter case information to add to database.";
        }
        private void childDobTxt_TextChanged(object sender, EventArgs e)
        {
            childDobTxt.BackColor = Color.White;
            statusLbl.ForeColor = Color.Green;
            statusLbl.Text = "**Status: Enter case information to add to database.";
        }
        private void interviewTxt_TextChanged(object sender, EventArgs e)
        {
            interviewTxt.BackColor = Color.White;
            statusLbl.ForeColor = Color.Green;
            statusLbl.Text = "**Status: Enter case information to add to database.";
        }
        //
        // Clear the form or start a new case
        //
        private void clearFormBtn_Click(object sender, EventArgs e)
        {
            ncaNumTxt.Clear();
            childFirstNameTxt.Clear();
            childLastNameTxt.Clear();
            childDobTxt.Clear();
            interviewTxt.Clear();
            g1FirstNameTxt.Clear();
            g1LastNameTxt.Clear();
            g2FirstNameTxt.Clear();
            g2LastNameTxt.Clear();
            perpFirstTxt.Clear();
            perpLastTxt.Clear();
            perpNickTxt.Clear();
            siblingFirstNameTxt.Clear();
            siblingLastNameTxt.Clear();
            otherVictimFirstNameTxt.Clear();
            otherVictimLastNameTxt.Clear();
            perpListBox.Items.Clear();
            siblingListBox.Items.Clear();
            otherVictimListBox.Items.Clear();
            pdfFilesListView.Items.Clear();
            mp4FilesListView.Items.Clear();
            pdfFilesListView.BackColor = Color.White;
            mp4FilesListView.BackColor = Color.White;
            selectHddListBox.ClearSelected();
            addCase.HddList = null;
            addCase.PerpFirstNames = null;
            addCase.PerpLastNames = null;
            addCase.PerpNicks = null;
            addCase.SiblingFirstNames = null;
            addCase.SiblingLastNames = null;
            addCase.OtherVictimFirstNames = null;
            addCase.OtherVictimLastNames = null;
            addCase = null;
            addCase = new Case();
            statusLbl.ForeColor = Color.Green;
            statusLbl.Text = "**Status: Enter case information to add to database.";
            // Would be nice to reset tab order to 0 here but not especially needed as it only takes 2 tabs to be back at the start from here
        }

        /* Can read from a resource file, cannot figure out how to write. Thinking to use database instead
        private static UnmanagedMemoryStream GetResourceStream(string resName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var strResources = assembly.GetName().Name + ".g.resources";
            var rStream = assembly.GetManifestResourceStream(strResources);
            var resourceReader = new ResourceReader(rStream);
            var items = resourceReader.OfType<DictionaryEntry>();
            var stream = items.First(x => (x.Key as string) == resName.ToLower()).Value;
            return (UnmanagedMemoryStream)stream;
        }
        */          

        //
        // Add the case to the database
        //
        private void addCaseBtn_Click(object sender, EventArgs e)
        {
            bool fileSuccess = false;
            /*if(ncaNumTxt.Text.Length == 4) Thinking to use database instead
            {
                string resName = "CaseCount.txt";
                var file = GetResourceStream(resName);
                using (var reader = new StreamReader(file))
                {
                    var line = reader.ReadLine();
                    int count = Int32.Parse(line);
                    count++;
                    MessageBox.Show(count.ToString());
                }
            }*/

            if(addCase.HddList.Count == 0)
            {
                statusLbl.ForeColor = Color.Red;
                statusLbl.Text = "**Status: Please choose which hard drive to save the database entry.";
                selectHddListBox.BackColor = Color.Red;
            }
            else if (ncaNumTxt.Text == "")
            {
                ncaNumTxt.BackColor = Color.Red;
                statusLbl.ForeColor = Color.Red;
                statusLbl.Text = "**Status: A NCA number is required to add a case to the database.";
            }
            else if(!checkDateFormat(childDobTxt.Text) && childDobTxt.Text != "")
            {
                statusLbl.ForeColor = Color.Red;
                statusLbl.Text = "**Status: Please enter child DOB in the exact format MM/DD/YYYY or leave blank if unknown.";
                childDobTxt.BackColor = Color.Red;
            }
            else if (!checkDateFormat(interviewTxt.Text) && interviewTxt.Text != "")
            {
                statusLbl.ForeColor = Color.Red;
                statusLbl.Text = "**Status: Please enter interview date in the exact format MM/DD/YYYY or leave blank if unknown.";
                interviewTxt.BackColor = Color.Red;
            }
            else
            {

                string targetPath = addCase.HddList[0] + ncaNumTxt.Text;
                addCase.CaseNum = ncaNumTxt.Text;
                addCase.ChildFirst = childFirstNameTxt.Text;
                addCase.ChildLast = childLastNameTxt.Text;
                addCase.ChildDob = childDobTxt.Text;
                addCase.InterviewDate = interviewTxt.Text;
                addCase.Guardian1First = g1FirstNameTxt.Text;
                addCase.Guardian1Last = g1LastNameTxt.Text;
                addCase.Guardian2First = g2FirstNameTxt.Text;
                addCase.Guardian2Last = g2LastNameTxt.Text;
                if (Directory.Exists(targetPath))
                {
                    if (MessageBox.Show("Folder " + ncaNumTxt.Text + " already exists. Overwrite?\nThis cannot be undone!", "Entry already exists",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        fileSuccess = addDirectory();
                    }
                }
                else
                {
                    fileSuccess = addDirectory();
                }
                

                if (fileSuccess)
                {
                    //
                    // Enter entry addCase into database here
                    DatabaseController database = new DatabaseController();
                    database.Insert(addCase.CaseNum, addCase.ChildFirst, addCase.ChildLast, addCase.ChildDob, addCase.InterviewDate, addCase.Guardian1First, addCase.Guardian1Last, addCase.Guardian2First, addCase.Guardian2Last, addCase.PerpList, addCase.SiblingList, addCase.VictimList, targetPath);
                    // database(targetPath)
                    // if(success){
                    statusLbl.ForeColor = Color.Blue;
                    statusLbl.Text = "**Status: NCA#: " + addCase.CaseNum + " was successfully added to the database!";
                    // } else {
                    // statusLbl.ForeColor = Color.Red;
                    // statusLbl.Text = "**Status: Error adding NCA#: " + addCase.CaseNum + " to the database.";
                    // }
                }
            }
        }

        //
        // Permanently delete the DCA# folder and database entry associated with dcaNumTxt
        //
        private void removeCaseBtn_Click(object sender, EventArgs e)
        {
            if (addCase.HddList.Count == 0)
            {
                statusLbl.ForeColor = Color.Red;
                statusLbl.Text = "**Status: Please choose which hard drive to remove the database entry.";
                selectHddListBox.BackColor = Color.Red;
            }
            else if (ncaNumTxt.Text == "")
            {
                ncaNumTxt.BackColor = Color.Red;
                statusLbl.ForeColor = Color.Red;
                statusLbl.Text = "**Status: Please enter a NCA# to remove.";
            }
            else
            {
                // Confirmation message box
                if (MessageBox.Show("Permanently delete NCA# " + ncaNumTxt.Text + " and ALL associated files?\nThis cannot be undone!", "Remove from database",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    try
                    {
                        string targetPath = addCase.HddList[0] + ncaNumTxt.Text;
                         
                        Directory.Delete(targetPath, true);
                        //
                        // Need to remove from database here
                        //
                        
                        // if(database delete success) {
                        MessageBox.Show("NCA# " + ncaNumTxt.Text + " deleted!");
                    }
                    catch (Exception ex)
                    {
                        statusLbl.ForeColor = Color.Red;
                        statusLbl.Text = "**Status: Error in file removal- see exception message for details.";
                        MessageBox.Show("Exception message: " + ex.Message);
                    }
                }
            }
        }

        //
        // Check if the date is formatted as MM/DD/YYYY
        //
        public bool checkDateFormat(string date)
        {
            if (date.Length == 10)
            {
                if (
                    (date[0] >= '0'  && date[0] <= '9') &&
                    (date[1] >= '0'  && date[1] <= '9') &&
                    (date[2] == '/') &&
                    (date[3] >= '0'  && date[3] <= '9') &&
                    (date[4] >= '0'  && date[4] <= '9') &&
                    (date[5] == '/') &&
                    (date[6] >= '0'  && date[6] <= '9') &&
                    (date[7] >= '0'  && date[7] <= '9') &&
                    (date[8] >= '0'  && date[8] <= '9') &&
                    (date[9] >= '0'  && date[9] <= '9')
                   )
                {
                    return true;
                }
            }
            return false;
        }

        //
        // Add directory
        //
        private bool addDirectory()
        {
            string targetPath = addCase.HddList[0] + ncaNumTxt.Text;
            string sourcePath;
            string fileName;
            string destPath;
            try
            {
                System.IO.Directory.CreateDirectory(targetPath);

                foreach (ListViewItem item in pdfFilesListView.Items)
                {
                    sourcePath = item.Text;
                    fileName = System.IO.Path.GetFileName(sourcePath);
                    destPath = System.IO.Path.Combine(targetPath, fileName);
                    System.IO.File.Copy(sourcePath, destPath, true);
                }

                foreach (ListViewItem item in mp4FilesListView.Items)
                {
                    sourcePath = item.Text;
                    fileName = System.IO.Path.GetFileName(sourcePath);
                    destPath = System.IO.Path.Combine(targetPath, fileName);
                    System.IO.File.Copy(sourcePath, destPath, true);
                }
                return true;
            }
            catch (Exception ex)
            {
                pdfFilesListView.BackColor = Color.Red;
                mp4FilesListView.BackColor = Color.Red;
                statusLbl.ForeColor = Color.Red;
                statusLbl.Text = "**Status: Error adding files to folder, please see exception message for details.";
                MessageBox.Show("Exception message: " + ex.Message);
                return false;
            }
        }

        //
        // Quit to dashboard
        //
        private void dashboardBtn_Click(object sender, EventArgs e)
        {
            Application.OpenForms["dashboard"].BringToFront();
            addCase = null; 
            Close();
        }
    }
}
