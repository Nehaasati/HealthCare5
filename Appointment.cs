 namespace HealthCare5;
class Appointment
{
    // Date and time of the appointment
    public DateTime Start;
    //patient ssn
    public string PatientSSN;
    //note for patient
    public string Notes;

    //constructor for appointment

    public Appointment(DateTime start, string patientSSN, string notes = "")
    {
        Start = start;
        PatientSSN = patientSSN;
        Notes = notes;
    }

    //appointment detail
    public string GetInfo()
    {
        string noteText = Notes;
        if (noteText == null || noteText == "")
        {
            noteText = "-";
        }
        return Start.ToString("yyyy-MM-dd HH:mm") + " | Patient: " + PatientSSN + " | " + noteText;
    }
}