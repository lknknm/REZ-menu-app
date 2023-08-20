using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace REZ
{
    public class Cart
    {
        Collection<Product> ItemsList;
        Collection<Account> accountsToDivide;
        AccountsList Accounts;
        double TotalPrice;
        string OrderId;

        public Cart(string orderId)
        {
            OrderId = orderId;
        }

        public void AddItem(Product item)
        {
            item.OrderId = OrderId;
            ItemsList.Add(item);
        }
        public void RemoveItem(Product item)
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

        public void CompleteOrder(Account[] accountsToDivide, Collection<Product> ItemsList, double TotalPrice, string OrderId)
        {
            int accountsQuantity = accountsToDivide.Length;

            foreach (Product item in ItemsList) 
            {
                item.DivideItemPrice(accountsToDivide, accountsQuantity);
            
                foreach (Account account in accountsToDivide)
                {
                    Product newItem = item.Clone() as Product;
                    account.AddItem(newItem);
                }
            }

            
        }

    }
}
