using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CohortAnalysis.Test
{
    [TestClass]
    public class OrderBucketCalculatorTest
    {
        [TestMethod]
        public void TestCalculateBucketNoOrder()
        {
            int id = 1;
            DateTime? orderDate = null;
            DateTime cohortDate = DateTime.Now;
            string cohortIdentifier = "1/1-1/7";
            int cohortPeriod = 0;
            ICustomerOrderDataPoint dataPoint = new CustomerOrderDataPoint(id, orderDate, cohortDate, cohortIdentifier, cohortPeriod);

            dataPoint = OrderBucketCalculator.CalculateBucket(dataPoint);
            Assert.AreEqual(0, dataPoint.CohortPeriod);
        }

        [TestMethod]
        public void TestCalculateBucketOrderOnSignupInBucket1()
        {
            int id = 1;
            DateTime? orderDate = DateTime.Now;
            DateTime cohortDate = DateTime.Now;
            string cohortIdentifier = "1/1-1/7";
            int cohortPeriod = 0;
            ICustomerOrderDataPoint dataPoint = new CustomerOrderDataPoint(id, orderDate, cohortDate, cohortIdentifier, cohortPeriod);

            dataPoint = OrderBucketCalculator.CalculateBucket(dataPoint);
            Assert.AreEqual(1, dataPoint.CohortPeriod);
        }

        [TestMethod]
        public void TestCalculateBucketOrderTwoDaysAfterSignupInBucket1()
        {
            int id = 1;
            DateTime? orderDate = DateTime.Now.AddDays(2);
            DateTime cohortDate = DateTime.Now;
            string cohortIdentifier = "1/1-1/7";
            int cohortPeriod = 0;
            ICustomerOrderDataPoint dataPoint = new CustomerOrderDataPoint(id, orderDate, cohortDate, cohortIdentifier, cohortPeriod);

            dataPoint = OrderBucketCalculator.CalculateBucket(dataPoint);
            Assert.AreEqual(1, dataPoint.CohortPeriod);
        }

        [TestMethod]
        public void TestCalculateBucketOrderSevenDaysAfterSignupInBucket2()
        {
            int id = 1;
            DateTime? orderDate = DateTime.Now.AddDays(7);
            DateTime cohortDate = DateTime.Now;
            string cohortIdentifier = "1/1-1/7";
            int cohortPeriod = 0;
            ICustomerOrderDataPoint dataPoint = new CustomerOrderDataPoint(id, orderDate, cohortDate, cohortIdentifier, cohortPeriod);

            dataPoint = OrderBucketCalculator.CalculateBucket(dataPoint);
            Assert.AreEqual(2, dataPoint.CohortPeriod);
        }

        [TestMethod]
        public void TestCalculateBucketOrder13DaysAfterSignupInBucket2()
        {
            int id = 1;
            DateTime? orderDate = DateTime.Now.AddDays(13);
            DateTime cohortDate = DateTime.Now;
            string cohortIdentifier = "1/1-1/7";
            int cohortPeriod = 0;
            ICustomerOrderDataPoint dataPoint = new CustomerOrderDataPoint(id, orderDate, cohortDate, cohortIdentifier, cohortPeriod);

            dataPoint = OrderBucketCalculator.CalculateBucket(dataPoint);
            Assert.AreEqual(2, dataPoint.CohortPeriod);
        }
        [TestMethod]
        public void TestCalculateBucketOrder14DaysAfterSignupInBucket3()
        {
            int id = 1;
            DateTime? orderDate = DateTime.Now.AddDays(14);
            DateTime cohortDate = DateTime.Now;
            string cohortIdentifier = "1/1-1/7";
            int cohortPeriod = 0;
            ICustomerOrderDataPoint dataPoint = new CustomerOrderDataPoint(id, orderDate, cohortDate, cohortIdentifier, cohortPeriod);

            dataPoint = OrderBucketCalculator.CalculateBucket(dataPoint);
            Assert.AreEqual(3, dataPoint.CohortPeriod);
        }
    }
}
