using Library.Infrastructure.Application;
using Library.Persistence.EF;
using Library.Persistence.EF.BookCategories;
using Library.Persistence.EF.Books;
using Library.Services.BookCategories;
using Library.Services.BookCategories.Contracts;
using Library.Services.Books;
using Library.Services.Books.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.RestApi
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

            services.AddDbContext<EFDataContext>(options =>
            {
                options.UseSqlServer("Server=.;Database=Library;Trusted_Connection=True;");
            });

            services.AddScoped<UnitOfWork, EFUnitOfWork>();

            services.AddScoped<BookCategroyServices, BookCategoryAppServices>();
            services.AddScoped<BookCategroyRepository, EFBookCategroyRepository>();

            services.AddScoped<BookServices, BookAppServices>();
            services.AddScoped<BookRepository, EFBookRepository>();

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
