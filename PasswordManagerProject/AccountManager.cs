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
        
        public AccountManager(List<Account> list)
        {
           AccountList = list;
        }

        public void AddAccount(Account account)
        {
            AccountList.Add(account);
        }

        public void DeleteAccount(int index)
        {
            AccountList.RemoveAt(index);
        }

        public void DisplayAccounts()
        {
            int count = 1;
            Console.WriteLine(" ====================================================================");
            foreach (Account account in AccountList)
            {
            Console.WriteLine(" || "+count+".            "+account.Description+"                            ||" );
                count++;
            }
            Console.WriteLine(" ====================================================================");
        }

        public void UpdatePassword(int index)
        {

            return;
        }
    }
}
