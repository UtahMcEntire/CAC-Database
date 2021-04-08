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
        //Console.WriteLine("casenum " + CaseNum);
        //Console.WriteLine("childfirst " + ChildFirst);
        //Console.WriteLine("childlast " + ChildLast);
        //Console.WriteLine("ChildDob " + ChildDob);
        //Console.WriteLine("interview " + InterviewDate);
        //Console.WriteLine("guardian1first " + Guardian1First);
        //Console.WriteLine("guardian1last " + Guardian1Last);
        //Console.WriteLine("guardian2first " + Guardian2First);
        //Console.WriteLine("guardian2last " + Guardian2Last);
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
                command.Parameters.AddWithValue("guard1First", Guardian1First == "" ? (object)DBNull.Value : Guardian1First);
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


    public int getSuppliedSearchAmount(Case queryCase)
    {
        int count = 0;

        count += queryCase.CaseNum != "" ? 1 : 0;
        count += queryCase.ChildFirst != "" ? 1 : 0;
        count += queryCase.ChildLast != "" ? 1 : 0;
        count += queryCase.ChildDob != "" ? 1 : 0;
        count += queryCase.InterviewDate != "" ? 1 : 0;
        count += queryCase.Guardian1First != "" ? 1 : 0;
        count += queryCase.Guardian1Last != "" ? 1 : 0;
        count += queryCase.Guardian2First != "" ? 1 : 0;
        count += queryCase.Guardian2Last != "" ? 1 : 0;
        count += queryCase.PerpFirstName != "" ? 1 : 0;
        count += queryCase.PerpLastName != "" ? 1 : 0;
        count += queryCase.PerpNick != "" ? 1 : 0;
        count += queryCase.SiblingFirstName != "" ? 1 : 0;
        count += queryCase.SiblingLastName != "" ? 1 : 0;
        count += queryCase.OtherVictimFirstName != "" ? 1 : 0;
        count += queryCase.OtherVictimLastName != "" ? 1 : 0;

        return count;
    }

    // Function to query entire DB
    public List<Case> Query(Case queryCase)
    {
        //string CaseNum, string ChildFirst, string ChildLast, string ChildDob, string InterviewDate, string Guardian1First, string Guardian1Last, string Guardian2First, string Guardian2Last, List<Perp> Perps, List<Sibling> Siblings, List<Victim> Victims

        List<Case> cases = GetAllDB();
        List<Case> foundCases = new List<Case>();
        int suppliedSearchAmount = getSuppliedSearchAmount(queryCase);
        int foundSearchAmounts;

        foreach (Case cs in cases)
        {
            foundSearchAmounts = 0;
            
            // Tests if the current CaseNum contains the query CaseNum
            if (queryCase.CaseNum != "" && cs.CaseNum.Contains(queryCase.CaseNum))
                foundSearchAmounts++;

            // Tests if the current CaseNum contains the query CaseNum
            if (queryCase.ChildFirst != "" && cs.ChildFirst.Contains(queryCase.ChildFirst))
                foundSearchAmounts++;

            // Tests if the current CaseNum contains the query CaseNum
            if (queryCase.ChildLast != "" && cs.ChildLast.Contains(queryCase.ChildLast))
                foundSearchAmounts++;

            // Tests if the current CaseNum contains the query CaseNum
            if (queryCase.ChildDob != "" && cs.ChildDob.Contains(queryCase.ChildDob))
                foundSearchAmounts++;

            // Tests if the current CaseNum contains the query CaseNum
            if (queryCase.InterviewDate != "" && cs.InterviewDate.Contains(queryCase.InterviewDate))
                foundSearchAmounts++;

            // Tests if the current Guardian1First contains the query Guardian1First
            if (queryCase.Guardian1First != "" && (cs.Guardian1First.Contains(queryCase.Guardian1First) || cs.Guardian1First.Contains(queryCase.Guardian2First)))
                foundSearchAmounts++;

            // Tests if the current Guardian1Last contains the query Guardian1Last
            if (queryCase.Guardian1Last != "" && (cs.Guardian1Last.Contains(queryCase.Guardian1Last) || cs.Guardian1Last.Contains(queryCase.Guardian2Last)))
                foundSearchAmounts++;

            // Tests if the current Guardian1First contains the query Guardian1First
            if (queryCase.Guardian2First != "" && (cs.Guardian2First.Contains(queryCase.Guardian2First) || cs.Guardian2First.Contains(queryCase.Guardian1First)))
                foundSearchAmounts++;

            // Tests if the current Guardian1Last contains the query Guardian1Last
            if (queryCase.Guardian2Last != "" && (cs.Guardian2Last.Contains(queryCase.Guardian2Last) || cs.Guardian2Last.Contains(queryCase.Guardian1Last)))
                foundSearchAmounts++;


            // Test the Perp/Sibling/Victim lists
            // Perps
            foreach(Perp p in cs.PerpList)
            {
                if (queryCase.PerpFirstName != "" && p.FirstName.Contains(queryCase.PerpFirstName))
                    foundSearchAmounts++;
                if (queryCase.PerpLastName != "" && p.LastName.Contains(queryCase.PerpLastName))
                    foundSearchAmounts++;
                if (queryCase.PerpNick != "" && p.Nick.Contains(queryCase.PerpNick))
                    foundSearchAmounts++;
            }

            // Sibling
            foreach (Sibling s in cs.SiblingList)
            {
                
                if (queryCase.SiblingFirstName != "" && s.FirstName.Contains(queryCase.SiblingFirstName))
                    foundSearchAmounts++;
                else if (queryCase.SiblingLastName != "" && s.LastName.Contains(queryCase.SiblingLastName))
                    foundSearchAmounts++;
            }

            // Victim
            foreach (Victim v in cs.VictimList)
            {
                if (queryCase.OtherVictimFirstName != "" && v.FirstName.Contains(queryCase.OtherVictimFirstName))
                    foundSearchAmounts++;
                else if (queryCase.OtherVictimLastName != "" && v.LastName.Contains(queryCase.OtherVictimLastName))
                    foundSearchAmounts++;
            }

            if (foundSearchAmounts == suppliedSearchAmount)
                foundCases.Add(cs);
        }
        return foundCases;
    }

    // Make private
    private List<Case> GetAllDB()
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
                        //Console.WriteLine("has rows");

                        // Reads the DB until there's nothing left to read
                        while (reader.Read())
                        {
                            //Console.WriteLine("reading");
                            
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
                                c.ChildLast = reader.GetString(reader.GetOrdinal("ChildLast"));
                            
                            index = reader.GetOrdinal("ChildDob");
                            if (!reader.IsDBNull(index))
                                c.ChildDob = reader.GetString(reader.GetOrdinal("ChildDob"));
                            
                            index = reader.GetOrdinal("InterviewDate");
                            if (!reader.IsDBNull(index))
                                c.InterviewDate = reader.GetString(reader.GetOrdinal("InterviewDate"));
                            
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
                                        switch (name)
                                        {
                                            case "first":
                                                xmlReader.Read();
                                                p.FirstName = xmlReader.Value.ToString();
                                                break;
                                            case "last":
                                                xmlReader.Read();
                                                p.LastName = xmlReader.Value.ToString();
                                                break;
                                            case "nick":
                                                xmlReader.Read();
                                                p.Nick = xmlReader.Value.ToString();
                                                break;
                                        }
                                    }

                                    if (xmlReader.NodeType == XmlNodeType.EndElement)
                                    {
                                        if (xmlReader.Name == "perp")
                                        {
                                            perps.Add(p);
                                            p = new Perp();
                                        }
                                        if (xmlReader.Name == "perps")
                                        {
                                            c.PerpList = perps;

                                            // Debug Print
                                            //foreach (Perp test in perps)
                                            //    Console.WriteLine(test.ToString());
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
                                        switch (name)
                                        {
                                            case "first":
                                                xmlReader.Read();
                                                s.FirstName = xmlReader.Value.ToString();
                                                break;
                                            case "last":
                                                xmlReader.Read();
                                                s.LastName = xmlReader.Value.ToString();
                                                break;
                                        }
                                    }

								    if (xmlReader.NodeType == XmlNodeType.EndElement)
								    {
									    if (xmlReader.Name == "sibling")
									    {
    										siblings.Add(s);
										    s = new Sibling();
									    }
									    if (xmlReader.Name == "siblings")
									    {
										    c.SiblingList = siblings;
                                            
                                            // Debug Print
                                            //foreach (Sibling test in siblings)
                                            //    Console.WriteLine(test.ToString());
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
                                        switch (name)
                                        {
                                            case "first":
                                                xmlReader.Read();
                                                v.FirstName = xmlReader.Value.ToString();
                                                break;
                                            case "last":
                                                xmlReader.Read();
                                                v.LastName = xmlReader.Value.ToString();
                                                break;
                                        }
                                    }

                                    if (xmlReader.NodeType == XmlNodeType.EndElement)
                                    {
                                        if (xmlReader.Name == "victim")
                                        {
                                            victims.Add(v);
                                            v = new Victim();
                                        }
                                        if (xmlReader.Name == "victims")
                                        {
                                            c.VictimList = victims;

                                            // Debug Print
                                            //foreach (Victim test in victims)
                                            //    Console.WriteLine(test.ToString());
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


    // Gets an NCA number for a year when NCA numbers are not present
    public string GetNextUnknown(string year)
    {
        int index; // Index of current ordinal read by reader

        // Sets up DB connection
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Executes a generic search
            using (SqlCommand command = new SqlCommand("SELECT ChildDataTable.* FROM ChildDataTable", connection))
            {

                // Connect to DB and verify connection
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                    Console.WriteLine("connected");

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    string temp;
                    List<int> indices = new List<int>();
                    List<string> existingItems = new List<string>();
                    string finalOutput;

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            index = reader.GetOrdinal("CaseNum");
                            if (!reader.IsDBNull(index))
                                temp = reader.GetString(reader.GetOrdinal("CaseNum"));
                            else
                                continue;

                            if (temp.StartsWith(year) && temp.Contains("0000"))
                            {
                                existingItems.Add(temp);
                                indices.Add(Int32.Parse(temp.Substring(9)));
                                Console.WriteLine("index:" + Int32.Parse(temp.Substring(9)));
                            }
                                
                        }
                    }

                    indices.Sort();
                    finalOutput = year + "-0000" + (existingItems.Count + 1).ToString();
                    if (existingItems.Contains(finalOutput) && indices.Count != 0)
                    {
                        int nextExpected = 1;
                        foreach (int i in indices)
                        {
                            if (i == nextExpected)
                                nextExpected++;
                            else
                            {
                                finalOutput = year + "-0000" + nextExpected.ToString();
                                break;
                            }
                                
                        }
                    }

                    Console.WriteLine(finalOutput);
                    return finalOutput;
                }
            }
        }
    }


    public void Delete(string CaseNum)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string deleteCommand = "DELETE FROM ChildDataTable WHERE CaseNum='" + CaseNum + "'";

            // Executes a generic search
            using (SqlCommand command = new SqlCommand(deleteCommand, connection))
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }


    public bool Exists(string CaseNum)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string existCommand = "SELECT * FROM ChildDataTable WHERE CaseNum='" + CaseNum + "'";

            // Executes a generic search
            using (SqlCommand command = new SqlCommand(existCommand, connection))
            {
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();

                        if (!reader.IsDBNull(reader.GetOrdinal("CaseNum")))
                            return reader.GetString(reader.GetOrdinal("CaseNum")) == CaseNum;

                    }
                    return false;
                }
            }
        }
    }


    private void Execute()
    {

    }
}