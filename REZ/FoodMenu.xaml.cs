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

namespace REZ
{
    public sealed partial class FoodMenu : Page
    {
        private string filter = "Tudo";
        private string jsonString;
        private Account User = AccountsList.SelectedAccount;
        public ShoppingCart Cart = AccountsList.Cart;
        public List<Product> products;

        public object ItemFeatureImage { get; private set; }

        public FoodMenu()
        {
            this.InitializeComponent();

            User = AccountsList.SelectedAccount;

            var jsonFilePath = Path.Combine(AppContext.BaseDirectory, "Properties", "Products.json");
            StreamReader reader = new(jsonFilePath);

            jsonString = reader.ReadToEnd();
            products = JsonConvert.DeserializeObject<List<Product>>(jsonString);
            var groupedProducts = products.GroupBy(p => p.SubCategory);
            myListView.Source = groupedProducts;
            DataContext = this;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Button selectedButton = new Button() { Content = "Tudo" };
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
                dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
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
            // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
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
                button.ClearValue(Button.StyleProperty); ;
            };

            clickedButton.Style = (Style)Resources["AccentButtonStyle"];

            products = JsonConvert.DeserializeObject<List<Product>>(jsonString);
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

    }
}
