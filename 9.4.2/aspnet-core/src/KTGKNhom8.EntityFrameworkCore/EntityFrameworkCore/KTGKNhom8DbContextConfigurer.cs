using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace KTGKNhom8.EntityFrameworkCore
{
    public static class KTGKNhom8DbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<KTGKNhom8DbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<KTGKNhom8DbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
