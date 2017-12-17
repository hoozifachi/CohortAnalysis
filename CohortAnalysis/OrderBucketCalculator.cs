namespace CohortAnalysis
{
    public class OrderBucketCalculator
    {
        public static ICustomerOrderDataPoint CalculateBucket(ICustomerOrderDataPoint dataPoint)
        {
            if (dataPoint.OrderDate.HasValue)
            {
                var diff = dataPoint.OrderDate - dataPoint.CohortDate;
                dataPoint.CohortPeriod = (diff.Value.Days / 7) + 1; 
            }
            return dataPoint;
        }
    }
}
