using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Child_Advocacy_Database
{
    public partial class Query : Form
    {
        // Most of this form is in testing phase. 
  
        // TODO: Link database to search
        // TODO: Figure out edit options
        // TODO: Link 'open file' to listbox selection
        // TODO: Link search button to status and check for problems
        // TODO: Link database to delete (deleteBtn_Click function)
        // TODO: Reorder the functions
        // TODO: ?? lots more


        Case queryCase;
        List<Case> queryCases;

        //
        // Intitialization and add drive list to the select hdd listbox
        //
        public Query()
        {
            InitializeComponent();
            queryCases = new List<Case>();
            queryCase = new Case();
            try
            {
                DriveInfo[] myDrives = DriveInfo.GetDrives();

                foreach (DriveInfo drive in myDrives)
                {
                    selectHddListBox.Items.Add(drive.Name + " " + drive.VolumeLabel);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // 
        // Selects the hard drives and adds them to the hard drive list if they are not duplicates
        //
        private void selectHddListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool addHddFlag;
            string addHdd;
            string tempHdd;


            statusLbl.ForeColor = Color.Green;
            statusLbl.Text = "**Status: Enter search criteria.";
            selectHddListBox.BackColor = Color.White;


            addHddFlag = false;
            tempHdd = selectHddListBox.SelectedItem.ToString();
            addHdd = "";
            if (tempHdd.Length > 3)
            {
                for (int j = 0; j < 3; j++)
                {
                    addHdd += tempHdd[j];
                }

                if (queryCase.HddList.Count == 0)
                {
                    queryCase.HddList.Add(addHdd);
                }
                else
                {
                    addHddFlag = true;
                    for (int i = 0; i < queryCase.HddList.Count; i++)
                    {
                        if (queryCase.HddList[i].Contains(addHdd))
                        {
                            addHddFlag = false;
                        }
                    }
                }
            }
            if (addHddFlag)
            {
                queryCase.HddList.Add(addHdd);
            }

            confirmHddListBox.Items.Clear();
            foreach (var hdd in queryCase.HddList)
            {
                confirmHddListBox.Items.Add(hdd);
            }
        }

        //
        // Remove a hard drive from the hard drive list and update the confirm hdd listbox
        //
        private void removeHddBtn_Click(object sender, EventArgs e)
        {
            if(confirmHddListBox.SelectedIndex != -1)
            {     
                string deletedHdd = confirmHddListBox.SelectedItem.ToString();
          
                int indexToDelete = -1;
                for(int i = 0; i < queryCase.HddList.Count; i++)
                {
                    if(deletedHdd == queryCase.HddList[i])
                    {
                        indexToDelete = i;
                    }
                }
                if(indexToDelete != -1)
                {
                    queryCase.HddList.RemoveAt(indexToDelete);
                }
                confirmHddListBox.Items.RemoveAt(confirmHddListBox.SelectedIndex);

                confirmHddListBox.Items.Clear();
                foreach (var hdd in queryCase.HddList)
                {
                    confirmHddListBox.Items.Add(hdd);
                }
                statusLbl.ForeColor = Color.Green;
                statusLbl.Text = "**Status: Enter search criteria.";
            }
            else
            {
                statusLbl.ForeColor = Color.Red;
                statusLbl.Text = "**Status: Please select a hard drive from the list to remove.";
            }
        }

        //
        // Reset the color of the NCA# textbox
        //
        private void ncaNumTxt_TextChanged(object sender, EventArgs e)
        {
            ncaNumTxt.BackColor = Color.White;
            statusLbl.ForeColor = Color.Green;
            statusLbl.Text = "**Status: Enter search criteria.";
        }

        //
        // Search for selected criteria
        //
        private void searchBtn_Click(object sender, EventArgs e)
        {
            //
            // queryCase data members to search for
            //
            
            if (!checkDateFormat(childDobTxt.Text) && childDobTxt.Text != "")
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

            // For testing:
            List<string> tempHDD = new List<string>();
            tempHDD = queryCase.HddList;

            queryCase = new Case();
            queryCase.HddList = tempHDD;
            queryCase.CaseNum = ncaNumTxt.Text;
            queryCase.ChildFirst = childFirstNameTxt.Text;
            queryCase.ChildLast = childLastNameTxt.Text;
            queryCase.ChildDob = childDobTxt.Text;
            queryCase.Guardian1First = g1FirstNameTxt.Text;
            queryCase.Guardian1Last = g1LastNameTxt.Text;
            queryCase.Guardian2First = g2FirstNameTxt.Text;
            queryCase.Guardian2Last = g2LastNameTxt.Text;
            queryCase.InterviewDate = interviewTxt.Text;
            queryCase.PerpFirstNames.Add(perpFirstTxt.Text);
            queryCase.PerpLastNames.Add(perpLastTxt.Text);
            queryCase.PerpNicks.Add(perpNickTxt.Text);
            queryCase.SiblingFirstNames.Add(siblingFirstNameTxt.Text);
            queryCase.SiblingLastNames.Add(siblingLastNameTxt.Text);
            queryCase.OtherVictimFirstNames.Add(otherVictimFirstNameTxt.Text);
            queryCase.OtherVictimLastNames.Add(otherVictimLastNameTxt.Text);
            /*queryCase.PerpFirstNames.Add("PressDownArrow");
            queryCase.PerpFirstNames.Add("ToSeeMoreNames");*/

            //
            // Add database query here 
            // A List<DatabaseItem> queryCases will be populated with the search results and added to the searchResultListBox
            // if(databaseSuccess){
            //   

            // This is for testing:
            queryCases.Add(queryCase);
            searchResultListBox.Items.Clear();
            foreach(var caseList in queryCases)
            {
                searchResultListBox.Items.Add(caseList.ToString()); // Overloaded ToString for queryCase class to print out the NCA and child first/last name
            }
            //      }   
            // }else{
            //

        }

        //
        // Remove the folders associated with the NCA# on all selected hard drives
        //
        private void deleteBtn_Click(object sender, EventArgs e)
        {
            bool deletedSuccess = false;
            if (queryCase.HddList.Count == 0)
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
                if (MessageBox.Show("Permanently delete NCA# " + ncaNumTxt.Text + " and ALL associated files from ALL selected hard drives?\n" +
                        "This cannot be undone!", "Remove from database",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    try
                    {
                        for (int i = 0; i < queryCase.HddList.Count; i++)
                        {
                            if (Directory.Exists(queryCase.HddList[i] + ncaNumTxt.Text))
                            {
                                Directory.Delete(queryCase.HddList[i] + ncaNumTxt.Text, true); // 'true' means to delete subfiles
                                //
                                // Need to remove from database here
                                //
                                deletedSuccess = true;
                            }
                        }
                        if (deletedSuccess)
                        {
                            MessageBox.Show("NCA# " + ncaNumTxt.Text + " deleted!");
                        }
                        else
                        {
                            statusLbl.ForeColor = Color.Red;
                            statusLbl.Text = "**Status: Error in file removal- folder not found.";
                            MessageBox.Show("NCA# " + ncaNumTxt.Text + " folder not found.");
                        }
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
        // Clear/reset the form
        //
        private void clearFormBtn_Click(object sender, EventArgs e)
        {
            ncaNumTxt.Clear();
            childFirstNameTxt.Clear();
            childLastNameTxt.Clear();
            childDobTxt.Clear();
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
            searchResultListBox.Items.Clear();
            selectHddListBox.BackColor = Color.White;
            confirmHddListBox.BackColor = Color.White;
            queryCases = null;
            queryCases = new List<Case>();
            statusLbl.ForeColor = Color.Green;
            statusLbl.Text = "**Status: Enter search criteria.";
            // Would be nice to reset tab order to 0 here 
        }

        //
        // Exit to dashboard
        //
        private void dashboardBtn_Click(object sender, EventArgs e)
        {
            Application.OpenForms["dashboard"].BringToFront();
            Close();
        }

        private void openFileBtn_Click(object sender, EventArgs e)
        {
            // Needs the Hdd associated with the searchResultListBox.SelectedItem
            // This needs to come from the database search and stored in the List<DatabaseItems> queryCases

            // This is for testing, need to have added a folder from the addform (or manually )
            // On this form choose only 1 hard drive, enter the NCA number of the previous folder and press search, then press open file -
            // Clicking the listbox does nothing right now
            // It should bring up the folder with the files in file explorer if you followed the steps correctly, otherwise it brings up default location 
            try
            {
                Process.Start("explorer.exe", queryCase.HddList[0] + queryCase.CaseNum);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void searchResultListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // When a search result is clicked then it will populate the textboxes with the List<DatabaseItem> queryCases[searchResultsListBox.SelectedIndex]
            // That way everything associated with the search can be seen as there is too much to fit into the listbox
            // 
            if (searchResultListBox.SelectedIndex != -1)
            {
                ncaNumTxt.Text = queryCases[searchResultListBox.SelectedIndex].CaseNum;
                childFirstNameTxt.Text = queryCases[searchResultListBox.SelectedIndex].ChildFirst;
                childLastNameTxt.Text = queryCases[searchResultListBox.SelectedIndex].ChildLast;
                childDobTxt.Text = queryCases[searchResultListBox.SelectedIndex].ChildDob;
                g1FirstNameTxt.Text = queryCases[searchResultListBox.SelectedIndex].Guardian1First;
                g1LastNameTxt.Text = queryCases[searchResultListBox.SelectedIndex].Guardian1Last;
                g2FirstNameTxt.Text = queryCases[searchResultListBox.SelectedIndex].Guardian2First;
                g2LastNameTxt.Text = queryCases[searchResultListBox.SelectedIndex].Guardian2Last;
                interviewTxt.Text = queryCases[searchResultListBox.SelectedIndex].InterviewDate;
                perpFirstTxt.Text = queryCases[searchResultListBox.SelectedIndex].printPerpFirst(); // Populate the textboxes, can press down arrow if it doesn't all fit
                perpLastTxt.Text = queryCases[searchResultListBox.SelectedIndex].printPerpLast();
                perpNickTxt.Text = queryCases[searchResultListBox.SelectedIndex].printPerpNick();
                siblingFirstNameTxt.Text = queryCases[searchResultListBox.SelectedIndex].printSiblingFirst();
                siblingLastNameTxt.Text = queryCases[searchResultListBox.SelectedIndex].printSiblingLast();
                otherVictimFirstNameTxt.Text = queryCases[searchResultListBox.SelectedIndex].printOtherVictimFirst();
                otherVictimLastNameTxt.Text = queryCases[searchResultListBox.SelectedIndex].printOtherVictimLast();
            }
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            // This will change to be when a listboxitem is selected and populates the textboxes,
            // that will be the query to send to edit, there needs to be a flag to check if the search was completed
            if (queryCase != null)
            {
                AddCaseForm editCase = new AddCaseForm(queryCase);
                statusLbl.ForeColor = Color.Green;
                statusLbl.Text = "**Status: Editing the most recently searched case.";
                editCase.Show();
                Close();
            }
            else
            {
                statusLbl.ForeColor = Color.Red;
                statusLbl.Text = "**Status: Please search for a case before attempting to edit.";
            }
        }

        //
        // Check the date format
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
        // Change color of child DOB
        //
        private void childDobTxt_TextChanged(object sender, EventArgs e)
        {
            childDobTxt.BackColor = Color.White;
            statusLbl.ForeColor = Color.Green;
            statusLbl.Text = "**Status: Enter search criteria.";
        }

        //
        // Change color of interview text
        //
        private void interviewTxt_TextChanged(object sender, EventArgs e)
        {
            interviewTxt.BackColor = Color.White;
            statusLbl.ForeColor = Color.Green;
            statusLbl.Text = "**Status: Enter search criteria.";
        }
    }
}