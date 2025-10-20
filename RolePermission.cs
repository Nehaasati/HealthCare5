using System.Collections.Generic;

namespace HealthCare5;

public class RolePermission
{
    public static List<Permission> GetPermissions(User.Role userRole)
    {
        List<Permission> permissions = new List<Permission>();
        switch (userRole)
        {
            case User.Role.SuperAdmin:
                permissions.Add(Permission.AllPermission);
                permissions.Add(Permission.AddLocation);
                permissions.Add(Permission.ApproveUser);
                permissions.Add(Permission.MangeUser);
                permissions.Add(Permission.RegisterAppointment);
                permissions.Add(Permission.RequestAppointment);
                permissions.Add(Permission.ViewJournal);
                permissions.Add(Permission.ViewLocation);
                permissions.Add(Permission.ViewOwnJournal);
                
                break;

            case User.Role.Admin:
                permissions.Add(Permission.AddLocation);
                permissions.Add(Permission.MangeUser);
                permissions.Add(Permission.ApproveUser);
                break;

            case User.Role.Personnel:
                permissions.Add(Permission.ApproveUser);
                permissions.Add(Permission.ViewLocation);
                permissions.Add(Permission.RegisterAppointment);
                permissions.Add(Permission.ViewJournal);
                break;

            case User.Role.Patient:
                permissions.Add(Permission.ViewLocation);
                permissions.Add(Permission.ViewOwnJournal);
                permissions.Add(Permission.RequestAppointment);
                break;
        }
        return permissions;
    }
}
