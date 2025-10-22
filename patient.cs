namespace HealthCare5;

class Patient : User
{
    public Permission.PatientStatus Status;
    public List<string> JournalEntries = new List<string>();

    public Patient(string ssn, string password)
        : base(ssn, password, Role.Patient)
    {
        Status = Permission.PatientStatus.Pending;
    }

    public void RequestRegistration()
    {
        Console.WriteLine("Registration request sent.");
    }

    public void ShowStatus()
    {
        Console.WriteLine("Status: " + Status);
    }

    public void AddJournalEntry(string entry)
    {
        JournalEntries.Add(entry);
    }

    public void ShowJournal()
    {
        Console.WriteLine($"Journal for patient {SSN}:");
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

    public void RequestAppointment(List<Location> locations, string locationName, DateTime dateTime, string note = "")
    {
        Location? selected = null;
        foreach (var loc in locations)
        {
            if (loc.Name.ToLower() == locationName.ToLower())
            {
                selected = loc;
                break;
            }
        }

        if (selected == null)
        {
            Console.WriteLine("Location not found.");
            return;
        }

        Appointment newAppointment = new Appointment(dateTime, this.SSN, note);
        selected.Appointments.Add(newAppointment); // Fixed property name from Appointment -> Appointments
        Console.WriteLine($"Appointment requested at {selected.Name} on {dateTime}.");
    }

    public void ViewAppointments(List<Location> locations)
    {
        Console.WriteLine($"Appointments for patient {SSN}:");
        bool any = false;

        foreach (var loc in locations)
        {
            foreach (var app in loc.Appointments)
            {
                if (app.PatientSSN == this.SSN)
                {
                    Console.WriteLine($"- {app.GetInfo()} at {loc.Name}");
                    any = true;
                }
            }
        }

        if (!any)
        {
            Console.WriteLine("No appointments scheduled.");
        }
    }
}
