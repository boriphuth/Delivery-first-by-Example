using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Core
{
    public class Account
    {
        public string Name { get; set; }

        public IList<AccountEntry> AccountEntries { get; set; }

        public void BuildSummary(string resultPath)
        {
            var reportHTML = "<html>";
            reportHTML += "<body>";
            reportHTML += "<head>";
            reportHTML += "<style>";
            reportHTML += "table { border-collapse: collapse }";
            reportHTML += "table, th, td { border: 1px solid black; }";
            reportHTML += "th, td { padding: 5px; }";
            reportHTML += ".text-left { text-align: left; }";
            reportHTML += ".text-right { text-align: right; }";
            reportHTML += ".text-top-left { vertical-align: top;text-align: left; }";
            reportHTML += ".border-bottom-white { border-bottom: 1px solid white; }";
            reportHTML += ".border-left-white { border-left: 1px solid white; }";
            reportHTML += ".border-left-right { border-right: 1px solid white; }";
            reportHTML += "</style>";
            reportHTML += "</head>";
            reportHTML += $"<h2>{Name}</h2>";

            reportHTML += "<table>";
            reportHTML += "<thead><tr>";
            reportHTML += "<th>ว.ด.ป</th>";
            reportHTML += "<th colspan=\"2\" class=\"text-left\">รายการ</th>";
            reportHTML += "<th style=\"text-align: center;\">บัญชี</th>";
            reportHTML += "<th>Debit</th><th>Credit</th>";
            reportHTML += "</tr></thead>";
            reportHTML += "<tbody>";

            var group_by_day = AccountEntries.GroupBy(x => x.Transaction.CreatedDate.Date);

            //var group_by_tran = AccountEntries.GroupBy(x => x.Transaction.Code);
            
            foreach (var tran_by_day in group_by_day)
            {
                var group_by_tran = tran_by_day.GroupBy(x => x.Transaction.Code);

                var firstRecord = tran_by_day.First();

                foreach (var tran in group_by_tran)
                {

                    foreach (var item in tran.Where(x => x.EntryType == AccountEntryType.Debit))
                    {
                        reportHTML += "<tr>";
                        if (item == firstRecord)
                        {
                            reportHTML += $"<td class=\"text-top-left\" rowspan=\"{tran_by_day.Count() + group_by_tran.Count()}\">{firstRecord.Transaction.CreatedDate.ToString("dd.MM.yy")}</td>";
                        }
                        reportHTML += $"<td class=\"border-bottom-white border-left-right\">{item.Description}</td><td class=\"border-bottom-white border-left-white\"></td><td class=\"border-bottom-white\">{item.AccountType}</td><td class=\"text-right border-bottom-white\">{item.Amount}</td><td class=\"text-right border-bottom-white\"></td>";
                        reportHTML += "</tr>";
                    }
                    foreach (var item in tran.Where(x => x.EntryType == AccountEntryType.Credit))
                    {
                        reportHTML += $"<tr><td class=\"border-bottom-white border-left-right\"></td><td class=\"border-bottom-white border-left-white\">{item.Description}</td><td class=\"border-bottom-white\">{item.AccountType}</td><td class=\"text-right border-bottom-white\"></td><td class=\"text-right border-bottom-white\">{item.Amount}</td></tr>";
                    }
                    reportHTML += $"<tr><td colspan=\"2\">{tran.First().Transaction.Description}</td><td></td><td></td><td></td></tr>";
                }
            }

            var debitEntries = AccountEntries.Where(x => x.EntryType == AccountEntryType.Debit);
            var creditEntries = AccountEntries.Where(x => x.EntryType == AccountEntryType.Credit);
            reportHTML += "</tbody>";
            reportHTML += "<tfoot>";
            reportHTML += $"<tr><td colspan=\"4\"></td><td class=\"text-right\">{debitEntries.Sum(x => x.Amount)}</td><td class=\"text-right\">{creditEntries.Sum(x => x.Amount)}</td></tr>";
            reportHTML += "</tfoot>";
            reportHTML += "</table>";

            reportHTML += "</body>";
            reportHTML += "</html>";
            File.WriteAllText($"{Path.Combine(resultPath, "report.html")}", reportHTML);
        }
    }
}
