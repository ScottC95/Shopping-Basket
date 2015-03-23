using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;

namespace QA
{
    public class ShoppingBasket
    {

        public List<OrderItem> Products { get; private set; }

        public ShoppingBasket()
        {
            Products = new List<OrderItem>();
        }

        /// <summary>
        /// If the product Value is the different and there is a quantity
        /// </summary>
        /// <param name="productName">Name of the Product</param>
        /// <param name="productValue">Value of the Product</param>
        /// <param name="quantity">Quantity of the Product</param>
        public void AddProduct(string productName, decimal productValue, int quantity)
        {
            for (int i = 0; i < NumberOfProducts; i++)
            {
                if (Products[i].ProductName == productName)
                {
                    Products[i].AddItems(productValue, quantity);
                    return;
                }
            }

            OrderItem item = new OrderItem(productName, productValue, quantity);
            Products.Add(item);
        }

        /// <summary>
        /// If there is no quanity 
        /// </summary>
        /// <param name="productName">Name of the Product</param>
        /// <param name="productValue">Value of the Product</param>
        public void AddProduct(string productName, decimal productValue)
        {
            for (int i = 0; i < NumberOfProducts; i++)
            {
                if (Products[i].ProductName == productName)
                {
                    Products[i].AddItems(productValue);
                }
            }

            OrderItem item = new OrderItem(productName, productValue);
            Products.Add(item);
        }




        public void RemoveProduct(string productName, int quantity)
        {
            for (int i = 0; i < NumberOfProducts; i++)
            {
                if (Products[i].ProductName == productName)
                {
                    Products[i].RemoveItems(quantity);
                    return;
                }
            }
            throw new InvalidOperationException(string.Format("Product {0} not found", productName));
        }

        public void RemoveProduct(string productName)
        {
            for (int i = 0; i < NumberOfProducts; i++)
            {
                if (Products[i].ProductName == productName)
                {
                    Products[i].RemoveItems();
                    return;
                }
            }
            throw new InvalidOperationException(string.Format("Product {0} not found", productName));
        }

        public void ClearBasket()
        {
            Products.Clear();
        }

        //returns the total quanity
        public int NumberOfItems
        {
            get
            {
                int stock = 0;
                foreach (OrderItem p in Products)
                {
                    stock += p.Quantity;
                }
                return stock;
            }
        }

        //returns the total value (Quantity * Value) 
        public decimal BasketTotal
        {
            get
            {
                decimal total = 0.0m;
                foreach (OrderItem p in Products)
                {
                    total += p.TotalOrder;
                }
                return total;
            }
        }

        //Returns how many different products
        public int NumberOfProducts
        {
            get
            {
                return Products.Count;
            }
        }

        public decimal CurrentPrice(string productName)
        {
            for (int i = 0; i < NumberOfProducts; i++)
            {
                if (Products[i].ProductName == productName)
                {
                    return Products[i].LatestPrice;
                }
            }
            throw new InvalidOperationException(string.Format("Product {0} not found", productName));
        }

        public bool IsProductInBasket(string productName)
        {
            for (int i = 0; i < NumberOfProducts; i++)
            {
                if (Products[i].ProductName == productName)
                {
                    return true;
                }
            }
            return false;
        }

        public void Edit(string productName, int newQuantity, decimal newLatestPrice)
        {
            for (int i = 0; i < Products.Count; i++)
            {
                if (Products[i].ProductName == productName)
                {
                    Products[i].EditItems(newQuantity, newLatestPrice);
                }
            }
            return;
        }

        public bool SaveBasket(string fileName)
        {

            if (System.IO.File.Exists(fileName))
            {
                if (!FileOpen(fileName))
                {
                    StreamWriter sw = new StreamWriter(fileName, true);
                    sw.AutoFlush = true;
                    foreach (OrderItem items in Products)
                    {
                        sw.WriteLine(items.ProductName);
                        sw.WriteLine(items.Quantity);
                        sw.WriteLine(items.LatestPrice);
                    }
                    sw.Close();
                    return true;
                }
                return false;
            }
            else
            {
                File.Create(fileName).Close();
                SaveBasket(fileName);
            }
            return true;
        }

        public bool LoadBasket(string filePath)
        {
            if (!FileOpen(filePath))
            {
                StreamReader sr = new StreamReader(filePath);
                List<string> lines = new List<string>();
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    lines.Add(line);
                }
                for (int i = 0; i < lines.Count - 1; i += 3)
                {
                    string name = lines[i], quantity = lines[i + 1], price = lines[i + 2];
                    int quantityInt;
                    int.TryParse(quantity, out quantityInt);
                    decimal priceDecimal;
                    decimal.TryParse(price, out priceDecimal);
                    AddProduct(name, priceDecimal, quantityInt);
                }
                sr.Close();
            }
            return false;
        }

        public bool FileOpen(string fileName)
        {
            FileStream stream = null;
            try
            {
                stream = File.Open(fileName, FileMode.Open);
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                if (stream != null) stream.Close();
            }

            return false;
        }
    }
}
