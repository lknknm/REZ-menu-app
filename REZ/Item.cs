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

    //Pensei em criar sub-classes ao invés de um atributo 'type', mas não sei se faz sentido, deixo aqui para discussão
    // public class Food : Item
    // {
        
    // }

    // public class Drink : Item
    // {

    // }
}

