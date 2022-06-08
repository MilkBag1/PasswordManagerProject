/*
 * Program:         PasswordManager.exe
 * Module:          AccountManager.cs
 * Date:            2020-05-28
 * Author:          Matt Taylor
 * Description:     Account Manager serves as main object for program functionality
 *                  performing writing and data manipulation
 */
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManagerProject
{
    internal class AccountManager
    {
        public List<Root> AccountList;
        public string dataPath;
        public string schemaPath;
        public AccountManager(List<Root> rootData, string data, string schema)
        {
            AccountList = rootData;
            dataPath = data;
            schemaPath = schema;
        }

        public void AddAccount()
        {
            bool valid = false;
            string Description = "";
            string userID = "";
            string passwordValue = "";

            Console.WriteLine();
            Console.WriteLine("Please enter in values for the following fields: ");
            Console.WriteLine();

            
            //DESCRIPTION VALIDATION
            while (!valid) 
            {
                Console.WriteLine("Description: ");
                Description = Console.ReadLine();
                char[] validString = Description.ToCharArray();
                if (validString.Length == 0)
                {
                    Console.WriteLine("Error: Description is required.");
                    Console.WriteLine();

                }
                else if (validString[0].Equals(' '))
                {
                    Console.WriteLine("Error: first character cannot be blank");
                    Console.WriteLine();
                }
                else
                {
                    valid = true;
                }
            }
            valid = false;

            //USERID VALIDATION
            while (!valid)
            {
                Console.WriteLine("UserID: ");
                userID = Console.ReadLine();
                char[] validString = userID.ToCharArray();
                if (validString.Length == 0)
                {
                    Console.WriteLine("Error: userID is required.");
                    Console.WriteLine();

                }
                else if (validString[0].Equals(' '))
                {
                    Console.WriteLine("Error: first character cannot be blank");
                    Console.WriteLine();
                }
                else
                {
                    valid = true;
                }
            }
            valid = false;

            //PASSWORD VALIDATION
            while (!valid)
            {
                Console.WriteLine("Password: ");
                passwordValue = Console.ReadLine();
                char[] validString = passwordValue.ToCharArray();
                if (validString.Length == 0)
                {
                    Console.WriteLine("Error: password is required.");
                    Console.WriteLine();

                }
                else if (!checkPassword(validString))
                {
                    Console.WriteLine("Error: no blank chars in pasword");
                    Console.WriteLine();
                }
                else
                {
                    valid = true; 
                }

                
            }
                    
            
            PasswordTester pt = new PasswordTester(passwordValue);
            var StremgthText = pt.StrengthLabel;
            var StrengthNumber = pt.StrengthPercent;

            Password newPassword = new Password(passwordValue, StrengthNumber, StremgthText, DateTime.Now.ToString());

            Console.WriteLine("Login url: ");
            var url = Console.ReadLine();
            if (!url.Contains("https://") && !url.Equals("")){
                url = "https://" + url;
            }
            

            Console.WriteLine("Account #: ");
            var accountNumber = Console.ReadLine();

            Account newAccount = new Account(Description, userID, url, accountNumber, newPassword);

            Root newRoot = new Root(newAccount);

            //VALIDATE NEW ACCOUNT AGAINST THE SCHEMA
            var jsonData = JsonConvert.SerializeObject(newRoot);
            JSchema schema = JSchema.Parse(File.ReadAllText(schemaPath));
            JObject jsonObject = JObject.Parse(jsonData);
            IList<string> errorMessages = new List<string>();

            if(jsonObject.IsValid(schema, out errorMessages))
            {
                //ADD TO LIST IF GOOD + UPDATE JSON
                AccountList.Add(newRoot);
                UpdateJSON();
            }
            else
            {
                foreach(string evt in errorMessages)
                {
                    Console.WriteLine("ACCOUNT ERROR: ");
                    Console.WriteLine(evt);
                }
                Console.WriteLine("ACCOUNT ERROR: Could not Add account, try again and ensure all fields are filled out correctly.");
            }

        

        }

        public void DeleteAccount(int index)
        {
            bool run = true;
            while (run) {
                Console.WriteLine("Are you sure you want to delete the account data for " + AccountList[index].Account.Description + " ?");
                Console.WriteLine("type yes / no");
                var command = Console.ReadLine();

                switch (command)
                {
                    case ("yes"):
                        Console.WriteLine("Deleting Account");
                        AccountList.Remove(AccountList[index]);
                        UpdateJSON();
                        run = false;
                        break;

                    case ("no"):
                        Console.WriteLine("Returning to the main Menu");
                        run = false;
                        break;

                    default:
                        Console.WriteLine("error: invalid entry. Enter either yes or no");
                        break;
                }
            }
            
            

        }

        public void DisplayAccounts()
        {
            int count = 1;
            Console.WriteLine(" ====================================================================");
            foreach (Root data in AccountList)
            {
            Console.WriteLine("    "+count+".            "+data.Account.Description+"                            " );
                count++;
            }
            Console.WriteLine(" ====================================================================");
        }

        public void MainMenu()
        {
            DateTime dateNow = DateTime.Now;
            Console.WriteLine(" ====================================================================");
            Console.WriteLine(" || Matt Taylor's Password Management System " + dateNow + " ||");
            Console.WriteLine(" ====================================================================");
            Console.WriteLine();
            Console.WriteLine(" ====================================================================");
            Console.WriteLine(" ||                       Accounts on File                         ||");
            Console.WriteLine(" ====================================================================");
            DisplayAccounts();
            Console.WriteLine("     Press # of account to select an entry");
            Console.WriteLine("     Press A to create a new entry");
            Console.WriteLine("     Press X to exit the program");
            Console.WriteLine(" ====================================================================");

        }

        public void AccountSelection(int index)
        {
            bool run = true;

            int selectedIndex = index - 1;
            Root accountSelected = AccountList[selectedIndex];
            while (run)
            {
                Console.WriteLine();
                Console.WriteLine(" ====================================================================");
                Console.WriteLine(" ||   " + index + ".     " + accountSelected.Account.Description + "                                       ||");
                Console.WriteLine(" ====================================================================");
                Console.WriteLine("  UserID:            " + accountSelected.Account.UserId);
                Console.WriteLine("  Password:          " + accountSelected.Account.Password.Value);
                Console.WriteLine("  Password Strength: " + accountSelected.Account.Password.StrengthText + " (" + accountSelected.Account.Password.StrengthNumber + "%)");
                Console.WriteLine("  Password Reset:    " + accountSelected.Account.Password.LastReset);
                Console.WriteLine("  Login url:         " + accountSelected.Account.LoginUrl);
                Console.WriteLine("  Account #:         " + accountSelected.Account.AccountNumber);
                Console.WriteLine(" ====================================================================");
                Console.WriteLine();
                Console.WriteLine("     Press P to change the password");
                Console.WriteLine("     Press D to delete this entry ");
                Console.WriteLine("     Press M to return to the main menu");
                Console.WriteLine();
                Console.WriteLine(" ====================================================================");
                Console.WriteLine();
                Console.WriteLine("     Enter a command: ");

                var command = Console.ReadLine();

                switch (command)
                {
                    case ("p"):
                        UpdatePassword(selectedIndex);
                        break;
                    case ("P"):
                        UpdatePassword(selectedIndex);
                        break;
                    case ("d"):
                        DeleteAccount(selectedIndex);
                        run = false;
                        break;
                    case ("D"):
                        DeleteAccount(selectedIndex);
                        run = false;
                        break;
                    case ("m"):
                        Console.WriteLine("  Returning to the main menu.");
                        run = false;
                        break;
                    case ("M"):
                        Console.WriteLine("  Returning to the main menu.");
                        run = false;
                        break;

                    default:
                        Console.WriteLine("Error; invalid input, please enter a valid command");
                        break;
                }
            }

        }

        public void UpdateJSON()
        {
            var jsonData = JsonConvert.SerializeObject(AccountList);
            File.WriteAllText(dataPath, jsonData);
        }

        public bool checkPassword(char[] stringToCheck)
        {
            //returns false if blanks in password
            foreach (char c in stringToCheck)
            {
                if (c == ' ')
                {
                    return false;
                }
            }
            return true;
        }

        public void UpdatePassword(int index)
        {
            string newPassword = "";
            bool valid = false;
            DateTime date = DateTime.Now;   
            Root selectedAccount = AccountList[index];

            //PASSWORD VALIDATION
            while (!valid)
            {
                Console.WriteLine("New Password: ");
                newPassword = Console.ReadLine();
                char[] validString = newPassword.ToCharArray();
                if (validString.Length == 0)
                {
                    Console.WriteLine("Error: password is required.");
                    Console.WriteLine();

                }
                else if (!checkPassword(validString))
                {
                    Console.WriteLine("Error: no blank chars in pasword");
                    Console.WriteLine();
                }
                else
                {
                    valid = true;
                }


            }

            PasswordTester pt = new PasswordTester(newPassword);

            selectedAccount.Account.Password.Value = newPassword;
            selectedAccount.Account.Password.StrengthText = pt.StrengthLabel;
            selectedAccount.Account.Password.StrengthNumber = pt.StrengthPercent;
            selectedAccount.Account.Password.LastReset = date.ToString();

            //VALIDATE NEW ACCOUNT AGAINST THE SCHEMA
            var jsonData = JsonConvert.SerializeObject(selectedAccount);
            JSchema schema = JSchema.Parse(File.ReadAllText(schemaPath));
            JObject jsonObject = JObject.Parse(jsonData);
            IList<string> errorMessages = new List<string>();

            if (jsonObject.IsValid(schema, out errorMessages))
            {
                // UPDATE JSON
                UpdateJSON();
            }
            else
            {
                foreach (string evt in errorMessages)
                {
                    Console.WriteLine(evt);
                }
            }

            
            
        }

    }
}
