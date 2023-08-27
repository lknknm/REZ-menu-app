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
        public static string OrderId = "0";

        public static List<Product> OrderProducts
        {
            get { return orderProducts; }
            set { orderProducts = value; }
        }

        //----------------------------------------------------------------------------
        public ShoppingCart()
        {
            OrderId = (int.Parse(OrderId) + 1).ToString();

            if (User != null) 
            {
                
                Debug.WriteLine($"[ShoppingCart] User: {User.Name}");
            }
            else
            {
                Debug.WriteLine($"[ShoppingCart] User is null");
            }
        }

        //----------------------------------------------------------------------------
        public static void SwitchAccount(Account user)
        {
            AccountsToDivide.Clear();
            AccountsToDivide.Add(user);
        }

        //----------------------------------------------------------------------------
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

        //----------------------------------------------------------------------------
        public static List<Product> RemoveItem(Product item)
        {
            OrderProducts.Remove(item);
            item.RemoveItemFromCart();
            return OrderProducts;
            //Remove from DB
        }

        //----------------------------------------------------------------------------
        public static void DefineUser(Account user)
        {
            User = user;
        }

        //----------------------------------------------------------------------------
        public static List<Account> AddSplitAccount(Account account)
        {
            AccountsToDivide.Add(account);
            return AccountsToDivide;
        }

        //----------------------------------------------------------------------------
        public static List<Account> RemoveSplitAccount(Account account)
        {
            AccountsToDivide.Remove(account);
            return AccountsToDivide;
        }

        //----------------------------------------------------------------------------
        public void CompleteOrder(List<Account> accountsToDivide, List<Product> orderItemsList)
        {
            int accountsQuantity = accountsToDivide.Count;
            
            foreach (Product item in orderItemsList) 
            {
                double valueForEach = item.DivideItemPrice(accountsToDivide, accountsQuantity);

                Debug.WriteLine($"contas para dividir: {accountsToDivide.Count}");
                foreach (Account account in accountsToDivide)
                {
                    Product newItem = item.Clone() as Product;
                    //newItem.Price = valueForEach;

                    bool addNewItem = true;
                    
                    foreach (Product itemInAccount in account.ItemsList)
                    {
                        if (newItem.Name == itemInAccount.Name)
                        {
                            itemInAccount.Quantity += newItem.Quantity;
                            itemInAccount.FinalPrice += valueForEach;
                            addNewItem = false;
                        }

                    }

                    if (addNewItem)
                    {
                        newItem.FinalPrice += valueForEach;
                        account.AddItem(newItem);
                    }

                    Debug.WriteLine($"adddNewItem: {addNewItem}");
                    Debug.WriteLine($"items na conta: {account.ItemsList.Count}");
                }

                item.RemoveItemFromCart();

            }
            AccountInfo.ProductsList = AccountsList.SelectedAccount.ItemsList;
            orderItemsList.Clear();
        }

        //----------------------------------------------------------------------------
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
