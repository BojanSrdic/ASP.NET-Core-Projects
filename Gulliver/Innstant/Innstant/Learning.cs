using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innstant
{
	class Learning
	{
		// Static vs DI opisat sta je radjeno i zasto i koja je razlika

		public void Cast()
		{
			string[] stringArray = { "123", "981", "372", "996" };

			// If we want to cast string to int we can not do taht directly!
			var a = stringArray.ElementAt(1);
			var b = stringArray[1];
			//var c = (int)b;
			var d = Convert.ToInt32(b);

			// https://www.tutorialsteacher.com/articles/convert-string-to-int
		}
        /*#region Ideas and Learning

public void NotUsed()
{
    var innstantDestinations = ProcessInnstantStaticData.ProcessInnstantDestinations();

    foreach (InnstantDestinations destination in innstantDestinations)
    {
        string a = destination.Contains;
        string[] destionationProperties = a.Split(";");

        int[] ints = Array.ConvertAll(destionationProperties, s => int.Parse(s));

        if (ints.Length != 1)
        {
            // Set Type prop to city or area
            destination.Type = "Area";

        }
        else { destination.Type = "City"; }

        bool v = a.Contains(";");
        destination.Type = destination.Contains.Contains(";") ? "Area" : "City";
    }
}

public void PrintListMethode()
{
    var list = ProcessInnstantStaticData.ProcessInnstantDestinations();
    // Lisata
    Console.WriteLine(list); // necu dobiti listu 
    // da bi ispisao listu u terminalu potrebno je da napravim foreach pelju

    foreach (var a in list)
    {
        Console.WriteLine(a.DestinationName);
    }
}

#endregion*/
        #region Save Data to CSV file
        public void SaveDataIntoCSV()
        {

        }
        #endregion
        // Kako ispisati listu?
        public void ReadList()
		{

		// Using LINQ
            //var israelDestinationIdList = new List<int>(innstantIsraelsDestinationList.Select(s => s.DestinationId));
		// see how where works

		// Using foreach
		/*var israelDestinationIdList = new List<int>();
		foreach (InnstantDestinations a in innstantIsraelsDestinationList)
		{
			israelDestinationIdList.Add(a.DestinationId);

		}*/
	}

		// SQL part 
		// https://stackoverflow.com/questions/1056323/difference-between-numeric-float-and-decimal-in-sql-server
		// https://www.w3schools.com/sql/sql_datatypes.asp
		// https://www.google.com/imgres?imgurl=https%3A%2F%2Fessentialsql.com%2Fwp-content%2Fuploads%2F2020%2F03%2Fdecimal.png&imgrefurl=https%3A%2F%2Fwww.essentialsql.com%2Fsql-server-decimal-data-type%2F&tbnid=y8UqYCope8mxRM&vet=12ahUKEwiYs4Pqkbf6AhUph_0HHS_dAi4QMygKegUIARDTAQ..i&docid=EJ63tAawH46_FM&w=296&h=211&q=decimal%20in%20sql&ved=2ahUKEwiYs4Pqkbf6AhUph_0HHS_dAi4QMygKegUIARDTAQ
		// https://stackoverflow.com/questions/13405572/sql-statement-to-get-column-type
		// https://www.google.com/search?q=difference+between+varchar+and+nvarchar&rlz=1C1GCEA_enRS974RS975&oq=difference+between+navchar+and+&aqs=chrome.1.69i57j0i13l9.10175j0j7&sourceid=chrome&ie=UTF-8
		// https://www.google.com/search?q=float+c%23+max+value&rlz=1C1GCEA_enRS974RS975&sxsrf=ALiCzsbDxA3wzpQ5QOZwJO8--GOUkHfsIg%3A1664357026505&ei=ohI0Y6apHtaM9u8P5KmLiAM&oq=float+c%23&gs_lcp=Cgdnd3Mtd2l6EAEYATIFCAAQxAIyBQgAEIAEMgUIABCABDIFCAAQgAQyBQgAEIAEMgYIABAeEBYyBggAEB4QFjIGCAAQHhAWMgYIABAeEBYyBggAEB4QFjoKCAAQRxDWBBCwAzoHCAAQsAMQQzoNCAAQ5AIQ1gQQsAMYAToPCC4Q1AIQyAMQsAMQQxgCOgQIABBDOgsILhCABBDHARCvAUoECEEYAEoECEYYAVDnAVidEmDMHmgBcAF4AIABf4gB8gKSAQMwLjOYAQCgAQHIARDAAQHaAQYIARABGAnaAQYIAhABGAg&sclient=gws-wiz
		// https://www.w3schools.com/sql/sql_insert.asp


	}
}


