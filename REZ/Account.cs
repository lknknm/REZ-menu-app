using System;
using System.Collections.ObjectModel;

namespace REZ
{
    public class Account
    {
        public string Name;
        private Collection<Product> itemsList;
        private double totalPrice = 0.0;
        
        public Account(string name)
        {
            Name = name;
            // TotalPrice = totalPrice;
        }

        public Collection<Product> ItemsList 
        {
            get { return itemsList; }
            set { itemsList = value; }
        }

        public double TotalPrice 
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }

        public void AddItem(Product item)
        {
            ItemsList.Add(item);
            totalPrice += item.Price;
        }
    }
}

