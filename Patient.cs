namespace HealthCare5;
// Patient inherited from user
class Patient : User
{
  //Add status feild
  public Permission.PatientStatus Status; // enum, not string
                                          // Add a list to store journal entries
  public List<string> JournalEntries = new List<string>();
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
    Console.WriteLine("Journal for patient {SSN}:");
    if (JournalEntries.Count == 0)
    {
      Console.WriteLine("No journal entries.");
    }
    else
    {
      foreach (string entry in JournalEntries)
      {
        Console.WriteLine("- " + entry);
      }
    }
  }

}