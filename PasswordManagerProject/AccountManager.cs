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
            
            Console.WriteLine();
            Console.WriteLine("Please enter in values for the following fields: ");
            Console.WriteLine();

            Console.WriteLine("Description: ");
            var Description = Console.ReadLine();
            

            Console.WriteLine("User ID: ");
            var userID = Console.ReadLine();
            

            Console.WriteLine("Password: ");
            var passwordValue = Console.ReadLine();
            
            PasswordTester pt = new PasswordTester(passwordValue);
            var StremgthText = pt.StrengthLabel;
            var StrengthNumber = pt.StrengthPercent;

            Password newPassword = new Password(passwordValue, StrengthNumber, StremgthText, DateTime.Now.ToString());

            Console.WriteLine("Login url: ");
            var login = Console.ReadLine();
            

            Console.WriteLine("Account #: ");
            var accountNumber = Console.ReadLine();

            Account newAccount = new Account(Description, userID, login, accountNumber, newPassword);

            Root newRoot = new Root(newAccount);

            //TODO: IT WORKS! 
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
                    Console.WriteLine(evt);
                }
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
            int selectedIndex = index - 1;
            Root accountSelected = AccountList[selectedIndex];
            Console.WriteLine();
            Console.WriteLine(" ====================================================================");
            Console.WriteLine(" ||   " + index + ".     " + accountSelected.Account.Description + "                                       ||");
            Console.WriteLine(" ====================================================================");
            Console.WriteLine("  UserID:            " + accountSelected.Account.UserId);
            Console.WriteLine("  Password:          " + accountSelected.Account.Password.Value);
            Console.WriteLine("  Password Strength: " + accountSelected.Account.Password.StrengthText +" (" +accountSelected.Account.Password.StrengthNumber  +"%)");
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
                    break;
                case ("D"):
                    DeleteAccount(selectedIndex);
                    break;
                case ("m"):
                    Console.WriteLine("  Returning to the main menu.");
                    break;
                case ("M"):
                    Console.WriteLine("  Returning to the main menu.");
                    break;

                default:
                    Console.WriteLine("Error; invalid input, please enter a valid command");
                    break;
            }

        }

        public void UpdateJSON()
        {
            var jsonData = JsonConvert.SerializeObject(AccountList);
            File.WriteAllText(dataPath, jsonData);
        }

        public void UpdatePassword(int index)
        {
            DateTime date = DateTime.Now;   
            Root selectedAccount = AccountList[index];
            Console.WriteLine("New Password:   ");
            var newPassword = Console.ReadLine();

            PasswordTester pt = new PasswordTester(newPassword);

            selectedAccount.Account.Password.Value = newPassword;
            selectedAccount.Account.Password.StrengthText = pt.StrengthLabel;
            selectedAccount.Account.Password.StrengthNumber = pt.StrengthPercent;
            selectedAccount.Account.Password.LastReset = date.ToString();

            UpdateJSON();
            
        }
    }
}
