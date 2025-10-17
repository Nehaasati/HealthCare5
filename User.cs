namespace HealthCare5;

public class User
{
    public string SSN;
    public string Password;

    public enum Role
    {
        SuperAdmin,
        Admin,
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

    public bool HasPermissions(Permission permission)
    {
		List<Permission> permissions = RolePermission.GetPermissions(this.UserRole);
		foreach (Permission per in permissions)
		{
			if (per == permission)
			{
				return true;
			}
		}
		return false;
    }
}


