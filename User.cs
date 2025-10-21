namespace HealthCare5;

class User
{
	// user sercurity number
	public string SSN;
	// user password
	public string Password;
	// enum for user role
   	public enum Role
	{
		Admin,
		Admins,
		Personnel,
		Patient,
	}

	public Role UserRole;
	//  create constructor for user 
	public User(string ssn, string password, Role role)
	{
		SSN = ssn;
		Password = password;
		UserRole = role;
	}

}


