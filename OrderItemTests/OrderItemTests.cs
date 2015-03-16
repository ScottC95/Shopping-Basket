using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QA;

namespace QATests
{
    [TestClass]
    public class OrderItemTests
    {
        /// <summary>
        /// Testing adding new product with Quantity
        /// </summary>
        [TestMethod]
        public void NewProductWithQuantityTest()
        {
            // Assemble 
            string name = "Orange";
            decimal price = 0.50m;
            int quantity = 5;
            bool expected = true, actual = false;

            // Act 
            OrderItem Actual = new OrderItem(name, price, quantity);
            if (Actual.ProductName == name)
            {
                actual = true;
            }

            // Assert
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        /// Testing without quantity
        /// </summary>
        [TestMethod]
        public void NewProductNoQuantityTest()
        {
            // Assemble 
            string name = "Orange";
            decimal price = 0.50m;
            bool expected = true, actual = false;

            // Act 
            OrderItem Actual = new OrderItem(name, price);
            if (Actual.ProductName == name && Actual.LatestPrice == price && Actual.Quantity == 1)
            {
                actual = true;
            }

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Adding one item without changing the value
        /// </summary>
        [TestMethod]
        public void AddOneItemTest()
        {
            // Assemble 
            string name = "Orange";
            decimal price = 0.50m;
            int quantity = 5;
            bool expected = true, actual = false;
            OrderItem Actual = new OrderItem(name, price, quantity);

            // Act 
            Actual.AddItems();
            if (Actual.Quantity == quantity + 1)
            {
                actual = true;
            }
            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Adding two items with quantity
        /// </summary>
        [TestMethod]
        public void AddTwoItemsTest()
        {
            // Assemble 
            string name = "Orange";
            decimal price = 0.50m;
            int quantity = 5, addingQuantity = 2;
            bool expected = true, actual = false;
            OrderItem Actual = new OrderItem(name, price, quantity);

            // Act 
            Actual.AddItems(addingQuantity);
            if (Actual.Quantity == quantity + addingQuantity)
            {
                actual = true;
            }
            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Adding two items with quantity with different LatestPrice
        /// </summary>
        [TestMethod]
        public void AddTwoItemsDifferentLatestPriceTest()
        {
            // Assemble 
            string name = "Orange";
            decimal price = 0.50m, addingPrice = 1.0m;
            int quantity = 5, addingQuantity = 2;
            bool expected = true, actual = false;
            OrderItem Actual = new OrderItem(name, price, quantity);

            // Act 
            Actual.AddItems(addingPrice, addingQuantity);
            if (Actual.Quantity == quantity + addingQuantity && Actual.LatestPrice == addingPrice)
            {
                actual = true;
            }
            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Removing 1 Item test
        /// </summary>
        [TestMethod]
        public void RemoveItemTest()
        {
            // Assemble 
            string name = "Orange";
            decimal price = 0.50m;
            int quantity = 5;
            bool expected = true, actual = false;
            OrderItem Actual = new OrderItem(name, price, quantity);

            // Act 
            Actual.RemoveItems();
            if (Actual.Quantity == 4)
            {
                actual = true;
            }
            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Removing 3 Items test
        /// </summary>
        [TestMethod]
        public void RemoveItemsTest()
        {
            // Assemble 
            string name = "Orange";
            decimal price = 0.50m;
            int quantity = 5, removeQuantity = 3;
            bool expected = true, actual = false;
            OrderItem Actual = new OrderItem(name, price, quantity);

            // Act 
            Actual.RemoveItems(removeQuantity);
            if (Actual.Quantity == 2)
            {
                actual = true;
            }
            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Removing more items than quantity
        /// </summary>
        [TestMethod]
        public void RemoveMoreItemsThanQuantityTest()
        {
            // Assemble 
            string name = "Orange";
            decimal price = 0.50m;
            int quantity = 5, removeQuantity = 6;
            bool expected = true, actual = false;
            OrderItem Actual = new OrderItem(name, price, quantity);

            // Act 
            Actual.RemoveItems(removeQuantity);
            if (Actual.Quantity == 0)
            {
                actual = true;
            }
            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Testing TotalOrder with adding
        /// </summary>
        [TestMethod]
        public void TotalOrderTest()
        {
            // Assemble 
            string name = "Orange";
            decimal price = 0.50m;
            int quantity = 5;
            bool expected = true, actual = false;
            OrderItem Actual = new OrderItem(name, price, quantity);

            // Act 
            Actual.AddItems();
            if (Actual.TotalOrder == 3.00m)
            {
                actual = true;
            }
            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
