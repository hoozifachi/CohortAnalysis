using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CohortAnalysis.Test
{
    [TestClass]
    public class CustomerOrderDataPointTest
    {
        [TestMethod]
        public void TestCreateDataPoint()
        {
            int customerId = 1;
            DateTime orderDate = DateTime.Now;
            DateTime cohortDate = DateTime.Now;
            string cohortIdentifier = "20150101";
            int cohortPeriod = 1;
            ICustomerOrderDataPoint dataPoint = new CustomerOrderDataPoint(
                customerId, orderDate, cohortDate, cohortIdentifier, cohortPeriod);

            Assert.AreEqual(customerId, dataPoint.Id);
            Assert.AreEqual(orderDate, dataPoint.OrderDate);
            Assert.AreEqual(cohortDate, dataPoint.CohortDate);
            Assert.AreEqual(cohortIdentifier, dataPoint.CohortIdentifier);
            Assert.AreEqual(cohortPeriod, dataPoint.CohortPeriod);
        }
    }
}
