using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            Application.Run(new Form1());

            SqlCommand cmd;
            SqlConnection con;
            // Adds the connection string for the Database.
            // The @ escapes the \ in the file locations
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\ChildDatabase.mdf;Integrated Security=True;Connect Timeout=30");
            //con.Open();
            using (cmd = new SqlCommand("INSERT INTO ChildDataTable (CaseNum, ChildFirst, ChildLast, ChildDob, InterviewDate, Guardian1First, Guardian1Last, Guardian2First, Guardian2Last, Perps, MappedLocation) VALUES (@case, @childFirst, @childLast, @childDob, @interview, @guard1First, @guard1Last, @guard2First, @guard2Last, @perps, @mappedLoc)", con))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("case", "1234567");
                cmd.Parameters.AddWithValue("childFirst", "child1F2");
                cmd.Parameters.AddWithValue("childLast", "child1L2");
                cmd.Parameters.AddWithValue("childDob", DateTime.Today);
                cmd.Parameters.AddWithValue("interview", DateTime.Today);
                cmd.Parameters.AddWithValue("guard1First", "G1F2");
                cmd.Parameters.AddWithValue("guard1Last", "G1L2");
                cmd.Parameters.AddWithValue("guard2First", "G2F2");
                cmd.Parameters.AddWithValue("guard2Last", "G2L2");
                cmd.Parameters.AddWithValue("perps", DBNull.Value);
                cmd.Parameters.AddWithValue("mappedLoc", DBNull.Value);
                
                
                try { 
                    int i;
                    con.Open();
                    i = cmd.ExecuteNonQuery();
                    Console.WriteLine(i);
                } catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
                
            }

            DatabaseItem Dbi = new DatabaseItem();
            
        }

        //string queryString = "";
        //SqlCommand command = new SqlCommand(queryString, connection);

    }
}

