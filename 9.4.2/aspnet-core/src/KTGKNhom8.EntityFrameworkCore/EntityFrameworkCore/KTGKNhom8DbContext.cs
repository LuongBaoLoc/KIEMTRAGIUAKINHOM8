using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using KTGKNhom8.Authorization.Roles;
using KTGKNhom8.Authorization.Users;
using KTGKNhom8.MultiTenancy;

namespace KTGKNhom8.EntityFrameworkCore
{
    public class KTGKNhom8DbContext : AbpZeroDbContext<Tenant, Role, User, KTGKNhom8DbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public KTGKNhom8DbContext(DbContextOptions<KTGKNhom8DbContext> options)
            : base(options)
        {
        }
    }
}
