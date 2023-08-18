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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace REZ
{
    public class Product
    {
        public string Name { get; set; }
        public string SubCategory { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageSource { get; set; }
    }

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FoodMenu : Page
    {
        string filter = "Tudo";
        string jsonString;
        List<Product> products;

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
        
        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        }
        private async void ShoppingCartButtonClick(object sender, RoutedEventArgs e)
        {
            //ToggleThemeTeachingTip1.IsOpen = true;

            ContentDialog dialog = new ContentDialog();

            // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
            dialog.XamlRoot = this.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            //dialog.PrimaryButtonClick += this.AddToCart;
            dialog.PrimaryButtonText = "Fazer pedido";
            dialog.CloseButtonText = "Cancelar";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = new ShoppingCartModal();

            var result = await dialog.ShowAsync();
        }

        private async void ToggleListTip(object sender, ItemClickEventArgs e)
        {
            ContentDialog dialog = new ContentDialog();
            var item = (e.ClickedItem as Product);

            // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
            dialog.XamlRoot = this.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = item.Name;
            //dialog.PrimaryButtonClick += this.AddToCart;
            dialog.PrimaryButtonText = "Adicionar";
            dialog.CloseButtonText = "Cancelar";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = new ItemDialogModal(item);

            var result = await dialog.ShowAsync();
        }
        private void myListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle selection change event here if needed
        }

        private void AddToCart()
        {

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
