using System;
using System.Collections.ObjectModel;

namespace REZ
{
    public class Item : ICloneable
    {
        public string Image;
        public double Price;
        private string orderId;
        public string Name;
        public string Description;
        public string Type; //food, drink, dessert, combo, etc

        public string OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }
        public Item(string name, string description, double price, string image, string type)
        {
            Name = name;
            Description = description;
            Price = price;
            Image = image;
            Type = type;
            //Criar items no DB

        }

        public object Clone()
        {
            throw new NotImplementedException();
        }

        public void DivideItemPrice(Account[] accountsToDivide, int accountsQuantity)
        {
            double valueForEachAccount = this.Price/accountsQuantity;

            foreach (Account account in accountsToDivide)
            {
                account.TotalPrice += valueForEachAccount;
            }
        }
    }

    //Pensei em criar sub-classes ao invés de um atributo 'type', mas não sei se faz sentido, deixo aqui para discussão
    // public class Food : Item
    // {
        
    // }

    // public class Drink : Item
    // {

    // }
}

