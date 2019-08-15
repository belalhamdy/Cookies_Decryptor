using System;
using System.Collections.Generic;
using System.IO;

namespace Cookies_Decryptor
{
    internal class  OperaPasswords:IBrowserData
    {
        private readonly PasswordModel Data;
        public string Name { get; }
        private const string LoginDataPath = @"\..\Roaming\Opera Software\Opera Stable\Login Data";
        public OperaPasswords()
        {
            Name = "Opera";
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var loginDataFile = Path.GetFullPath(appData + LoginDataPath);
            Data = new PasswordModel(Name, loginDataFile);
        }

        public IEnumerable<LoginData> GetLoginData()
        {
            return Data.GetLoginData();
        }
    }
}
