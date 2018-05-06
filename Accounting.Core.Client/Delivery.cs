using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Accounting.Core.Client
{
    [TestClass]
    public class Delivery
    {
        [TestMethod]
        public void sell_goods_and_build_generalJournal_report()
        {
            var generalJournal = new Account()
            {
                Name = "สมุดรายวันทั่วไป",
                AccountEntries = new List<AccountEntry>()
            };

            // ธุรกรรมการค้า 001 - ขายสินค้า รับเงินมา 1000 บาท
            var transaction1 = new AccountTransaction("001")
            {
                Description = "ขายสินค้าให้นาย ก. รับเงินสด 1000 บาท"
            };
            generalJournal.AccountEntries.Add(new AccountEntry(transaction1)
            {
                AccountType = AccountType.Cash,
                EntryType = AccountEntryType.Debit,
                Amount = 1000,
                Description = "เงินสด"
            });
            generalJournal.AccountEntries.Add(new AccountEntry(transaction1)
            {
                AccountType = AccountType.Revenue,
                EntryType = AccountEntryType.Credit,
                Amount = 1000,
                Description = "รายได้ขายสินค้า"
            });

            // ธุรกรรมการค้า 002 - จ่ายสินค้าคงเหลือเพื่อขาย นาย ก. 1 ชิ้น 800 บาท
            var transaction2 = new AccountTransaction("002")
            {
                Description = "จ่ายสินค้าขายนาย ก. 1 ชิ้น 800 บาท"
            };
            generalJournal.AccountEntries.Add(new AccountEntry(transaction2)
            {
                AccountType = AccountType.Cost,
                EntryType = AccountEntryType.Debit,
                Amount = 800,
                Description = "ต้นทุนขาย"
            });
            generalJournal.AccountEntries.Add(new AccountEntry(transaction2)
            {
                AccountType = AccountType.Goods,
                EntryType = AccountEntryType.Credit,
                Amount = 800,
                Description = "สินค้าคงเหลือ"
            });


            // ธุรกรรมการค้า 003 - ขายสินค้า รับเงินโอน PromptPay 2000 บาท
            var transaction3 = new AccountTransaction("003")
            {
                Description = "ขายสินค้าให้นาย ข. รับเงินโอนผ่าน PromptPay 2000 บาท"
            };
            generalJournal.AccountEntries.Add(new AccountEntry(transaction3)
            {
                AccountType = AccountType.PromptPay,
                EntryType = AccountEntryType.Debit,
                Amount = 2000,
                Description = "บัญชี PromptPay"
            });
            generalJournal.AccountEntries.Add(new AccountEntry(transaction3)
            {
                AccountType = AccountType.Revenue,
                EntryType = AccountEntryType.Credit,
                Amount = 2000,
                Description = "รายได้ขายสินค้า"
            });

            // ธุรกรรมการค้า 004 - จ่ายสินค้าขายนาย ข. 2 ชิ้น 1800 บาท
            var transaction4 = new AccountTransaction("004")
            {
                Description = "จ่ายสินค้าขายนาย ข. 2 ชิ้น 1800 บาท"
            };
            generalJournal.AccountEntries.Add(new AccountEntry(transaction4)
            {
                AccountType = AccountType.Cost,
                EntryType = AccountEntryType.Debit,
                Amount = 1600,
                Description = "ต้นทุนขาย"
            });
            generalJournal.AccountEntries.Add(new AccountEntry(transaction4)
            {
                AccountType = AccountType.Goods,
                EntryType = AccountEntryType.Credit,
                Amount = 1600,
                Description = "สินค้าคงเหลือ"
            });

            // สรุปบัญชีบันทึกรายวันทั่วไป
            generalJournal.BuildSummary("results");

            // Posting
            // Trail Balance
        }
    }
}
