using System.Collections.Generic;
using KTGKNhom8.Roles.Dto;

namespace KTGKNhom8.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
