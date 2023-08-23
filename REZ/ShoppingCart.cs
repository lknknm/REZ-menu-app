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
        private static List<Product> orderProducts = new List<Product> { };
        public static List<Account> AccountsToDivide = new List<Account> { };
        // public AccountsList OpenAccounts;
        public static Account User = AccountsList.SelectedAccount;
        public double TotalPrice;
        public string OrderId;

        public static List<Product> OrderProducts
        {
            get { return orderProducts; }
            set { orderProducts = value; }
        }

       

        public ShoppingCart(string orderId)
        {
            OrderId = orderId;
            
        }

        public void AddItem(Product item)
        {
            bool itemFound = false;
            item.OrderId = OrderId;
            //int oldQuantity = item.Quantity;

            if (OrderProducts.Count == 0)
            {
                OrderProducts.Add(item);
            }
            else
            {
                foreach (Product orderProduct in orderProducts)
                {
                    if (item.Name == orderProduct.Name)
                    {
                        orderProduct.Quantity = item.Quantity;
                        itemFound = true;
                    }
                }

                if (itemFound == false)
                {
                    OrderProducts.Add(item);
                }
            
            }
            //Add to DB
        }
        public void RemoveItem(Product item)
        {
            OrderProducts.Remove(item);
            //Remove from DB
        }

        public static void DefineUser(Account user)
        {
            AccountsToDivide.Add(user);
        }

        public void SelectAccount(Account account)
        {
            AccountsToDivide.Add(account);
        }

        public void UnselectAccount(Account account)
        {
            AccountsToDivide.Remove(account);
        }

        public void CompleteOrder(List<Account> accountsToDivide, List<Product> orderItemsList)
        {
            int accountsQuantity = accountsToDivide.Count;

            foreach (Product item in orderItemsList) 
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
            dialog.PrimaryButtonClick += delegate { CompleteOrder(AccountsToDivide, OrderProducts); };
            dialog.CloseButtonText = "Cancelar";
            dialog.DefaultButton = ContentDialogButton.Primary;

            if (ShoppingCart.OrderProducts.Count > 0)
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
