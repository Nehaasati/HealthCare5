using HealthCare5;
using System;
using System.Collections.Generic;
using System.Linq;

// ===== Users and Pending Patients =====
List<User> users = new();
users.Add(new User("111", "1111", User.Role.SuperAdmin));
users.Add(new User("222", "2222", User.Role.Admin));

List<Patient> pendingPatients = new(); // pending registration requests
Permission permission = new Permission();

User? active_user = null;
bool running = true;

// ===== Main Loop =====
while (running)
{
    Console.Clear();

    if (active_user == null)
    {
        // ===== Login =====
        Console.WriteLine("=== HealthCare System Login ===");
        Console.Write("Username (SSN): ");
        string? username = Console.ReadLine();
        Console.Write("Password: ");
        string? password = Console.ReadLine();

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
        // ===== Menu =====
        Console.Clear();
        Console.WriteLine($"Logged in as: {active_user.UserRole}");

        if (active_user.UserRole == User.Role.Patient)
            Console.WriteLine("[1] Request Registration");
        else if (active_user.UserRole == User.Role.Admin || active_user.UserRole == User.Role.SuperAdmin)
            Console.WriteLine("[1] Approve/Deny Patient Registrations");

        Console.WriteLine("[q] Logout");
        Console.Write("Select option: ");
        string? choice = Console.ReadLine();

        switch (choice)
        {
            // ===== Patient Request Registration =====
            case "1" when active_user.UserRole == User.Role.Patient:
                Console.Clear();
                Patient p = active_user as Patient;
                if (p != null)
                {
                    p.RequestRegistration();
                    pendingPatients.Add(p);
                }
                Console.ReadKey();
                break;

            // ===== Admin Approve/Deny =====
            case "1" when active_user.UserRole == User.Role.Admin || active_user.UserRole == User.Role.SuperAdmin:
                Console.Clear();
                Console.WriteLine("=== Pending Patient Registrations ===");

                if (pendingPatients.Count == 0)
                {
                    Console.WriteLine("No pending patients.");
                    Console.ReadKey();
                    break;
                }

                foreach (Patient pp in pendingPatients)
                    Console.WriteLine($"- {pp.SSN} [{pp.Status}]");

                Console.Write("Enter SSN to process: ");
                string? targetSSN = Console.ReadLine();
                Console.Write("Approve (A) / Deny (D)? ");
                string? action = Console.ReadLine();

                Patient? patient = pendingPatients.FirstOrDefault(pat => pat.SSN == targetSSN);

                if (patient != null)
                {
                    if (action?.ToUpper() == "A")
                    {
                        permission.ApprovePatient(patient);
                        users.Add(patient); // add to main users list
                        pendingPatients.Remove(patient);
                    }
                    else if (action?.ToUpper() == "D")
                    {
                        permission.DenyPatient(patient);
                        pendingPatients.Remove(patient);
                    }
                    else
                        Console.WriteLine("Invalid action.");
                }
                else
                    Console.WriteLine("Patient not found.");

                Console.ReadKey();
                break;

            // ===== Logout =====
            case "q":
                active_user = null;
                break;

            default:
                Console.WriteLine("Invalid option.");
                Console.ReadKey();
                break;
        }
    }
}
