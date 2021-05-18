using System;

namespace Expenses.Data.Model.Authentication.Users
{
    public class User
    {
        public Guid? Id { get; set; }

        public string Username { get; set; }
        public string Email { get; set; }

        public UserRole Role { get; set; }

        public User() 
        { }

        public User(string username)
        {
            this.Username = username;
        }
    }
}
