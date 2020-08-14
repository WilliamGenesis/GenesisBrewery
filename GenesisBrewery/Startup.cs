using ApplicationLayer.Business;
using ApplicationLayer.Validations;
using DataAccessLayer.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using ApplicationLayer.Persistence;
using DataAccessLayer.Persistence;
using ApplicationLayer.Queries;
using DataAccessLayer.Queries;

namespace GenesisBrewery
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
            services.AddControllers();

            RegisterApplication(services);
            RegisterDataAccess(services);

            RegisterContext(services);
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

        private void RegisterContext(IServiceCollection services)
        {
            services.AddDbContext<GenesisBreweryContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        private void RegisterApplication(IServiceCollection services)
        {
            services.AddTransient<IBrandService, BrandService>();
            services.AddTransient<IWholesalerService, WholesalerService>();
            services.AddTransient<IBrandValidation, BrandValidation>();
            services.AddTransient<IWholesalerValidation, WholesalerValidation>();
        }

        private void RegisterDataAccess(IServiceCollection services)
        {
            services.AddTransient<IBrandPersistence, BrandPersistence>();
            services.AddTransient<IBrandQuery, BrandQuery>();
            services.AddTransient<IWholesalerPersistence, WholesalerPersistence>();
            services.AddTransient<IWholesalerQuery, WholesalerQuery>();
        }
    }
}
