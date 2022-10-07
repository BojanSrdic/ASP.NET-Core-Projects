using Innstant.DataAccess;
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
                    services.AddScoped<IInnstantStaticDataReader, InnstantStaticDataReader>();
                    services.AddScoped<IInnstantService, InnstantService>();
                    services.AddScoped<ISaveInnstantStaticData, SaveInnstantStaticData>();
                    services.AddScoped<IGulliverService, GulliverService>();
                    services.AddScoped<IProcessInnstantStaticData, ProcessInnstantStaticData>();
                    services.AddScoped<IInnstantDataAccessLayer, InnstantDataAccessLayer>();
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
    }

	public class Start
	{
        private readonly ILogger<Start> _log;
        private readonly IConfiguration _config;
        private readonly IInnstantStaticDataReader _innstantStaticDataReader;
        private readonly IInnstantService _innstantService;
        private readonly IGulliverService _gulliverService;

        public Start(ILogger<Start> log, IConfiguration config, IInnstantStaticDataReader innstantStaticDataReader,
            IInnstantService innstantService, IGulliverService gulliverService)
		{
            _log = log;
            _config = config;
            _innstantStaticDataReader = innstantStaticDataReader;
            _innstantService = innstantService;
            _gulliverService = gulliverService;
        }

        public void Run()
        {
            var a = _config.GetValue<int>("Value from appsettings");
            _log.LogInformation("Message: {a}", a);
            _log.LogWarning("warning");
            _log.LogError("Error message");
            Console.WriteLine("Console message");

            #region Task 1: Insert Innstant static data into DB - [Innstant_Destinationss] - [Innstant_Hotel_Destination] - [Innstant_Hotels]
            // SeedData();
            #endregion

            #region Task 2: Create Destination mapping table - [Innstant_Gulliver_Destination_Cconversions]
            //_gulliverService.CreateGulliverDestinationTable();
            #endregion


            #region Task 3: Craete Innstant_Gulliver_Hotels_Conversion table
            #endregion

            #region Task 4: Insert Innstant static data about rooms into DB 
            // Receive the rooms list task: GLV-12036
            _innstantService.HitInnstantAPI();
            #endregion

        }

        public void SeedData()
		{
            // Run InnstantScheme
            var innstantIsraelsDestinations = _innstantStaticDataReader.InnstantDestinationsParser();
            var innstantIsraelHotelDestinationList = _innstantStaticDataReader.InnstantIsraelHotelDestinationParser(innstantIsraelsDestinations);
            _innstantStaticDataReader.InnstantIsraelHotelsParser(innstantIsraelHotelDestinationList);
        }
    }
}

// Question: City or area? what we do not have anything written in last Contains
// Proverio sam yaya provider Yaya ID, name Guliver ID gulier Name

// TAsk 2:
// Read about HashSet

// Prepraviti static metode dok je jos vreme kako bi mogli testirati aplikaciju
