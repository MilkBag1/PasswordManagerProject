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
using System.IO;
using Newtonsoft;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;

namespace PasswordManagerProject
{
    class Program
    {
        static void Main(string[] args)
        {


                       // C:\$INFO3067_ASP_NET\PasswordManagerProject\PasswordManagerProject\AccountData.json
                        string filePath = "C:\\tmp\\AccountData.json";
                        string schemaPath = "C:\\tmp\\AccountData.json";



                        string jsonText = File.ReadAllText(filePath);

                        var data = JsonConvert.DeserializeObject<List<Root>>(jsonText);

                        List<Account> accountList = new List<Account>();

                        foreach(var item in data)
                        {
                            accountList.Add(item.Account);
                        }

                        AccountManager manager = new AccountManager(accountList);

                        DateTime dateNow = DateTime.Now;
                        Console.WriteLine(" ====================================================================");
                        Console.WriteLine(" || Matt Taylor's Password Management System " + dateNow + " ||");
                        Console.WriteLine(" ====================================================================");
                        Console.WriteLine();
                        Console.WriteLine(" ====================================================================");
                        Console.WriteLine(" ||                       Accounts on File                         ||");
                        Console.WriteLine(" ====================================================================");
                        manager.DisplayAccounts();
        }
    }
}