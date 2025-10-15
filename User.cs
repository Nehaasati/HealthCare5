namespace App;

class User
{
	public string SSN; //Social Security Number
	public string Password;

	public User(string ssn, string password)
	{
		SSN = ssn;
		Password = password;
	}
}