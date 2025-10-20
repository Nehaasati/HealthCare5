namespace HealthCare5;

class User
{
	public string SSN;
	public string Password;

	public enum Role
	{
		Admin,
		Admins,
		Personnel,
		Patient,
	}

	public Role UserRole;
	public User(string ssn, string password, Role role)
	{
		SSN = ssn;
		Password = password;
		UserRole = role;
	}

}


