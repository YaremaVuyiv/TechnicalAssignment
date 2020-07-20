using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TechnicalAssignment.Data;
using TechnicalAssignment.Data.Repositories;
using TechnicalAssignment.Data.Repositories.Contracts;
using TechnicalAssignment.Parsers;
using TechnicalAssignment.Parsers.Contracts;
using TechnicalAssignment.Services;
using TechnicalAssignment.Services.Contracts;
using TechnicalAssignment.Validators;
using TechnicalAssignment.Validators.Contracts;

namespace TechnicalAssignment
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("TransactionDbConnection");

            services.AddDbContext<TransactionDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation();

            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ITransactionParserFactory, TransactionParserFactory>();
            services.AddScoped<ITransactionValidatorFactory, TransactionValidatorFactory>();
            services.AddScoped<ITransactionService, TransactionService>();

            services.AddMediatR(Assembly.GetExecutingAssembly());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
