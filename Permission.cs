namespace HealthCare5;

class Permission
{

  // enum representing possible status of a patient
  public enum PatientStatus
  {
    Pending, //the patient is waiting approval
    Approved, // patient is approved
    Denied   // patient is deny
  }


  public enum PermissionType
  {
    CanViewScheduleLocation,
    CanApproveAppointmentRequests,
    CanModifyAppointments,
    CanRegisterAppointments,
    CanMarkJournalEntriesWithDifferentReadPermissions,
    CanViewPatientsJournaEntries,


  }



  // Approve Patient if status is pending
  
  public void ApprovePatient(Patient patient)
  {
    if (patient.Status.ToString().Contains("Pending"))
    {
      patient.Status = PatientStatus.Approved;
      Console.WriteLine("Patient approved: " + patient.SSN);
    }
    else
    // show message patient is pending
    {
      Console.WriteLine("Patient not pending.");
    }
  }
 
 // Deny Patient if status is pending
  public void DenyPatient(Patient patient)
  {
    if (patient.Status.ToString().Contains("Pending"))
    {
      patient.Status = PatientStatus.Denied;
      Console.WriteLine("Patient denied: " + patient.SSN);
    }
    else
   // show message patient is not pending 
    {
      Console.WriteLine("Patient not pending.");
    }
  }

//Displays a list of all patients who are currently pending  for approval.
  public void ShowPendingPatients(List<User> users)
  {
    Console.WriteLine("Pending patients:");
    bool any = false;

    foreach (User u in users)
    {
      if (u is Patient patient)
      {
        if (patient.Status.ToString().Contains("Pending"))
        {
          Console.WriteLine("- " + patient.SSN);
          any = true;
        }
      }
    }

    if (!any)
    {
      Console.WriteLine("No pending patients.");
    }
  }
}


