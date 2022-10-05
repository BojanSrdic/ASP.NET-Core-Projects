using Innstant.InnstantAPI;
using Innstant.Models;
using Innstant.Services;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.FileIO;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace Innstant
{
	class Program
	{
		static void Main(string[] args)
		{
            // Configuration of appsetings.json, Serilog and DI
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            Log.Logger.Information("Application starting");

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => 
                {
                    services.AddTransient<Start>();
                    services.AddScoped<InnstantStaticDataReader>();
                    services.AddScoped<InnstantService>();
                })
                .UseSerilog()
                .Build();

            var svc = ActivatorUtilities.CreateInstance<Start>(host.Services);
            svc.Run();

        }

      
        // Talk to appsettings.json
        static void BuildConfig(IConfigurationBuilder builder)
		{
			#region Theoretical part
			    // How to create connection to appsetting.json file
			    #region .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
			    // What will that do?
			    // It is going to create or add to our builder the ability to talk to appsettings.json
			    // wherever you are running exe look in the same directory, so the current directory where you are running
			    // Look for appsettings.json and add that as your settings file and that is not optional,
			    // and if appsettings.json file changes reloade it
			    #endregion

			    #region .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
			    // We want also to override it for diferent env (Dev or Staging or Prod)
			    //.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}");
			    // appsettings.Development.json
			    // what is this doing
			    // istraziti malo :D
			    #endregion

			    #region .AddEnvironmentVariables();
			    // EnvirounmentVariables
			    #endregion
			#endregion

			builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();
        }
		

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


        #region Save Data to CSV file
        public void SaveDataIntoCSV()
        {

        }
        #endregion

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
    }

    public class Start
	{
        private readonly ILogger<Start> _log;
        private readonly IConfiguration _config;
        private readonly InnstantStaticDataReader _innstantStaticDataReader;
        private readonly InnstantService _innstantService;

        public Start(ILogger<Start> log, IConfiguration config, InnstantStaticDataReader innstantStaticDataReader,
            InnstantService innstantService)
		{
            _log = log;
            _config = config;
            _innstantStaticDataReader = innstantStaticDataReader;
            _innstantService = innstantService;
        }

        public void Run()
        {
            var a = _config.GetValue<int>("Value from appsettings");
            _log.LogInformation("Message: {a}", a);
            _log.LogWarning("warning");
            _log.LogError("Error message");
            Console.WriteLine("Console message");

            /*
            #region Task 1: Create Gulliver Destination tabel
            //GulliverService.CreateGulliverDestinationTable();
            #endregion

            #region Task 2: Receive Israel hotel list

            // Read all destinations from israel "IL"
            var innstantIsraelsDestinationList = _innstantStaticDataReader.InnstantDestinationsParser();

            // Create mapping table between Israel Hotels and Destinations
            var israelHotelDestinationMappingTable = _innstantStaticDataReader.InnstantIsraelHotelDestinationParser(innstantIsraelsDestinationList);

            // Extract Israel Hotel Id from Hotels_Destinations mapping table
            var innstantIsraelHotelIdList = new List<int>();

            foreach (InnstantHotelDestination row in israelHotelDestinationMappingTable)
            {
                innstantIsraelHotelIdList.Add(row.HotelId);
            }

            // Read all Hotels
            _innstantStaticDataReader.InnstantIsraelHotelsParser(innstantIsraelHotelIdList);

            // Save Hotels to the database

            #endregion
            */
            #region Task 3: Craete Innstant_Gulliver_Hotels_Conversion table
            #endregion

            #region Task 4: Innstant rooms
            _innstantService.HitInnstantAPI();
			#endregion


		}
	}
}

// Question: City or area? what we do not have anything written in last Contains
// Proverio sam yaya provider Yaya ID, name Guliver ID gulier Name

// TAsk 2:
// Read about HashSet

// Prepraviti static metode dok je jos vreme kako bi mogli testirati aplikaciju
