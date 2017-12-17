using System;

namespace CustomerDataModel
{
    public interface IOrder
    {
        int Id { get; set; }
        int OrderNumber { get; set; }
        int CustomerId { get; set; }
        DateTime CreatedDate { get; set; }
    }
}
