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
    public sealed partial class AccountInfo : Page
    {
        public static Account User = AccountsList.SelectedAccount;
        public static List<Product> ProductsList;
        public List<Account> OpenedAccounts = new List<Account> { };
        public List<Account> ClosedAccounts = new List<Account> { };


        public AccountInfo()
        {
            this.InitializeComponent();


            myListView.ItemsSource = ProductsList;
            //DataContext = this;
        }

        public void ShowUserInformation(Account currentUser)
        {

        }

        private async void AddAccount(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = this.XamlRoot;
            dialog.Style = Microsoft.UI.Xaml.Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = "Ol�! Vamos come�ar?";
            dialog.PrimaryButtonText = "Adicionar usu�rio";
            dialog.CloseButtonText = "Cancelar";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = new AddAccountModal();
            var result = await dialog.ShowAsync();
        }
        private async void CloseAccount(object sender, RoutedEventArgs e)
        {
            ContentDialog CloseAccountDialog = new ContentDialog();
            CloseAccountDialog.XamlRoot = this.XamlRoot;
            CloseAccountDialog.Style = Microsoft.UI.Xaml.Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            CloseAccountDialog.Title = "Deseja mesmo fechar sua conta?";
            CloseAccountDialog.PrimaryButtonText = "Sim, fechar conta";
            CloseAccountDialog.CloseButtonText = "Cancelar";
            CloseAccountDialog.DefaultButton = ContentDialogButton.Primary;
            CloseAccountDialog.Content = new CloseAccountConfirmation();
            var result = await CloseAccountDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                Frame.Navigate(typeof(AccountClosed), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
            }
        }

        private void AccountInfoRedirect(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AccountInfo), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private async void BackToMainMenu(object sender, RoutedEventArgs e)
        {
           Frame.Navigate(typeof(MainPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        }
    }
}
