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
using System.Text.RegularExpressions;
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
        

        public AddAccountModal()
        {
            this.InitializeComponent();
            DataContext = this;
        }

        public Account CreateNewAccount()
        {
            
            Account newAccount = new Account(Name.Text, CPF.Text);
            return newAccount;

        }

        private bool IsValidString(string str)
        {
            if (str.Length < 3 || !System.Text.RegularExpressions.Regex.IsMatch(str, @"^[a-zA-Z]+$"))
            {
                return false;
            }
            return true;
        }

        private async void ShowAlert(string message)
        {
            ContentDialog alert = new ContentDialog
            {
                Title = "Alerta",
                Content = message,
                CloseButtonText = "OK"
            };

            await alert.ShowAsync();
        }

    }
}
