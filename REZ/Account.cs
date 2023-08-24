using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace REZ
{
    public class Account
    {
        public string Name;
        private List<Product> itemsList;
        private double totalPrice = 0.0;
        
        

        public List<Product> ItemsList 
        {
            get { return itemsList; }
            set { itemsList = value; }
        }

        public Account(string name)
        {
            itemsList = new List<Product> { };
            Name = name;
            // TotalPrice = totalPrice;
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

