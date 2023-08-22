using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace REZ
{
    public class AccountsList
    {
        private List<Account> accountsList; 

        public List<Account> Accounts
        {
            get { return accountsList; }
            set { accountsList = value; }
        }
        public AccountsList()
        {
            accountsList = new List<Account> { };
            //Criar a conta no DB
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

