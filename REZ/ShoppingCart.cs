using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Diagnostics;

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
            if (User != null) 
            {
                
                Debug.WriteLine($"[ShoppingCart] User: {User.Name}");
            }
            else
            {
                Debug.WriteLine($"[ShoppingCart] User is null");
            }
            
            
        }

        public static void SwitchAccount(Account user)
        {
            AccountsToDivide.Clear();
            AccountsToDivide.Add(user);
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
        public static List<Product> RemoveItem(Product item)
        {
            OrderProducts.Remove(item);
            item.RemoveItemFromCart();
            return OrderProducts;
            //Remove from DB
        }

        public static void DefineUser(Account user)
        {
            User = user;
        }

        public List<Account> SelectAccount(Account account)
        {
            AccountsToDivide.Add(account);
            return AccountsToDivide;
        }

        public List<Account> UnselectAccount(Account account)
        {
            AccountsToDivide.Remove(account);
            return AccountsToDivide;
        }

        public void CompleteOrder(List<Account> accountsToDivide, List<Product> orderItemsList)
        {
            int accountsQuantity = accountsToDivide.Count;
            
            foreach (Product item in orderItemsList) 
            {
                double valueForEach = item.DivideItemPrice(accountsToDivide, accountsQuantity);
                foreach (Account account in accountsToDivide)
                {

                    bool addNewItem = true;
                    foreach (Product itemInAccount in account.ItemsList)
                    {
                        if (item.Name == itemInAccount.Name)
                        {
                            itemInAccount.Quantity += item.Quantity;
                            addNewItem = false;
                        }

                    }

                    if (addNewItem)
                    {
                        Product newItem = item.Clone() as Product;
                        newItem.Price = valueForEach;
                        account.AddItem(newItem);
                    }

                }
                item.RemoveItemFromCart();

            }
            AccountInfo.ProductsList = AccountsList.SelectedAccount.ItemsList;
            orderItemsList.Clear();
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
                dialog.Content = new ShoppingCartModal(cart, dialog);
                var result = await dialog.ShowAsync();
            }
            else
            {
                tt.IsOpen = true;
            }
        }

    }
}
