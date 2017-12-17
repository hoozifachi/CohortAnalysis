using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDataModel
{
    public class Customer : ICustomer
    {
        public Customer(int customerId, DateTime createdDT)
        {
            Id = customerId;
            CreatedDate = createdDT;
            Orders = new List<IOrder>();
        }

        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<IOrder> Orders { get; set; }
    }
}
