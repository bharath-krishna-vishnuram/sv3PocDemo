using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace Trial.MvcDemoApplication;

public class MvcDemoApplicationWebTestStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<MvcDemoApplicationWebTestModule>();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.InitializeApplication();
    }
}
