using CIB.PhoneBook.Application;
using CIB.PhoneBook.Domain;
using CIB.PhoneBook.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CIB.PhoneBook.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsApi",
                    builder => builder.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });

            services.AddDbContext<PhoneBookDbContext>(options =>
            {
                MigrateDb();
                options.UseSqlServer(Configuration.GetConnectionString("PhoneBookApiApiDefaultConnection"));
            });

            services.AddScoped<IPhoneBookRepository, PhoneBookRepository>();
            services.AddScoped<IPhoneBookService, PhoneBookService>();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            
            app.UseCors("CorsApi");

            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        //Normally wouldn't do a migration in startup because the user needed to do this needs elevated permissions.
        //My 1st choice is to do migrations as part of CI/CD.
        //Alternative is to have two separate connections strings - one for migration and one for normal operations (if somehow a breach is achieved on endpoints then at least the normal user is restricted)
        //The localDb connection string is fine to check into source control but would never include any other connection string in the settings and would retrieve it from e.g. KeyVault in real environment
        private void MigrateDb()
        {
            var dbContextFactory = new DbContextFactory();
            var dbContext = dbContextFactory.CreateDbContext(new string[0]);
            
            dbContext.Database.Migrate();
        }
    }
}
