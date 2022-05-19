/*
 * Program:         PasswordManager.exe
 * Module:          PasswordManager.cs
 * Date:            <enter a date>
 * Author:          <enter your name>
 * Description:     Some free starting code for INFO-3138 project 1, the Password Manager
 *                  application. All it does so far is demonstrate how to obtain the system date 
 *                  and how to use the PasswordTester class provided.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;            // File class

namespace PasswordManagerProject
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "AccountData.json";
            AccountManager accountManager = new AccountManager(filePath);

            DateTime dateNow = DateTime.Now;
            Console.WriteLine(" ====================================================================");
            Console.WriteLine(" || Matt Taylor's Password Management System " + dateNow + " ||");
            Console.WriteLine(" ====================================================================");
            Console.WriteLine();
            Console.WriteLine(" ====================================================================");
            Console.WriteLine(" ||                       Accounts on File                         ||");
            Console.WriteLine(" ====================================================================");
        }
    }
}