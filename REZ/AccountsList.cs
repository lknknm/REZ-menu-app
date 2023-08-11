using System;
using System.Collections.ObjectModel;

namespace REZ
{
    public class AccountsList
    {
        Collection<Account> accountsList; 
        public AccountsList()
        {
        }

        public Collection<Account> CleanList(Collection<Account> accountsList)
        {
            return accountsList;
        }
    }
}

