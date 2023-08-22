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
using System.Reflection.Metadata;
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
        public AccountsList accountsList = new AccountsList();
        private Account User = AccountsList.SelectedAccount;
        public ShoppingCart shoppingCart = AccountsList.Cart;
        public FoodMenu foodMenu;

        public MainPage()
        {
            this.InitializeComponent();

            
            //pegar o nome do usuário para criar a conta e inicializar o carrinho
            User = new Account("Andres");
            ShoppingCart.DefineUser(User);

        }
        private void OpenFoodMenu(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Frame.Navigate(typeof(FoodMenu), button, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void ShoppingCart_ButtonClick(object sender, RoutedEventArgs e)
        {
            shoppingCart.OpenShoppingModal(this, shoppingCart, ToggleThemeTeachingTip1);

        }

        

        private void SwitchUser_ButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            AccountsList.SwitchAccounts(button.Content.ToString());
        }

        

    }
}

