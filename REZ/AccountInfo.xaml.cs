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
    public sealed partial class AccountInfo : Page
    {
        public Account User = AccountsList.SelectedAccount;
        public static List<Product> ProductsList = AccountsList.SelectedAccount.ItemsList;
        public List<Account> OpenedAccounts = AccountsList.Accounts;
        public List<Account> ClosedAccounts = new List<Account> { };


        public AccountInfo()
        {
            this.InitializeComponent();
            ProductsList = UpdateUser(User);
            Debug.WriteLine($"[AccountInfo] Current account: {User.Name}");
            Debug.WriteLine($"[AccountInfo] Active Accounts: {OpenedAccounts}");

            double subtotal = SubtotalValueCalculator(ProductsList);
            string subtotalValue = subtotal.ToString("0.00");
            double taxa = subtotal * 0.1;
            string taxaServico = taxa.ToString("0.00");
            double total = subtotal + taxa;
            string totalPrice = total.ToString("0.00");

            Subtotal.Text = $"R$ {subtotalValue}";
            Taxa.Text = $"R$ {taxaServico}";
            TotalPrice.Text = $"R$ {totalPrice}";

            myListView.ItemsSource = ProductsList;


        }

        public double SubtotalValueCalculator(List<Product> productsList)
        {
            double finalValue = 0.0;

            foreach (Product product in productsList)
            {
                finalValue += product.Price;
            }

            return finalValue;
        }

        public void ShowUserInformation(Account currentUser)
        {

        }

        private async void CloseAccount(object sender, RoutedEventArgs e)
        {
            ContentDialog CloseAccountDialog = new ContentDialog();
            CloseAccountDialog.XamlRoot = this.XamlRoot;
            CloseAccountDialog.Style = Microsoft.UI.Xaml.Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            CloseAccountDialog.Title = "Deseja mesmo fechar sua conta?";
            CloseAccountDialog.PrimaryButtonText = "Sim, fechar conta";
            CloseAccountDialog.PrimaryButtonClick += delegate { OpenedAccounts = AccountsList.RemoveAccount(User, OpenedAccounts); };
            CloseAccountDialog.CloseButtonText = "Cancelar";
            CloseAccountDialog.DefaultButton = ContentDialogButton.Primary;
            CloseAccountDialog.Content = new CloseAccountConfirmation();
            var result = await CloseAccountDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                Frame.Navigate(typeof(AccountClosed), OpenedAccounts, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
            }
        }

        private void AccountInfoRedirect(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AccountInfo), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void BackToMainMenu(object sender, RoutedEventArgs e)
        {
           Frame.Navigate(typeof(MainPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        }

        public async void SwitchAccount(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = this.XamlRoot;
            dialog.Style = Microsoft.UI.Xaml.Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = "Olá! Vamos começar?";
            dialog.PrimaryButtonText = "Trocar usuário";
            dialog.CloseButtonText = "Cancelar";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = new SwitchAccountModal();
            //dialog.PrimaryButtonClick += delegate { AddAccount(dialog.Content); };
            var result = await dialog.ShowAsync();

        }


        private async void CreateAccount(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = this.XamlRoot;
            dialog.Style = Microsoft.UI.Xaml.Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = "Olá! Vamos começar?";
            dialog.PrimaryButtonText = "Adicionar usuário";
            dialog.CloseButtonText = "Cancelar";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = new AddAccountModal();
            dialog.PrimaryButtonClick += delegate { AddAccount(dialog.Content); };
            var result = await dialog.ShowAsync();
        }

        public void AddAccount(object sender)
        {
            AddAccountModal aam = sender as AddAccountModal;
            Account NewUser = aam.CreateNewAccount();
            List<Account> accountsList = AccountsList.AddNewAccount(NewUser);
            UpdateUser(AccountsList.SelectedAccount);
        }

        public List<Product> UpdateUser(Account user)
        {

            User = AccountsList.SwitchAccounts(user.Name);
            Debug.WriteLine($"[AccountInfo] Account changed to {User.Name}");
            CurrentUsername.Content = User.Name;
            myListView.ItemsSource = User.ItemsList;
            Greetings.Text = $"Ola, {AccountsList.SelectedAccount.Name}!";
            TitleGreetings.Text = $"Ola, {AccountsList.SelectedAccount.Name}!";
            ShoppingCart.DefineUser(User);
            return User.ItemsList;


        }
    }
}
