using System;
using System.Collections.Generic;

namespace HealthCare5;

public class Permission  // <-- Make the class public
{
    public enum PatientStatus
    {
        Pending,
        Approved,
        Denied
    }

    public void ApprovePatient(Patient patient)
    {
        if (patient.Status == PatientStatus.Pending.ToString())
        {
            patient.Status = PatientStatus.Approved.ToString();
            Console.WriteLine("Approved: " + patient.SSN);
        }
        else
        {
            Console.WriteLine("Not pending.");
        }
    }

    public void DenyPatient(Patient patient)
    {
        if (patient.Status == PatientStatus.Pending.ToString())
        {
            patient.Status = PatientStatus.Denied.ToString();
            Console.WriteLine("Denied: " + patient.SSN);
        }
        else
        {
            Console.WriteLine("Not pending.");
        }
    }

    public void ShowPendingPatients(List<User> users)
    {
        Console.WriteLine("Pending patients:");
        bool any = false;

        foreach (User u in users)
        {
            Patient? p = u as Patient;
            if (p != null && p.Status == PatientStatus.Pending.ToString())
            {
                Console.WriteLine("- " + p.SSN);
                any = true;
            }
        }

        if (!any)
        {
            Console.WriteLine("None.");
        }
    }
}
