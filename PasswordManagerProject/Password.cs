/*
 * Program:         PasswordManager.exe
 * Module:          Password.cs
 * Date:            2020-05-28
 * Author:          Matt Taylor
 * Description:     Password class
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManagerProject
{
    internal class Password
    {

        public string? Value { get; set; }
        public int? StrengthNumber { get; set; }
        public string? StrengthText { get; set; }
        public string? LastReset { get; set; }

        
        public Password(string? value, int strengthNumber, string? strengthText, string? lastReset)
        {
            Value = value;
            StrengthNumber = strengthNumber;
            StrengthText = strengthText;
            LastReset = lastReset;
        }
    }
}
