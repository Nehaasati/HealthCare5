namespace HealthCare5;

class Patient : User
{
  public Permission.PatientStatus Status; // enum, not string

  public Patient(string ssn, string password)
      : base(ssn, password, Role.Patient)
  {
    Status = Permission.PatientStatus.Pending;
  }

  public void RequestRegistration()
  {
    System.Console.WriteLine("Registration request sent.");
  }

  public void ShowStatus()
  {
    System.Console.WriteLine("Status: " + Status);
  }

  public void ShowJournal()
  {
    System.Console.WriteLine("Journal is empty.");
  }
}


