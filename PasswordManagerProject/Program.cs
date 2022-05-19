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
             
            string filePath = "C:\\tmp\\AccountData.json";
            string schemaPath = "C:\\tmp\\AccountData.json";
            bool run = true;

            string jsonText = File.ReadAllText(filePath);

            var data = JsonConvert.DeserializeObject<List<Root>>(jsonText);
                                                  
            AccountManager manager = new AccountManager(data, filePath, schemaPath);

            
            //loading phase completed, now user interaction
            while (run)
            {

                manager.MainMenu();
                Console.WriteLine();
                Console.WriteLine("Enter a command:      ");
                var command = Console.ReadLine();
                int commandNumber;

                bool isNumber = Int32.TryParse(command, out commandNumber);
                if (isNumber)
                {
                    if (commandNumber < 1 || commandNumber > manager.AccountList.Count())
                    {
                        Console.WriteLine("Invalid option");
                                            }
                    else
                    {
                        manager.AccountSelection(commandNumber);
                    }

                }
                else
                {
                    
                    switch (command)
                    {
                        case ("a"):
                            manager.AddAccount();
                            break;
                        case ("A"):
                            manager.AddAccount();
                            break;
                        case ("x"):
                            run = false;
                            Console.WriteLine("Thank you for using the account management program");
                            break;
                        case ("X"):
                            run = false;
                            Console.WriteLine("Thank you for using the account management program");
                            break;

                            default:
                            Console.WriteLine("Error; invalid input, please enter a valid command");
                            break;
                    }
                }
                
            }
        }
    }
}