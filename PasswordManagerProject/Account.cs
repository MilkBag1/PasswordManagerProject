/*
 * Program:         PasswordManager.exe
 * Module:          Account.cs
 * Date:            2020-05-28
 * Author:          Matt Taylor
 * Description:     Account class
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManagerProject
{
    internal class Account
    {
        public string? Description { get; set; }
        public string? UserId { get; set; }
        public string? LoginUrl { get; set; }
        public string? AccountNumber { get; set; }
        public Password? Password { get; set; }         

        public Account(string description, string userId, string loginUrl, string accountNumber, Password password)
        {
            Description = description;
            UserId = userId;
            LoginUrl = loginUrl;
            AccountNumber = accountNumber;
            Password = password;
        }
    }
}
