HealthCare5
Group Project:
Neha asati          31asatineha@gmail.com
Pedram Basimfar     Pedram.basim@gmail.com
Arbaz Shah          arbazhussain067@gmail.com
Kifle Haile Selassie kifle.uni@gmail.com

**Project Description – Health Care System**
**By Neha, Arbaz, Pedram, and Kiffel**

The Health Care System project is a comprehensive management platform designed to simplify essential healthcare functions like patient registration, appointment scheduling, role-based access control for various user types, including administrators, healthcare personnel, and patients. 

**DataStructures**
The central data structure of the system is built around a flat list of users, ensuring easy access and management of user information. Each user record includes:
•	Username
•	Password
•	Social Security Number (SSN)
•	Role (Admin, Personnel, or Patient)
•	Permissions (read/write access to certain operations)

Other supporting data structures include:
•	Appointments list (linked to patients and personnel)
•	Journal entries (containing medical information with permission levels)
•	Location list (for clinics or hospitals managed by admins)

**Step 1: Login and Logout System**
**Purpose:**
The first functionality we implemented was the login/logout system. This step was crucial to establish secure access for users and differentiate between roles (Admin, Personnel, Patient). It ensures that only registered users can access the system features.
**Implementation details:**
•	Created a simple console interface for username/SSN and password input.
•	Used a List<User> to store users in memory.
•	Used a Lis<Location> to track the location.
•	Compared input with stored user credentials to authenticate users.
•	Added a logout option and quit command to exit the system.
•	Create an active_user variable to keep track of who is logged in at any given time.
**Why important:**
•	Provides secure authentication for all system users.

**Step 2: Patient Registration
Purpose:**
Allows new users to register as patients. New registrations are initially Pending and require admin approval before the patient can access the system.
**Implementation details:**
•	Input: New SSN and password.
•	Output: Creates a Patient object with Status = Pending.
•	Checks for duplicate SSN to prevent conflicts.
**Why important:**
•	Implements workflow for patient onboarding.
•	Ensures patients cannot access system functions until approved.
•
**Step 3: Admin Actions
Purpose:**
Admins manage locations, approve/deny patients, and (in future versions) manage permissions for other admins and personnel accounts.
Implemented features:
1.	Add Location — Admin can create new locations.
2.	View Locations — Admin can see all added locations.
3.	View Pending Patients — Uses Permission.ShowPendingPatients(users) to list pending registrations.
4.	Approve or Deny Patient — Changes patient status from Pending → Approved/Denied.
**Why important:**
•	Allows admins to control patient access.
•	Adds location management functionality.
•	Prepares system for future features like personnel management and region assignments.

**Step 4: Patient Actions
Purpose:**
Patients can view their own information and journal. Currently, the journal is a placeholder.
Implemented features:
•	View Status — Shows the patient’s current registration status.
**Why important:**
•	Gives patients feedback on their registration status.
•	Prepares for future appointment requests and journal viewing.


**Step 5: Personnel Actions
Purpose:**
Personnel users can view location information. In the future, they will handle appointments, journal entries, and schedules.
Implemented features:
•	View Locations — Displays all existing locations.
**Why important:**
•	Introduces role-based access control.
•	Lays groundwork for scheduling, appointments, and journal access.


**Step 6: Permission 
Purpose:**
•	Implements a simple system to manage patient approval and admin permissions.
•	Approve/deny patients, foundation for admin permission system
**Implemented features:**
•	Enum PatientStatus { Pending, Approved, Denied } — used to control patient workflow.
•	Admins can approve or deny patients.
**Why important:**
•	Establishes role-based logic.


**Technology Used**
This project was created as a group effort to model a healthcare management system in the actual world.it emphasizes modularity, security, and maintainability, following best practices in C# development.

