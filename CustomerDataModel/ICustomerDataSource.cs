using System.Collections.Generic;

namespace CustomerDataModel
{
    public interface ICustomerDataSource
    {
        /// <summary>
        /// Returns a collections of customers with their corresponding orders.
        /// </summary>
        /// <returns></returns>
        ICollection<ICustomer> GetCustomers();
    }
}
