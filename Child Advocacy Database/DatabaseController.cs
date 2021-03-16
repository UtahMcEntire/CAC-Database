using System;
using System.Data.SqlClient;

public class DatabaseController
{
	private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\ChildDatabase.mdf;Integrated Security=True;Connect Timeout=30";

	

	public DatabaseController()
	{
	}

	public int Insert(string CaseNum, string ChildFirst, string ChildLast, DateTime ChildDob, DateTime InterviewDate, string Guardian1First, string Guardian1Last, string Guardian2First, string Guardian2Last)

	
    {
		SqlCommand command;
		SqlConnection connection;

		using (connection = new SqlConnection(connectionString))
        {
			using (command = new SqlCommand("INSERT INTO " +
				"ChildDataTable " +
				"(CaseNum, ChildFirst, ChildLast, ChildDob, InterviewDate, Guardian1First, Guardian1Last, Guardian2First, Guardian2Last, Perps, MappedLocation) " +
				"VALUES " +
				"(@case, @childFirst, @childLast, @childDob, @interview, @guard1First, @guard1Last, @guard2First, @guard2Last, @perps, @mappedLoc)", 
				connection))
            {
				command.CommandType = System.Data.CommandType.Text;

				DBNull.Value.ToString();
                command.Parameters.AddWithValue("case", CaseNum == null ? (object)DBNull.Value : CaseNum);
                command.Parameters.AddWithValue("childFirst", ChildFirst == null ? (object)DBNull.Value : ChildFirst);
				command.Parameters.AddWithValue("childLast", ChildLast == null ? (object)DBNull.Value : ChildLast);
				command.Parameters.AddWithValue("childDob", ChildDob.ToString() == "1/1/0001 12:00:00 AM" ? (object)DBNull.Value : ChildDob);
				command.Parameters.AddWithValue("interview", InterviewDate.ToString() == "1/1/0001 12:00:00 AM" ? (object)DBNull.Value : InterviewDate);
				command.Parameters.AddWithValue("guard1First", Guardian1First == null ? (object)DBNull.Value : Guardian2First);
				command.Parameters.AddWithValue("guard1Last", Guardian1Last == null ? (object)DBNull.Value : Guardian1Last);
				command.Parameters.AddWithValue("guard2First", Guardian2First == null ? (object)DBNull.Value : Guardian2First);
				command.Parameters.AddWithValue("guard2Last", Guardian2Last == null ? (object)DBNull.Value : Guardian2Last);
				command.Parameters.AddWithValue("perps", DBNull.Value);
				command.Parameters.AddWithValue("mappedLoc", DBNull.Value);
				Console.WriteLine("work");

				try
				{
					int err;
					connection.Open();
					err = command.ExecuteNonQuery();
					Console.WriteLine(err);
				}
				catch (SqlException ex)
				{
                    System.Windows.Forms.MessageBox.Show(ex.Message, ex.GetType().ToString());
				}
			}
        }
		
		return 1;
    }


	private void Execute()
    {

    }


}
