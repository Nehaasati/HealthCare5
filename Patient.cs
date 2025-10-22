namespace HealthCare5;
// Patient inherited from user
class Patient : User
{
  //Add status feild
  public Permission.PatientStatus Status; // enum, not string
                                          // Add a list to store journal entries


  public List<Permission.JournalEntry> JournalEntries = new List<Permission.JournalEntry>();


  public void AddJournalEntry(string text, DateTime date, Permission.ReadLevel readLevel)
  {

    JournalEntries.Add(new Permission.JournalEntry(text, date, readLevel));

  }


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

  public void ShowJournal(User viewer) // Shows what user is viewing.
  {
    Console.WriteLine($"Journal for patient {SSN}:");
    if (JournalEntries.Count == 0)
    {
      Console.WriteLine("No journal entries.");
    }
    else
    {
      foreach (Permission.JournalEntry entry in JournalEntries) // Loops thru JournalEntry objects
      {

        if (entry.ReadLevel == Permission.ReadLevel.All ||

            (entry.ReadLevel == Permission.ReadLevel.AdminOnly && viewer.UserRole == User.Role.Admin) || // Admin can read and admin can view

            (entry.ReadLevel == Permission.ReadLevel.PersonnelOnly && viewer.UserRole == User.Role.Personnel) || // only personel can view and read.

            (viewer.UserRole == User.Role.Patient && viewer.SSN == this.SSN)) // checks to see if viewer has patient role by checking with ssn-number patient can view journal.
        {
          Console.WriteLine($"{entry.Date.ToShortDateString()} - {entry.Text} (ReadLevel: {entry.ReadLevel})"); // Displays the journal entry's date, content, and read permission level in a readable format.

        }
      }
    }
  }
}

