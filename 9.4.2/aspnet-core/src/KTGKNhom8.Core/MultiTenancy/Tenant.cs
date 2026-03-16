using Abp.MultiTenancy;
using KTGKNhom8.Authorization.Users;

namespace KTGKNhom8.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
