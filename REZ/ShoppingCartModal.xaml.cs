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


public class Product
{
    public string Name { get; set; }
    public string SubCategory { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
}

namespace REZ
{


    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShoppingCartModal : Page
    { 
        string filter = "Cafeteria";
        string jsonString;
        List<Product> products;
    public ShoppingCartModal()
        {
            this.InitializeComponent();

            var jsonFilePath = Path.Combine(AppContext.BaseDirectory, "Properties", "Products.json");
            StreamReader reader = new(jsonFilePath);

            jsonString = reader.ReadToEnd();
            products = JsonConvert.DeserializeObject<List<Product>>(jsonString);
            products = products.FindAll(p => p.Category == filter);
            var groupedProducts = products.GroupBy(p => p.SubCategory);
            myListView.Source = groupedProducts;
            DataContext = this;
        }
    }
}
