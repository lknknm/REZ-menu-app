using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Xml.Linq;
using Windows.System;

namespace REZ
{
    public class AccountsList
    {
        private static List<Account> accountsList = new List<Account>() { };
        private static Account selectedAccount;
        private static ShoppingCart shoppingCart;

        public static List<Account> Accounts
        {
            get { return accountsList; }
            set { accountsList = value; }
        }

        public static Account SelectedAccount
        {
            get { return selectedAccount; }
            set { selectedAccount = value; } 
            
        }
        public static ShoppingCart Cart
        {
            get { return shoppingCart; }
            set { shoppingCart = value; }
        }

        //----------------------------------------------------------------------------
        public AccountsList()
        {
            shoppingCart = new ShoppingCart();
            //Criar a conta no DB
        }

        //----------------------------------------------------------------------------
        public static List<Account> AddNewAccount(Account account)
        {

            Accounts.Add(account);
            Debug.WriteLine($"Active accounts: {Accounts.Count}");
            return Accounts;
        }

        //----------------------------------------------------------------------------
        public static void RemoveAccount(Account account)
        {
            //Remover conta do DB
            Accounts.Remove(account);
            if (Accounts.Count > 0)
            {
                SelectedAccount = Accounts[0];

            }
            else
            {
                SelectedAccount = null;

            }

        }

        //----------------------------------------------------------------------------
        public static Account SwitchAccounts(Account newAccount)
        {
            if (Accounts.Count > 0)
            {
                foreach (Account user in Accounts)
                {
                    user.IsSelected = false;
                    user.IsEnabled = true;

                    if (user == newAccount)
                    {
                        SelectedAccount = user;
                        user.IsSelected = true;
                        user.IsEnabled = false;
                        ShoppingCart.SwitchAccount(user);

                    }

                }

            }

            return SelectedAccount;
        }

        //----------------------------------------------------------------------------
        public static List<Account> GetAvailableUsers()
        {
            List<Account> availableUsers = new List<Account>();
            foreach (var account in Accounts)
            {
                if (SelectedAccount.CPF != account.CPF)
                {
                    availableUsers.Add(account);
                }

            }

            return availableUsers;
        }

        //----------------------------------------------------------------------------
        public static async void OpenSwitchAccountModal(Page page, Action<Account> UpdateUser)
        {
            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = page.XamlRoot;
            dialog.Style = Microsoft.UI.Xaml.Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = "Select an account.";
            dialog.PrimaryButtonText = "Switch account";
            dialog.CloseButtonText = "Cancel";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = new SwitchAccountModal();
            dialog.PrimaryButtonClick += delegate { UpdateUser(SwitchAccountModal.UserToChange); };
            var result = await dialog.ShowAsync();

        }

        //----------------------------------------------------------------------------
        public static async void OpenAddAccountModal(Page page, Action<Account> UpdateUser)
        {
            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = page.XamlRoot;
            dialog.Style = Microsoft.UI.Xaml.Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = "Hello, let's get started!";
            dialog.PrimaryButtonText = "Add account";
            if (Accounts.Count > 0)
                dialog.CloseButtonText = "Cancel";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = new AddAccountModal(dialog);
            dialog.PrimaryButtonClick += delegate { AddAccount(dialog.Content, UpdateUser); };
            var result = await dialog.ShowAsync();
        }

        public static void AddAccount(object sender, Action<Account> UpdateUser)
        {
            AddAccountModal aam = sender as AddAccountModal;
            Account NewUser = aam.CreateNewAccount();
            List<Account> accountsList = AddNewAccount(NewUser);
            UpdateUser(AccountsList.SelectedAccount);
        }

        //----------------------------------------------------------------------------
        public static async void OpenCloseAccountModal(Page page, Frame frame)
        {
            ContentDialog CloseAccountDialog = new ContentDialog();
            CloseAccountDialog.XamlRoot = page.XamlRoot;
            CloseAccountDialog.Style = Microsoft.UI.Xaml.Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            CloseAccountDialog.Title = "Are you sure you want to close this account?";
            CloseAccountDialog.PrimaryButtonText = "Yes, close account";
            CloseAccountDialog.PrimaryButtonClick += delegate { RemoveAccount(SelectedAccount); };
            CloseAccountDialog.CloseButtonText = "Cancel";
            CloseAccountDialog.DefaultButton = ContentDialogButton.Primary;
            CloseAccountDialog.Content = new CloseAccountConfirmation();
            var result = await CloseAccountDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                frame.Navigate(typeof(AccountClosed), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
            }

        }

    }
}

