using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CustomerDataModel.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCreateCustomer()
        {
            var customerId = 1000;
            var createdDT = DateTime.Now;
            ICustomer customer = new Customer(customerId, createdDT);

            Assert.AreEqual(customerId, customer.Id);
            Assert.AreEqual(createdDT, customer.CreatedDate);
            Assert.AreEqual(0, customer.Orders.Count);
        }

        [TestMethod]
        public void TestAddOrders()
        {
            var customerId = 1000;
            var createdDT = DateTime.Now;
            ICustomer customer = new Customer(customerId, createdDT);

            int orderId = 2000;
            int orderNumber = 1;
            DateTime orderDT = DateTime.Now;
            IOrder order = new Order(orderId, orderNumber, customerId, orderDT);
            customer.Orders.Add(order);

            Assert.AreEqual(1, customer.Orders.Count);

            orderId = 3000;
            orderNumber = 2;
            orderDT = DateTime.Now;
            order = new Order(orderId, orderNumber, customerId, orderDT);
            customer.Orders.Add(order);

            Assert.AreEqual(2, customer.Orders.Count);

        }
    }
}
