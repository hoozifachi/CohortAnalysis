using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDataModel
{
    public class OrderLoader
    {
        public static ICollection<IOrder> ParseCsvData(string csvData)
        {
            var orders = new List<IOrder>();

            using (var reader = new StringReader(csvData))
            {
                // This could use some better data validation, but the cohort analysis is the priority right now.

                var line = reader.ReadLine();
                if (line.StartsWith("id")) { /* Skip the first line */ }

                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(',');

                    var id = int.Parse(parts[0]);
                    var orderNumber = int.Parse(parts[1]);
                    var customerId = int.Parse(parts[2]);
                    var createdDT = DateTime.Parse(parts[3]);

                    var order = new Order(id, orderNumber, customerId, createdDT);
                    orders.Add(order);
                }
            }

            return orders;
        }

        public static ICollection<IOrder> LoadCsvData(string filename)
        {
            using (var reader = new StreamReader(filename))
            {
                return ParseCsvData(reader.ReadToEnd());
            }
        }
    }
}
