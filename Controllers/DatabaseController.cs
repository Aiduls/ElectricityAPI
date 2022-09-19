using System;
using System.IO;
using System.Collections;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using CsvHelper.Configuration;
using CsvHelper;
using System.Diagnostics;
using System.Globalization;
using ElectricityAPI1.Models;
using ElectricityAPI1.Data;
using Microsoft.AspNetCore.Http.Features;

namespace ElectricityAPI1.Controllers
{
    public class DatabaseController
    {
        private readonly DataContext dataContext;
        public string filesDirectory = "Files\\";

        public DatabaseController()
        {
            var analyticsController = new AnalyticsController();
            analyticsController.Index();
        }
        public DatabaseController(DataContext context)
        {
            dataContext = context;
        }

        public List<Electricity> GetElectricityList()
        {
            List<string> files = Directory.GetFiles(filesDirectory, "*.csv", SearchOption.TopDirectoryOnly).ToList<string>();
            if (files == null) LogSystem.logError("No downloaded files found.");

            string mainFile = filterData(files);
            return initialize_from_csv(mainFile);
            
        }
        private string filterData(List<string> files)
        {
            LogSystem.log("Filtering data started."); 

            string newFile = "Files\\filteredData.csv";
            var isHeaderWritten = false;

            if (File.Exists(newFile))
            {
                File.Delete(newFile);
                files.Remove(newFile);
            }

            foreach (string file in files)
            {
                try
                {
                    using var sr = new StreamReader(file);
                    using var sw = new StreamWriter(newFile, true);
                    string line;
                    string headerLine = $"{sr.ReadLine()},P+-";
                    if (!isHeaderWritten)
                    {
                        sw.WriteLine(headerLine);
                        isHeaderWritten = true;
                    }
                    while ((line = sr.ReadLine()) != null)
                    {

                        List<string> values = new(line.Split(','));
                        if (values[4] == "") values[4] = "0";
                        if (values[6] == "") values[6] = "0";

                        if (values[1] != "Namas" || float.Parse(values[4]) > 1)
                            continue;

                        float diff = float.Parse(values[6]) - float.Parse(values[4]);
                        line += $",{diff}";
                        sw.WriteLine(line);
                    }
                }
                catch (Exception ex)
                {
                    LogSystem.logError($"Exception when filtering files found: {ex}");
                    throw ex;
                }
            }
            LogSystem.log("Filtering data and writing to main file completed");

            return newFile;
        }

        List<Electricity> initialize_from_csv(string dataFile)
        {
            LogSystem.log("Parsing the main CSV file started.");

            List<Electricity> dataList;
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ",",
                MissingFieldFound = null,
                HeaderValidated = null
            };

            using (var reader = new StreamReader(dataFile))
            using (var csv = new CsvReader(reader, config))
            {
                var records = csv.GetRecords<CsvRow>().Select(row => new Electricity()
                {
                    Network = row.Network,
                    Name = row.Name,
                    ObjType = row.ObjType,
                    ObjNo = row.ObjNo,
                    ElectricityUsed = row.ElectricityUsed,
                    ElectricityMade = row.ElectricityMade,
                    Date = row.Date,
                    ElectricityDifference = row.ElectricityDifference
                });

                LogSystem.log("Parsing the main CSV file finished.");
                return records.ToList();
            }
        }
    }
}
