namespace HealthCare5;

class Permission
{
  public enum PatientStatus
  {
    Pending,
    Approved,
    Denied
  }

  public void ApprovePatient(Patient patient)
  {
    if (patient.Status.ToString().Contains("Pending"))
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
    if (patient.Status.ToString().Contains("Pending"))
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
      if (u is Patient patient)
      {
        if (patient.Status.ToString().Contains("Pending"))
        {
          Console.WriteLine("- " + patient.SSN);
          any = true;
        }
      }
    }

    if (!any)
    {
      Console.WriteLine("No pending patients.");
    }
  }
}


