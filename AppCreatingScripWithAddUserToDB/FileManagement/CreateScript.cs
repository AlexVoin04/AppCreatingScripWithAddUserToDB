using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AppCreatingScripWithAddUserToDB.Model;

namespace AppCreatingScripWithAddUserToDB.FileManagement
{
    public static class CreateScript
    {
        private static readonly string PathAdd = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\CreateLogin.sql";
        private static readonly string PathDel = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\RemoveLogin.sql";

        public static void CreateLogins(List<Model.Group> groups) 
        {
            StringBuilder request = new();
            foreach (Model.Group group in groups)
            {
                request.AppendLine(group.GenerateSqlRequests().ToString());
                Console.WriteLine($"Группа {group.GetTitleGroup()} записана на добавление!");
            }
            SaveFile(request, FileOperationStatus.Creation);
        }

        public static void RemoveLogins(List<string> groupsToRemove) 
        {
            StringBuilder deleteRequest = new StringBuilder();
            deleteRequest.AppendLine("-- Генерация SQL-запросов на удаление логинов");
            deleteRequest.AppendLine("USE [master]\nGO\nDECLARE @Sql NVARCHAR(MAX) = '';");
            foreach (string group in groupsToRemove)
            {
                deleteRequest.AppendLine($"\nSELECT @Sql += 'DROP LOGIN [' + name + ']; ' + CHAR(13) \nFROM sys.server_principals");
                deleteRequest.AppendLine($"WHERE type IN ('S', 'U') \nAND name LIKE '{group}-%';");
                Console.WriteLine($"Группа {group} записана на удаление!");
            }
            deleteRequest.AppendLine("\nEXEC sp_executesql @Sql;\nGO");
            SaveFile(deleteRequest, FileOperationStatus.Removal);
        }
        
        

        public static void SaveFile(StringBuilder content, FileOperationStatus fileOperationStatus)
        {
            string path = "";
            if (fileOperationStatus == FileOperationStatus.Creation)
                path = PathAdd;
            else if (fileOperationStatus == FileOperationStatus.Removal)
                path = PathDel;
            Console.WriteLine(PathAdd); 
            try 
            {
                // полная перезапись файла
                using StreamWriter writer = new(path, false);
                writer.WriteLine(content);
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка при работе с файлом: {ex.Message}");
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Произошла непредвиденная ошибка: {ex.Message}");
            }
            
        }
    }
}
