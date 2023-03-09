using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using StripeCreator.DAL;

namespace StripeCreator.WPF.Data
{
    internal class AppDbContextFactory : IDesignTimeDbContextFactory<StripeCreatorDb>
    {
        private const string SqlServerMigrationAssembly = "StripeCreator.DAL.SqlServer";
        private const string ConnectionString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = StripeCreatorDb; Integrated Security = True; Multiple Active Result Sets = True;";

        public StripeCreatorDb CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StripeCreatorDb>();
            var options = optionsBuilder
                .UseSqlServer(ConnectionString, x => x.MigrationsAssembly(SqlServerMigrationAssembly))
                .Options;
            return new StripeCreatorDb(options);
        }
    }
}
