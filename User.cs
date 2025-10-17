namespace HealthCare5;

class User
{
	public string SSN; //Social Security Number
	public string Email;
	public string Password;
	public string Status;

	public User(string ssn, string email, string password, string status = "Active")
	{
		SSN = ssn;
		Email = email;
		Password = password;
		Status = status;
	}
}