using System;

namespace CustomerDataModel
{
    public class Order : IOrder
    {
        public Order(int orderId, int orderNumber, int customerId, DateTime createdDT)
        {
            Id = orderId;
            OrderNumber = orderNumber;
            CustomerId = customerId;
            CreatedDate = createdDT;
        }

        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
