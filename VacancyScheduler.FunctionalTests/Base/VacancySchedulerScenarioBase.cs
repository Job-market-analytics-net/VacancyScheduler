using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace VacancyScheduler.FunctionalTests.Base;

public class VacancySchedulerScenarioBase
{
    private const string ApiUrlBase = "api/v1/vacancy";

    public TestServer CreateServer()
    {
        var factory = new VacancySchedulerApplication();
        return factory.Server;
    }

    public static class Get
    {
        public static string GetVacancyScheduler(int id)
        {
            return $"{ApiUrlBase}/{id}";
        }

        public static string GetVacancySchedulerByCustomer(string customerId)
        {
            return $"{ApiUrlBase}/{customerId}";
        }
    }

    public static class Post
    {
        public static string VacancyScheduler = $"{ApiUrlBase}/";
        public static string CheckoutOrder = $"{ApiUrlBase}/checkout";
    }

    private class VacancySchedulerApplication : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IStartupFilter, AuthStartupFilter>();
            });

            builder.ConfigureAppConfiguration(c =>
            {
                var directory = Path.GetDirectoryName(typeof(VacancySchedulerScenarioBase).Assembly.Location)!;

                c.AddJsonFile(Path.Combine(directory, "appsettings.VacancyScheduler.json"), optional: false);
            });

            return base.CreateHost(builder);
        }

        private class AuthStartupFilter : IStartupFilter
        {
            public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
            {
                return app =>
                {
                    app.UseMiddleware<AutoAuthorizeMiddleware>();

                    next(app);
                };
            }
        }
    }
}
