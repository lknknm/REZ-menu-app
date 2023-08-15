using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Media.Animation;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace REZ
{
    public class Product
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FoodMenu : Page
    {


        public FoodMenu()
        {
            this.InitializeComponent();

            List<Product> products = new List<Product>()
            {
                new Product { Name = "Product 1", Category = "Pratos", Description = "Description 1", Price = 48 },
                new Product { Name = "Product 2", Category = "Pratos", Description = "Description 2", Price = 48 },
                new Product { Name = "Product 3", Category = "Drinks", Description = "Description 3", Price = 48 },
                new Product { Name = "Product 4", Category = "Drinks", Description = "Description 4", Price = 48 },
            };

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
    }
}
