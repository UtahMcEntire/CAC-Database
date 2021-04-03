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
        bool isEditForm = false;
        string hdd = "";
        

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
            isEditForm = true;
            ncaNumTxt.Text = addCase.CaseNum;
            childFirstNameTxt.Text = addCase.ChildFirst;
            childLastNameTxt.Text = addCase.ChildLast;
            childDobTxt.Text = addCase.ChildDob;
            g1FirstNameTxt.Text = addCase.Guardian1First;
            g1LastNameTxt.Text = addCase.Guardian1Last;
            g2FirstNameTxt.Text = addCase.Guardian2First;
            g2LastNameTxt.Text = addCase.Guardian2Last;
            interviewTxt.Text = addCase.InterviewDate;

            foreach (Perp p in addCase.PerpList)
            {
                perpListBox.Items.Add(p.ToString());
            }
            foreach (Sibling s in addCase.SiblingList)
            {
                siblingListBox.Items.Add(s.ToString());
            }
            foreach (Victim v in addCase.VictimList)
            {
                otherVictimListBox.Items.Add(v.ToString());
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
                    hdd = addHdd;
                }


                statusLbl.ForeColor = Color.Green;
                statusLbl.Text = "**Status: Enter case information to add to database.";
            }
            // for testing purposes
            //MessageBox.Show("Testing HDD: " + hdd[0]);

        }

        //
        // Add a perpetrator to the perp list box
        //
        private void AddPerpBtn_Click(object sender, EventArgs e)
        {
            string perpFirst = perpFirstTxt.Text;
            string perpLast = perpLastTxt.Text;
            string perpNick = perpNickTxt.Text;

            //// Adds "" (empty string) if nothing was added to the text box, this is important for removal
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
            if (perpListBox.SelectedIndex != -1)
            {
                //addCase.PerpFirstName.RemoveAt(perpListBox.SelectedIndex);
                //addCase.PerpLastName.RemoveAt(perpListBox.SelectedIndex);
                //addCase.PerpNick.RemoveAt(perpListBox.SelectedIndex);

                // New code using the Perp class
                addCase.PerpList.RemoveAt(perpListBox.SelectedIndex);

                /* Testing
                foreach(var x in addCase.PerpFirstName)
                {
                    MessageBox.Show("Testing names left in list perp first name: " + x);
                }
                foreach(var x in addCase.PerpLastName)
                {
                    MessageBox.Show("Testing names left in list last perp name: " + x);
                }
                foreach(var x in addCase.PerpNick)
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
            if (siblingListBox.SelectedIndex != -1)
            {
                // New code using the Sibling class
                addCase.SiblingList.RemoveAt(siblingListBox.SelectedIndex);

                /* Testing
                foreach(var x in addCase.SiblingFirstName)
                {
                    MessageBox.Show("Testing names left in list sibling first name: " + x);
                }
                foreach(var x in addCase.SiblingLastName)
                {
                    MessageBox.Show("Testing names left in list sibling last name: " + x);
                }
                */
                siblingListBox.Items.Remove(siblingListBox.SelectedItem);
            }
        }

        //
        // Add an 'other victim' to the victim list box
        //
        private void addVictimBtn_Click(object sender, EventArgs e)
        {
            string victimFirst = otherVictimFirstNameTxt.Text;
            string victimLast = otherVictimLastNameTxt.Text;

            // New code using the Victim class
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
            if (otherVictimListBox.SelectedIndex != -1)
            {
                // New code using the Victim class
                addCase.VictimList.RemoveAt(otherVictimListBox.SelectedIndex);

                /* Testing 
                foreach(var x in addCase.OtherVictimFirstName)
                {
                    MessageBox.Show("Testing names left in list other victim first name: " + x);
                }
                foreach(var x in addCase.OtherVictimLastName)
                {
                    MessageBox.Show("Testing names left in list other victim last name: " + x);
                }
                */
                otherVictimListBox.Items.Remove(otherVictimListBox.SelectedItem);
            }
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
            hdd = "";
            addCase.PerpFirstName = null;
            addCase.PerpLastName = null;
            addCase.PerpNick = null;
            addCase.SiblingFirstName = null;
            addCase.SiblingLastName = null;
            addCase.OtherVictimFirstName = null;
            addCase.OtherVictimLastName = null;
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

            if(hdd == "")
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
            else if (!checkDateFormat(childDobTxt.Text) && childDobTxt.Text != "")
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

                string targetPath = hdd + ncaNumTxt.Text;
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
                        if (isEditForm == true)
                        {
                            //
                            // Remove old database entry here if it exists (for edit functionality)
                            // 
                        }
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
                    database.GetAllDB();
                    
                    
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
            if (hdd == "")
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
                        string targetPath = hdd + ncaNumTxt.Text;
                         
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
                    (date[0] >= '0' && date[0] <= '9') &&
                    (date[1] >= '0' && date[1] <= '9') &&
                    (date[2] == '/') &&
                    (date[3] >= '0' && date[3] <= '9') &&
                    (date[4] >= '0' && date[4] <= '9') &&
                    (date[5] == '/') &&
                    (date[6] >= '0' && date[6] <= '9') &&
                    (date[7] >= '0' && date[7] <= '9') &&
                    (date[8] >= '0' && date[8] <= '9') &&
                    (date[9] >= '0' && date[9] <= '9')
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
            string targetPath = hdd + ncaNumTxt.Text;
            string sourcePath;
            string fileName;
            string destPath;
            try
            {
                if (isEditForm == false)
                {
                    try
                    {
                        Directory.Delete(targetPath, true);
                    }
                    catch(Exception ex)
                    {
                        statusLbl.ForeColor = Color.Red;
                        statusLbl.Text = "**Status: " + ex.Message;
                    }
                }

                
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
                statusLbl.Text = "**Status: " + ex.Message;
                //MessageBox.Show("Exception message: " + ex.Message);
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