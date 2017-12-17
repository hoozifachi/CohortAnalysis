using System;
using System.Collections.Generic;
using System.IO;

namespace CustomerDataModel
{
    public class CustomerLoader
    {
        public static ICollection<ICustomer> ParseCsvData(string csvData)
        {
            var customers = new List<ICustomer>();

            using (var reader = new StringReader(csvData))
            {
                // This could use some better data validation, but the cohort analysis is the priority right now.

                var line = reader.ReadLine();
                if (line.StartsWith("id")) { /* Skip the first line */ }

                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(',');

                    var id = int.Parse(parts[0]);
                    var createdDT = DateTime.Parse(parts[1]);

                    var customer = new Customer(id, createdDT);
                    customers.Add(customer);
                }
            }

            return customers;
        }

        public static ICollection<ICustomer> LoadCsvData(string filename)
        {
            using (var reader = new StreamReader(filename))
            {
                return ParseCsvData(reader.ReadToEnd());
            }
        }
    }
}
