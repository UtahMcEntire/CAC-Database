using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Child_Advocacy_Database
{
    class XmlDb
    {
        public void Insert(Case addCase)
        {
            //StringWriter xmlString = new StringWriter();
            //XmlSerializer serializer = new XmlSerializer(typeof(Case));
            //serializer.Serialize(xmlString, addCase);

            //var path = Directory.GetCurrentDirectory() + "DB.xml";

            ////System.IO.FileStream file = System.IO.File.Create(path);
            ////serializer.Serialize(file, addCase);

            //Console.WriteLine(xmlString.ToString());
            //XmlDocument toAdd = new XmlDocument();
            //toAdd.LoadXml(xmlString.ToString());

            //XmlDocument db = new XmlDocument();
            //db.Load(path);
            //db.AppendChild(toAdd);
            //var el = db.CreateElement("Case");
            ////el.InnerXml = toAdd.DocumentType.InnerXml;
            ////db.DocumentElement.AppendChild(el);

            //db.Save(path);

            // Sets the filename and path for the database
            var filename = "DB.xml";
            var currentDir = Directory.GetCurrentDirectory();
            var path = Path.Combine(currentDir, filename);


            // Connects to the DB and adds the item to it
            XElement db;
            if (!File.Exists(path))
                db = new XElement("Cases", ToXElement(addCase));
            else
            {
                db = XElement.Load(path);
                db.Add(ToXElement(addCase));
            }

            db.Save(path);
            
                
            //// Not needed
            IEnumerable<XElement> cases = db.Elements();
            //foreach (var element in cases)
            //{
            //    //Console.WriteLine(element.Element("PerpList"));
            //    if (element.Element("CaseNum").Value == "2020-1")
            //    {
            //        //Console.WriteLine(element);
            //        element.DescendantsAndSelf().Remove();
            //    }

            //}

            //// Not Needed
            int count = 0;
            foreach (var element in cases)
            {
                Console.WriteLine(element);
                Console.WriteLine(count++);
            }


            //for (int i = 0; i < 45000; i++)
            //    db.Add(ToXElement(addCase));

            
            //foreach (var element in cases)
            //{
            //    Console.WriteLine(element);
            //}

            //db.Save(path);
        }

        private XElement ToXElement(Case toCase)
        {
            // Allows for the production of the XML string
            StringWriter xmlString = new StringWriter();
            XmlSerializer serializer = new XmlSerializer(typeof(Case));

            // Removes some junk from the serialization
            var xmlSerializerNamespaces = new XmlSerializerNamespaces();
            xmlSerializerNamespaces.Add(string.Empty, string.Empty);
            
            // Creates the actual XML string
            serializer.Serialize(xmlString, toCase, xmlSerializerNamespaces);

            return XElement.Parse(xmlString.ToString());
        }


        public void Delete(string CaseNum)
        {
            // Sets the filename and path for the database
            var filename = "DB.xml";
            var currentDir = Directory.GetCurrentDirectory();
            var path = Path.Combine(currentDir, filename);

            // Loads the db
            if (!File.Exists(path))
                return;
            XElement db = XElement.Load(path);
            IEnumerable<XElement> cases = db.Elements();
            
            // Iterates throught the DB
            foreach (var element in cases)
                //Deletes the unwanted case
                if (element.Element("CaseNum").Value == CaseNum)
                {
                    //Console.WriteLine(element);
                    element.DescendantsAndSelf().Remove();
                    break;
                }

            // Debug Print
            //foreach (var element in cases)
            //    Console.WriteLine(element);

            db.Save(path);
        }

        private int getSuppliedSearchAmount(Case queryCase)
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

        public List<Case> Query(Case queryCase)
        {
            // Sets the filename and path for the database
            var filename = "DB.xml";
            var currentDir = Directory.GetCurrentDirectory();
            var path = Path.Combine(currentDir, filename);

            // Sets up the returned list
            List<Case> foundCases = new List<Case>();
            int suppliedSearchAmount = getSuppliedSearchAmount(queryCase);
            int foundSearchAmounts = 0;

            // Loads the db
            if (!File.Exists(path))
                return foundCases;

            XElement db = XElement.Load(path);
            IEnumerable<XElement> cases = db.Elements();

            foreach (var element in cases)
            {
                foundSearchAmounts = 0;

                // Tests if the current CaseNum contains the query CaseNum
                if (queryCase.CaseNum != "" && element.Element("CaseNum").Value.ToLower().Contains(queryCase.CaseNum.ToLower()))
                    foundSearchAmounts++;

                // Tests if the current CaseNum contains the query CaseNum
                if (queryCase.ChildFirst != "" && element.Element("ChildFirst").Value.ToLower().Contains(queryCase.ChildFirst.ToLower()))
                    foundSearchAmounts++;

                // Tests if the current CaseNum contains the query CaseNum
                if (queryCase.ChildLast != "" && element.Element("ChildLast").Value.ToLower().Contains(queryCase.ChildLast.ToLower()))
                    foundSearchAmounts++;

                // Tests if the current CaseNum contains the query CaseNum
                if (queryCase.ChildDob != "" && element.Element("ChildDob").Value.ToLower().Contains(queryCase.ChildDob.ToLower()))
                    foundSearchAmounts++;

                // Tests if the current CaseNum contains the query CaseNum
                if (queryCase.InterviewDate != "" && element.Element("InterviewDate").Value.ToLower().Contains(queryCase.InterviewDate.ToLower()))
                    foundSearchAmounts++;

                // Tests if the current Guardian1First contains the query Guardian1First
                if (queryCase.Guardian1First != "" && (element.Element("Guardian1First").Value.ToLower().Contains(queryCase.Guardian1First.ToLower()) || element.Element("Guardian2First").Value.ToLower().Contains(queryCase.Guardian2First.ToLower())))
                    foundSearchAmounts++;

                // Tests if the current Guardian1Last contains the query Guardian1Last
                if (queryCase.Guardian1Last != "" && (element.Element("Guardian1Last").Value.ToLower().Contains(queryCase.Guardian1Last.ToLower()) || element.Element("Guardian2Last").Value.ToLower().Contains(queryCase.Guardian2Last.ToLower())))
                    foundSearchAmounts++;

                // Tests if the current Guardian1First contains the query Guardian1First
                if (queryCase.Guardian2First != "" && (element.Element("Guardian2First").Value.ToLower().Contains(queryCase.Guardian2First.ToLower()) || element.Element("Guardian2First").Value.ToLower().Contains(queryCase.Guardian1First.ToLower())))
                    foundSearchAmounts++;

                // Tests if the current Guardian1Last contains the query Guardian1Last
                if (queryCase.Guardian2Last != "" && (element.Element("Guardian2Last").Value.ToLower().Contains(queryCase.Guardian2Last.ToLower()) || element.Element("Guardian2Last").Value.ToLower().Contains(queryCase.Guardian1Last.ToLower())))
                    foundSearchAmounts++;


                // Perps/Siblings/Victims
                // Tests if the current CaseNum contains the query CaseNum
                if (queryCase.PerpFirstName != "" && element.Element("PerpList").Element("Perp").Element("FirstName").Value.ToLower().Contains(queryCase.PerpFirstName.ToLower()))
                    foundSearchAmounts++;

                // Tests if the current CaseNum contains the query CaseNum
                if (queryCase.PerpLastName != "" && element.Element("PerpList").Element("Perp").Element("LastName").Value.ToLower().Contains(queryCase.PerpLastName.ToLower()))
                    foundSearchAmounts++;

                // Tests if the current CaseNum contains the query CaseNum
                if (queryCase.PerpNick != "" && element.Element("PerpList").Element("Perp").Element("Nick").Value.ToLower().Contains(queryCase.PerpNick.ToLower()))
                    foundSearchAmounts++;


                // Sibling
                // Tests if the current CaseNum contains the query CaseNum
                if (queryCase.SiblingFirstName != "" && element.Element("SiblingList").Element("FirstName").Value.ToLower().Contains(queryCase.SiblingFirstName.ToLower()))
                    foundSearchAmounts++;

                // Tests if the current CaseNum contains the query CaseNum
                if (queryCase.SiblingLastName != "" && element.Element("SiblingList").Element("Sibling").Element("LastName").Value.ToLower().Contains(queryCase.SiblingLastName.ToLower()))
                    foundSearchAmounts++;

                // Victim
                // Tests if the current CaseNum contains the query CaseNum
                if (queryCase.OtherVictimFirstName != "" && element.Element("Victim").Element("FirstName").Value.ToLower().Contains(queryCase.OtherVictimFirstName.ToLower()))
                    foundSearchAmounts++;

                // Tests if the current CaseNum contains the query CaseNum
                if (queryCase.OtherVictimLastName != "" && element.Element("Victim").Element("LastName").Value.ToLower().Contains(queryCase.OtherVictimLastName.ToLower()))
                    foundSearchAmounts++;


                if (suppliedSearchAmount == foundSearchAmounts)
                    foundCases.Add(DeSerializer(element));

                if (suppliedSearchAmount == foundSearchAmounts)
                {
                    Console.WriteLine("FOUND");
                    Console.WriteLine(DeSerializer(element).ToString());
                }
            }
            return foundCases;
        }

        private Case DeSerializer(XElement element)
        {
            var serializer = new XmlSerializer(typeof(Case));
            return (Case)serializer.Deserialize(element.CreateReader());
        }

        public bool Exists(string CaseNum)
        {
            // Sets the filename and path for the database
            var filename = "DB.xml";
            var currentDir = Directory.GetCurrentDirectory();
            var path = Path.Combine(currentDir, filename);

            // Loads the db
            if (!File.Exists(path))
                return false;

            XElement db = XElement.Load(path);
            IEnumerable<XElement> cases = db.Elements();

            foreach (var element in cases)
                if (element.Element("CaseNum").Value == CaseNum)
                    return true;

            return false;
        }

        public string GetNextUnknown(string year)
        {
            int count = 0;
            List<int> indices = new List<int>();
            List<string> existingItems = new List<string>();
            string finalOutput;
            string temp;

            // Sets the filename and path for the database
            var filename = "DB.xml";
            var currentDir = Directory.GetCurrentDirectory();
            var path = Path.Combine(currentDir, filename);

            if (!File.Exists(path))
                finalOutput = year + "-00001";
            else
            {
                // Loads the db
                XElement db = XElement.Load(path);
                IEnumerable<XElement> cases = db.Elements();

                foreach (var element in cases)
                {
                    temp = element.Element("CaseNum").Value;
                    if (temp.Contains("0000"))
                    {
                        existingItems.Add(temp);
                        indices.Add(Int32.Parse(temp.Substring(9)));
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
            }
            return finalOutput;
        }
    }
}
