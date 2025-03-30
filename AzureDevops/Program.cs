using System.Text.Json.Serialization;
using AzureDevops.Middlewares;
using eCommerce.Infrastructure;
using eCommerce.Core;
using eCommerce.Core.Mappers;
using FluentValidation.AspNetCore;

namespace AzureDevops
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            builder.Services.AddInfrastructure();
            builder.Services.AddCore();
            builder.Services.AddAutoMapper(typeof(ApplicationUserMappingProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(RegisterRequestMappingProfile).Assembly);

            builder.Services.AddFluentValidationAutoValidation();

            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            builder.Services.AddControllers().AddJsonOptions(
                options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            var app = builder.Build();

            app.UseExceptionHandlingMiddleware();

            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
