namespace CurrencyRateAgregator.Api
{
    using Autofac;
    using CurrencyRateAgregator.Api.Models;
    using CurrencyRateAgregator.Api.Services;
    using CurrencyRateAgregator.Api.Services.BY;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Newtonsoft.Json.Converters;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMemoryCache();
            services
                .AddControllers()
                .AddNewtonsoftJson(
                    opts => opts.SerializerSettings.Converters.Add(new StringEnumConverter())
                );
            services.AddTransient<IRateExtrator, RateExtrator>();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<ProviderBY>().Keyed<IProvider>(Country.BY);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
