using System;
using System.Collections.Generic;
using System.Linq;
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

        public void SaveToFileScript()
        {

        }
    }
}
