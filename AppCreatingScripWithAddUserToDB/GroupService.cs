using AppCreatingScripWithAddUserToDB.FileManagement;
using AppCreatingScripWithAddUserToDB.Model;

namespace AppCreatingScripWithAddUserToDB
{
    internal class GroupService
    {
        public static void AddGroup(List<Group> groups)
        {
            bool added = true;
            while (added)
            {
                string titleGroup = GetNotNullString("Введите название группы для добавления: ");

                while (groups.Any(g => g.GetTitleGroup().Equals(titleGroup, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine("Ошибка: Группа с таким названием уже существует. Пожалуйста, введите другое название.");
                    titleGroup = GetNotNullString("Введите название группы для добавления: ");
                }

                string? numberInStr;
                int numberPeopleInGroup;

                do
                {
                    Console.Write("Введите количество человек в группе: ");
                    numberInStr = Console.ReadLine();

                    if (!int.TryParse(numberInStr, out numberPeopleInGroup) || numberPeopleInGroup <= 0)
                    {
                        Console.WriteLine("Ошибка: Введите число большее 0");
                    }
                } while (!int.TryParse(numberInStr, out numberPeopleInGroup) || numberPeopleInGroup <= 0);

                groups.Add(new Group(titleGroup, numberPeopleInGroup));

                string answer = GetNotNullString("Удалить еще одну группу? Y/N?: ");
                if (answer.ToLower() == "n" || answer.ToLower() == "н")
                {
                    added = false;
                }
            }
            CreateScript.CreateLogins(groups);
        }

        public static void RemoveGroup(List<string> groups)
        {
            bool added = true;
            while (added)
            {
                string titleGroup = GetNotNullString("Введите название группы для удаления: ");

                if (groups.Contains(titleGroup))
                {
                    Console.WriteLine("Ошибка: Группа с таким названием уже существует. Пожалуйста, введите другое название.");
                    continue;
                }
                else
                {
                    groups.Add(titleGroup);
                }

                string answer = GetNotNullString("Удалить еще одну группу? Y/N?: ");
                if (answer.ToLower() == "n" || answer.ToLower() == "н" )
                {
                    added = false;
                }
            }

            CreateScript.RemoveLogins(groups);
        }

        private static string GetNotNullString(string message)
        {
            string? titleGroup;
            do
            {
                Console.Write(message);
                titleGroup = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(titleGroup))
                {
                    Console.WriteLine("Ошибка: Значение не может быть пустым.");
                }
            } while (string.IsNullOrWhiteSpace(titleGroup));

            return titleGroup;
        }
    }
}
