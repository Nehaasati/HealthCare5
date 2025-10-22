using HealthCare5;
using System.Diagnostics;

// Lists to store users and locations
List<User> users = new();
List<Location> locations = new();
Permission permission = new();

User? active_user = null;

// Sample users
users.Add(new User("111", "1111", User.Role.Admin));
users.Add(new User("222", "2222", User.Role.Admin));
users.Add(new User("333", "3333", User.Role.Personnel));
users.Add(new User("888", "8888", User.Role.Personnel));
users.Add(new Patient("444", "4444") { Status = Permission.PatientStatus.Approved });
Patient patient555 = new Patient("555", "5555") { Status = Permission.PatientStatus.Approved };
patient555.JournalEntries.Add("Fever on Oct 20.");
users.Add(patient555);

// Sample location and appointments
locations.Add(new Location("BVC", "LUND"));
locations[0].Appointments.Add(new Appointment(new DateTime(2025, 10, 22, 9, 0, 0), "333", "General checkup"));
locations[0].Appointments.Add(new Appointment(new DateTime(2025, 10, 22, 10, 30, 0), "888", "Dental cleaning"));

// Program loop
bool running = true;

while (running)
{
    Console.Clear();
    if (active_user == null)
    {
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

            bool exists = users.Exists(u => u.SSN == newSSN);
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
        Console.WriteLine($"Logged in as: {active_user.SSN} ({active_user.UserRole})\n");

        // Admin
        if (active_user.UserRole == User.Role.Admin || active_user.UserRole == User.Role.Admins)
        {
            Console.WriteLine("1. Add location");
            Console.WriteLine("2. View locations");
            Console.WriteLine("3. View pending patients");
            Console.WriteLine("4. Approve or deny patient");
            Console.WriteLine("5. View location schedule");
            Console.WriteLine("6. Assign permission to admin");
            Console.WriteLine("7. Assign region to admin");
            Console.WriteLine("l. Logout");
            Console.Write("Choose: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
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
                    break;

                case "2":
                    Console.Clear();
                    Console.WriteLine("All locations:");
                    foreach (var loc in locations)
                        Console.WriteLine($"- {loc.Name}: {loc.Description}");
                    Console.ReadLine();
                    break;

                case "3":
                    Console.Clear();
                    permission.ShowPendingPatients(users);
                    Console.ReadLine();
                    break;

                case "4":
                    Console.Clear();
                    permission.ShowPendingPatients(users);
                    Console.Write("Patient SSN: ");
                    string? ssn = Console.ReadLine();
                    Patient? selected = users.Find(u => u is Patient p && p.SSN == ssn) as Patient;
                    if (selected == null) Console.WriteLine("Patient not found.");
                    else
                    {
                        Console.Write("Approve (a) or Deny (d)? ");
                        string? action = Console.ReadLine();
                        if (action == "a") permission.ApprovePatient(selected);
                        else if (action == "d") permission.DenyPatient(selected);
                        else Console.WriteLine("Invalid choice.");
                    }
                    Console.ReadLine();
                    break;

                case "5":
                    ShowLocationSchedule(locations);
                    break;

                case "6":
                    Console.Clear();
                    Console.Write("Admin SSN: ");
                    string? adminSSN = Console.ReadLine();
                    User? adminUser = users.Find(u => (u.UserRole == User.Role.Admin || u.UserRole == User.Role.Admins) && u.SSN == adminSSN);
                    if (adminUser != null)
                    {
                        Console.WriteLine("Select permission number:");
                        int i = 1;
                        foreach (var perm in Enum.GetValues(typeof(Permission.PermissionType)))
                        {
                            Console.WriteLine($"{i}. {perm}");
                            i++;
                        }
                        Console.Write("Choice: ");
                        if (int.TryParse(Console.ReadLine(), out int permChoice) && permChoice >= 1 && permChoice <= Enum.GetValues(typeof(Permission.PermissionType)).Length)
                        {
                            var selectedPerm = (Permission.PermissionType)Enum.GetValues(typeof(Permission.PermissionType)).GetValue(permChoice - 1)!;
                            permission.AssignPermissionsToAdmin(adminUser, selectedPerm);
                        }
                    }
                    else Console.WriteLine("Admin not found.");
                    Console.ReadLine();
                    break;

                case "7":
                    Console.Clear();
                    Console.Write("Admin SSN: ");
                    string? regionSSN = Console.ReadLine();
                    User? regionAdmin = users.Find(u => (u.UserRole == User.Role.Admin || u.UserRole == User.Role.Admins) && u.SSN == regionSSN);
                    if (regionAdmin != null)
                    {
                        Console.Write("Region: ");
                        string? region = Console.ReadLine();
                        if (!string.IsNullOrEmpty(region)) permission.AssignRegionToAdmin(regionAdmin, region);
                    }
                    else Console.WriteLine("Admin not found.");
                    Console.ReadLine();
                    break;

                case "l":
                    active_user = null;
                    break;
            }
        }

        // Patient
        else if (active_user.UserRole == User.Role.Patient)
        {
            Patient patientUser = (Patient)active_user;
            Console.WriteLine("1. View status");
            Console.WriteLine("2. View journal");
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
                    patientUser.ShowJournal();
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
            Console.WriteLine("2. View schedule");
            Console.WriteLine("3. View patient journal");
            Console.WriteLine("l. Logout");
            Console.Write("Choose: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    if (active_user.HasPermission(Permission.PermissionType.CanViewScheduleLocation))
                    {
                        Console.Clear();
                        Console.WriteLine("Locations:");
                        foreach (var loc in locations) Console.WriteLine($"- {loc.Name}: {loc.Description}");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("No permission.");
                        Console.ReadLine();
                    }
                    break;

                case "2":
                    if (active_user.HasPermission(Permission.PermissionType.CanViewScheduleLocation)) ShowLocationSchedule(locations);
                    else { Console.WriteLine("No permission."); Console.ReadLine(); }
                    break;

                case "3":
                    Console.Clear();
                    Console.Write("Enter patient SSN: ");
                    string? patientSSN = Console.ReadLine();
                    Patient? targetPatient = users.Find(u => u is Patient p && p.SSN == patientSSN) as Patient;
                    if (targetPatient == null) Console.WriteLine("Patient not found.");
                    else if (targetPatient.Status != Permission.PatientStatus.Approved) Console.WriteLine("Patient not approved.");
                    else targetPatient.ShowJournal();
                    Console.ReadLine();
                    break;

                case "l":
                    active_user = null;
                    break;
            }
        }
    }
}

// Show location schedule
bool ShowLocationSchedule(List<Location> locations)
{
    Console.Clear();
    if (locations.Count == 0)
    {
        Console.WriteLine("No locations.");
        Console.ReadLine();
        return false;
    }

    Console.WriteLine("Locations:");
    for (int i = 0; i < locations.Count; i++)
        Console.WriteLine($"{i + 1}. {locations[i].Name} - {locations[i].Description}");

    Console.Write("Select location number: ");
    if (!int.TryParse(Console.ReadLine(), out int number) || number < 1 || number > locations.Count)
    {
        Console.WriteLine("Invalid number.");
        Console.ReadLine();
        return false;
    }

    Location selected = locations[number - 1];
    Console.Clear();
    Console.WriteLine($"Schedule for {selected.Name}:");
    if (selected.Appointments.Count == 0) Console.WriteLine("No appointments.");
    else foreach (var app in selected.Appointments) Console.WriteLine("- " + app.GetInfo());

    Console.ReadLine();
    return true;
}

Console.WriteLine("Program closed.");
