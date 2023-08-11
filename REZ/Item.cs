using System;

namespace REZ
{
    public class Item
    {
        string image;
        double price;
        string OrderId;
        string name;
        string description;
        string itemType; //food, drink, dessert, combo, etc
        public Item()
        {

        }

        public void DivideItemPrice(Account[] accountsToDivide, Item item)
        {

        }
    }

    public class Food : Item
    {
        
    }

    public class Drink : Item
    {

    }
}

