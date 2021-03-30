using System;
using System.Collections.Generic;

namespace Child_Advocacy_Database
{

	public class Perp
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Nick { get; set; }

		public Perp(string FName, string LName, string Nick)
		{
			this.FirstName = FName;
			this.LastName = LName;
			this.Nick = Nick;
		}

		public override string ToString()
		{
			return FirstName + " " + LastName + " \"" + Nick + "\"";
		}

		// A function used to convert the class into a XML string
		public String ToXmlString()
		{
			string toXML = "<perp>";
			if (this.FirstName != null)
				toXML += "<first>" + this.FirstName + "</first>";
			if (this.LastName != null)
				toXML += "<last>" + this.LastName + "</last>";
			if (this.Nick != null)
				toXML += "<nick>" + this.Nick + "</nick>";
			toXML += "</perp>";

			return toXML;
		}
	}


	public class Sibling
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }

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
			if (this.FirstName != null)
				toXML += "<first>" + this.FirstName + "</first>";
			if (this.LastName != null)
				toXML += "<last>" + this.LastName + "</last>";
			toXML += "</sibling>";

			return toXML;
		}
	}

	public class Victim
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }

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
			if (this.FirstName != null)
				toXML += "<first>" + this.FirstName + "</first>";
			if (this.LastName != null)
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


		public List<string> PerpFirstNames { get; set; }
		public List<string> PerpLastNames { get; set; }
		public List<string> PerpNicks { get; set; }
		public List<string> SiblingFirstNames { get; set; }
		public List<string> SiblingLastNames { get; set; }
		public List<string> OtherVictimFirstNames { get; set; }
		public List<string> OtherVictimLastNames { get; set; }
		public List<string> HddList { get; set; }
		string[] AddCaseHdd { get; set; }


		public List<Perp> PerpList = new List<Perp>();
		public List<Sibling> SiblingList = new List<Sibling>();
		public List<Victim> VictimList = new List<Victim>();




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
			HddList = new List<string>();
			PerpFirstNames = new List<string>();
			PerpLastNames = new List<string>();
			PerpNicks = new List<string>();
			SiblingFirstNames = new List<string>();
			SiblingLastNames = new List<string>();
			OtherVictimFirstNames = new List<string>();
			OtherVictimLastNames = new List<string>();
		}

		public string printPerpFirst()
		{
			string name = "";
			foreach (var perp in PerpFirstNames)
			{
				name += perp + " ";
			}
			if (name != " ")
				return name;
			else
				return "";
		}

		public string printPerpLast()
		{
			string name = "";
			foreach (var perp in PerpLastNames)
			{
				name += perp + " ";
			}
			if (name != " ")
				return name;
			else
				return "";
		}

		public string printPerpNick()
		{
			string name = "";
			foreach (var perp in PerpNicks)
			{
				name += perp + " ";
			}
			if (name != " ")
				return name;
			else
				return "";
		}

		public string printSiblingFirst()
		{
			string name = "";
			foreach (var sibling in SiblingFirstNames)
			{
				name += sibling + " ";
			}
			if (name != " ")
				return name;
			else
				return "";
		}

		public string printSiblingLast()
		{
			string name = "";
			foreach (var sibling in SiblingLastNames)
			{
				name += sibling + " ";
			}
			if (name != " ")
				return name;
			else
				return "";
		}

		public string printOtherVictimFirst()
		{
			string name = "";
			foreach (var vic in OtherVictimFirstNames)
			{
				name += vic + " ";
			}
			if (name != " ")
				return name;
			else
				return "";
		}

		public string printOtherVictimLast()
		{
			string name = "";
			foreach (var vic in OtherVictimLastNames)
			{
				name += vic + " ";
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