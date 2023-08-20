using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace REZ
{

    public sealed partial class MainPage : Page
    {
        public AccountsList accountsList;
        private static Account User;
        public ShoppingCart shoppingCart;
        public FoodMenu foodMenu;

        public MainPage()
        {
            this.InitializeComponent();
        }
        private async void OpenFoodMenu(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(FoodMenu), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void ShoppingCartButtonClick(object sender, RoutedEventArgs e)
        {
            shoppingCart.OpenShoppingModal(this, shoppingCart, ToggleThemeTeachingTip1);

        }

    }
}

