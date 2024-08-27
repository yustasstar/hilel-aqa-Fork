using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarTechnology.Data
{
    public class ManufacturerData
    {
        public string Manufacturer { get; set; }
        public int Id { get; set; }
        public string aaaaa { get; set; }
    }

    public static class DataLoader
    {
        public static string GetManufacturer()
        {
            var save = new ManufacturerData();
            save.Manufacturer = "save text";
            save.Id = 20;

            TestDataLoader.SaveJson<ManufacturerData>(@"C:\Users\yusta\source\repos\#DATA\m2.json", save);
            var testData = TestDataLoader.LoadJson(@"C:\Users\yusta\source\repos\#DATA\m.json");

            // Use test data in the test
            return testData.Manufacturer;

        }
    }
}
