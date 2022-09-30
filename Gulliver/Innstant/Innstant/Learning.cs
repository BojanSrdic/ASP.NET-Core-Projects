using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innstant
{
	class Learning
	{
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

