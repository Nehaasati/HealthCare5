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
    if (patient.Status == PatientStatus.Pending.ToString())
    {
      patient.Status = PatientStatus.Approved.ToString();
      System.Console.WriteLine("Approved: " + patient.SSN);
    }
    else
    {
      System.Console.WriteLine("Not pending.");
    }
  }

  public void DenyPatient(Patient patient)
  {
    if (patient.Status == PatientStatus.Pending.ToString())
    {
      patient.Status = PatientStatus.Denied.ToString();
      System.Console.WriteLine("Denied: " + patient.SSN);
    }
    else
    {
      System.Console.WriteLine("Not pending.");
    }
  }

  public void ShowPendingPatients(List<User> users)
  {
    System.Console.WriteLine("Pending patients:");
    bool any = false;

    foreach (User u in users)
    {
      
      Patient? p = u as Patient;

      if (p != null)
      {
        
        if (p.Status == PatientStatus.Pending.ToString())
        {
          System.Console.WriteLine("- " + p.SSN);
          any = true;
        }
      }
    }

    if (!any)
    {
      System.Console.WriteLine("None.");
    }
  }
}

