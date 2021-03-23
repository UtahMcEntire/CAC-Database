using Child_Advocacy_Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class DatabaseController
{
	private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\ChildDatabase.mdf;Integrated Security=True;Connect Timeout=30";

	

	public DatabaseController()
	{
	}

	public int Insert(string CaseNum, string ChildFirst, string ChildLast, string ChildDob, string InterviewDate, string Guardian1First, string Guardian1Last, string Guardian2First, string Guardian2Last)

	
    {
		SqlCommand command;
		SqlConnection connection;

		using (connection = new SqlConnection(connectionString))
        {
			using (command = new SqlCommand("INSERT INTO " +
				"ChildDataTable " +
				"(CaseNum, ChildFirst, ChildLast, ChildDob, " +
				"InterviewDate, " +
				"Guardian1First, Guardian1Last, Guardian2First, Guardian2Last, " +
				"Perps, MappedLocation) " +
				"VALUES " +
				"(@case, @childFirst, @childLast, @childDob, " +
				"@interview, " +
				"@guard1First, @guard1Last, @guard2First, @guard2Last, " +
				"@perps, @mappedLoc)", 
				connection))
            {
				command.CommandType = System.Data.CommandType.Text;

				DBNull.Value.ToString();
                command.Parameters.AddWithValue("case", CaseNum); // Required Parameter
                command.Parameters.AddWithValue("childFirst", ChildFirst == null ? (object)DBNull.Value : ChildFirst);
				command.Parameters.AddWithValue("childLast", ChildLast == null ? (object)DBNull.Value : ChildLast);
				command.Parameters.AddWithValue("childDob", ChildDob == null ? (object)DBNull.Value : ChildDob);
				command.Parameters.AddWithValue("interview", InterviewDate == null ? (object)DBNull.Value : InterviewDate);
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

	public List<Case> Query(string CaseNum, string ChildFirst, string ChildLast, string ChildDob, string InterviewDate, string Guardian1First, string Guardian1Last, string Guardian2First, string Guardian2Last)
    {
		List<Case> cases = new List<Case>();
		int index; // Index of current ordinal read by reader

		using (SqlConnection connection = new SqlConnection(connectionString))
		{
			using (SqlCommand command = new SqlCommand("SELECT * FROM " +
				"ChildDataTable AS " +
				"(Case, ChildFirst, ChildLast, ChildDob, " +
				"Interview, " +
				"Guard1First, Guard1Last, Guard2First, Guard2Last, " +
				"Perps, MappedLoc", 
				connection))
			{
				connection.Open();

				using (SqlDataReader reader = command.ExecuteReader())
				{
					if (reader.HasRows)
					{
						while (reader.Read())
						{
							Case c = new Case();

							c.CaseNum = reader.GetString(reader.GetOrdinal("Case"));

							index = reader.GetOrdinal("ChildFirst");
							if (ChildFirst != null && !reader.IsDBNull(index))
								 c.ChildFirst = reader.GetString(reader.GetOrdinal("ChildFirst"));

							index = reader.GetOrdinal("ChildLast");
							if (ChildLast != null && !reader.IsDBNull(index))
								c.ChildLast = reader.GetString(reader.GetOrdinal("ChildLast"));

							index = reader.GetOrdinal("ChildDob");
							if (ChildDob.ToString() != "1/1/0001 12:00:00 AM" && !reader.IsDBNull(index))
								c.ChildDob = reader.GetDateTime(reader.GetOrdinal("ChildDob"));

							index = reader.GetOrdinal("Interview");
							if (InterviewDate.ToString() != "1/1/0001 12:00:00 AM" && !reader.IsDBNull(index))
								c.InterviewDate = reader.GetDateTime(reader.GetOrdinal("Interview"));

							index = reader.GetOrdinal("Guard1First");
							if (Guardian1First != null && !reader.IsDBNull(index))
								c.Guardian1First = reader.GetString(reader.GetOrdinal("Guard1First"));

							index = reader.GetOrdinal("Guard1First");
							if (Guardian1Last != null && !reader.IsDBNull(index))
								c.Guardian1Last = reader.GetString(reader.GetOrdinal("Guard1Last"));

							index = reader.GetOrdinal("Guard2First");
							if (Guardian2First != null && !reader.IsDBNull(index))
								c.Guardian2First = reader.GetString(reader.GetOrdinal("Guard2First"));

							index = reader.GetOrdinal("Guard2Last");
							if (Guardian2Last != null && !reader.IsDBNull(index))
								c.Guardian2Last = reader.GetString(reader.GetOrdinal("Guard2Last"));

						}
					}
				}
			}
		}
				return cases;
    }


	private void Execute()
    {

    }


}
