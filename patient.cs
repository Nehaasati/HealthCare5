using System;

namespace HealthCare5;

public class Patient : User
{
    

  public string Status;
 
  public Patient(string ssn, string password)
      : base(ssn, password, Role.Patient)
  {
    Status = Permission.PatientStatus.Pending.ToString();
  }
 
  public void RequestRegistration()
  {
    System.Console.WriteLine("Registration request sent.");
  }
 
  public void ShowStatus()
  {
    System.Console.WriteLine("Status: " + Status);
  }
}