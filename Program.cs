using HealthCare5;
using System.Diagnostics;

List<User> users = new();
users.Add(new User("123", "1234")); // example user already in system

List<Location> locations = new();
User? active_user = null;

bool running = true;

while (running)
{
    Console.Clear();

    if (active_user == null)
    {
        Console.WriteLine("=== HealthCare System ===");
        Console.WriteLine("1. Login");
        Console.WriteLine("2. Request registration as patient");
        Console.WriteLine("q. Quit");
        Console.Write("Select your option: ");

        switch (Console.ReadLine())
        {
            case "1":
                Console.Clear();
                Console.WriteLine("=== Login ===");
                Console.Write("Username (SSN): ");
                string? username = Console.ReadLine();

                Console.Write("Password: ");
                string? password = Console.ReadLine();

                Debug.Assert(username != null);
                Debug.Assert(password != null);

                foreach (User user in users)
                {
                    if (user.SSN == username && user.Password == password)
                    {
                        active_user = user;
                        break;
                    }
                }

                if (active_user == null)
                {
                    Console.WriteLine("Invalid username or password.\n Press Enter to try again.");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Login successful!\n Press Enter to continue.");
                    Console.ReadLine();
                }
                break;

            case "2":
                Console.Clear();
                Console.WriteLine("=== Request Registration as Patient ===");
                Console.Write("Enter your SSN: ");
                string? newSSN = Console.ReadLine();

                Console.Write("Enter your desired password: ");
                string? newPassword = Console.ReadLine();

                if (string.IsNullOrEmpty(newSSN) || string.IsNullOrEmpty(newPassword))
                {
                    Console.WriteLine("Invalid input. Press Enter to continue.");
                    Console.ReadLine();
                }
                else
                {
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
                        Console.WriteLine("An account with this SSN already exists. Press Enter to continue.");
                    }
                    else
                    {
                        users.Add(new User(newSSN, newPassword, "Pending"));
                        Console.WriteLine("Registration request sent! Wait for admin approval.");
                    }
                    Console.ReadLine();
                }
                break;

            case "q":
                running = false;
                break;

            default:
                Console.WriteLine("Invalid choice. Press Enter to continue.");
                Console.ReadLine();
                break;
        }
    }
    else
    {
        Console.Clear();
        Console.WriteLine("=== HealthCare System ===");
        Console.WriteLine($"Logged in as: {active_user.SSN}");
        Console.WriteLine();
        Console.WriteLine("1. Add Location");
        Console.WriteLine("2. View Locations");
        Console.WriteLine("l. Logout");
        Console.WriteLine("q. Quit");
        Console.Write("Select option: ");

        string? choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.Clear();
                Console.WriteLine("=== Add New Location ===");
                Console.Write("Location Name: ");
                string? name = Console.ReadLine();
                Console.Write("Description: ");
                string? description = Console.ReadLine();

                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(description))
                {
                    locations.Add(new Location(name, description));
                    Console.WriteLine("Location added successfully!");
                }
                else
                {
                    Console.WriteLine("Invalid input. Try again.");
                }
                Console.ReadLine();
                break;

            case "2":
                Console.Clear();
                Console.WriteLine("=== All Locations ===");
                if (locations.Count == 0)
                {
                    Console.WriteLine("No locations added yet.");
                }
                else
                {
                    foreach (Location loc in locations)
                    {
                        Console.WriteLine($"- {loc.Name}: {loc.Description}");
                    }
                }
                Console.ReadLine();
                break;

            case "l":
                active_user = null;
                Console.WriteLine("You have been logged out. Press Enter to continue.");
                Console.ReadLine();
                break;

            case "q":
                running = false;
                break;
        }
    }
}

Console.WriteLine("Program closed.");
