using System.Text;

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

        public StringBuilder AccountDefinition() 
        {
            StringBuilder accountBody = new();
            accountBody.Append($"CREATE LOGIN [{GetLogin()}] WITH" +
                    $" PASSWORD=N'{GetPassword()}', DEFAULT_DATABASE=[master], CHECK_EXPIRATION=ON , CHECK_POLICY=ON\nGO\n");
            accountBody.Append($"ALTER SERVER ROLE [bulkadmin] ADD MEMBER [{GetLogin()}]\nGO\n");
            accountBody.Append($"ALTER SERVER ROLE [dbcreator] ADD MEMBER [{GetLogin()}]\nGO\n");
            accountBody.Append($"USE [master]\nGO\nDENY VIEW ANY DATABASE TO [{GetLogin()}]\nGO\n");
            return accountBody;
        }
    }
}
