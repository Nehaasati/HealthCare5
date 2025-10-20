using HealthCare5;
using System.Diagnostics;

List<User> users = new();
List<Location> locations = new();
Permission permission = new();

User? active_user = null;

// sample users
users.Add(new User("111", "1111", User.Role.Admin));
users.Add(new User("222", "2222", User.Role.Admins));
users.Add(new User("333", "3333", User.Role.Personnel));
users.Add(new Patient("444", "4444") { Status = Permission.PatientStatus.Approved.ToString() });

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
                        if (pat.Status == Permission.PatientStatus.Pending.ToString())
                        {
                            Console.WriteLine("Registration pending.");
                            Console.ReadLine();
                            found = true;
                            break;
                        }
                        else if (pat.Status == Permission.PatientStatus.Denied.ToString())
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
            else if (choice == "l")
            {
                active_user = null;
            }
        }

        // ---- Patient ----
        else if (active_user.UserRole == User.Role.Patient)
        {
            Patient patientUser = (Patient)active_user;
            Console.WriteLine("1. View status");
            Console.WriteLine("2. View Journal");
            Console.WriteLine("l. Logout");
            Console.Write("Choose: ");
            string? choice = Console.ReadLine();

            if (choice == "1")
            {
                patientUser.ShowStatus();
                Console.ReadLine();
            }
            if(choice == "2")
            {
                patientUser.ShowJournal();
                Console.ReadLine();
            }
            else if (choice == "l")
            {
                active_user = null;
            }
        }



        // ---- Personnel ----
        else if (active_user.UserRole == User.Role.Personnel)
        {
            Console.WriteLine("1. View locations");
            Console.WriteLine("l. Logout");
            Console.Write("Choose: ");
            string? choice = Console.ReadLine();

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
            else if (choice == "l")
            {
                active_user = null;
            }
        }
    }
}

Console.WriteLine("Program closed.");




