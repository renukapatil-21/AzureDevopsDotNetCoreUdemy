using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core.Services;
using eCommerce.Core.IServiceContracts;
using FluentValidation;
using eCommerce.Core.Validators;


namespace eCommerce.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddTransient<IUsersService, UserService>();
            services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();
            return services;
        }

    }
}
