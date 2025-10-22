namespace HealthCare5;
// Patient inherited from user
class Patient : User
{
  //Add status feild
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

//show current status 
  public void ShowStatus()
  {
    System.Console.WriteLine("Status: " + Status);
  }

  public void ShowJournal()
  {
    System.Console.WriteLine("Journal is empty.");
  }
}


