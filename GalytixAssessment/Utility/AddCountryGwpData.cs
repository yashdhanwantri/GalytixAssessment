using CsvHelper;
using GalytixAssessment.DAL;
using GalytixAssessment.DAL.Models;
using System.Globalization;

namespace GalytixAssessment.API.Utility
{
    public static class LoadData
    {
        public static void AddCountryData(WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetService<GalytixDbContext>();

            using var reader = new StreamReader("Data\\gwpByCountry.csv");
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var data = csv.GetRecords<GwpByCountry>();

            db.GwpByCountry.AddRange(data);
            db.SaveChanges();
        }
    }
}
