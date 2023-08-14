using System;
using System.Collections.ObjectModel;

namespace REZ
{
    public class AccountsList
    {
        private Collection<Account> accountsList; 

        public Collection<Account> Accounts
        {
            get { return accountsList; }
            set { accountsList = value; }
        }
        public AccountsList()
        {
        }

        public Account CreateAccount(string accountName)
        {
            Account newAccount = new(accountName);
            this.Accounts.Add(newAccount);
            //Criar a conta no DB
            return newAccount;
        }

        public void RemoveAccount(Account account)
        {
            //Remover conta do DB
            this.Accounts.Remove(account);
        }


        public void CleanList()
        {
            //deletar todas as contas do DB
            this.Accounts.Clear();
            
        }
    }
}

