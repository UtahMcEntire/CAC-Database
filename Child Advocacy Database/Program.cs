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
            Application.Run(new Dashboard());

            /* 
            Template Database Insert
            DatabaseController db = new DatabaseController();
            DateTime date = DateTime.Now;
            db.Insert("c1234356", "test2", "test2Last", date, date, "guardian1F 2", "guardian1L 2", "guard2F 2", "g2L 2");
            */ 
        }

        //string queryString = "";
        //SqlCommand command = new SqlCommand(queryString, connection);

    }
}

