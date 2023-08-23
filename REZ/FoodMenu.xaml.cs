using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Newtonsoft.Json;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI;
using Windows.UI.Popups;
using Windows.Foundation;
using Microsoft.UI.Xaml.Input;
using System.Security.Claims;
using Microsoft.UI.Xaml.Navigation;
using System.Security;
using static System.Net.Mime.MediaTypeNames;
using Application = Microsoft.UI.Xaml.Application;

namespace REZ
{
    public sealed partial class FoodMenu : Page
    {
        private string filter = "Tudo";
        private Account User = AccountsList.SelectedAccount;
        public ShoppingCart Cart = AccountsList.Cart;

        public object ItemFeatureImage { get; private set; }

        public FoodMenu()
        {
            this.InitializeComponent();

            User = AccountsList.SelectedAccount;
            var groupedProducts = MainPage.Products.GroupBy(p => p.SubCategory);
            myListView.Source = groupedProducts;
            DataContext = this;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Button selectedButton = new Button() { Content = "Tudo" };
            
            if (e.Parameter is Button)
            {
                Button button = e.Parameter as Button;
                filter = button.Name;

                foreach (Button foodMenuButton in CategoriesButtonRow.Children)
                {
                    if ((string)foodMenuButton.Content == filter)
                    {
                        selectedButton = foodMenuButton;
                        break;
                    }

                };

                FilterByCategory(selectedButton, filter);
            }
            if (e.Parameter is string)
            {
                string searchText = e.Parameter as string;
                SearchBar.Text = searchText;
                FilterBySearch(searchText);
            }
            else
            {
                dynamic parameters = e.Parameter as dynamic;
                AutoSuggestBox autoSuggestBox = parameters.Sender as AutoSuggestBox;
                AutoSuggestBoxTextChangedEventArgs args = parameters.Args as AutoSuggestBoxTextChangedEventArgs;
                AutoSuggestBox_TextChanged(autoSuggestBox, args);
            }
            

        }

        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Frame.Navigate(typeof(MainPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
            }
            catch (Exception ex)
            {
                ContentDialog dialog = new ContentDialog();
                dialog.XamlRoot = this.XamlRoot;
                dialog.Style = Microsoft.UI.Xaml.Application.Current.Resources["DefaultContentDialogStyle"] as Style;
                dialog.DefaultButton = ContentDialogButton.Primary;
                dialog.Content = ex;
                var result = await dialog.ShowAsync();
            }
            
        }

        private void ShoppingCart_ButtonClick(object sender, RoutedEventArgs e)
        {

            Cart.OpenShoppingModal(this, Cart, ToggleThemeTeachingTip1);

        }

        private async void ToggleListTip(object sender, ItemClickEventArgs e)
        {
            ContentDialog dialog = new ContentDialog();
            var item = e.ClickedItem as Product;

            dialog.XamlRoot = this.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = item.Name;
            dialog.PrimaryButtonText = "Adicionar";
            dialog.PrimaryButtonClick += delegate { Cart.AddItem(item); };
            
            dialog.CloseButtonText = "Cancelar";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = new ItemDialogModal(item);

            var result = await dialog.ShowAsync();
        }


        private void MyListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle selection change event here if needed
        }

        private void FilterByCategory_ButtonClick(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            filter = (sender as Button)?.Content?.ToString();
            FilterByCategory(clickedButton, filter);

        }


        private void FilterByCategory(Button clickedButton, string filter)
        {
            foreach (Button button in CategoriesButtonRow.Children)
            {
                button.ClearValue(Button.StyleProperty);
            };

            clickedButton.Style = (Style)Resources["AccentButtonStyle"];

            List<Product> products = MainPage.Products;
            if (filter != "Tudo")
            {
                products = products.FindAll(p => p.Category == filter);
            }
            var groupedProducts = products.GroupBy(p => p.SubCategory);
            myListView.Source = groupedProducts;
        }

        private void SwitchUser_ButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            AccountsList.SwitchAccounts(button.Content.ToString());
        }

        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput) 
            {
                FilterBySearch(sender.Text);
            }
        }

        private void FilterBySearch(string searchText)
        {
            List<Product> products = MainPage.Products;

            if (searchText.Length > 0)
            {
                products = products.Where(x => x.Name.ToLower().Contains(searchText.ToLower())).ToList();
            }

            var groupedProducts = products.GroupBy(p => p.SubCategory);
            myListView.Source = groupedProducts;
            
        }

        

    }
}
