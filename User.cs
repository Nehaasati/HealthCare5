namespace HealthCare5;

class User
{
	public string SSN; //Social Security Number
	public string Password;
	public string Status;

	public User(string ssn, string password, string status = "Active")
	{
		SSN = ssn;
		Password = password;
		Status = status;
	}
}