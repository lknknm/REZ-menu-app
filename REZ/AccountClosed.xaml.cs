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
    /// <summary>
    /// This page is to be displayed when the user chooses to close his account.
    /// </summary>
    public sealed partial class AccountClosed : Page
    {
        Account NextUser;
        List<Account> AccountList;
        public AccountClosed()
        {
            this.InitializeComponent();
            DataContext = this;
        }
        private void BackToMainMenu(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage), AccountList, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            if (e.Parameter is List<Account>)
            {
                AccountList = e.Parameter as List<Account>;

            }


        }
    }
}
