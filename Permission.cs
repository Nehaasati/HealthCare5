namespace HealthCare5;

class Permission
{
  // Enum representing possible status of a patient
  public enum PatientStatus
  {
    Pending,  // The patient is waiting for approval
    Approved, // Patient has been approved
    Denied    // Patient has been denied
  }

  // Enum representing access permissions for personnel or admin
  public enum PermissionType
  {
    CanViewScheduleLocation,
    CanApproveAppointmentRequests,
    CanModifyAppointments,
    CanRegisterAppointments,
    CanMarkJournalEntriesWithDifferentReadPermissions,
    CanViewPatientsJournaEntries
  }

  // Check if user has the requested permission
  public static bool HasPermission(User user, PermissionType type)
  {
    //Both Admin and Personnel have all permissions for now
    if (user.UserRole == User.Role.Admin || user.UserRole == User.Role.Personnel)
      return true;
    return false;
  }

  // Approve patient (with option to change decision if previously denied)
  public void ApprovePatient(Patient patient)
  {
    if (patient.Status == PatientStatus.Pending)
    {
      patient.Status = PatientStatus.Approved;
      Console.WriteLine($"Patient approved: {patient.SSN}");
    }
    else if (patient.Status == PatientStatus.Denied)
    {
      Console.WriteLine($"Patient {patient.SSN} was previously denied.");
      Console.Write("Change to Approved? (y/n): ");
      string? input = Console.ReadLine();
      if (input?.ToLower() == "y")
      {
        patient.Status = PatientStatus.Approved;
        Console.WriteLine("Decision changed to Approved.");
      }
      else
      {
        Console.WriteLine("No change made.");
      }
    }
    else
    {
      Console.WriteLine("Patient is already approved.");
    }
  }

  // Deny patient (with option to change decision if previously approved)
  public void DenyPatient(Patient patient)
  {
    if (patient.Status == PatientStatus.Pending)
    {
      patient.Status = PatientStatus.Denied;
      Console.WriteLine($"Patient denied: {patient.SSN}");
    }
    else if (patient.Status == PatientStatus.Approved)
    {
      Console.WriteLine($"Patient {patient.SSN} was previously approved.");
      Console.Write("Change to Denied? (y/n): ");
      string? input = Console.ReadLine();
      if (input?.ToLower() == "y")
      {
        patient.Status = PatientStatus.Denied;
        Console.WriteLine("Decision changed to Denied.");
      }
      else
      {
        Console.WriteLine("No change made.");
      }
    }
    else
    {
      Console.WriteLine("Patient is already denied.");
    }
  }

  // Displays a list of all patients currently pending for approval
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
}



