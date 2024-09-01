using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AppCreatingScripWithAddUserToDB.Model
{
    public static class CreateScript
    {
        private static string Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\CreateLogin.sql";

        public static void SaveFile(List<Group> Groups)
        {
            Console.WriteLine(Path);
            // полная перезапись файла 
            using (StreamWriter writer = new StreamWriter(Path, false))
            {
                foreach (Group group in Groups)
                {
                    StringBuilder request = new StringBuilder();
                    foreach (Account account in group._Accounts)
                    {
                        request.Append($"CREATE LOGIN [{account.GetLogin()}] WITH" +
                            $" PASSWORD=N'{account.GetPassword()}', DEFAULT_DATABASE=[master], CHECK_EXPIRATION=ON , CHECK_POLICY=ON\nGO\n");
                        request.Append($"ALTER SERVER ROLE [bulkadmin] ADD MEMBER [{account.GetLogin()}]\nGO\n");
                        request.Append($"ALTER SERVER ROLE [dbcreator] ADD MEMBER [{account.GetLogin()}]\nGO\n");
                        request.Append($"USE [master]\nGO\nDENY VIEW ANY DATABASE TO [{account.GetLogin()}]\nGO\n");
                    }
                    
                    writer.WriteLine(request);
                    Console.WriteLine("Файл сохранен на рабочий стол!");
                }

            }
        }
    }
}
