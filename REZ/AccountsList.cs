using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace REZ
{
    public class AccountsList
    {
        private static List<Account> accountsList;
        private static Account selectedAccount;
        private static ShoppingCart shoppingCart;

        public List<Account> Accounts
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
            accountsList = new List<Account> { };
            shoppingCart = InicializeNewShoppingCart();
            //Criar a conta no DB
        }

        public void RemoveAccount(Account account)
        {
            //Remover conta do DB
            Accounts.Remove(account);
        }

        public static void SwitchAccounts(string newUsername)
        {
            foreach (Account user in AccountsList.accountsList)
            {
                if (user.Name == newUsername)
                {
                    SelectedAccount = user;
                }
            }
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
    }
}

