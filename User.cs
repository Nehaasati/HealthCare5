namespace HealthCare5;

class User
{
	public string SSN;
	public string Password;

	public string Role;
	public User(string ssn, string password, string role)
	{
		SSN = ssn;
		Password = password;
		Role = role;
	}
}
