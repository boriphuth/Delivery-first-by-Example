using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Core
{
    public enum AccountType
    {
        // กลุ่มสินทรัพพย์ (Assets)
        Goods, // สินค้า
        Cash, // เงินสด
        PromptPay, // บัญชี PromptPay
        Dept, // บัญชีลูกหนี้

        // กลุ่มหนีสิน (Liabilities)
        Liability, // หนีสินค้างจ่าย

        // กลุ่มส่วนของเจ้าของ (Shareholders' Equity)
        Revenue, // รายรับ
        Cost, // รายจ่าย
        Capital // ทุนที่เป็นส่วนของเจ้าของ
    }
}
