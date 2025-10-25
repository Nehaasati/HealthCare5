namespace HealthCare5;
class Location
{
  public string Name;
  public string Description;
  // List to store appointments for this location
  public List<Appointment> Appointments;
  // Constructor
  public Location(string name, string description)
  {
    Name = name;
    Description = description;
    Appointments = new List<Appointment>(); // initialize the list
  }

  // Adds a new appointment to the location's list of appointments
   public void AddAppointment(Appointment appt)
  {
    Appointments.Add(appt);
  }

}
