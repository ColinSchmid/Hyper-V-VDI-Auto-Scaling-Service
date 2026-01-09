using System.Security.Principal;

namespace HyperV.VDIAutoScaling.Service.Runtime
{
    public static class RuntimeGuard
    {
        public static void EnsureRunningWithSufficientPrivileges()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);

            bool isSystem =
                identity.Name.Equals(
                    @"NT AUTHORITY\SYSTEM",
                    StringComparison.OrdinalIgnoreCase
                    );

            bool isLocalAdmin =
                principal.IsInRole(WindowsBuiltInRole.Administrator);

            if (!isSystem && !isLocalAdmin)
            {
                throw new InvalidOperationException(
                    "Service must run as LocalSystem or under a service account with local administrator privileges."
                );
            }
        }
    }
}
