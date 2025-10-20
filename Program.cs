/*
Health Care System
Beskrivning
Vi är nu i en situation som många av våra län och regioner ofta ser ut att
hamna i. Det vill säga, vi ska bygga ett nytt system för sjukvården. För att
göra detta så finns det vissa krav och riktlinjer som vi måste följa. Nedanför
hittar du en sektion med de kraven.

För att göra detta ordentligt så behöver vi bestämma oss om vad vår centrala
datastruktur ska vara för kommunikation mellan alla system som vi pratar
mellan. Så du kommer även finna tekniska rekommendationer och andra förslag i
en separat sektion nedanför, du kan välja att följa dem eller göra andra val.
Det viktiga är att systemet följer de user-features som är definierade. Om du
har frågor eller funderingar, tänk på vad som skulle behövas i ett realistiskt
scenario, och fråga även utbildaren om hjälp.

Det finns ingen anledning att stressa genom projektet, det är för många
features definierade för att bygga systememt med den korta tid som vi har på
oss. Arbeta istället ordentligt med varje del av projektet och dokumentera vad
ni gör. Så målet är alltså inte att vara feature-complete, det ska istället
vara stabilt gjort. Det kommer även förmodligen komma flera krav under
projektets gång så undvig gärna att försöka lösa problem som inte kommit upp
än, fokusera istället på varje problem för sig, steg för steg.

Krav och specifikationer
Nedan så kommer du finna en lista på projektets krav, på engelska, och
tillsammans med de kraven så ska följande generella guidelines följas:

Systemet ska vara designat med sekretess och säkerhet i åtanke, användare ska
endast ha tillgång till det deras roll behöver, inte mer.
Krav vid projektstart är följande:

As a user, I need to be able to log in.

As a user, I need to be able to log out.

As a user, I need to be able to request registration as a patient.

As an admin with sufficient permissions, I need to be able to give admins the permission to handle the permission system, in fine granularity.

As an admin with sufficient permissions, I need to be able to assign admins to certain regions.

As an admin with sufficient permissions, I need to be able to give admins the permission to handle registrations.

As an admin with sufficient permissions, I need to be able to give admins the permission to add locations.

As an admin with sufficient permissions, I need to be able to give admins the permission to create accounts for personnel.

As an admin with sufficient permissions, I need to be able to give admins the permission to view a list of who has permission to what.

As an admin with sufficient permissions, I need to be able to add locations.

As an admin with sufficient permissions, I need to be able to accept user registration as patients.

As an admin with sufficient permissions, I need to be able to deny user registration as patients.

As an admin with sufficient permissions, I need to be able to create accounts for personnel.

As an admin with sufficient permissions, I need to be able to view a list of who has permission to what.

As personnel with sufficient permissions, I need to be able to view a patients journal entries.

As personnel with sufficient permissions, I need to be able to mark journal entries with different levels of read permissions.

As personnel with sufficient permissions, I need to be able to register appointments.

As personnel with sufficient permissions, I need to be able to modify appointments.

As personnel with sufficient permissions, I need to be able to approve appointment requests.

As personnel with sufficient permissions, I need to be able to view the schedule of a location.

As a patient, I need to be able to view my own journal.

As a patient, I need to be able to request an appointment.

As a logged in user, I need to be able to view my schedule.

Tekniska rekommendationer
Använd filsystemet som persistent storage.
Enums är oftast korrekt lösning för logik istället för strings.
Gör systemet event-baserat istället för byggt på journaler + andra
datastrukturer.
Oavsätt vad er centrala datastruktur är, ha den i en platt lista så den är
lättillgänglig i all er kod
*/
using HealthCare5;
using System.Diagnostics;

List<User> users = new();
List<Location> locations = new();
Permission permission = new();
User? active_user = null;

// sample users
users.Add(new User("111", "1111", User.Role.SuperAdmin));
users.Add(new User("222", "2222", User.Role.Admin));
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
        if (active_user.UserRole == User.Role.SuperAdmin || active_user.UserRole == User.Role.Admin)
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




