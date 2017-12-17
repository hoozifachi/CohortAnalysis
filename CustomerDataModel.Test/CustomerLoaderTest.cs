using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CustomerDataModel.Test
{
    [TestClass]
    public class CustomerLoaderTest
    {
        [TestMethod]
        public void TestLoadCustomerCSV()
        {
            string csvData = @"id,created
35410,2015-07-03 22:01:11
35417,2015-07-03 22:11:23
35412,2015-07-03 22:02:52
";
            ICollection<ICustomer> customers = CustomerLoader.ParseCsvData(csvData);

            Assert.AreEqual(3, customers.Count);

            var customerEnumerator = customers.GetEnumerator();
            customerEnumerator.MoveNext();
            var customer = customerEnumerator.Current;
            Assert.AreEqual(35410, customer.Id);
            Assert.AreEqual(DateTime.Parse("2015-07-03 22:01:11"), customer.CreatedDate);

            customerEnumerator.MoveNext();
            customer = customerEnumerator.Current;
            Assert.AreEqual(35417, customer.Id);
            Assert.AreEqual(DateTime.Parse("2015-07-03 22:11:23"), customer.CreatedDate);

            customerEnumerator.MoveNext();
            customer = customerEnumerator.Current;
            Assert.AreEqual(35412, customer.Id);
            Assert.AreEqual(DateTime.Parse("2015-07-03 22:02:52"), customer.CreatedDate);
        }
    }
}
