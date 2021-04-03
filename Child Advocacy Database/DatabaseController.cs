using Child_Advocacy_Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Xml;

public class DatabaseController
{
    private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\ChildDatabase.mdf;Integrated Security=True;Connect Timeout=30";



    public DatabaseController()
    {
    }

    public int Insert(string CaseNum, string ChildFirst, string ChildLast, string ChildDob, string InterviewDate, string Guardian1First, string Guardian1Last, string Guardian2First, string Guardian2Last, List<Perp> PerpList, List<Sibling> SiblingList, List<Victim> VictimList, string Location)


    {
        Console.WriteLine("casenum " + CaseNum);
        Console.WriteLine("childfirst " + ChildFirst);
        Console.WriteLine("childlast " + ChildLast);
        Console.WriteLine("ChildDob " + ChildDob);
        Console.WriteLine("interview " + InterviewDate);
        Console.WriteLine("guardian1first " + Guardian1First);
        Console.WriteLine("guardian1last " + Guardian1Last);
        Console.WriteLine("guardian2first " + Guardian2First);
        Console.WriteLine("guardian2last " + Guardian2Last);
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


    // Function to query entire DB
    // TODO: Implement query of the XML fields
    public List<Case> Query(Case queryCase)
    {
        //string CaseNum, string ChildFirst, string ChildLast, string ChildDob, string InterviewDate, string Guardian1First, string Guardian1Last, string Guardian2First, string Guardian2Last, List<Perp> Perps, List<Sibling> Siblings, List<Victim> Victims

        List<Case> cases;
        List<Case> foundCases = new List<Case>();
        bool done; // Used with the Perp/Sibling/Victim list parsing

        cases = GetAllDB();

        foreach (Case cs in cases)
        {
            // Tests if the current CaseNum contains the query CaseNum
            if (queryCase.CaseNum != "" && cs.CaseNum.Contains(queryCase.CaseNum))
            {
                foundCases.Add(cs);
                continue;
            }

            // Tests if the current CaseNum contains the query CaseNum
            if (queryCase.ChildFirst != "" && cs.ChildFirst.Contains(queryCase.ChildFirst))
            {
                foundCases.Add(cs);
                continue;
            }
            // Tests if the current CaseNum contains the query CaseNum
            if (queryCase.ChildLast != "" && cs.ChildLast.Contains(queryCase.ChildLast))
            {
                foundCases.Add(cs);
                continue;
            }

            // Tests if the current CaseNum contains the query CaseNum
            if (queryCase.ChildDob != "" && cs.ChildDob.Contains(queryCase.ChildDob))
            {
                foundCases.Add(cs);
                continue;
            }

            // Tests if the current CaseNum contains the query CaseNum
            if (queryCase.InterviewDate != "" && cs.InterviewDate.Contains(queryCase.InterviewDate))
            {
                foundCases.Add(cs);
                continue;
            }

            // Tests if the current Guardian1First contains the query Guardian1First
            if (queryCase.Guardian1First != "" && (cs.Guardian1First.Contains(queryCase.Guardian1First) || cs.Guardian1First.Contains(queryCase.Guardian2First)))
            {
                foundCases.Add(cs);
                continue;
            }

            // Tests if the current Guardian1Last contains the query Guardian1Last
            if (queryCase.Guardian1Last != "" && (cs.Guardian1Last.Contains(queryCase.Guardian1Last) || cs.Guardian1Last.Contains(queryCase.Guardian2Last)))
            {
                foundCases.Add(cs);
                continue;
            }

            // Tests if the current Guardian1First contains the query Guardian1First
            if (queryCase.Guardian2First != "" && (cs.Guardian2First.Contains(queryCase.Guardian2First) || cs.Guardian2First.Contains(queryCase.Guardian1First)))
            {
                foundCases.Add(cs);
                continue;
            }

            // Tests if the current Guardian1Last contains the query Guardian1Last
            if (queryCase.Guardian2Last != "" && (cs.Guardian2Last.Contains(queryCase.Guardian2Last) || cs.Guardian2Last.Contains(queryCase.Guardian1Last)))
            {
                foundCases.Add(cs);
                continue;
            }


            // Test the Perp/Sibling/Victim lists
            // Perps
            done = false;
            foreach(Perp p in cs.PerpList)
            {
                if (done)
                    break;
                if (queryCase.PerpFirstName != "" && p.FirstName.Contains(queryCase.PerpFirstName))
                {
                    foundCases.Add(cs);
                    done = true;
                }
                else if (queryCase.PerpLastName != "" && p.LastName.Contains(queryCase.PerpLastName))
                {
                    foundCases.Add(cs);
                    done = true;
                }
                else if (queryCase.PerpNick != "" && p.Nick.Contains(queryCase.PerpNick))
                {
                    foundCases.Add(cs);
                    done = true;
                }
            }

            // Sibling
            done = false;
            foreach (Sibling s in cs.SiblingList)
            {
                if (done)
                    break;
                if (queryCase.SiblingFirstName != "" && s.FirstName.Contains(queryCase.SiblingFirstName))
                {
                    foundCases.Add(cs);
                    done = true;
                }
                else if (queryCase.SiblingLastName != "" && s.LastName.Contains(queryCase.SiblingLastName))
                {
                    foundCases.Add(cs);
                    done = true;
                }
            }

            // Victim
            done = false;
            foreach (Victim v in cs.VictimList)
            {
                if (done)
                    break;
                if (queryCase.OtherVictimFirstName != "" && v.FirstName.Contains(queryCase.OtherVictimFirstName))
                {
                    foundCases.Add(cs);
                    done = true;
                }
                else if (queryCase.OtherVictimLastName != "" && v.LastName.Contains(queryCase.OtherVictimLastName))
                {
                    foundCases.Add(cs);
                    done = true;
                }
            }


        }

        return foundCases;
    }

    // Make private
    public List<Case> GetAllDB()
    {
        List<Case> cases = new List<Case>();
        int index; // Index of current ordinal read by reader

        // Establishes the DB connection
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Creates the command to query the DB
            using (SqlCommand command = new SqlCommand("SELECT ChildDataTable.* FROM ChildDataTable", connection))
            {
                // Connects to the DB
                connection.Open();

                // Verifies that the connection has been opened
                // May need to make this encapsulate the lower code
                if (connection.State == System.Data.ConnectionState.Open)
                    Console.WriteLine("connected");

                // Creates the SQL reader to actually read the DB
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Makes sure the reader has something to read
                    if (reader.HasRows)
                    {
                        Console.WriteLine("has rows");

                        // Reads the DB until there's nothing left to read
                        while (reader.Read())
                        {
                            Console.WriteLine("reading");
                            
                            // Creates a dummy case to store all data into
                            Case c = new Case();
                            
                            // Parsing the XML and converting it to List<>
                            XmlReader xmlReader;
                            string name;


                            index = reader.GetOrdinal("CaseNum");
                            if (!reader.IsDBNull(index))
                                c.CaseNum = reader.GetString(reader.GetOrdinal("CaseNum"));
                            else
                                continue;

                            index = reader.GetOrdinal("ChildFirst");
                            if (!reader.IsDBNull(index)) 
                                c.ChildFirst = reader.GetString(reader.GetOrdinal("ChildFirst"));
                            
                            index = reader.GetOrdinal("ChildLast");
                            if (!reader.IsDBNull(index))
                                c.ChildLast = reader.GetString(reader.GetOrdinal("ChildFirst"));
                            
                            index = reader.GetOrdinal("ChildDob");
                            if (!reader.IsDBNull(index))
                                c.ChildDob = reader.GetString(reader.GetOrdinal("ChildFirst"));
                            
                            index = reader.GetOrdinal("InterviewDate");
                            if (!reader.IsDBNull(index))
                                c.InterviewDate = reader.GetString(reader.GetOrdinal("ChildFirst"));
                            
                            index = reader.GetOrdinal("Guardian1First");
                            if (!reader.IsDBNull(index))
                                c.Guardian1First = reader.GetString(reader.GetOrdinal("Guardian1First"));
                            
                            index = reader.GetOrdinal("Guardian1First");
                            if (!reader.IsDBNull(index))
                                c.Guardian1Last = reader.GetString(reader.GetOrdinal("Guardian1Last"));
                            
                            index = reader.GetOrdinal("Guardian2First");
                            if (!reader.IsDBNull(index))
                                c.Guardian2First = reader.GetString(reader.GetOrdinal("Guardian2First"));
                            
                            index = reader.GetOrdinal("Guardian2Last");
                            if (!reader.IsDBNull(index))
                                c.Guardian2Last = reader.GetString(reader.GetOrdinal("Guardian2Last"));


                            // Parse XML entries
                            // Perps
                            index = reader.GetOrdinal("Perps");
                            if (!reader.IsDBNull(index))
                            {
                                // Creates a dummy Perps list
                                List<Perp> perps = new List<Perp>();
                                Perp p = new Perp();

                                // Gets the XML from the DB
                                xmlReader = reader.GetXmlReader(index);

                                // Parses the XML to extract needed data
                                while (xmlReader.Read())
                                {
                                    if (xmlReader.NodeType == XmlNodeType.Element)
                                    {
                                        name = xmlReader.Name;
                                        xmlReader.Read(); // Moves the reader forward
                                        switch (name)
                                        {
                                            case "first":
                                                p.FirstName = xmlReader.Value.ToString();
                                                break;
                                            case "last":
                                                p.LastName = xmlReader.Value.ToString();
                                                break;
                                            case "nick":
                                                p.Nick = xmlReader.Value.ToString();
                                                break;
                                        }
                                    }
                                    if (xmlReader.NodeType == XmlNodeType.EndElement)
                                    {
                                        if (xmlReader.Name == "perp")
                                        {
                                            perps.Add(p);
                                            Console.WriteLine("Perp found: " + p.ToString());
                                            p = new Perp();
                                        }
                                        if (xmlReader.Name == "perps")
                                        {
                                            c.PerpList = perps;
                                        }
                                    }
                                }
                            }


						    // Siblings
						    index = reader.GetOrdinal("Siblings");
						    if (!reader.IsDBNull(index))
						{
							// Creates a dummy Siblings list
							List<Sibling> siblings = new List<Sibling>();
							Sibling s = new Sibling();

							// Gets the XML from the DB
							xmlReader = reader.GetXmlReader(index);

							// Parses the XML to extract needed data
							while (xmlReader.Read())
							{
								if (xmlReader.NodeType == XmlNodeType.Element)
								{
									name = xmlReader.Name;
									xmlReader.Read(); // Moves the reader forward
									switch (name)
									{
										case "first":
											s.FirstName = xmlReader.Value.ToString();
											break;
										case "last":
											s.LastName = xmlReader.Value.ToString();
											break;
									}
								}
								if (xmlReader.NodeType == XmlNodeType.EndElement)
								{
									if (xmlReader.Name == "sibling")
									{
										siblings.Add(s);
										Console.WriteLine("Sibling found: " + s.ToString());
										s = new Sibling();
									}
									if (xmlReader.Name == "siblings")
									{
										c.SiblingList = siblings;
									}
								}
							}
						}


                            // Victims
                            index = reader.GetOrdinal("Victims");
                            if (!reader.IsDBNull(index))
                            {
                                // Creates a dummy Victims list
                                List<Victim> victims = new List<Victim>();
                                Victim v = new Victim();

                                // Gets the XML from the DB
                                xmlReader = reader.GetXmlReader(index);

                                // Parses the XML to extract needed data
                                while (xmlReader.Read())
                                {
                                    if (xmlReader.NodeType == XmlNodeType.Element)
                                    {
                                        name = xmlReader.Name;
                                        xmlReader.Read(); // Moves the reader forward
                                        switch (name)
                                        {
                                            case "first":
                                                v.FirstName = xmlReader.Value.ToString();
                                                break;
                                            case "last":
                                                v.LastName = xmlReader.Value.ToString();
                                                break;
                                        }
                                    }
                                    if (xmlReader.NodeType == XmlNodeType.EndElement)
                                    {
                                        if (xmlReader.Name == "victim")
                                        {
                                            victims.Add(v);
                                            Console.WriteLine("Victim found: " + v.ToString());
                                            v = new Victim();
                                        }
                                        if (xmlReader.Name == "victims")
                                        {
                                            c.VictimList = victims;
                                        }
                                    }
                                }
                            }

                            cases.Add(c);
                        }
                    }
                }
            }
        }

        // Debug
        foreach (Case cs in cases)
        {
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