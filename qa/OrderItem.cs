using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QA
{
    public class OrderItem
    {
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal LatestPrice { get; private set; }

        public decimal TotalOrder
        {
            get
            {
                return this.Quantity * this.LatestPrice;
            }
        }

        public OrderItem(string productName, decimal latestPrice)
        {
            this.ProductName = productName;
            this.LatestPrice = latestPrice;
            this.Quantity = 1;
        }

        public OrderItem(string productName, decimal latestPrice, int quantity)
        {
            this.ProductName = productName;
            this.LatestPrice = latestPrice;
            this.Quantity = quantity;
        }

        public int AddItems(decimal latestPrice, int quantity)
        {
            this.Quantity += quantity;
            this.LatestPrice = latestPrice;
            return this.Quantity;
        }

        public int AddItems(decimal latestPrice)
        {
            this.Quantity++;
            this.LatestPrice = latestPrice;
            return this.Quantity;
        }

        public int AddItems(int quantity)
        {
            this.Quantity += quantity;
            return this.Quantity;
        }

        
        public int AddItems()
        {
            this.Quantity++;
            return this.Quantity;
        }

        public int RemoveItems(int quantity)
        {
            this.Quantity -= quantity;
            if (this.Quantity < 0)
            {
                this.Quantity = 0;
            }
            return this.Quantity;
        }

        public int EditItems(int quantity, decimal price)
        {
            this.Quantity = quantity;
            this.LatestPrice = price;
            return this.Quantity;
        }

        public int RemoveItems()
        {
            this.Quantity--;
            if (this.Quantity < 0)
            {
                this.Quantity = 0;
            }
            return this.Quantity;
        }
    }
}
