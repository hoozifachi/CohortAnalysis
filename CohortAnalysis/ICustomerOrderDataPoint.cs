using System;

namespace CohortAnalysis
{
    public interface ICustomerOrderDataPoint
    {
        DateTime CohortDate { get; set; }
        string CohortIdentifier { get; set; }
        int CohortPeriod { get; set; }
        int Id { get; set; }
        DateTime? OrderDate { get; set; }
    }
}