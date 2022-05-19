using Newtonsoft.Json;
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
            
        }

        public void DeleteAccount(int index)
        {
            AccountList.RemoveAt(index);
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

        public void UpdatePassword(int index)
        {
            DateTime date = DateTime.Now;   
            Root selectedAccount = AccountList[index];
            Console.WriteLine("New Password:   ");
            var newPassword = Console.ReadLine();

            PasswordTester pt = new PasswordTester(newPassword);

            selectedAccount.Account.Password.Value = newPassword;
            selectedAccount.Account.Password.StrengthText = pt.StrengthLabel;
            selectedAccount.Account.Password.StrengthNumber = pt.StrengthPercent.ToString();
            selectedAccount.Account.Password.LastReset = date.ToString();

            var jsonData = JsonConvert.SerializeObject(AccountList);
            File.WriteAllText(dataPath, jsonData);
            
        }
    }
}
