// See https://aka.ms/new-console-template for more informatio
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

List<Group> Groups = new List<Group>();

Console.WriteLine("Приложения по созданию скрипта, для добавления пользователей на сервер баз данных MS SQL Server");
Console.WriteLine();
while (true)
{
    bool added = true;
    while (added)
    {
        Console.Write("Введите название группы: ");
        string titleGroup = Console.ReadLine();

        Console.Write("Введите количество человек в группе: ");
        int numberPeopleInGroup = int.Parse(Console.ReadLine());

        Groups.Add(new Group(titleGroup, numberPeopleInGroup));
        
        Console.Write("Добавить еще одну группу? Y/N?: ");
        string answear = Console.ReadLine();
        if (answear == "N" || answear == "n")
        {
            added = false;
        }
    }

    //foreach (Group group in Groups)
    //{
    //    foreach (Account acc in group._Accounts)
    //    {
    //        acc.PrintAccount();
    //    }
    //}

    CreateScript.SaveFile(Groups);

}