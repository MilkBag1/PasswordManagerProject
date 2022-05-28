/*
 * Program:         PasswordManager.exe
 * Module:          Root.cs
 * Date:            2020-05-28
 * Author:          Matt Taylor
 * Description:     Root Class
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManagerProject
{
    internal class Root
    {
        public Account Account { get; set; }

        public Root(Account account)
        {
           
            Account = account;

        }
    }
}
