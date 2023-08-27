using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;
using System.Diagnostics;
using System.Security.Principal;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace REZ
{
    public sealed partial class ShoppingCartModal : Page
    {
        public ContentDialog Dialog;
        public Account User = AccountsList.SelectedAccount;
        public List<Account> CreatedAccounts = AccountsList.Accounts;
        public List<Product> OrderProducts = ShoppingCart.OrderProducts;
        public List<Account> AccountsToDivide = ShoppingCart.AccountsToDivide;

        //----------------------------------------------------------------------------
        public ShoppingCartModal(ShoppingCart shoppingCart, ContentDialog dialog)
        {

            this.InitializeComponent();
            Greetings.Text = $"Hello, {User.Name}!";
            Dialog = dialog;
            var groupedProducts = OrderProducts.GroupBy(p => p.SubCategory);
            UpdatePrice(OrderProducts);
            myListView.Source = groupedProducts;
            AccountsComboBox.ItemsSource = CreatedAccounts;
            DataContext = this;
        }

        //----------------------------------------------------------------------------
        private void AddAccountToSplit(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            
            foreach (Account account in CreatedAccounts)
            {
                if (account.Name == (string)checkBox.Content && account.CPF == (string)checkBox.Tag && account.IsEnabled == true)
                {
                    AccountsToDivide = ShoppingCart.AddSplitAccount(account);
                    break;
                }
            }
            
        }

        //----------------------------------------------------------------------------
        private void RemoveAccountToSplit(object sender, RoutedEventArgs e)
        {
            CheckBox comboBox = sender as CheckBox;

            foreach (Account account in ShoppingCart.AccountsToDivide)
            {
                if (account.Name == (string)comboBox.Content && account.CPF == (string)comboBox.Tag)
                {
                    AccountsToDivide = ShoppingCart.AddSplitAccount(account);
                    break;
                }
            }
        }

        //----------------------------------------------------------------------------
        public void UpdatePrice(List<Product> productsList)
        {
            double subtotal = SubtotalValueCalculator(productsList);
            string subtotalValue = subtotal.ToString("0.00");
            double taxa = subtotal * 0.1;
            string taxaServico = taxa.ToString("0.00");
            double total = subtotal + taxa;
            string totalPrice = total.ToString("0.00");
            Subtotal.Text = $"R$ {subtotalValue}";
            Taxa.Text = $"R$ {taxaServico}";
            TotalPrice.Text = $"R$ {totalPrice}";
        }

        //----------------------------------------------------------------------------
        public double SubtotalValueCalculator(List<Product> productsList)
        {
            double finalValue = 0.0;

            foreach(Product product in productsList)
            {
                finalValue += product.Price;
            }

            return finalValue;
        }

        //----------------------------------------------------------------------------
        public List<Account> GetAvailableUsers(List<Account> createdAccounts)
        {
            List<Account> availableUsers = new List<Account>();
            foreach (var account in createdAccounts)
            {
                if (User.CPF != account.CPF)
                {
                    availableUsers.Add(account);
                }
                
            }

            return availableUsers;
        }

        //----------------------------------------------------------------------------
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            ShoppingCart.AccountsToDivide = (List<Account>)comboBox.SelectedValue;
            //Cart.select
            //Cart.unselect
            
        }

        //----------------------------------------------------------------------------
        public void DeleteItem(object sender, RoutedEventArgs e)
        {
            Button deleteButton = sender as Button;
            if (deleteButton != null)
            {
                Product productToDelete = deleteButton.DataContext as Product;
                if (productToDelete != null)
                {
                    OrderProducts = ShoppingCart.RemoveItem(productToDelete);
                    var groupedProducts = OrderProducts.GroupBy(p => p.SubCategory);
                    myListView.Source = groupedProducts;
                    UpdatePrice(OrderProducts);

                    if (OrderProducts.Count == 0)
                    {
                        Dialog.Hide();
                    }

                }

            }

        }

    }
}