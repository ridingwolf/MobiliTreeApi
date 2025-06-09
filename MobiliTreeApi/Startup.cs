using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MobiliTree.Domain.Repositories;
using MobiliTree.Domain.Services;
using MobiliTree.FakeData;
using MobiliTree.FakeData.Repositories;

namespace MobiliTreeApi
{
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
            services.AddMvc();
            services.AddTransient<IInvoiceService, InvoiceService>();
            
            LoadFakeDataServices(services);
        }

        private static void LoadFakeDataServices(IServiceCollection services)
        {
            services.AddTransient<ISessionsRepository, SessionsRepositoryFake>();
            services.AddTransient<ICustomerRepository, CustomerRepositoryFake>();
            services.AddTransient<IParkingFacilityRepository, ParkingFacilityRepositoryFake>();
            services.AddSingleton(SeedCustomer.GetAll());
            services.AddSingleton(SeedServiceProfile.GetAll());
            services.AddSingleton(SeedSessions.GetAll());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
