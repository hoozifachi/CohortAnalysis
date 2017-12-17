using System.Collections.Generic;
using System.Linq;

namespace CustomerDataModel
{
    /// <summary>
    /// This class loads customers and their orders from CSV files.
    /// </summary>
    public class CsvCustomerDataSource : ICustomerDataSource
    {
        private string _customerFile;
        private string _orderFile;

        public CsvCustomerDataSource(string customerFile, string orderFile)
        {
            _customerFile = customerFile;
            _orderFile = orderFile;
        }

        public ICollection<ICustomer> GetCustomers()
        {
            var customers = CustomerLoader.LoadCsvData(_customerFile);
            var orders = OrderLoader.LoadCsvData(_orderFile);

            // Associate orderw with the appropriate customer
            foreach (ICustomer customer in customers)
            {
                customer.Orders = orders.Where(o => o.CustomerId == customer.Id).ToList();
            }

            return customers;
        }
    }
}
