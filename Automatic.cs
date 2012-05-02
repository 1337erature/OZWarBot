using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core;

namespace ConsoleApplication1
{
    class Automatic
    {
        [STAThread]
        static void Main(string[] args)
        {
            System.Console.WriteLine("OZWar Bot v1.0");
            System.Console.WriteLine("Инициализация IE...");
            WatiN.Core.IE ieb = new IE("http://ozwar.ru/forum/index.php?app=core&module=global&section=login");
            ieb.Visible = false;
            System.Console.WriteLine("Авторизация...");
            ieb.WaitForComplete();
            ieb.TextField(WatiN.Core.Find.ByName("ips_username")).TypeText("BenderFromFuture");
            ieb.TextField(WatiN.Core.Find.ByName("ips_password")).TypeText("102050");
            ieb.Button(WatiN.Core.Find.ByClass("input_submit")).Click();
            ieb.WaitForComplete();
            ieb.GoTo("http://ozwar.ru/forum/index.php?/user/609-benderfromfuture/");
            ieb.WaitForComplete();
            System.Console.WriteLine("Баланс: " + ieb.Span(WatiN.Core.Find.ByClass("fc")).Text.ToString());
        }
        static string Auth(string[] args)
        {
            string bal = "nein";

            return bal;
        }
    }
}
