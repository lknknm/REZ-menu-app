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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace REZ
{
    public sealed partial class ShoppingCartModal : Page
    {
        public Account User = AccountsList.SelectedAccount;
        public List<Product> OrderProducts = ShoppingCart.OrderProducts;
        public List<Account> AccountsToDivide = ShoppingCart.AccountsToDivide;

        public ShoppingCartModal(ShoppingCart shoppingCart)
        {

            this.InitializeComponent();
            var groupedProducts = OrderProducts.GroupBy(p => p.SubCategory);
           
            double subtotal = SubtotalValueCalculator(OrderProducts);
            string subtotalValue = subtotal.ToString("0.00");
            double taxa = subtotal * 0.1;
            string taxaServico = taxa.ToString("0.00");
            double total = subtotal + taxa;
            string totalPrice = total.ToString("0.00");

            Subtotal.Text = $"R$ {subtotalValue}";
            Taxa.Text = $"R$ {taxaServico}";
            TotalPrice.Text = $"R$ {totalPrice}";
            myListView.Source = groupedProducts;
            DataContext = this;
        }


        public double SubtotalValueCalculator(List<Product> productsList)
        {
            double finalValue = 0.0;

            foreach(Product product in productsList)
            {
                finalValue += product.Price;
            }

            return finalValue;
        }


        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            ShoppingCart.AccountsToDivide = (List<Account>)comboBox.SelectedValue;
            
        }

        private void DeleteItem(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}