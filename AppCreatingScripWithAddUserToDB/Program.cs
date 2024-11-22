// See https://aka.ms/new-console-template for more informatio
using AppCreatingScripWithAddUserToDB;
using AppCreatingScripWithAddUserToDB.Model;


//USE[master]
//GO
//CREATE LOGIN [123] WITH PASSWORD = N'123' MUST_CHANGE, DEFAULT_DATABASE =[master], CHECK_EXPIRATION = ON, CHECK_POLICY = ON
//GO
//ALTER SERVER ROLE [bulkadmin] ADD MEMBER[123]
//GO
//ALTER SERVER ROLE [dbcreator] ADD MEMBER[123]
//GO
//DENY VIEW ANY DATABASE TO [[123]]
//GO

internal class Program
{
    private static void Main(string[] args)
    {
        List<Group> groups = new();
        List<string> groupsToRemove = new();

        Console.WriteLine("Приложения по созданию скрипта, для добавления и удаления пользователей на сервере баз данных MS SQL Server\n");
        while (true)
        {
            Console.WriteLine("Выберите действие:\n1 - Добавить группу\n2 - Удалить группу" +
                "\n3 - Очистить группы для добавления\n4-Очистить группы для удаления\n0 - Выход");
            Console.Write("Действие: ");
            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    GroupService.AddGroup(groups);
                    break;
                case "2":
                    GroupService.RemoveGroup(groupsToRemove);
                    break;
                case "3":
                    groups.Clear();
                    break;
                case "4":
                    groupsToRemove.Clear();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }
    }
}