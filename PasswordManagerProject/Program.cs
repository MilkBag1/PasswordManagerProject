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
             
            string filePath = "C:\\Temp\\AccountData.json";
            string schemaPath = "C:\\Temp\\jsonSchema.json";
            bool run = true;
            List<Root> data = new List<Root>();
            List<Root> validAccountData = new List<Root>();

            //Load data and create account manager
            if (!File.Exists(filePath))
            {
                using (StreamWriter sw = File.CreateText(filePath));
                
            }
            else
            {
                string jsonText = File.ReadAllText(filePath);

                data = JsonConvert.DeserializeObject<List<Root>>(jsonText);

                foreach(Root account in data)
                {
                    var jsonData = JsonConvert.SerializeObject(account);
                    JSchema schema = JSchema.Parse(File.ReadAllText(schemaPath));
                    JObject jsonObject = JObject.Parse(jsonData);
                    IList<string> errorMessages = new List<string>();

                    if (jsonObject.IsValid(schema, out errorMessages))
                    {
                        validAccountData.Add(account);
                    }
                }
                                
            }



            AccountManager manager = new AccountManager(validAccountData, filePath, schemaPath);

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