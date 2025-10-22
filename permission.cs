namespace HealthCare5;

class Permission
{
    public enum PatientStatus
    {
        Pending,
        Approved,
        Denied
    }

    public enum PermissionType
    {
        CanViewScheduleLocation,
        CanApproveAppointmentRequests,
        CanModifyAppointments,
        CanRegisterAppointments,
        CanMarkJournalEntriesWithDifferentReadPermissions,
        CanViewPatientsJournalEntries, // Fixed typo
    }

    public void ApprovePatient(Patient patient)
    {
        if (patient.Status == PatientStatus.Pending)
        {
            patient.Status = PatientStatus.Approved;
            Console.WriteLine("Patient approved: " + patient.SSN);
        }
        else
        {
            Console.WriteLine("Patient not pending.");
        }
    }

    public void DenyPatient(Patient patient)
    {
        if (patient.Status == PatientStatus.Pending)
        {
            patient.Status = PatientStatus.Denied;
            Console.WriteLine("Patient denied: " + patient.SSN);
        }
        else
        {
            Console.WriteLine("Patient not pending.");
        }
    }

    public void ShowPendingPatients(List<User> users)
    {
        Console.WriteLine("Pending patients:");
        bool any = false;

        foreach (User u in users)
        {
            if (u is Patient patient && patient.Status == PatientStatus.Pending)
            {
                Console.WriteLine("- " + patient.SSN);
                any = true;
            }
        }

        if (!any)
        {
            Console.WriteLine("No pending patients.");
        }
    }

    public void AssignPermissionsToAdmin(User admin, PermissionType permission)
    {
        if (admin.UserRole != User.Role.Admin && admin.UserRole != User.Role.Admins)
        {
            Console.WriteLine("User is not an admin.");
            return;
        }

        if (!admin.Permissions.Contains(permission))
        {
            admin.Permissions.Add(permission);
            Console.WriteLine($"Permission {permission} assigned to {admin.SSN}");
        }
        else
        {
            Console.WriteLine("Admin already has this permission.");
        }
    }

    public void AssignRegionToAdmin(User admin, string region)
    {
        if (admin.UserRole != User.Role.Admin && admin.UserRole != User.Role.Admins)
        {
            Console.WriteLine("User is not an admin.");
            return;
        }

        admin.Region = region; // Now works because User.cs has Region property
        Console.WriteLine($"Region '{region}' assigned to admin {admin.SSN}");
    }
}
