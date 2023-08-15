using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Newtonsoft.Json;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace REZ
{
    public class Product
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string SubCategory {get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FoodMenu : Page
    {
        string filter = "Tudo";
        string jsonString;
        List<Product> products;

        public FoodMenu()
        {
            this.InitializeComponent();

            string jsonFilePath = "C:/Users/USER/Documents/Microsoft-Course/projeto/REZ-apresentacao/REZ-menu-app/REZ/Properties/Products.json";
            StreamReader reader = new(jsonFilePath);
            jsonString = reader.ReadToEnd();
            products = JsonConvert.DeserializeObject<List<Product>>(jsonString);
            var groupedProducts = products.GroupBy(p => p.Category);
            myListView.Source = groupedProducts;

        }
        
        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        }
        private void TestButtonClick1(object sender, RoutedEventArgs e)
        {
            ToggleThemeTeachingTip1.IsOpen = true;
        }
        private async void ToggleListTip(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog();

            // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
            dialog.XamlRoot = this.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = "Product 1";
            dialog.PrimaryButtonText = "Save";
            dialog.SecondaryButtonText = "Don't Save";
            dialog.CloseButtonText = "Cancel";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = new ItemDialogModal();

            var result = await dialog.ShowAsync();

        }
        private void myListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle selection change event here if needed
        }

        private void FilterByCategory(object sender, RoutedEventArgs e)
        {
            filter = (sender as Button)?.Content?.ToString();
            products = JsonConvert.DeserializeObject<List<Product>>(jsonString);
            if (filter != "Tudo")
            {
                products = products.FindAll(p => p.Category == filter);
            }
            var groupedProducts = products.GroupBy(p => p.Category);
            myListView.Source = groupedProducts;

        }

    }
}
