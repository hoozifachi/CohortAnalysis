using System;
using System.Collections.Generic;

namespace CustomerDataModel
{
    public interface ICustomer
    {
        DateTime CreatedDate { get; set; }
        int Id { get; set; }
        ICollection<IOrder> Orders { get; set; }
    }
}