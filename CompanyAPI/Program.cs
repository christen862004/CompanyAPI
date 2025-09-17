
namespace CompanyAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().ConfigureApiBehaviorOptions(options => {
                options.SuppressModelStateInvalidFilter = true;
            });



            //name policy 
            builder.Services.AddCors(options => {
                options.AddPolicy("MyPolicy", policy => {
                    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                
                });
            });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();//get requ handel wwwroot

            app.UseCors("MyPolicy");//midleware
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
