namespace HealthCare5;

class Personnel : User
{
  public Personnel(string ssn, string password)
      : base(ssn, password, Role.Personnel)
  {
  }

  // View journal entries of an approved patient
  public void ViewPatientJournal(List<User> users)
  {
    Console.Clear();
    Console.WriteLine("=== View Patient Journal ===");
    Console.Write("Enter patient SSN: ");
    string? ssn = Console.ReadLine();

    Patient? target = null;
    foreach (User u in users)
    {
      if (u is Patient p && p.SSN == ssn)
      {
        target = p;
        break;
      }
    }

    if (target == null)
    {
      Console.WriteLine("Patient not found.");
    }
    else if (target.Status != Permission.PatientStatus.Approved)
    {
      Console.WriteLine("Patient is not approved. Journal access denied.");
    }
    else
    {
      Console.WriteLine($"Journal for patient {target.SSN}:");
      target.ShowJournal();
    }

    Console.WriteLine("\nPress Enter to continue...");
    Console.ReadLine();
  }

  // Register a new appointment (used when personnel schedules visits)
  public void RegisterAppointment(List<Location> locations)
  {
    Console.Clear();
    Console.WriteLine("=== Register Appointment ===");

    if (locations.Count == 0)
    {
      Console.WriteLine("No locations available.");
      Console.ReadLine();
      return;
    }

    Console.WriteLine("Available locations:");
    for (int i = 0; i < locations.Count; i++)
    {
      Console.WriteLine($"{i + 1}. {locations[i].Name} - {locations[i].Description}");
    }

    Console.Write("Choose location number: ");
    string? locInput = Console.ReadLine();

    if (!int.TryParse(locInput, out int locNumber) || locNumber < 1 || locNumber > locations.Count)
    {
      Console.WriteLine("Invalid location number.");
      Console.ReadLine();
      return;
    }

    Location selectedLocation = locations[locNumber - 1];

    Console.Write("Patient SSN: ");
    string? ssn = Console.ReadLine();

    Console.Write("Date (yyyy-MM-dd): ");
    string? dateStr = Console.ReadLine();

    Console.Write("Time (HH:mm): ");
    string? timeStr = Console.ReadLine();

    Console.Write("Notes: ");
    string? notes = Console.ReadLine();

    if (string.IsNullOrEmpty(ssn) || string.IsNullOrEmpty(dateStr) || string.IsNullOrEmpty(timeStr))
    {
      Console.WriteLine("Missing input.");
      Console.ReadLine();
      return;
    }

    try
    {
      DateTime dateTime = DateTime.Parse($"{dateStr} {timeStr}");
      Appointment appt = new Appointment(dateTime, ssn, notes ?? "");
      selectedLocation.Appointment.Add(appt);
      Console.WriteLine("Appointment registered successfully!");
    }
    catch
    {
      Console.WriteLine("Invalid date or time format.");
    }

    Console.ReadLine();
  }
}

