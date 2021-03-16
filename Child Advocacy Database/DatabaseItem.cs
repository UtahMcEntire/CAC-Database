using System;

namespace Child_Advocacy_Database {

	public class DatabaseItem
	{
		string CaseNum { get; set; }
		string ChildFirst { get; set; }
		string ChildLast { get; set; }
		DateTime ChildDob { get; set; }
		DateTime InterviewDate { get; set; }
		string Guardian1First { get; set; }
		string Guardian1Last { get; set; }
		string Guardian2First { get; set; }
		string Guardian2Last { get; set; }

		/* 
		 * May not be final datatype. 
		 * Planned for this to be a 2d array of first/last name pairs 
		 * that will be converted to xml
		 * */
		string[][] Perps { get; set; }

		/* 
		 * May not be final datatype. 
		 * Planned for this to be an array of:
		 * location, encryption key, and maybe other items
		 * that will be converted to xml 
		*/
		string[] Mappedloc { get; set; }


		/*
		 * Giant Constructor: Sets all values to be defaulted as DBNull.
		 * Can also be used to initialize all values at once.
		 * May not make it into the final version, but it's here now.
		 */
		public DatabaseItem()
		{
			if (CaseNum == null)
				Console.WriteLine("it null");
				
			Console.WriteLine(CaseNum);
			Console.WriteLine(ChildFirst);
			Console.WriteLine(ChildLast);
			Console.WriteLine(ChildDob);
			Console.WriteLine(InterviewDate);
			if (InterviewDate.ToString() == "1/1/0001 12:00:00 AM")
				Console.WriteLine("Date NULL too");
			Console.WriteLine(Guardian1First);
			Console.WriteLine(Guardian1Last);
			Console.WriteLine(Guardian2First);
			Console.WriteLine(Guardian2Last);
			Console.WriteLine(Perps);
			Console.WriteLine(Mappedloc);

		}
	}
}