namespace HealthCare5;

public class User
{
    public string SSN;
    public string Password;
    public Role UserRole; // <-- this is required

    public enum Role
    {
        SuperAdmin,
        Admin,
        Personnel,
        Patient
    }

    public User(string ssn, string password, Role role)
    {
        SSN = ssn;
        Password = password;
        UserRole = role;
    }
}
