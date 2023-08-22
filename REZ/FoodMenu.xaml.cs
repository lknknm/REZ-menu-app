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
        private static Account User = new("Andres");
        public ShoppingCart Cart = new("1223", User);
        public List<Product> products;

        public object ItemFeatureImage { get; private set; }

        public FoodMenu()
        {
            this.InitializeComponent();

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
            base.OnNavigatedTo(e);

            if (e.Parameter is ShoppingCart shoppingCart)
            {
                Cart = shoppingCart;
            }
        }

        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Frame.Navigate(typeof(MainPage), Cart, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
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

        private void ShoppingCartButtonClick(object sender, RoutedEventArgs e)
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


        private void myListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle selection change event here if needed
        }

        private void FilterByCategory(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            // Reset all buttons to normal background
            foreach (Button button in CategoriesButtonRow.Children)
            {
                button.ClearValue(Button.StyleProperty); ;
            };
            clickedButton.Style = (Style)Resources["AccentButtonStyle"];

            filter = (sender as Button)?.Content?.ToString();
            products = JsonConvert.DeserializeObject<List<Product>>(jsonString);
            if (filter != "Tudo")
            {
                products = products.FindAll(p => p.Category == filter);
            }
            var groupedProducts = products.GroupBy(p => p.SubCategory);
            myListView.Source = groupedProducts;
        }

    }
}
