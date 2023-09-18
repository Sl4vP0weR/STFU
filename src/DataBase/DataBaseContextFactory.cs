namespace STFU.DataBase;

public class DataBaseContextFactory : IDesignTimeDbContextFactory<DataBaseContext>
{
    public DataBaseContext CreateDbContext(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Configuration.AddUserSecrets(typeof(DataBaseContextFactory).Assembly);

        var services = builder.Services;
        var configuration = builder.Configuration;

        services.AddDataBase(configuration);

        var host = builder.Build();

        var scope = host.Services.CreateScope();

        return scope.ServiceProvider.GetRequiredService<DataBaseContext>();
    }
}