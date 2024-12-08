using System.Text;

namespace AppCreatingScripWithAddUserToDB.Model
{
    public class Group
    {
        private string _TitleGroup { get; set; }
        private int _NumberPeopleInGroup;
        public List<Account> _Accounts = new List<Account>();

        public Group(string titleGroup, int numberPeopleInGroup)
        {
            _TitleGroup = titleGroup;
            _NumberPeopleInGroup = numberPeopleInGroup;
            CreateAccountGroup();
        }

        public string GetTitleGroup()
        {
            return _TitleGroup;
        }

        public int GetNumberPeopleInGroup()
        {
            return _NumberPeopleInGroup;
        }

        private void CreateAccountGroup()
        {
            if (_NumberPeopleInGroup > 0)
            {
                for (int i = 1; i <= _NumberPeopleInGroup; i++)
                {
                    Account account = new(_TitleGroup + "-" + i.ToString("D2"));
                    _Accounts.Add(account);
                }
            }
        }

        public StringBuilder GenerateSqlRequests()
        {
            StringBuilder request = new();
            request.Append($"-- Группа {GetTitleGroup()}\n");
            foreach (Account account in _Accounts)
            {
                request.Append(account.AccountDefinition());
            }
            return request;
        }
    }
}
