using System.Collections.Generic;
using KTGKNhom8.Roles.Dto;

namespace KTGKNhom8.Web.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }
    }
}