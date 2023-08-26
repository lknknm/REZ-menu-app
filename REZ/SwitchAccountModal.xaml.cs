using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace REZ
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SwitchAccountModal : Page
    {
        public List<Account> OpenAccounts = AccountsList.Accounts;
        public static Account UserToChange;


        //----------------------------------------------------------------------------
        public SwitchAccountModal()
        {
            this.InitializeComponent();
            AccountsOptions.ItemsSource = AccountsList.GetAvailableUsers(); ;
            DataContext = this;

        }

        private void SelectedUserChanged(object sender, SelectionChangedEventArgs e)
        {
            UserToChange = AccountsOptions.SelectedItem as Account;

        }
    }
}
