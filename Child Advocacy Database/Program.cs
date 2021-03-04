using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

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
            Application.Run(new Form1());

            SqlConnection connection = new SqlConnection();
            // Adds the connection string for the Database.
            // The @ escapes the \ in the file locations
            connection.ConnectionString =
                @"Data Source=(LocalDB).\MSSQLLocalDB;" + 
                @"AttachDbFilename=|DataDirectory|.\Database1.mdf;" +
                "Integrated Security=True;";
            connection.Open();
            string queryString = "";
            SqlCommand command = new SqlCommand(queryString, connection);

        }
    }
}
