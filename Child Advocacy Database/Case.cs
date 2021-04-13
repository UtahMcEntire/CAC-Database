using System;
using System.Collections.Generic;

namespace Child_Advocacy_Database
{
	public class Perp
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Nick { get; set; }

		public Perp()
        {
			// Default Constructor
        }
		public Perp(string FName, string LName, string Nick)
		{
			this.FirstName = FName;
			this.LastName = LName;
			this.Nick = Nick;
		}

		public override string ToString()
		{
			return FirstName + " " + LastName + " " + Nick;
		}

		// A function used to convert the class into a XML string
		public String ToXmlString()
		{
			string toXML = "<perp>";
			if (this.FirstName != "")
				toXML += "<first>" + this.FirstName + "</first>";
			if (this.LastName != "")
				toXML += "<last>" + this.LastName + "</last>";
			if (this.Nick != "")
				toXML += "<nick>" + this.Nick + "</nick>";
			toXML += "</perp>";

			return toXML;
		}
	}


	public class Sibling
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public Sibling()
        {
			// Default Constructor
        }
		public Sibling(string FName, string LName)
		{
			this.FirstName = FName;
			this.LastName = LName;
		}

		public override string ToString()
		{
			return FirstName + " " + LastName;
		}

		// A function used to convert the class into a XML string
		public String ToXmlString()
		{
			string toXML = "<sibling>";
			if (this.FirstName != "")
				toXML += "<first>" + this.FirstName + "</first>";
			if (this.LastName != "")
				toXML += "<last>" + this.LastName + "</last>";
			toXML += "</sibling>";

			return toXML;
		}
	}

	public class Victim
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public Victim()
        {
			// Default Constructor
        }

		public Victim(string FName, string LName)
		{
			this.FirstName = FName;
			this.LastName = LName;
		}

		public override string ToString()
		{
			return FirstName + " " + LastName;
		}

		// A function used to convert the class into a XML string
		public String ToXmlString()
		{
			string toXML = "<victim>";
			if (this.FirstName != "")
				toXML += "<first>" + this.FirstName + "</first>";
			if (this.LastName != "")
				toXML += "<last>" + this.LastName + "</last>";
			toXML += "</victim>";

			return toXML;
		}
	}


	public class Case
	{
		public string CaseNum { get; set; }
		public string ChildFirst { get; set; }
		public string ChildLast { get; set; }
		public string ChildDob { get; set; }
		public string InterviewDate { get; set; }
		public string Guardian1First { get; set; }
		public string Guardian1Last { get; set; }
		public string Guardian2First { get; set; }
		public string Guardian2Last { get; set; }


		// All of these below are only to be populated for query.
		// If you need to add a single perp, sibling, or victim use the lists defined further down.
		public string PerpFirstName { get; set; }
		public string PerpLastName { get; set; }
		public string PerpNick { get; set; }
		public string SiblingFirstName { get; set; }
		public string SiblingLastName { get; set; }
		public string OtherVictimFirstName { get; set; }
		public string OtherVictimLastName { get; set; }
		//public List<string> HddList { get; set; }


		public List<Perp> PerpList;
		public List<Sibling> SiblingList;
		public List<Victim> VictimList;




		/* 
		 * May not be final datatype. 
		 * Planned for this to be a 2d array of first/last name pairs 
		 * that will be converted to xml
		 * */
		public string[][] Perps { get; set; }

		/* 
		 * May not be final datatype. 
		 * Planned for this to be an array of:
		 * location, encryption key, and maybe other items
		 * that will be converted to xml 
		*/
		public string[] Mappedloc { get; set; }


		/*
		 * Giant Constructor: Sets all values to be defaulted as DBNull.
		 * Can also be used to initialize all values at once.
		 * May not make it into the final version, but it's here now.
		 */
		public Case()
		{
			//HddList = new List<string>();
			//PerpFirstName = new List<string>();
			//PerpLastName = new List<string>();
			//PerpNick = new List<string>();
			//SiblingFirstName = new List<string>();
			//SiblingLastName = new List<string>();
			//OtherVictimFirstName = new List<string>();
			//OtherVictimLastName = new List<string>();
			PerpList = new List<Perp>();
			SiblingList = new List<Sibling>();
			VictimList = new List<Victim>();
		}

		public string printPerpFirst()
		{
			string name = "";
			foreach (Perp p in PerpList)
			{
				name += p.FirstName + " ";
			}
			if (name != " ")
				return name;
			else
				return "";
		}

		public string printPerpLast()
		{
			string name = "";
			foreach (Perp p in PerpList)
			{
				name += p.LastName + " ";
			}
			if (name != " ")
				return name;
			else
				return "";
		}

		public string printPerpNick()
		{
			string name = "";
			foreach (Perp p in PerpList)
			{
				name += p.Nick + " ";
			}
			if (name != " ")
				return name;
			else
				return "";
		}

		public string printSiblingFirst()
		{
			string name = "";
			foreach (Sibling s in SiblingList)
			{
				name += s.FirstName + " ";
			}
			if (name != " ")
				return name;
			else
				return "";
		}

		public string printSiblingLast()
		{
			string name = "";
			foreach (Sibling s in SiblingList)
			{
				name += s.LastName + " ";
			}
			if (name != " ")
				return name;
			else
				return "";
		}

		public string printOtherVictimFirst()
		{
			string name = "";
			foreach (Victim v in VictimList)
			{
				name += v.FirstName + " ";
			}
			if (name != " ")
				return name;
			else
				return "";
		}

		public string printOtherVictimLast()
		{
			string name = "";
			foreach (Victim v in VictimList)
			{
				name += v.LastName + " ";
			}
			if (name != " ")
				return name;
			else
				return "";
		}

		public override string ToString()
		{
			return CaseNum + " - " + ChildFirst + " " + ChildLast;
		}
	}
}