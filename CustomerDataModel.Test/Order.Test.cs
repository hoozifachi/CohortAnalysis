using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CustomerDataModel.Test
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestCreateOrder()
        {
            int orderId = 2000;
            int orderNumber = 1;
            int customerId = 1000;
            DateTime createdDT = DateTime.Now;
            IOrder order = new Order(orderId, orderNumber, customerId, createdDT);

            Assert.AreEqual(orderId, order.Id);
            Assert.AreEqual(orderNumber, order.OrderNumber);
            Assert.AreEqual(customerId, order.CustomerId);
            Assert.AreEqual(createdDT, order.CreatedDate);
        }
    }
}
