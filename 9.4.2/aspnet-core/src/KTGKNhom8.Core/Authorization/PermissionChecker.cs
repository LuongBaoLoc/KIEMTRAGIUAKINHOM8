using Abp.Authorization;
using KTGKNhom8.Authorization.Roles;
using KTGKNhom8.Authorization.Users;

namespace KTGKNhom8.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
