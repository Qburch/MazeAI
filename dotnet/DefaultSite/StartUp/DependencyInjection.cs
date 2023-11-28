using DefaultSite.Services.Interfaces;
using DefaultSite.Services;

namespace DefaultSite.Web.Api.StartUp
{
    public class DependencyInjection
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            if (configuration is IConfigurationRoot)
            {
                services.AddSingleton<IConfigurationRoot>(configuration as IConfigurationRoot); 
            }

            services.AddSingleton<IConfiguration>(configuration);
            
            services.AddSingleton<IBoardService, BoardService>();
            services.AddSingleton<IMazeAIService, MazeAIService>();

        }
    }
}
