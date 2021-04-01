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

    public int Insert(string CaseNum, string ChildFirst, string ChildLast, string ChildDob, string InterviewDate, string Guardian1First, string Guardian1Last, string Guardian2First, string Guardian2Last, List<Perp> PerpList, List<Sibling> SiblingList, List<Victim> VictimList, string Location)


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
                "Perps, Siblings, Victims, Location) " +
                "VALUES " +
                "(@case, @childFirst, @childLast, @childDob, " +
                "@interview, " +
                "@guard1First, @guard1Last, @guard2First, @guard2Last, " +
                "@perps, @siblings, @victims, @mappedLoc)",
                connection))
            {
                command.CommandType = System.Data.CommandType.Text;

                if (ChildFirst == "")
                    Console.WriteLine("ChildFirst Null");
                else
                    Console.WriteLine(ChildFirst);
                command.Parameters.AddWithValue("case", CaseNum); // Required Parameter
                command.Parameters.AddWithValue("childFirst", ChildFirst == "" ? (object)DBNull.Value : ChildFirst);
                command.Parameters.AddWithValue("childLast", ChildLast == "" ? (object)DBNull.Value : ChildLast);
                command.Parameters.AddWithValue("childDob", ChildDob == "" ? (object)DBNull.Value : ChildDob);
                command.Parameters.AddWithValue("interview", InterviewDate == "" ? (object)DBNull.Value : InterviewDate);
                command.Parameters.AddWithValue("guard1First", Guardian1First == "" ? (object)DBNull.Value : Guardian2First);
                command.Parameters.AddWithValue("guard1Last", Guardian1Last == "" ? (object)DBNull.Value : Guardian1Last);
                command.Parameters.AddWithValue("guard2First", Guardian2First == "" ? (object)DBNull.Value : Guardian2First);
                command.Parameters.AddWithValue("guard2Last", Guardian2Last == "" ? (object)DBNull.Value : Guardian2Last);

                // Add Perps to DB
                Console.WriteLine(PerpList.Count);
                if (PerpList.Count != 0)
                {
                    string toXML = "";
                    foreach (Perp p in PerpList)
                    {
                        toXML += p.ToXmlString();
                    }
                    //XmlDocument doc = new XmlDocument();
                    //doc.LoadXml("<perps>" + toXML + "</perps>");

                    command.Parameters.AddWithValue("perps", "<perps>" + toXML + "</perps>");
                }
                else
                    command.Parameters.AddWithValue("perps", DBNull.Value);


                // Add Siblings to DB
                if (SiblingList.Count != 0)
                {
                    string toXML = "";
                    foreach (Sibling s in SiblingList)
                    {
                        toXML += s.ToXmlString();
                    }
                    //XmlDocument doc = new XmlDocument();
                    //doc.LoadXml("<siblings>" + toXML + "</siblings>");

                    command.Parameters.AddWithValue("siblings", "<siblings>" + toXML + "</siblings>");
                }
                else
                    command.Parameters.AddWithValue("siblings", DBNull.Value);


                // Add Victims to DB
                if (VictimList.Count != 0)
                {
                    string toXML = "";
                    foreach (Victim v in VictimList)
                    {
                        toXML += v.ToXmlString();
                    }
                    //XmlDocument doc = new XmlDocument();
                    //doc.LoadXml("<victims>" + toXML + "</victims>");

                    command.Parameters.AddWithValue("victims", "<victims>" + toXML + "</victims>");
                }
                else
                    command.Parameters.AddWithValue("victims", DBNull.Value);




                command.Parameters.AddWithValue("mappedLoc", Location == null ? (object)DBNull.Value : Location);
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

    public List<Case> Query(string CaseNum, string ChildFirst, string ChildLast, string ChildDob, string InterviewDate, string Guardian1First, string Guardian1Last, string Guardian2First, string Guardian2Last, List<Perp> Perps, List<Sibling> Siblings, List<Victim> Victims)
    {
        List<Case> cases = new List<Case>();
        int index; // Index of current ordinal read by reader

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand("SELECT ChildDataTable.* FROM ChildDataTable", connection))
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                    Console.WriteLine("connected");

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        Console.WriteLine("has rows");
                        while (reader.Read())
                        {
                            Console.WriteLine("reading");
                            Case c = new Case();

                            index = reader.GetOrdinal("CaseNum");
                            if (CaseNum != null && !reader.IsDBNull(index))
                            {
                                string readInfo = reader.GetString(reader.GetOrdinal("CaseNum"));
                                if (CaseNum == readInfo)
                                    c.CaseNum = readInfo;
                            }
                            else
                                continue;

                            index = reader.GetOrdinal("ChildFirst");
                            if (ChildFirst != null && !reader.IsDBNull(index)) 
                            {
                                string readInfo = reader.GetString(reader.GetOrdinal("ChildFirst"));
                                if (ChildFirst == readInfo)
                                    c.ChildFirst = readInfo;
                            }

                            index = reader.GetOrdinal("ChildLast");
                            if (ChildLast != null && !reader.IsDBNull(index))
                            {
                                string readInfo = reader.GetString(reader.GetOrdinal("ChildLast"));
                                if (ChildLast == readInfo)
                                    c.ChildLast = readInfo;
                            }

                            index = reader.GetOrdinal("ChildDob");
                            if (ChildDob != null && !reader.IsDBNull(index))
                            {
                                string readInfo = reader.GetString(reader.GetOrdinal("ChildDob"));
                                if (ChildDob == readInfo)
                                    c.ChildDob = readInfo;
                            }

                            index = reader.GetOrdinal("InterviewDate");
                            if (InterviewDate != null && !reader.IsDBNull(index))
                            {
                                string readInfo = reader.GetString(reader.GetOrdinal("InterviewDate"));
                                if (InterviewDate == readInfo)
                                    c.InterviewDate = readInfo;
                            }

                            index = reader.GetOrdinal("Guardian1First");
                            if (Guardian1First != null && !reader.IsDBNull(index))
                            {
                                string readInfo = reader.GetString(reader.GetOrdinal("Guardian1First"));
                                if (Guardian1First == readInfo)
                                    c.Guardian1First = readInfo;
                            }

                            index = reader.GetOrdinal("Guardian1First");
                            if (Guardian1Last != null && !reader.IsDBNull(index))
                            {
                                string readInfo = reader.GetString(reader.GetOrdinal("Guardian1Last"));
                                if (Guardian1Last == readInfo)
                                    c.Guardian1Last = readInfo;
                            }

                            index = reader.GetOrdinal("Guardian2First");
                            if (Guardian2First != null && !reader.IsDBNull(index))
                            {
                                string readInfo = reader.GetString(reader.GetOrdinal("Guardian2First"));
                                if (Guardian2First == readInfo)
                                    c.Guardian2First = readInfo;
                            }

                            index = reader.GetOrdinal("Guardian2Last");
                            if (Guardian2Last != null && !reader.IsDBNull(index))
                            {
                                string readInfo = reader.GetString(reader.GetOrdinal("Guardian2Last"));
                                if (Guardian2Last == readInfo)
                                    c.Guardian2Last = readInfo;
                            }

                            Console.WriteLine("Made it to add");
                            cases.Add(c);
                        }
                    }
                }
            }
        }
        
        // Debug
        foreach (Case cs in cases) {
            cs.ToString();
        }

        return cases;
    }


    public string GetNextUnknown(string year)
    {
        List<Case> cases = new List<Case>();
        int index; // Index of current ordinal read by reader

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand("SELECT ChildDataTable.* FROM ChildDataTable", connection))
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                    Console.WriteLine("connected");

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    int count = 1;


                    return year + "-9999" + count.ToString();
                }
            }
        }
    }


    private void Execute()
    {

    }


}