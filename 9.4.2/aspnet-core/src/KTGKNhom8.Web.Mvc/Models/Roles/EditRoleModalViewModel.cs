using Abp.AutoMapper;
using KTGKNhom8.Roles.Dto;
using KTGKNhom8.Web.Models.Common;

namespace KTGKNhom8.Web.Models.Roles
{
    [AutoMapFrom(typeof(GetRoleForEditOutput))]
    public class EditRoleModalViewModel : GetRoleForEditOutput, IPermissionsEditViewModel
    {
        public bool HasPermission(FlatPermissionDto permission)
        {
            return GrantedPermissionNames.Contains(permission.Name);
        }
    }
}
