using System.Collections.Generic;
using KTGKNhom8.Roles.Dto;

namespace KTGKNhom8.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}
