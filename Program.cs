using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using WatiN.Core;

namespace ConsoleApplication1
{
    public static class WatiNExtensions
    {
        public static void TypeTextQuickly(this TextField textField, string text)
        {
            textField.SetAttributeValue("value", text);
        }
    }
    class Program
    {

        static int RandomInt(int vMIn, int vMax)
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            return rand.Next(vMIn, vMax);
        }
        [STAThread]
        static void Main(string[] args)
        {
            //Делаем компилятор счастливым
            string username = "";
            System.Console.WriteLine("OZWar Bot v1.0");
            System.Console.WriteLine("Инициализация IE...");
            //Инициализация ватина
            WatiN.Core.IE ieb = new IE("http://ozwar.ru/forum/index.php?app=core&module=global&section=login");
            ieb.Visible = false;
            int ii = 0;
            while (ii == 0)
            {
                System.Console.WriteLine("Авторизация...");
                ieb.WaitForComplete();
                //Вводим данные
                System.Console.WriteLine("Введите имя пользователя:");
                username = System.Console.ReadLine();
                System.Console.WriteLine("Введите пароль:");
                string password = System.Console.ReadLine();
                ieb.TextField(WatiN.Core.Find.ByName("ips_username")).TypeText(username);
                ieb.TextField(WatiN.Core.Find.ByName("ips_password")).TypeText(password);
                ieb.Button(WatiN.Core.Find.ByClass("input_submit")).Click();
                ieb.WaitForComplete();
                if (ieb.Link(WatiN.Core.Find.ByTitle(username)).Exists)
                {
                    ii = 1;
                }
            }
            //Смотрим баланс
            ieb.GoTo(ieb.Link(WatiN.Core.Find.ByTitle(username)).Url);
            ieb.WaitForComplete();
            System.Console.WriteLine("Баланс: " + ieb.Span(WatiN.Core.Find.ByClass("fc")).Text.ToString());
            System.Console.WriteLine("Грузим ссылки...");
            //Грузим ссылки
            var ar1 = new List<string>();
            System.IO.StreamReader file = new System.IO.StreamReader(@"c:\list.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                ar1.Add(line);
            }
            //Грузим фразы
            System.Console.WriteLine("Грузим фразы...");
            var ar2 = new List<string>();
            System.IO.StreamReader file2 = new System.IO.StreamReader(@"c:\phrases.txt");
            string line2 = "";
            while ((line2 = file2.ReadLine()) != null)
            {
                ar2.Add(line2);
            }
            int co = 0;
            while (co < ar1.Count)
            {
                ieb.GoTo(ar1[co]);
                ieb.WaitForComplete();
                if (!ieb.Link(WatiN.Core.Find.ByTitle("Изменить")).Exists)
                {
                    System.Console.WriteLine("Текущая ссылка: " + ar1[co]);
                    int rnd = RandomInt(0, ar2.Count);
                    System.Console.WriteLine("Пишем");
                    ieb.TextField(WatiN.Core.Find.ByName("Post")).TypeTextQuickly(ar2[rnd] + "[color=#222222]Эта информация тут только для дебага, цыц, вы этого не видели. Не, ну серьёзно. Ну, а раз видели, значит её сейчас, к сожалению, сейчас не станет. Это сообщение отправлено ботом OreNew, пожалуйста, не читайте его.[/color]");
                    ieb.Form(WatiN.Core.Find.ById("ips_fastReplyForm")).Submit();
                    ieb.WaitForComplete();
                    ieb.GoTo(ieb.Link(WatiN.Core.Find.ByTitle("Изменить")).Url);
                    ieb.WaitForComplete();
                    ieb.TextField(WatiN.Core.Find.ByName("Post")).TypeText(ar2[rnd]);
                    ieb.Form(WatiN.Core.Find.ById("postingform")).Submit();
                    System.Console.WriteLine("Выполнено");
                    ieb.GoTo("http://ozwar.ru/forum/index.php?/user/609-benderfromfuture/");
                    ieb.WaitForComplete();
                    System.Console.WriteLine("Баланс: " + ieb.Span(WatiN.Core.Find.ByClass("fc")).Text.ToString());
                    co++;
                }
                else
                {
                    System.Console.WriteLine("Найдены следы нас!");
                    co++;
                }
                System.Console.WriteLine("Ссылки кончились, бот завершил свою работу");
                ieb.Link(WatiN.Core.Find.ByTitle("Выход")).Click();
                System.Console.WriteLine("Выход из профиля...");
                System.Console.ReadLine();
            }
        }
    }
}
