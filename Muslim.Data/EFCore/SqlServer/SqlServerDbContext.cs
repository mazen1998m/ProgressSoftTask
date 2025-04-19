using Muslim.ConfigureTable;

namespace Muslim.Data.EFCore.SqlServer;

public class SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : DbContext(options)
{

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        try
        {
            DatabaseInitializer.CreateMigration(modelBuilder);

        }
        catch (Exception ex)
        {
            ELog.Log(ex);
        }



    }
}
































