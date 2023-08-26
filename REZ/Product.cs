using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace REZ
{
    public class Product : ICloneable
    {
        private string imageSource;
        private double price;
        private string orderId;
        private string name;
        private string description;
        private string category;
        private string subCategory;
        private int quantity;

        public string OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }
        public string ImageSource
        {
            get { return imageSource; }
            set { imageSource = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Category
        {
            get { return category; }
            set { category = value; }
        }
        public string SubCategory
        {
            get { return subCategory; }
            set { subCategory = value; }
        }
        public double Price
        {
            get { return quantity > 0? price * quantity : price; }
            set { price = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        
        //----------------------------------------------------------------------------
        public Product(string name, string description, double price, string imageSource, string category)
        {
            Quantity = 0;
            Name = name;
            Description = description;
            Price = price;
            ImageSource = imageSource;
            Category = category;
            

            //Criar items no DB

        }

        //----------------------------------------------------------------------------
        public object Clone()
        {
            return new Product(this.Name, this.Description, this.Price, this.imageSource, this.Category)
            {
                SubCategory = this.SubCategory,
                Quantity = this.Quantity
            };
        }

        //----------------------------------------------------------------------------
        public void RemoveItemFromCart()
        {
            Quantity = 0;
        }

        //----------------------------------------------------------------------------
        public double DivideItemPrice(List<Account> accountsToDivide, int accountsQuantity)
        {
            double valueForEachAccount = this.Price/accountsQuantity;

            foreach (Account account in accountsToDivide)
            {
                account.TotalPrice += valueForEachAccount;
            }
            return valueForEachAccount;
        }

    }
}

