using System;
using System.Collections.ObjectModel;

namespace REZ
{
    public class Account
    {
        public string Name;
        private Collection<Item> itemsList;
        private double totalPrice = 0.0;
        
        public Account(string name)
        {
            Name = name;
            // TotalPrice = totalPrice;
        }

        public Collection<Item> ItemsList 
        {
            get { return itemsList; }
            set { itemsList = value; }
        }

        public double TotalPrice 
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }

        // public void MoveItemToAnotherAccount(Account originAccount, Account DestinyAccount)
        // {
                         // Não sei se isso é necessário
        // }

        public void AddItem(Item item)
        {
            ItemsList.Add(item);
            totalPrice += item.Price;
        }
    }
}

