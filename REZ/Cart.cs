using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace REZ
{
    public class Cart
    {
        Collection<Item> ItemsList;
        Collection<Account> accountsToDivide;
        AccountsList Accounts;
        double TotalPrice;
        string OrderId;

        public Cart(string orderId)
        {
            OrderId = orderId;
        }

        public void AddItem(Item item)
        {
            item.OrderId = OrderId;
            ItemsList.Add(item);
        }
        public void RemoveItem(Item item)
        {
            ItemsList.Remove(item);
        }

        public void SelectAccount(Account account)
        {
            accountsToDivide.Add(account);
        }

        public void UnselectAccount(Account account)
        {
            accountsToDivide.Remove(account);
        }

        public void CompleteOrder(Account[] accountsToDivide, Collection<Item> ItemsList, double TotalPrice, string OrderId)
        {
            int accountsQuantity = accountsToDivide.Length;

            foreach (Item item in ItemsList) 
            {
                item.DivideItemPrice(accountsToDivide, accountsQuantity);
            
                foreach (Account account in accountsToDivide)
                {
                    Item newItem = item.Clone() as Item;
                    account.AddItem(newItem);
                }
            }

            
        }

    }
}
