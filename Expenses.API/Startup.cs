using Expenses.API;
using Expenses.API.Utilities.Credentials.Configuration;
using Expenses.API.Utilities.MediatR;
using Expenses.Data.Access.Database;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Expenses
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly JsonSerializerOptions jsonOptions;
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.jsonOptions = new JsonSerializerOptions();
            this.jsonOptions.PropertyNamingPolicy = null;
            this.jsonOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            this.jsonOptions.WriteIndented = true;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddFluentValidation();

            AssemblyScanner
                .FindValidatorsInAssembly(Sentinel.Assembly)
                .ForEach(item => services.AddScoped(item.InterfaceType, item.ValidatorType));

            services
                .AddMediatR(Sentinel.Assembly)
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(FluentValidationPipelineBehavior<,>));

            services
                .AddOptions<ExpenseConnectionSettings>()
                .Bind(this.configuration.GetSection("Database"));

            services
                .AddScoped<IExpenseDatabase, ExpenseConnection>();

            services
                .AddOptions<EmailCredentials>()
                .Bind(this.configuration.GetSection("EmailCredentials"));

            services
                .AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection()
               .UseRouting()
               .UseAuthorization()
               .UseExceptionHandler(builder => 
                    builder.Use((context, next) =>
                    {
                        var handler = context.Features.Get<IExceptionHandlerFeature>();
                        var error = handler.Error;

                        if (error != null)
                        {
                            // ProblemDetails has it's own content type
                            context.Response.ContentType = "application/json";

                            var problem = new ProblemDetails();

                            if(error is ValidationException)
                            {
                                problem.Status = (int) HttpStatusCode.BadRequest;
                                problem.Title = "Invalid Data Sent";
                                problem.Detail = error.Message;
                            }
                            else
                            {
                                problem.Status = (int)HttpStatusCode.InternalServerError;
                                problem.Title = env.IsDevelopment() ? "An error occured: " + error.Message : "An error occured";
                                problem.Detail = env.IsDevelopment() ? error.ToString() : null;
                            }
                            
                            // This is often very handy information for tracing the specific request
                            var traceId = Activity.Current?.Id ?? context?.TraceIdentifier;
                            if (traceId != null)
                                problem.Extensions["traceId"] = traceId;

                            //Serialize the problem details object to the Response as JSON (using System.Text.Json)
                            var stream = context.Response.Body;
                            JsonSerializer.SerializeAsync(stream, problem, this.jsonOptions);
                        }
                        return Task.CompletedTask;
                    })
                )
               .UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
