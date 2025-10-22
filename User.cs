using System.Security;

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
    public List<Permission.PermissionType> Permissions;
    public string? Region; // Added for admin region

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
            Permissions.Add(Permission.PermissionType.CanViewPatientsJournalEntries); // Fixed typo
        }
    }

    public bool HasPermission(Permission.PermissionType p)
    {
        return Permissions.Contains(p);
    }
}
