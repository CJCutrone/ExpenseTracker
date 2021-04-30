using System;

namespace Expenses.Data.Model.Authentication.Users
{
    public class User
    {
        public Guid? Id { get; set; }
        public string Username { get; set; }

        public User(string username)
        {
            this.Username = username;
        }
    }
}
