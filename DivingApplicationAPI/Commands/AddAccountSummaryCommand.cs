using CrankBankAPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrankBankAPI.Commands
{
    public class AddAccountSummaryCommand : ICommand
    {

        public string AccountNumber { get; set; }

        public AccountTypeEnum Type { get; set; }

        public string Name { get; set; }

        public double Balance { get; set; }
    }
}
