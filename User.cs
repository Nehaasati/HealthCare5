using System.Security;

namespace HealthCare5;

class User
{
	// user sercurity number
	public string SSN;

	public string Password;   // user password

	public enum Role // enum for user role


	{
		Admin,
		Admins,
		Personnel,
		Patient,
	}

	public Role UserRole;//  create constructor for user 
	public List<Permission.PermissionType> Permissions;



	public User(string ssn, string password, Role role)
	{
		SSN = ssn;
		Password = password;
		UserRole = role;
		Permissions = new List<Permission.PermissionType>();

		if (UserRole == Role.Personnel)

		{
			Permissions.Add(Permission.PermissionType.CanViewScheduleLocation);
			Permissions.Add(Permission.PermissionType.CanApproveAppointmentRequests);
			Permissions.Add(Permission.PermissionType.CanModifyAppointments);
			Permissions.Add(Permission.PermissionType.CanRegisterAppointments);
			Permissions.Add(Permission.PermissionType.CanMarkJournalEntriesWithDifferentReadPermissions);
			Permissions.Add(Permission.PermissionType.CanViewPatientsJournaEntries);


		}
	}


	public bool HasPermission(Permission.PermissionType p)


	{
		return Permissions.Contains(p);

	}

}












