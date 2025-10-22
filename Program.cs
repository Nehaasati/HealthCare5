using HealthCare5;
using System.Diagnostics;
// Create lists to store users and locations 
List<User> users = new();
List<Location> locations = new();
// Create a Permission object for handling approvals
Permission permission = new();

User? active_user = null;

// sample users
// Add some sample users
users.Add(new User("111", "1111", User.Role.Admin));
users.Add(new User("222", "2222", User.Role.Admin));
users.Add(new User("333", "3333", User.Role.Personnel));
users.Add(new User("888", "8888", User.Role.Personnel));
users.Add(new Patient("444", "4444") { Status = Permission.PatientStatus.Approved });
Patient patient555 = new Patient("555", "5555") { Status = Permission.PatientStatus.Approved };
patient555.JournalEntries.Add("Fever on Oct 20.");
users.Add(patient555);

locations.Add(new Location("BVC", "LUND"));
locations[0].Appointment.Add(new Appointment(new DateTime(2025, 10, 22, 9, 0, 0), "333", "General checkup"));
locations[0].Appointment.Add(new Appointment(new DateTime(2025, 10, 22, 10, 30, 0), "888", "Dental cleaning"));

// Keep program running until user quits
bool running = true;

while (running)
{
    // clear console
    Console.Clear();
    // If no user is logged in
    if (active_user == null)
    {
        // main menu
        Console.WriteLine("=== HealthCare System ===");
        Console.WriteLine("1. Login");
        Console.WriteLine("2. Register as patient");
        Console.WriteLine("q. Quit");
        Console.Write("Choose: ");

        string? choice = Console.ReadLine();
        //Login
        if (choice == "1")
        {
            Console.Clear();
            Console.Write("SSN: ");
            string? username = Console.ReadLine();
            Console.Write("Password: ");
            string? password = Console.ReadLine();

            Debug.Assert(username != null);
            Debug.Assert(password != null);
            // Track if user found
            bool found = false;
            // Check user list for matching SSN and password
            foreach (User user in users)
            {
                if (user.SSN == username && user.Password == password)
                {
                    //check registration status
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
                    // Login successful
                    active_user = user;
                    Console.WriteLine("Login successful.");
                    Console.ReadLine();
                    found = true;
                    break;
                }
            }
            // If no match found

            if (!found)
            {
                Console.WriteLine("Wrong SSN or password.");
                Console.ReadLine();
            }
        }
        // REGISTER AS PATIENT 
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
            // Check if SSN already exists
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
            // Create and add new patient
            {
                Patient newPatient = new(newSSN, newPassword);
                users.Add(newPatient);
                newPatient.RequestRegistration();
            }

            Console.ReadLine();
        }
        //quite
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
            // Add a new location
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
            // View all locations
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
            // Show pending patients
            else if (choice == "3")
            {
                Console.Clear();
                permission.ShowPendingPatients(users);
                Console.ReadLine();
            }
            // Approve or deny patient registration
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

                ShowLocationSchedule(locations);
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
            // Show current patient status
            if (choice == "1")
            {
                patientUser.ShowStatus();
                Console.ReadLine();
            }
            // Show patient journal
            else if (choice == "2")
            {
                patientUser.ShowJournal();
                Console.ReadLine();
            }
            else if (choice == "l")
            {
                active_user = null;
            }
        }



        // Personnel
        else if (active_user.UserRole == User.Role.Personnel)
        {
            Console.WriteLine("1. View locations");
            Console.WriteLine("l. Logout");
            Console.WriteLine("2. View schedule for a location");
            Console.WriteLine("3. View patient journal");
            Console.WriteLine("4. Register new Appointmet");
            Console.Write("Choose: ");
            string? choice = Console.ReadLine();

            // View all available locations

            if (choice == "1")
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
            else if (choice == "2")
            {

                ShowLocationSchedule(locations);
            }
            else if (choice == "3")
            {
                Console.Clear();
                Console.WriteLine("=== View Patient Journal ===");
                Console.Write("Enter patient SSN: ");
                string? ssn = Console.ReadLine();

                if (string.IsNullOrEmpty(ssn))
                {
                    Console.WriteLine("Invalid SSN.");
                    Console.ReadLine();
                    continue;
                }

                Patient? targetPatient = null;
                foreach (User u in users)
                {
                    if (u is Patient p && p.SSN == ssn)
                    {
                        targetPatient = p;
                        break;
                    }
                }

                if (targetPatient == null)
                {
                    Console.WriteLine("Patient not found.");
                }
                else
                {
                    // Only show if patient is Approved 
                    if (targetPatient.Status != Permission.PatientStatus.Approved)
                    {
                        Console.WriteLine("Patient is not approved. Journal access denied.");
                    }
                    else
                    {
                        targetPatient.ShowJournal();
                    }
                }
                Console.ReadLine();
            }


            else if (choice == "4")
            {
                Console.Clear();
                Console.WriteLine("Register New Appointment");

                // If no locations exist
                if (locations.Count == 0)
                {
                    Console.WriteLine("No locations available.");
                    Console.ReadLine();
                    continue;
                }

                // Show locations with numbers (1, 2, 3...)
                Console.WriteLine("Available locations:");
                int locNumber = 1;
                foreach (Location loc in locations)
                {
                    Console.WriteLine(locNumber + ". " + loc.Name + " - " + loc.Description);
                    locNumber++;
                }

                // Ask user to pick a location by number
                Console.Write("Enter location number (e.g. 1): ");
                string? locChoice = Console.ReadLine();

                // Find selected location using simple loop
                Location? selectedLocation = null;
                int counter = 1;
                foreach (Location loc in locations)
                {
                    if (locChoice == counter.ToString())
                    {
                        selectedLocation = loc;
                        break;
                    }
                    counter++;
                }

                if (selectedLocation == null)
                {
                    Console.WriteLine("Invalid location number.");
                    Console.ReadLine();
                    continue;
                }

                // Get appointment details
                Console.Write("Patient SSN: ");
                string? patientSSN = Console.ReadLine();

                Console.Write("Date (yyyy-MM-dd): ");
                string? dateInput = Console.ReadLine();

                Console.Write("Time (HH:mm): ");
                string? timeInput = Console.ReadLine();

                Console.Write("Reason: ");
                string? reason = Console.ReadLine();

                // Check if any field is empty
                if (string.IsNullOrEmpty(patientSSN) ||
                string.IsNullOrEmpty(dateInput) ||
                string.IsNullOrEmpty(timeInput) ||
                string.IsNullOrEmpty(reason))
                {
                    Console.WriteLine("All fields are required.");
                    Console.ReadLine();
                    continue;
                }

                // Try to create the appointment time
                try
                {
                    // Combine date and time
                    string fullDateTime = dateInput + " " + timeInput;
                    DateTime appointmentTime = DateTime.Parse(fullDateTime);

                    // Create and add appointment
                    Appointment newAppointment = new Appointment(appointmentTime, patientSSN, reason);
                    selectedLocation.AddAppointment(newAppointment);

                    Console.WriteLine("Appointment registered successfully!");
                }
                catch
                {
                    Console.WriteLine("Invalid date or time format. Please use yyyy-MM-dd and HH:mm.");
                }

                Console.ReadLine();
            }
            else if (choice == "l")
            {
                active_user = null;
            }
        }     

        bool ShowLocationSchedule(List<Location> locations)
        {

            Console.Clear();

            if (locations.Count == 0)
            {
                Console.WriteLine("No locations yet.");
                Console.ReadLine();
                return false;
            }

            // Show all available locations
            Console.WriteLine("Locations:");
            for (int i = 0; i < locations.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + locations[i].Name + " - " + locations[i].Description);
            }

            // Ask user to choose a location
            Console.Write("Enter location number: ");
            string choice = Console.ReadLine();

            int number = 0;

            // Check if input is a number
            if (int.TryParse(choice, out number) == false)
            {
                Console.WriteLine("Please enter a valid number.");
                Console.ReadLine();
                return false;
            }

            // Check if number is in range
            if (number < 1 || number > locations.Count)
            {
                Console.WriteLine("Invalid location number.");
                Console.ReadLine();
                return false;
            }

            // Get selected location
            Location selected = locations[number - 1];
            Console.Clear();
            Console.WriteLine("Schedule for: " + selected.Name + " - " + selected.Description);

            // Show appointments
            if (selected.Appointment.Count == 0)
            {
                Console.WriteLine("No appointments scheduled.");
            }
            else
            {
                for (int i = 0; i < selected.Appointment.Count; i++)
                {
                    Appointment a = selected.Appointment[i];
                    Console.WriteLine("- " + a.GetInfo());
                }
            }

            Console.ReadLine();
            return true;
        }

    }
    
    
}

Console.WriteLine("Program closed.");




