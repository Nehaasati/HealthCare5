using System;
using System.Collections.Generic;
using HealthCareSystem;

class TestPermissions
{
    static void Main()
    {
        Admin arbaz = new Admin("arbaz", "Arbaz Shah");
        Admin neha = new Admin("neha", "Neha Asati");

        arbaz.GrantPermission(AdminPermission.HandlePermissionSystem);
        arbaz.GrantPermission(AdminPermission.ViewPermissionList);

        Console.WriteLine($"Admin: {arbaz.GetFullName()}");
        foreach (var perm in arbaz.GetPermissions())
        {
            Console.WriteLine($"- {perm}");
        }

        Console.WriteLine($"Can Neha view permissions? {neha.HasPermission(AdminPermission.ViewPermissionList)}");
    }
}