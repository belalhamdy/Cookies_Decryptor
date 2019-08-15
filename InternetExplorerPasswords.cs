using System.Collections.Generic;
using System.Linq;
using Windows.Security.Credentials;

namespace Cookies_Decryptor
{
    /// <summary>
    /// useful link https://stackoverflow.com/questions/17741424/retrieve-credentials-from-windows-credentials-store-using-c-sharp
    /// To enable it:
    /// 1- Unload project file
    /// 2- Edit it
    /// 3- Add<TargetPlatformVersion>8.0</TargetPlatformVersion> to the PropertyGroup part
    /// 4- Add reference to Windows.Security (you'll have a list of Windows Libraries)
    /// 5- Add System.Runtime.WindowsRuntime.dll located in C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETCore\v4.5
    /// </summary>
    class InternetExplorerPasswords :IBrowserData
    {
        public string Name => "Internet_Explorer";

        public IEnumerable<LoginData> GetLoginData()
        {
            var result = new List<LoginData>();
            var vault = new PasswordVault();
            var credentials = vault.RetrieveAll();
            for (var i = 0; i < credentials.Count; ++i)
            {
                var cred = credentials.ElementAt(i);
                cred.RetrievePassword();

                result.Add(new LoginData
                {
                    Url = cred.Resource,
                    Username = cred.UserName,
                    Password = cred.Password
                });
            }
            return result;
        }
    }
}
