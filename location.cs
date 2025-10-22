namespace HealthCare5;

class Location
// create Location with name and
{
  public string Name;
  public string Description;
  // add list for appointment
     public List<Appointment> Appointment = new List<Appointment>();
  // use constructor

  
  public Location(string name, string description)
  {
    Name = name;
    Description = description;
  }
  // Adds a new appointment to the location's list of appointments
   public void AddAppointment(Appointment appt)
        {
            Appointment.Add(appt);
        }

}
