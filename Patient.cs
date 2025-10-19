namespace HealthCare5;

class Patient : User
{
  public string Status; // Pending, Approved, Denied
  public string Journal;

  public Patient(string ssn, string password)
      : base(ssn, password, Role.Patient)
  {
    Status = Permission.PatientStatus.Pending.ToString();
  }

  public void RequestRegistration()
  {
    System.Console.WriteLine("Registration request sent.");
  }

  public void ShowJournal()
  {
    System.Console.WriteLine("My journal: " + Journal);
  }

  public void ShowStatus()
  {
    System.Console.WriteLine("Status: " + Status);
  }
}

