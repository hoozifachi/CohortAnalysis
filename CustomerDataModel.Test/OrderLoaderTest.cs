using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CustomerDataModel.Test
{
    [TestClass]
    public class OrderLoaderTest
    {
        [TestMethod]
        public void TestParseOrderCsv()
        {
            string orderData = @"id,order_number,user_id,created
1709,36,344,2014-10-28 00:20:01
1406,7,608,2014-10-14 23:44:53
1716,6,2296,2014-10-28 17:47:07
";

            ICollection<IOrder> orders = OrderLoader.ParseCsvData(orderData);

            Assert.AreEqual(3, orders.Count);

            var ordersEnumerator = orders.GetEnumerator();
            ordersEnumerator.MoveNext();
            var order = ordersEnumerator.Current;
            Assert.AreEqual(1709, order.Id);
            Assert.AreEqual(36, order.OrderNumber);
            Assert.AreEqual(344, order.CustomerId);
            Assert.AreEqual(DateTime.Parse("2014-10-28 00:20:01"), order.CreatedDate);
        }
    }
}
