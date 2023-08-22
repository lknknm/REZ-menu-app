using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace REZ
{
    public class ShoppingCart
    {
        public List<Product> OrderProducts = new List<Product> { };
        public List<Account> accountsToDivide;
        public AccountsList Accounts;
        public double TotalPrice;
        public string OrderId;

        public ShoppingCart(string orderId, Account user)
        {
            OrderId = orderId;
            accountsToDivide = new List<Account> {user};
        }

        public void AddItem(Product item)
        {
            item.OrderId = OrderId;
            OrderProducts.Add(item);
            //Add to DB
        }
        public void RemoveItem(Product item)
        {
            OrderProducts.Remove(item);
            //Remove from DB
        }

        public void SelectAccount(Account account)
        {
            accountsToDivide.Add(account);
        }

        public void UnselectAccount(Account account)
        {
            accountsToDivide.Remove(account);
        }

        public void CompleteOrder(List<Account> accountsToDivide, Collection<Product> ItemsList, double TotalPrice, string OrderId)
        {
            int accountsQuantity = accountsToDivide.Count;

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

        public async void OpenShoppingModal(Page page, ShoppingCart cart, TeachingTip tt)
        {
            ContentDialog dialog = new ContentDialog();

            // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
            dialog.XamlRoot = page.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.PrimaryButtonText = "Fazer pedido";
            dialog.CloseButtonText = "Cancelar";
            dialog.DefaultButton = ContentDialogButton.Primary;

            if (cart.OrderProducts.Count > 0)
            {
                dialog.Content = new ShoppingCartModal(cart);
                var result = await dialog.ShowAsync();
            }
            else
            {
                tt.IsOpen = true;
            }
        }

    }
}
