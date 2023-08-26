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
        public string CPF;
        private List<Product> itemsList;
        private double totalPrice = 0.0;
        
        public List<Product> ItemsList 
        {
            get { return itemsList; }
            set { itemsList = value; }
        }

        public double TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }

        //----------------------------------------------------------------------------
        public Account(string name, string cpf)
        {
            itemsList = new List<Product> { };
            Name = name;
            CPF = cpf;
            AccountsList.SelectedAccount = this;
        }

        //----------------------------------------------------------------------------
        public void AddItem(Product item)
        {
            ItemsList.Add(item);
            totalPrice += item.Price;
        }
    }
}

