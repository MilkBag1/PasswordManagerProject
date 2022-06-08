/*
 * Program:         PasswordManager.exe
 * Module:          PasswordManager.cs
 * Date:            2020-05-28
 * Author:          Matt Taylor
 * Description:     Password Manager Program working with JSON
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
            //Files for JSON schema and Account Data are in debug//net6.0//JSON 
            //Working directory should not be changed in order for program functionality

            string filePath = Directory.GetCurrentDirectory() + "\\JSON\\AccountData.json";
            string schemaPath = Directory.GetCurrentDirectory() + "\\JSON\\jsonSchema.json";


            bool run = true;
            List<Root> AccountData = new List<Root>();

            //Load data and create account manager
            if (!File.Exists(filePath))
            {
                using (StreamWriter sw = File.CreateText(filePath));
                
            }
            else
            {
                string jsonText = File.ReadAllText(filePath);

                AccountData = JsonConvert.DeserializeObject<List<Root>>(jsonText);

                
                                
            }


            AccountManager manager = new AccountManager(AccountData, filePath, schemaPath);

            //User interaction
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
                            Console.WriteLine("Thank you for using the password management system");
                            break;
                        case ("X"):
                            run = false;
                            Console.WriteLine("Thank you for using the password management system");
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