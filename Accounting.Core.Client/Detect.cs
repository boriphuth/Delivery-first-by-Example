using System;
using System.Linq;
using System.IO;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Accounting.Core.Client
{
    [TestClass]
    public class Detect
    {
        [TestMethod]
        public void watch_sell_goods_and_build_generalJournal_report()
        {
            // Detect
            var targetWatch = Path.Combine("results", "report.html");
            var reportExists = File.Exists(targetWatch);
            Assert.IsTrue(reportExists, "Invalid report file " + targetWatch);

            // From File
            var doc = new HtmlDocument();
            doc.Load(targetWatch);

            var totalDebit = Convert.ToDecimal(doc.DocumentNode
                .SelectNodes("//tfoot/tr/td")
                .Skip(1).First()
                .InnerText);

            var totalCredit = Convert.ToDecimal(doc.DocumentNode
                .SelectNodes("//tfoot/tr/td")
                .Last()
                .InnerText);

            Assert.AreEqual(totalDebit, totalCredit, "Wrong entry");
        }
    }
}
