using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Child_Advocacy_Database
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var main_form = new Dashboard();
            main_form.Show();
            Application.Run();

            //
            // BUNCH OF DEBUG GARBAGE TO BE REMOVED AFTER TESTING
            //
            //XmlDb db = new XmlDb();
            //Case cs = new Case();
            //cs.CaseNum = "2020";
            //cs.ChildFirst = "first2";
            //cs.ChildLast = "last2";
            //cs.ChildDob = "11/11/2000";
            //cs.InterviewDate = "01/21/2000";
            //cs.PerpList.Add(new Perp("pete", "the", "perp"));
            //cs.PerpList.Add(new Perp("mitchell", "the", "terrible"));
            //cs.PerpList.Add(new Perp("Perpy", "McPerpenstein", "El Perpero"));
            //cs.SiblingList.Add(new Sibling("second child", "something"));
            //cs.SiblingList.Add(new Sibling("third child", "something else"));
            //cs.VictimList.Add(new Victim("a", "victim"));
            //cs.VictimList.Add(new Victim("b", "other victim"));
            //cs.Guardian1First = "Parent1";
            //cs.Guardian1Last = "LastName";
            //cs.Guardian2First = "Parent2";
            //cs.Guardian2Last = "DifferentLastName";
            //cs.Location = "E:/programFiles";
            //db.Insert(cs);
            //if (cs.PerpFirstName == "")
            //    Console.WriteLine("Perpfirst:" + cs.PerpFirstName);
            //List<Case> cases = db.Query(cs);

            //if (cases.Count != 0)
            //    Console.WriteLine(cases[0].ToString());
        }
    }
}

