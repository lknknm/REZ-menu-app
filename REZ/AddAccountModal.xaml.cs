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
    public sealed partial class AddAccountModal : Page
    {
        public static Account NewUser;
        public static string NewCPF;

        public AddAccountModal()
        {
            this.InitializeComponent();
            DataContext = this;
        }

        public (string, string) CreateNewAccount()
        {
            return (Name.Text, CPF.Text);
            //MainPage.CreateAccount(Name.Text, NewCPF);
            //Debug.WriteLine($"Users list length: {AccountsList.Accounts.Count}");
            //Debug.WriteLine($"Current User: {AccountsList.SelectedAccount.Name}");
            //MainPage.UpdateUser(AccountsList.SelectedAccount);
        }
    }
}
