using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

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
                    Account account = new Account(_TitleGroup + "-" + i);
                    _Accounts.Add(account);
                }
            }
        }

        public StringBuilder GenerateSqlRequests()
        {
            StringBuilder request = new StringBuilder();
            request.Append($"-- Группа {GetTitleGroup()}\n");
            foreach (Account account in _Accounts)
            {
                request.Append($"CREATE LOGIN [{account.GetLogin()}] WITH" +
                    $" PASSWORD=N'{account.GetPassword()}', DEFAULT_DATABASE=[master], CHECK_EXPIRATION=ON , CHECK_POLICY=ON\nGO\n");
                request.Append($"ALTER SERVER ROLE [bulkadmin] ADD MEMBER [{account.GetLogin()}]\nGO\n");
                request.Append($"ALTER SERVER ROLE [dbcreator] ADD MEMBER [{account.GetLogin()}]\nGO\n");
                request.Append($"USE [master]\nGO\nDENY VIEW ANY DATABASE TO [{account.GetLogin()}]\nGO\n");
            }
            return request;
        }
    }
}
