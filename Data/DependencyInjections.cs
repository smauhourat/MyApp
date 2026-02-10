using Data.Persistence;
using Data.Repositories;
using Domain;
using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddData(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IRepository<PersonEntity, Guid>, PersonRepository>();
            services.AddScoped<IRepository<VisitEntity, Guid>, VisitRepository>();
            services.AddScoped<ICodeRepository<PersonEntity>, PersonRepository>();
            services.AddScoped<IVisitRepository<VisitEntity>, VisitRepository>();

            return services;
        }
    }
}
