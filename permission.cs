using System;
using System.Collections.Generic;

namespace HealthCareSystem
{
    public class Admin
    {
        private string username;
        private string fullName;
        private List<AdminPermission> permissions;

        public Admin(string u, string f)
        {
            username = u;
            fullName = f;
            permissions = new List<AdminPermission>();
        }

        public string GetUsername()
        {
            return username;
        }

        public string GetFullName()
        {
            return fullName;
        }

        public void GrantPermission(AdminPermission permission)
        {
            if (!permissions.Contains(permission))
            {
                permissions.Add(permission);
            }
        }

        public bool HasPermission(AdminPermission permission)
        {
            return permissions.Contains(permission);
        }

        public List<AdminPermission> GetPermissions()
        {
            return permissions;
        }
    }

    public enum AdminPermission
    {
        HandlePermissionSystem,
        AssignAdminsToRegions,
        HandleRegistrations,
        AddLocations,
        CreatePersonnelAccounts,
        ViewPermissionList,
        AcceptPatientRegistration,
        DenyPatientRegistration
    }
}