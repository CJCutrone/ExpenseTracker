using System;

namespace Expenses.Data.Model.Authentication.Credentials
{
    public class CredentialType
    {
        public Guid Id { get; set; }
	    public string Name { get; set; }
	    public string Code { get; set; }
	    public string Description { get; set; }
    }
}
