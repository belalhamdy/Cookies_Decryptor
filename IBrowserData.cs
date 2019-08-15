using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookies_Decryptor
{
    interface IBrowserData
    {
        string Name { get; }
        IEnumerable<LoginData> GetLoginData();
    }
}
