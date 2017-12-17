using CustomerDataModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CohortAnalysis.Test
{
    [TestClass]
    public class CustomerCohortLoaderTest
    {
        [TestMethod]
        public void TestLoadCustomerCohorts()
        {
            var customers = new List<ICustomer>
            {
                new Customer(1, DateTime.Parse("2015-01-01 10:52:00")),
                new Customer(2, DateTime.Parse("2015-01-08 02:22:36")),
                new Customer(3, DateTime.Parse("2015-02-14 23:23:23"))
            };

            DateTime startDate = DateTime.Parse("2015-01-01 00:00:00");
            var customerCohortLoader = new CustomerCohortLoader(startDate);

            ICollection<ICustomerOrderDataPoint> dataPoints = customerCohortLoader.IdentifyCohorts(customers);

            Assert.AreEqual(3, dataPoints.Count);
            var enumerator = dataPoints.GetEnumerator();

            enumerator.MoveNext();
            var dataPoint = enumerator.Current;
            Assert.AreEqual(1, dataPoint.Id);
            Assert.IsFalse(dataPoint.OrderDate.HasValue);
            Assert.AreEqual(DateTime.Parse("2015-01-01 10:52:00"), dataPoint.CohortDate);
            Assert.AreEqual("1/1-1/7", dataPoint.CohortIdentifier);
            Assert.AreEqual(0, dataPoint.CohortPeriod);

            enumerator.MoveNext();
            dataPoint = enumerator.Current;
            Assert.AreEqual(2, dataPoint.Id);
            Assert.IsFalse(dataPoint.OrderDate.HasValue);
            Assert.AreEqual(DateTime.Parse("2015-01-08 02:22:36"), dataPoint.CohortDate);
            Assert.AreEqual("1/8-1/14", dataPoint.CohortIdentifier);
            Assert.AreEqual(0, dataPoint.CohortPeriod);

            enumerator.MoveNext();
            dataPoint = enumerator.Current;
            Assert.AreEqual(3, dataPoint.Id);
            Assert.IsFalse(dataPoint.OrderDate.HasValue);
            Assert.AreEqual(DateTime.Parse("2015-02-14 23:23:23"), dataPoint.CohortDate);
            Assert.AreEqual("2/12-2/18", dataPoint.CohortIdentifier);
            Assert.AreEqual(0, dataPoint.CohortPeriod);
        }

        [TestMethod]
        public void GetCohortIdentifierForDate()
        {
            DateTime startDate = DateTime.Parse("2015-01-01 00:00:00");
            var customerCohortLoader = new CustomerCohortLoader(startDate);

            var cohortDate = DateTime.Parse("2015-01-01 10:52:00");
            Assert.AreEqual("1/1-1/7", customerCohortLoader.GetCohortIdentifierForDate(cohortDate));

            cohortDate = DateTime.Parse("2015-01-08 02:22:36");
            Assert.AreEqual("1/8-1/14", customerCohortLoader.GetCohortIdentifierForDate(cohortDate));

            cohortDate = DateTime.Parse("2015-02-14 23:23:23");
            Assert.AreEqual("2/12-2/18", customerCohortLoader.GetCohortIdentifierForDate(cohortDate));
        }
    }
}
