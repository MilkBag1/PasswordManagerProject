using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManagerProject
{
    internal class AccountManager
    {
        public List<Account> AccountList;
        public string jsonFileRoute;
        public AccountManager(string file)
        {
           jsonFileRoute = file;
        }

        public void AddAccount(Account account)
        {
            AccountList.Add(account);
        }

        public void DeleteAccount(int index)
        {
            AccountList.RemoveAt(index);
        }

        public void UpdatePassword(int index)
        {
            Account accountToUpdate = AccountList[index];
            accountToUpdate.UpdatePassword();
            
            
        }
    }
}
