using HealthCare5;
using System.Diagnostics;

List<User> users = new();
users.Add(new User("123", "1234")); // example user

List<Location> locations = new();   // store added locations
User? active_user = null;

bool running = true;

while (running)
{
    Console.Clear();

    if (active_user == null)
    {
        Console.WriteLine("=== HealthCare System Login ===");
        Console.Write("Username (SSN): ");
        string? username = Console.ReadLine();

        Console.Clear();
        Console.WriteLine("=== HealthCare System Login ===");
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
            Console.WriteLine("Invalid username or password. Press any key to try again.");
            Console.ReadKey();
        }
    }
    else
    {
        Console.Clear();
        Console.WriteLine("=== HealthCare System ===");
        Console.WriteLine("[1] Add Location");
        Console.WriteLine("[2] View Locations");
        Console.WriteLine("[l] Logout");
        Console.WriteLine("[q] - quit");

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
            case "q": running = false; break;

            case "l":
                active_user = null; // logout
                break;
        }
    }
}
