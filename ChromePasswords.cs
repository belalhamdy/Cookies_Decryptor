using System;
using System.Collections.Generic;
using System.IO;

namespace Cookies_Decryptor
{
    internal class ChromePasswords:IBrowserData
    {
        private readonly PasswordModel _data;
        public string Name { get; }
        private const string LoginDataPath = @"\..\Local\Google\Chrome\User Data\Default\Login Data";

        public ChromePasswords()
        {
            Name = "Google_Chrome";
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var loginDataFile = Path.GetFullPath(appData + LoginDataPath);
            _data = new PasswordModel(Name, loginDataFile);
        }
        public IEnumerable<LoginData> GetLoginData()
        {
            return _data.GetLoginData();
        }
    }
}
