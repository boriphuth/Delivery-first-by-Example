using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Core
{
    public class AccountTransaction
    {
        public string Description { get; set; }
        public string Code { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public AccountTransaction(string code)
        {
            CreatedDate = DateTime.Now;
            Code = code;
        }
    }
}
