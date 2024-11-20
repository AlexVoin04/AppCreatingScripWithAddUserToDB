// See https://aka.ms/new-console-template for more informatio
using AppCreatingScripWithAddUserToDB.FileManagement;
using AppCreatingScripWithAddUserToDB.Model;
using System.Diagnostics.Metrics;


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
        List<Group> groups = new List<Group>();
        List<string> groupsToRemove = new List<string>();

        Console.WriteLine("Приложения по созданию скрипта, для добавления и удаления пользователей на сервере баз данных MS SQL Server");
        Console.WriteLine();
        while (true)
        {
            Console.WriteLine("Выберите действие:\n1 - Добавить группу\n2 - Удалить группу\n0 - Выход");
            Console.Write("Действие: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    AddGroup(groups);
                    break;
                case "2":
                    RemoveGroup(groupsToRemove);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }

        }
    }

    private static void AddGroup(List<Group> groups)
    {
        bool added = true;
        while (added)
        {
            Console.Write("Введите название группы для добавления: ");
            string titleGroup = Console.ReadLine();

            Console.Write("Введите количество человек в группе: ");
            int numberPeopleInGroup = int.Parse(Console.ReadLine());

            groups.Add(new Group(titleGroup, numberPeopleInGroup));

            Console.Write("Добавить еще одну группу? Y/N?: ");
            string answer = Console.ReadLine();
            if (answer == "N" || answer == "n")
            {
                added = false;
            }
        }

        CreateScript.CreateLogins(groups);

        //foreach (Group group in Groups)
        //{
        //    foreach (Account acc in group._Accounts)
        //    {
        //        acc.PrintAccount();
        //    }
        //}
    }

    private static void RemoveGroup(List<string> groups)
    {
        bool added = true;
        while (added)
        {
            Console.Write("Введите название группы для удаления: ");
            string titleGroup = Console.ReadLine();

            groups.Add(titleGroup);

            Console.Write("Удалить еще одну группу? Y/N?: ");
            string answer = Console.ReadLine();
            if (answer == "N" || answer == "n")
            {
                added = false;
            }
        }

        CreateScript.RemoveLogins(groups);
    }
}