using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace EmpAspnetBoilerplate.EntityFrameworkCore
{
    public static class EmpAspnetBoilerplateDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<EmpAspnetBoilerplateDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<EmpAspnetBoilerplateDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
