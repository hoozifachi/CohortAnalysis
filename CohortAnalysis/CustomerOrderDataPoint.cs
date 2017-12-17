using System;

namespace CohortAnalysis
{
    public class CustomerOrderDataPoint : ICustomerOrderDataPoint
    {
        public CustomerOrderDataPoint(int id, DateTime? orderDate, DateTime cohortDate,
            string cohortIdentifier, int cohortPeriod)
        {
            Id = id;
            OrderDate = orderDate;
            CohortDate = cohortDate;
            CohortIdentifier = cohortIdentifier;
            CohortPeriod = cohortPeriod;
        }

        public int Id { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime CohortDate { get; set; }
        public string CohortIdentifier { get; set; }
        public int CohortPeriod { get; set; }

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3},{4}", Id, OrderDate,
                CohortDate, CohortIdentifier, CohortPeriod);
        }
    }
}