// Goal: Insert dependency injection, Serilog and appsettings.json in console app
// https://www.youtube.com/watch?v=GAOCe-2nXqc&ab_channel=IAmTimCorey
// Logger
// Check Nuget dependency serilog.sinks.File
// serilog.sinks.elasticsearch

// Appsettings as nesxt topis
// https://www.youtube.com/watch?v=_2_qksdQKCE&ab_channel=IAmTimCorey

// SQL DECIMAL TYPE
// https://www.sqlshack.com/understanding-sql-decimal-data-type/
// https://kb.objectrocket.com/postgresql/decimal-vs-double-in-sql-600#:~:text=Double%20types%20are%20used%20when,takes%208%20bytes%20storage%20size.


/*
 
    #region Bojan komplikovanije resenje :D Preko DataTable
        public void ConvertCSVtoSQL()
        {
            try
			{
                // Ovde postoji jedna velika mana a to je komplikovanost koda
                // Da bi citali podatke iz dataTable treba nam cela kompleksa logika sto se vidi u primeru kreiramo celu metodu koja cita redove pa dok pronadjemo sve te redove malo je komplikovano
                // string[] hotelDestionationLines = File.ReadAllLines(@"..\\..\\..\\InnstantStaticData\\hotel_destinations.csv");
                // preskocili smo ceo kompleksan proces i smanjili kolicinu koda :D
                // Tako da pogledati dusanovo resenje
                string csv_file_path = @"C:\Users\b.srdic\Desktop\ASP.NET Core\Git\ASP.NET-Core-Projects\Gulliver\Innstant\Innstant\InnstantStaticData\hotel_destinations.csv";
                DataTable csvData = GetDataTabletFromCSVFile(csv_file_path);
                InsertDataIntoSQLServerUsingSQLBulkCopy(csvData);
            }
			catch(Exception ex)
			{
                Console.WriteLine(ex);
			}
            
        }

        private DataTable GetDataTabletFromCSVFile(string csv_file_path)
        {
            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = csvReader.ReadFields();
                    foreach (string column in colFields)
                    {
                        DataColumn datecolumn = new DataColumn(column);
                        datecolumn.AllowDBNull = true;
                        csvData.Columns.Add(datecolumn);
                    }
                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }
                        csvData.Rows.Add(fieldData);
                    }
                }
            }
            catch (Exception ex)
            {
                var a = ex.Message;
                return null;
            }
            return csvData;
        }

        // Copy the DataTable to SQL Server using SqlBulkCopyReceived an invalid column 
        public void InsertDataIntoSQLServerUsingSQLBulkCopy(DataTable csvFileData)
        {
            using (SqlConnection dbConnection = new SqlConnection(@"Data Source = BSRDIC; Initial Catalog = Test; Integrated Security = True"))
            {
                dbConnection.Open();
                using (SqlBulkCopy s = new SqlBulkCopy(dbConnection))
                {
                    s.DestinationTableName = "Destinations";

                    foreach (var column in csvFileData.Columns)
                        s.ColumnMappings.Add(column.ToString(), column.ToString());

                    s.WriteToServer(csvFileData);
                }
            }
        }
        #endregion
 
 */