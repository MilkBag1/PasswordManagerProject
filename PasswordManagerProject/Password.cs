using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManagerProject
{
    internal class Password
    {

        public string Value { get; set; }
        public string StrengthNumber { get; set; }
        public string StrengthText { get; set; }
        public string LastReset { get; set; }

        public Password(string value)
        {
            Value = value;
            PasswordTester pt = new PasswordTester(Value);

            StrengthText = pt.StrengthLabel;
            StrengthNumber = pt.StrengthPercent.ToString();
        }

    }
}
