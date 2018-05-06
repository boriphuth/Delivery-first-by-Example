using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Core
{
    public class AccountEntry
    {
        public AccountEntry(AccountTransaction transaction)
        {
            Transaction = transaction;
        }
        public AccountTransaction Transaction { get; private set; }
        public AccountType AccountType { get; set; }
        public AccountEntryType EntryType { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}
