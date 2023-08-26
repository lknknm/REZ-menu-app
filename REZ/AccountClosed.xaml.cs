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
using Microsoft.UI.Xaml.Media.Animation;
using static System.Net.WebRequestMethods;
using System.Diagnostics;
using Windows.System;

namespace REZ
{
    
    public sealed partial class AccountClosed : Page
    {
        //Account NextUser;
        public static List<Account> Accounts = AccountsList.Accounts;
        public static Account User = AccountsList.SelectedAccount;

        //----------------------------------------------------------------------------
        public AccountClosed()
        {
            this.InitializeComponent();
            UpdateUser(User);
            DataContext = this;
        }

        //----------------------------------------------------------------------------
        private void BackToMainMenu(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        }

        //----------------------------------------------------------------------------
        private void BackToMainMenu(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {

                Frame.Navigate(typeof(MainPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });

            }

        }

        //----------------------------------------------------------------------------
        public void UpdateUser(Account user)
        {
            try
            {
                User = AccountsList.SwitchAccounts(user);
                ShoppingCart.DefineUser(User);
            }
            catch (Exception ex) 
            {
                Debug.WriteLine(ex);
            }
            
            
            Debug.WriteLine($"[AccountClosed] contas ativas: {AccountsList.Accounts.Count}");
        }

    }
}
