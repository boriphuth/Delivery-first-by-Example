using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Core
{
    /// <summary>
    /// บันทึกบัญชีแบบสองช่อง คือ Debit และ Credit
    /// </summary>
    public enum AccountEntryType
    {
        Debit, 
        Credit
    }
}
