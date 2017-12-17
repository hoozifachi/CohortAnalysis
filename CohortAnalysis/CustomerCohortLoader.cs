using CustomerDataModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CohortAnalysis
{
    public class CustomerCohortLoader
    {
        private DateTime _startDate;

        public CustomerCohortLoader(DateTime startDate)
        {
            _startDate = startDate;
        }

        public ICollection<ICustomerOrderDataPoint> IdentifyCohorts(ICollection<ICustomer> customers)
        {
            var dataPoints = new List<ICustomerOrderDataPoint>();

            foreach (ICustomer customer in customers)
            {
                string cohortIdentifier = GetCohortIdentifierForDate(customer.CreatedDate);

                if (customer.Orders.Count > 0)
                {
                    // Create a data point for each order
                    foreach (IOrder order in customer.Orders)
                    {
                        DateTime? orderDate = order.CreatedDate;
                        var dataPoint = new CustomerOrderDataPoint(customer.Id, orderDate,
                            customer.CreatedDate, cohortIdentifier, 0);
                        dataPoints.Add(dataPoint);
                    } 
                }
                else
                {
                    // Create a data point for a custmer with no orders.
                    var dataPoint = new CustomerOrderDataPoint(customer.Id, null,
                        customer.CreatedDate, cohortIdentifier, 0);
                    dataPoints.Add(dataPoint);
                }
            }

            return dataPoints;
        }

        public string GetCohortIdentifierForDate(DateTime cohortDate)
        {
            var diff = cohortDate - _startDate;
            int weeksSinceStartDate = diff.Days / 7;
            DateTime weekStartDate = _startDate.AddDays(weeksSinceStartDate * 7);
            DateTime weekEndDate = weekStartDate.AddDays(6);
            return string.Format("{0}-{1}", weekStartDate.ToString("M/d"), weekEndDate.ToString("M/d"));
        }
    }
}
