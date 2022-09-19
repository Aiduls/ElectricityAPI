using ElectricityAPI1.Controllers;
using ElectricityAPI1.Data;
using ElectricityAPI1.Models;
using Microsoft.AspNetCore.HttpOverrides;
using System.Data;
using System.Xml.Linq;
using System.Management.Automation;
using EFCore.BulkExtensions;

namespace ElectricityAPI1
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            dataContext = context;
        }
        public void SeedDataContext()
        {
            /*PowerShell.Create().AddCommand("Add-Migration")
                               .AddParameter("InitialCreate")
                               .Invoke();
            PowerShell.Create().AddCommand("Update-Database")
                               .Invoke();*/
            // does not work. You have to add commands 'Add-Migration InitialCreate' and 'Update-Database' in Package Manager Console yourself.

            var databaseController = new DatabaseController();

            List<Electricity> electricities  = databaseController.GetElectricityList();

            LogSystem.log("Uploading data to database started");
            try
            {
                dataContext.BulkInsert(electricities);
                dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                LogSystem.logError($"Exception caught when uploading data to database: {ex}");
                throw;
            }
            
        }
    }
}