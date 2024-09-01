using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCreatingScripWithAddUserToDB.Model
{
    public class Account
    {
        private string _Login;
        private string _Password;

        public Account(string login)
        {
            _Login = login;
            _Password = "Passw0rd";
        }

        public string GetLogin()
        {
            return _Login;
        }
        public string GetPassword()
        {
            return _Password;
        }

        public void PrintAccount()
        {
            Console.WriteLine(_Login + " "+ _Password);
        }
    }
}
