using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QA;

namespace QATests
{
    [TestClass]
    public class ShoppingBasketTests
    {

        /// <summary>
        /// Testing adding new product with Quantity
        /// </summary>
        [TestMethod]
        public void NewProductWithQuantityTest()
        {
            // Assemble 
            ShoppingBasket Basket = new ShoppingBasket();
            string name = "Orange";
            decimal price = 0.50m;
            int quantity = 5;
            bool expected = true, actual = false;

            // Act
            Basket.AddProduct(name, price, quantity);
            if (Basket.Products[0].Quantity == 5)
            {
                actual = true;
            }

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Testing adding new product no quantity
        /// </summary>
        [TestMethod]
        public void NewProductNoQuantityTest()
        {
            // Assemble 
            ShoppingBasket Basket = new ShoppingBasket();
            string name = "Orange";
            decimal price = 0.50m;
            bool expected = true, actual = false;

            // Act
            Basket.AddProduct(name, price);
            if (Basket.Products[0].Quantity == 1)
            {
                actual = true;
            }

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Adding quantity
        /// </summary>
        [TestMethod]
        public void AddingQuantityTest()
        {
            // Assemble 
            ShoppingBasket Basket = new ShoppingBasket();
            string name = "Orange";
            decimal price = 0.50m;
            int addingQuantity = 4;
            bool expected = true, actual = false;
            Basket.AddProduct(name, price);
            // Act
            Basket.AddProduct(name, price, addingQuantity);
            if (Basket.Products[0].Quantity == 5)
            {
                actual = true;
            }

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Adding new price
        /// </summary>
        [TestMethod]
        public void AddingNewPriceTest()
        {
            // Assemble 
            ShoppingBasket Basket = new ShoppingBasket();
            string name = "Orange";
            decimal price = 0.50m, addingPrice = 1.00m;
            bool expected = true, actual = false;
            Basket.AddProduct(name, price);

            // Act
            Basket.AddProduct(name, addingPrice);
            if (Basket.Products[0].Quantity == 2 && Basket.Products[0].LatestPrice == 1.00m)
            {
                actual = true;
            }

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Removing Without Quantity 
        /// </summary>
        [TestMethod]
        public void RemoveWithoutQuantityTest()
        {
            // Assemble 
            ShoppingBasket Basket = new ShoppingBasket();
            string name = "Orange";
            decimal price = 0.50m;
            int quantity = 3;
            bool expected = true, actual = false;
            Basket.AddProduct(name, price, quantity);

            // Act
            Basket.RemoveProduct(name);
            if (Basket.Products[0].Quantity == 2)
            {
                actual = true;
            }

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Removing With Quantity
        /// </summary>
        [TestMethod]
        public void RemoveQuantityTest()
        {
            // Assemble 
            ShoppingBasket Basket = new ShoppingBasket();
            string name = "Orange";
            decimal price = 0.50m;
            int quantity = 3, removeQuantity = 2;
            bool expected = true, actual = false;
            Basket.AddProduct(name, price, quantity);

            // Act
            Basket.RemoveProduct(name, removeQuantity);
            if (Basket.Products[0].Quantity == 1)
            {
                actual = true;
            }

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Two Different Products in the List
        /// </summary>
        [TestMethod]
        public void TwoProductsInListTest()
        {
            // Assemble 
            ShoppingBasket Basket = new ShoppingBasket();
            string name = "Orange", name2 = "Apple";
            decimal price = 0.50m, price2 = 1.00m;
            int quantity = 3, quantity2 = 5;
            bool expected = true, actual = false;
            Basket.AddProduct(name, price, quantity);

            // Act
            Basket.AddProduct(name2, price2, quantity2);
            if (Basket.Products[0].ProductName == "Orange" && Basket.Products[1].ProductName == "Apple")
            {
                actual = true;
            }

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Testing list clearing
        /// </summary>
        [TestMethod]
        public void ClearingTest()
        {
            // Assemble 
            ShoppingBasket Basket = new ShoppingBasket();
            string name = "Orange", name2 = "Apple";
            decimal price = 0.50m, price2 = 1.00m;
            int quantity = 3, quantity2 = 5;
            bool expected = true, actual = false;
            Basket.AddProduct(name, price, quantity);
            Basket.AddProduct(name2, price2, quantity2);

            // Act
            Basket.ClearBasket();
            if (Basket.Products.Count == 0)
            {
                actual = true;
            }

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Number of items test
        /// </summary>
        [TestMethod]
        public void NumberOfItemsTest()
        {
            // Assemble 
            ShoppingBasket Basket = new ShoppingBasket();
            string name = "Orange", name2 = "Apple";
            decimal price = 0.50m, price2 = 1.00m;
            int quantity = 3, quantity2 = 5, expected = 8, actual = 0;
            Basket.AddProduct(name, price, quantity);
            Basket.AddProduct(name2, price2, quantity2);

            // Act
            actual = Basket.NumberOfItems;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Number of Products test
        /// </summary>
        [TestMethod]
        public void NumberOfProductsTest()
        {
            // Assemble 
            ShoppingBasket Basket = new ShoppingBasket();
            string name = "Orange", name2 = "Apple", name3 = "Bike";
            decimal price = 0.50m, price2 = 1.00m, price3 = 100.00m;
            int quantity = 3, quantity2 = 5, quantity3 = 2, expected = 3, actual = 0;
            Basket.AddProduct(name, price, quantity);
            Basket.AddProduct(name2, price2, quantity2);
            Basket.AddProduct(name3, price3, quantity3);

            // Act
            actual = Basket.NumberOfProducts;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// CurrentPrice test
        /// </summary>
        [TestMethod]
        public void CurrentPriceTest()
        {
            // Assemble 
            ShoppingBasket Basket = new ShoppingBasket();
            string name = "Orange", name2 = "Apple", name3 = "Bike";
            decimal price = 0.50m, price2 = 1.00m, price3 = 100.00m, expected = 1.00m, actual = 0m;
            int quantity = 3, quantity2 = 5, quantity3 = 2;
            Basket.AddProduct(name, price, quantity);
            Basket.AddProduct(name2, price2, quantity2);
            Basket.AddProduct(name3, price3, quantity3);

            // Act
            actual = Basket.CurrentPrice(name2);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// CurrentPrice test Incorrect Product
        /// </summary>
        [TestMethod]
        public void CurrentPriceIncorrectProductTest()
        {
            // Assemble 
            ShoppingBasket Basket = new ShoppingBasket();
            string name = "Orange", name2 = "Apple", name3 = "Bike";
            decimal price = 0.50m, price2 = 1.00m, price3 = 100.00m;
            int quantity = 3, quantity2 = 5, quantity3 = 2;
            Basket.AddProduct(name, price, quantity);
            Basket.AddProduct(name2, price2, quantity2);
            Basket.AddProduct(name3, price3, quantity3);

            // Act
            try
            {
                Basket.CurrentPrice("ABC");
            }
            catch (Exception ex)
            {
                // Assert
                Assert.IsTrue(ex is InvalidOperationException);
            }
        }

        /// <summary>
        /// IsProductInBasket test
        /// </summary>
        [TestMethod]
        public void IsProductInBasketTest()
        {
            // Assemble 
            ShoppingBasket Basket = new ShoppingBasket();
            string name = "Orange", name2 = "Apple", name3 = "Bike";
            decimal price = 0.50m, price2 = 1.00m, price3 = 100.00m;
            bool expected = true, actual = false;
            int quantity = 3, quantity2 = 5, quantity3 = 2;
            Basket.AddProduct(name, price, quantity);
            Basket.AddProduct(name2, price2, quantity2);
            Basket.AddProduct(name3, price3, quantity3);

            // Act
            actual = Basket.IsProductInBasket("Apple");

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
