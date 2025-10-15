using App;
using System.Diagnostics;

List<User> users = new();
User? active_user = null;



bool running = true;

while (running)
{
    Console.Clear();

    if (active_user == null)
    {
        Console.WriteLine("=== HealthCare System Login ===");
        Console.Write("Username: ");
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

        Console.WriteLine("[q] - quit");

        switch (Console.ReadLine())
        {
            case "q": running = false; break;
        }

    }
}