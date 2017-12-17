using CohortAnalysis;
using CustomerDataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CohortAnalysisRunner
{
    class Program
    {
        public static void Main(string[] args)
        {
            GenerateReport("customers.csv", "orders.csv");
        }

        private static void GenerateReport(string customerFile, string orderFile)
        {
            ICollection<ICustomer> customers = LoadCustomerData(customerFile, orderFile);
            var data = IdentifyCohorts(customers);

            data = data.Select(d => OrderBucketCalculator.CalculateBucket(d)).ToList();

            //using (var writer = new StreamWriter("data.csv"))
            //{
            //    foreach (var item in data)
            //    {
            //        writer.WriteLine(item.ToString());
            //    }
            //}

            var reportGenerator = new HtmlReportGenerator("report.html");
            reportGenerator.WriteReport(data);
        }

        private static ICollection<ICustomerOrderDataPoint> IdentifyCohorts(ICollection<ICustomer> customers)
        {
            var startDate = customers.OrderBy(c => c.CreatedDate).First().CreatedDate;
            var dataLoader = new CustomerCohortLoader(startDate);

            return dataLoader.IdentifyCohorts(customers);
        }

        private static ICollection<ICustomer> LoadCustomerData(string customerFile, string orderFile)
        {
            var dataSource = new CsvCustomerDataSource(customerFile, orderFile);

            var customers = dataSource.GetCustomers();
            return customers;
        }
    }
}
