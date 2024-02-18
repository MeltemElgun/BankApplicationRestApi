
using BankApplicationRestApi.Business;
using BankApplicationRestApi.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace BankApplicationRestApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //db resolve and initialize
            builder.Services.AddDbContext<BankApplicationContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("BankApplicationConnection")));

            #region Scrutor resolvers  
            var typeBaseService = typeof(BaseService);

            var assembly = typeBaseService.Assembly; 

            builder.Services.Scan(selector =>
                    selector
                        .FromAssemblies(assembly)
                        .AddClasses(classSelector => classSelector.AssignableTo(typeof(BaseService)))
                        .AsImplementedInterfaces()
                        .WithScopedLifetime()
                    );

            #endregion


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
