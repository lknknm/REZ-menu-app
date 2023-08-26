using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
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

        public AccountsList()
        {
            //Accounts = new List<Account> { };
            shoppingCart = InicializeNewShoppingCart();
            //Criar a conta no DB
        }

        public static List<Account> AddNewAccount(Account account)
        {

            Accounts.Add(account);
            Debug.WriteLine($"Contas ativas: {Accounts.Count}");
            return Accounts;
        }

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


        public void CloseAccounts()
        {
            //deletar todas as contas do DB
            Accounts.Clear();
            InicializeNewShoppingCart();
            
        }

        private ShoppingCart InicializeNewShoppingCart()
        {
            Random random = new Random(); // mudar para um gerador de Id de respeito
            string newOrderId = random.Next(0, 100).ToString();
            return new ShoppingCart(newOrderId);

        }

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
            dialog.Title = "Olá! Vamos começar?";
            dialog.PrimaryButtonText = "Trocar usuário";
            dialog.CloseButtonText = "Cancelar";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = new SwitchAccountModal();
            dialog.PrimaryButtonClick += delegate { UpdateUser(SwitchAccountModal.UserToChange); };
            var result = await dialog.ShowAsync();

        }


    }
}

