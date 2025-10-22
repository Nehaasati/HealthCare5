using HealthCare5;
using System.Diagnostics;

// Create lists to store users and locations 
List<User> users = new();
List<Location> locations = new();
// Create a Permission object for handling approvals
Permission permission = new();

User? active_user = null;

// sample users
users.Add(new User("111", "1111", User.Role.Admin));
users.Add(new User("222", "2222", User.Role.Admin));
users.Add(new User("333", "3333", User.Role.Personnel));
users.Add(new User("888", "8888", User.Role.Personnel));
users.Add(new Patient("444", "4444") { Status = Permission.PatientStatus.Approved });
Patient patient555 = new Patient("555", "5555") { Status = Permission.PatientStatus.Approved };
patient555.AddJournalEntry("Fever on Oct 20.", new DateTime(2025, 10, 20), Permission.ReadLevel.PersonnelOnly);
patient555.AddJournalEntry("Hearing test result normal", new DateTime(2025, 10, 20), Permission.ReadLevel.All);
users.Add(patient555);

locations.Add(new Location("BVC", "LUND"));
locations[0].Appointment.Add(new Appointment(new DateTime(2025, 10, 22, 9, 0, 0), "333", "General checkup"));
locations[0].Appointment.Add(new Appointment(new DateTime(2025, 10, 22, 10, 30, 0), "888", "Dental cleaning"));

// Keep program running until user quits
bool running = true;

