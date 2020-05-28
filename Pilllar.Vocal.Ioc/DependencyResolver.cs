using System;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Pilllar.Vocal.Ioc
{
    public class DependencyResolver
    {
        public static void Resolve(IServiceCollection services)
        {
            //Validators
            //services.AddSingleton<IValidator<ExampleDTO>, ExampleValidator>();

            //Repositories
            //services.AddScoped<IExampleRepository, ExampleRepository>();

            //Services
            //services.AddScoped<IExampleService, ExampleService>();

        }
    }
}
