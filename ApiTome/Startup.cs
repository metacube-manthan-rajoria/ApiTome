using ApiTome.Filters;

namespace ApiTome;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<DataFilter>();
        });
    }
}
