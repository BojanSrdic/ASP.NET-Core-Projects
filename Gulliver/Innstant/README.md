## Task 1: Create the mapping table of the distance between Gulliver and Innstant.

Create a new table in the Gulliver database. Tabel columns should contain (GulliverDestinationId, GulliverDestinationName,InnstantDestinationId, InnstantDestinationName, Type)

The table should map Gulliver and Innstant destination.

The list of Innstant’s destinations was received from the static database and put here.

The table should contain the next parameters (look at the table below):

Use the Innstant’s destination file for the p.6 current requirements.

The column “Type” should be fulfilled by the next rule:
if the destination has more than one value in the “contains” column assigns the “area” type for this destination. Another way (if only one value in the “contains” column) is to assign the city type. 

If the destination has the type “area” then it should contain all of the cities listed in Innstant’s destination file and mapped with the corresponding Gulliver’s area.



// problems
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/cs0120?f1url=%3FappId%3Droslyn%26k%3Dk(CS0120)
// Ako izbrisem static iz metode nece raditi moram main da namestim da ne bude static


// DataFrame in C#

// https://stackoverflow.com/questions/50065731/pandas-dataframe-or-similar-in-c-net
// http://bluemountaincapital.github.io/Deedle/index.html


// Way two! 
/*SqlCommand cmd = new SqlCommand();
cmd.Connection = dbConnection;
cmd.CommandType = CommandType.Text; or CommandType.StoredProcedure
cmd.CommandText = query;*/

## Task 2: Receive the Israel hotels list

It’s necessary to receive the list of Israeli hotels from Innstant.

Acceptance criteria:

Filter the list of the hotels (which received by the method GLV-11706: Search the hotelsOPEN ) by destination ID

The list of relevant destinations here (this table received from the static data https://static-data-console.innstant-servers.com/static-files/download and filtered by the parameter “Country ID=IL”).

As a result of filtering the list of hotels, a table should be obtained:

Hotel ID

Hotel name

destination id 

destination name