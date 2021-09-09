using EBook_Library.Common;
using EBook_Library.Core.Implementation;
using EBook_Library.Core.Interface;
using EBook_Library.Data;
using EBook_Library.Midddlewares;
using EBook_Library.Models;
using EBook_Library.Profiles;
using EBookLibrary.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace EBook_Library
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

            services.AddControllers().AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EBook_Library", Version = "v1" });
            });
            services.AddDbContextPool<EBookContext>(option => option
            .UseSqlite(Configuration.GetConnectionString("DatabaseConnection")));

            services.Configure<IdentityOptions>(options =>
            {
                //Require each user to have a uniqe email
                options.User.RequireUniqueEmail = true;

                //Password configuratiosn
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
            });

            services.AddIdentity<AppUser, IdentityRole>()
              .AddEntityFrameworkStores<EBookContext>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<LogService>();
            services.AddAutoMapper(typeof(MappingProfile));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EBook_Library v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            //app.UseMiddleware<RequestLoggingMiddleware>();

            app.UseAuthorization();

            Preseeder.EnsurePopulated(app);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