while (running)
{
    Console.Clear();
    if (active_user == null)
    {
        // Main menu
        Console.WriteLine("=== HealthCare System ===");
        Console.WriteLine("1. Login");
        Console.WriteLine("2. Register as patient");
        Console.WriteLine("q. Quit");
        Console.Write("Choose: ");
        string? choice = Console.ReadLine();

        if (choice == "1")
        {
            Console.Clear();
            Console.Write("SSN: ");
            string? username = Console.ReadLine();
            Console.Write("Password: ");
            string? password = Console.ReadLine();

            Debug.Assert(username != null);
            Debug.Assert(password != null);

            bool found = false;

            foreach (User user in users)
            {
                if (user.SSN == username && user.Password == password)
                {
                    if (user is Patient pat)
                    {
                        if (pat.Status == Permission.PatientStatus.Pending)
                        {
                            Console.WriteLine("Registration pending.");
                            Console.ReadLine();
                            found = true;
                            break;
                        }
                        else if (pat.Status == Permission.PatientStatus.Denied)
                        {
                            Console.WriteLine("Registration denied.");
                            Console.ReadLine();
                            found = true;
                            break;
                        }
                    }

                    active_user = user;
                    Console.WriteLine("Login successful.");
                    Console.ReadLine();
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                Console.WriteLine("Wrong SSN or password.");
                Console.ReadLine();
            }
        }
        else if (choice == "2")
        {
            Console.Clear();
            Console.WriteLine("=== Patient Registration ===");
            Console.Write("New SSN: ");
            string? newSSN = Console.ReadLine();
            Console.Write("New password: ");
            string? newPassword = Console.ReadLine();

            if (string.IsNullOrEmpty(newSSN) || string.IsNullOrEmpty(newPassword))
            {
                Console.WriteLine("Missing input.");
                Console.ReadLine();
                continue;
            }

            bool exists = false;
            foreach (User u in users)
            {
                if (u.SSN == newSSN)
                {
                    exists = true;
                    break;
                }
            }

            if (exists)
            {
                Console.WriteLine("SSN already exists.");
            }
            else
            {
                Patient newPatient = new(newSSN, newPassword);
                users.Add(newPatient);
                newPatient.RequestRegistration();
            }

            Console.ReadLine();
        }
        else if (choice == "q")
        {
            running = false;
        }
    }
    else
    {
        Console.Clear();
        Console.WriteLine("Logged in as: " + active_user.SSN + " (" + active_user.UserRole + ")");
        Console.WriteLine();

        // ---- SuperAdmin / Admin ----
        if (active_user.UserRole == User.Role.Admin || active_user.UserRole == User.Role.Admins)
        {
            Console.WriteLine("1. Add location");
            Console.WriteLine("2. View locations");
            Console.WriteLine("3. View pending patients");
            Console.WriteLine("4. Approve or deny patient");
            Console.WriteLine("5. View schedule for a location");
            Console.WriteLine("l. Logout");
            Console.Write("Choose: ");
            string? choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Clear();
                Console.Write("Location name: ");
                string? name = Console.ReadLine();
                Console.Write("Description: ");
                string? desc = Console.ReadLine();

                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(desc))
                {
                    locations.Add(new Location(name, desc));
                    Console.WriteLine("Location added.");
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }
                Console.ReadLine();
            }
            else if (choice == "2")
            {
                Console.Clear();
                Console.WriteLine("All locations:");
                if (locations.Count == 0)
                    Console.WriteLine("No locations yet.");
                else
                    foreach (var loc in locations)
                        Console.WriteLine("- " + loc.Name + ": " + loc.Description);
                Console.ReadLine();
            }
            else if (choice == "3")
            {
                Console.Clear();
                permission.ShowPendingPatients(users);
                Console.ReadLine();
            }
            else if (choice == "4")
            {
                Console.Clear();
                permission.ShowPendingPatients(users);
                Console.Write("Patient SSN: ");
                string? ssn = Console.ReadLine();

                Patient? selected = null;
                foreach (User u in users)
                {
                    if (u is Patient p && p.SSN == ssn)
                    {
                        selected = p;
                        break;
                    }
                }

                if (selected == null)
                {
                    Console.WriteLine("Patient not found.");
                }
                else
                {
                    Console.Write("Approve (a) or Deny (d)? ");
                    string? action = Console.ReadLine();

                    if (action == "a")
                        permission.ApprovePatient(selected);
                    else if (action == "d")
                        permission.DenyPatient(selected);
                    else
                        Console.WriteLine("Invalid choice.");
                }
                Console.ReadLine();
            }
            else if (choice == "5")
            {
                ShowLocationSchedule(locations); // Ändrat
            }
            else if (choice == "l")
            {
                active_user = null;
            }
        }

        // Patient
        else if (active_user.UserRole == User.Role.Patient)
        {
            Patient patientUser = (Patient)active_user;
            Console.WriteLine("1. View status");
            Console.WriteLine("2. View Journal");
            Console.WriteLine("l. Logout");
            Console.Write("Choose: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    patientUser.ShowStatus();
                    Console.ReadLine();
                    break;
                case "2":
                    patientUser.ShowJournal(patientUser);
                    Console.ReadLine();
                    break;
                case "l":
                    active_user = null;
                    break;
            }
        }

        // Personnel
        else if (active_user.UserRole == User.Role.Personnel)
        {
            Console.WriteLine("1. View locations");
            Console.WriteLine("2. View schedule for a location");
            Console.WriteLine("3. View patient journal");
            Console.WriteLine("4. Approve Appointment");
            Console.WriteLine("5. Modify Appointment");
            Console.WriteLine("6. Register Appointment");
            Console.WriteLine("7. Journal Entries With Different Read Permission");
            Console.WriteLine("8. Patients Journal Entries");
            Console.WriteLine("l. Logout");

            Console.Write("Choose: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    if (active_user.HasPermission(Permission.PermissionType.CanViewScheduleLocation))
                    {
                        Console.Clear();
                        Console.WriteLine("All locations:");
                        if (locations.Count == 0)
                            Console.WriteLine("No locations yet.");
                        else
                            foreach (var loc in locations)
                                Console.WriteLine("- " + loc.Name + ": " + loc.Description);
                        Console.WriteLine("Press Enter to continue");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("You do not have permission to view locations");
                        Console.ReadLine();
                    }
                    break;

                case "2":
                    if (active_user.HasPermission(Permission.PermissionType.CanViewScheduleLocation))
                        ShowLocationSchedule(locations); // Ändrat
                    else
                    {
                        Console.WriteLine("You do not have permission to view schedule");
                        Console.ReadLine();
                    }
                    break;

                case "3": // Ändrat: Patientjournal fixad
                    Console.Clear();
                    Console.WriteLine("=== View Patient Journal ===");
                    Console.Write("Enter patient SSN: ");
                    string? ssn3 = Console.ReadLine();

                    Patient? targetPatient = users.Find(u => u is Patient p && p.SSN == ssn3) as Patient;

                    if (targetPatient == null)
                        Console.WriteLine("Patient not found.");
                    else if (targetPatient.Status != Permission.PatientStatus.Approved)
                        Console.WriteLine("Patient is not approved. Journal access denied.");
                    else
                        targetPatient.ShowJournal(active_user);

                    Console.ReadLine();
                    break;

                case "4":
                    if (active_user.HasPermission(Permission.PermissionType.CanApproveAppointmentRequests))
                        Console.WriteLine("View appointment requests");
                    else
                        Console.WriteLine("You do not have permission to approve requests");
                    Console.ReadLine();
                    break;

                case "5":
                    if (active_user.HasPermission(Permission.PermissionType.CanModifyAppointments))
                        Console.WriteLine("View modify appointments");
                    else
                        Console.WriteLine("You do not have permission to modify appointments");
                    Console.ReadLine();
                    break;

                case "6":
                    if (active_user.HasPermission(Permission.PermissionType.CanRegisterAppointments))
                        Console.WriteLine("View register appointments");
                    else
                        Console.WriteLine("You do not have permission to register appointments");
                    Console.ReadLine();
                    break;

                case "7": // Journal Entries With Different Read Permission
                    if (active_user.HasPermission(Permission.PermissionType.CanMarkJournalEntriesWithDifferentReadPermissions))
                    {
                        Console.Clear();
                        Console.WriteLine("=== View Patient Journal ===");
                        Console.Write("Enter patient SSN: ");
                        string? patientSSN = Console.ReadLine();


                        Patient? selectedpatient = users.Find(u => u is Patient p && p.SSN == patientSSN) as Patient;

                        if (selectedpatient == null)
                        {
                            Console.WriteLine("Patient not found.");
                        }
                        else if (selectedpatient.Status != Permission.PatientStatus.Approved)
                        {
                            Console.WriteLine("Patient is not approved. Journal access denied.");
                        }
                        else
                        {

                            selectedpatient.ShowJournal(active_user);
                        }

                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("You do not have permission to view these entries");
                        Console.ReadLine();
                    }
                    break;

                case "8": // Patients Journal Entries
                    if (active_user.HasPermission(Permission.PermissionType.CanViewPatientsJournaEntries))
                    {
                        Console.Clear();
                        Console.WriteLine("=== View Patient Journal (All entries you are allowed to see) ===");
                        Console.Write("Enter patient SSN: ");
                        string? patientSSN = Console.ReadLine();


                        Patient? selectedPatient = users.Find(u => u is Patient p && p.SSN == patientSSN) as Patient;

                        if (selectedPatient == null)
                        {
                            Console.WriteLine("Patient not found.");
                        }
                        else if (selectedPatient.Status != Permission.PatientStatus.Approved)
                        {
                            Console.WriteLine("Patient is not approved. Journal access denied.");
                        }
                        else
                        {

                            selectedPatient.ShowJournal(active_user);
                        }

                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("You do not have permission to view patients journal entries");
                        Console.ReadLine();
                    }
                    break;


                case "l":
                    active_user = null;
                    break;
            }
        }
    }
}

// Ändrat: Flyttad funktion utanför alla loopar och switchar
bool ShowLocationSchedule(List<Location> locations)
{
    Console.Clear();

    if (locations.Count == 0)
    {
        Console.WriteLine("No locations yet.");
        Console.ReadLine();
        return false;
    }

    Console.WriteLine("Locations:");
    for (int i = 0; i < locations.Count; i++)
        Console.WriteLine((i + 1) + ". " + locations[i].Name + " - " + locations[i].Description);

    Console.Write("Enter location number: ");
    string choice = Console.ReadLine() ?? "";

    if (!int.TryParse(choice, out int number) || number < 1 || number > locations.Count)
    {
        Console.WriteLine("Invalid location number.");
        Console.ReadLine();
        return false;
    }

    Location selected = locations[number - 1];
    Console.Clear();
    Console.WriteLine("Schedule for: " + selected.Name + " - " + selected.Description);

    if (selected.Appointment.Count == 0)
        Console.WriteLine("No appointments scheduled.");
    else
        foreach (var a in selected.Appointment)
            Console.WriteLine("- " + a.GetInfo());

    Console.ReadLine();
    return true;
}

Console.WriteLine("Program closed.");
