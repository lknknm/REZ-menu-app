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
        public string SubCategory { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FoodMenu : Page
    {

        
        public FoodMenu()
        {
            this.InitializeComponent();

            var jsonFilePath = Path.Combine(AppContext.BaseDirectory, "Properties", "Products.json");
            StreamReader reader = new(jsonFilePath);
            string jsonString = reader.ReadToEnd();
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(jsonString);
            
            var groupedProducts = products.GroupBy(p => p.SubCategory);
            myListView.Source = groupedProducts;
            DataContext = this;

        }
        
        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        }
        private void TestButtonClick1(object sender, RoutedEventArgs e)
        {
            ToggleThemeTeachingTip1.IsOpen = true;
        }
        private async void ToggleListTip(object sender, ItemClickEventArgs e)
        {
            ContentDialog dialog = new ContentDialog();
            var itemId = (e.ClickedItem as Product);

            // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
            dialog.XamlRoot = this.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = itemId.Name;
            dialog.PrimaryButtonText = "Adicionar";
            dialog.CloseButtonText = "Cancelar";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = new ItemDialogModal(itemId);

            var result = await dialog.ShowAsync();
        }
        private void myListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle selection change event here if needed
        }

    }
}
