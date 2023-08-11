using System;
using System.Collections.ObjectModel;

namespace REZ
{
    public class Account
    {
        string name;
        Collection<Item> ItemsList;
        double totalPrice;
        
        public Account()
        {

        }

        public void MoveItemToAnotherAccount(Account originAccount, Account DestinyAccount)
        {

        }

        public Account CreateAccount(string AccountName)
        {
            Account newAccount = new Account();
            return newAccount;
        }

        public void RemoveAccount()
        {

        }

        public void CleanUp()
        {
            
        }
    }
}

