namespace DefaultSite.Web.Api.StartUp
{
    public class Cors
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options => {
                options.AddDefaultPolicy(
                    policy => {
                        policy.WithOrigins("http://localhost:3000", "http://localhost:3001")
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });
        }
    }
}
