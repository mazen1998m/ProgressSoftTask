using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Muslim.Domain.LoggerService;

namespace Muslim.Data.EFCore.SqlServer;
public class SqlServerContextFactory : IDesignTimeDbContextFactory<SqlServerDbContext>
{

    public SqlServerDbContext CreateDbContext(string[] args)
    {
        // Build configuration
        try
        {
            var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false)
               .Build();

            // Get the connection string
            var connectionString = configuration.GetConnectionString("SqlServerConnection");

            // Configure DbContextOptions
            var optionsBuilder = new DbContextOptionsBuilder<SqlServerDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new SqlServerDbContext(optionsBuilder.Options);
        }
        catch (Exception ex)
        {
            ELog.Log(ex);
            throw;
        }
    }
}
