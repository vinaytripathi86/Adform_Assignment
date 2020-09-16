using AutoMapper;
using CorrelationId;
using CorrelationId.DependencyInjection;
using CorrelationId.HttpClient;
using Homework_Adform.CommonLibrary.Contracts.DAL;
using Homework_Adform.CommonLibrary.Contracts.Services;
using Homework_Adform.CommonLibrary.Helpers;
using Homework_Adform.CommonLibrary.Models;
using Homework_Adform.DAL;
using Homework_Adform.DAL.DBContexts;
using Homework_Adform.Services;
using Homework_Adform.TodoAPI.Graphql;
using Homework_Adform.TodoAPI.Graphql.ModelTypes;
using Homework_Adform.TodoAPI.Handlers;
using Homework_Adform.TodoAPI.Middleware;
using HotChocolate;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;
using System.Text;

namespace Homework_Adform
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;            
        }

        public IConfiguration Configuration { get; }
        private const string AllowAllCors = "AllowAll";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<HomeworkDBContext>(opts => opts.UseSqlServer(ConnectionStringConnectionHelper.GetConnectionString(Configuration)));
            services.AddAutoMapper(c => c.AddProfile<AutoMapping>(), typeof(Startup));
            services.AddHttpContextAccessor();
            services.AddGraphQL(s => SchemaBuilder.New()
                .AddServices(s)
                .AddType<LabelType>()
                .AddType<ItemsType>()
                .AddType<ListsType>()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddAuthorizeDirectiveType()
                .Create());

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JWT Token Secret"))
                };

                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
            });

            services.AddTransient<NoOpDelegatingHandler>();

            services.AddHttpClient("Homework_Client")
                .AddCorrelationIdForwarding() // add the handler to attach the correlation ID to outgoing requests for this named client
                .AddHttpMessageHandler<NoOpDelegatingHandler>();

            services.AddDefaultCorrelationId(options =>
            {
                options.CorrelationIdGenerator = () => Guid.NewGuid().ToString();
                options.AddToLoggingScope = true;
                options.EnforceHeader = false;
                options.IgnoreRequestHeader = false;
                options.IncludeInResponse = true;
                options.RequestHeader = "Custom-Correlation-Id";
                options.ResponseHeader = "X-Correlation-Id";
                options.UpdateTraceIdentifier = false;
            });


            services.AddControllers(config =>
            {
                config.RespectBrowserAcceptHeader = true;
            }).AddXmlDataContractSerializerFormatters();

            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
            });

            services.AddCors(options =>
            {
                options.AddPolicy(AllowAllCors,
                                  builder =>
                                  {
                                      builder.AllowAnyHeader();
                                      builder.AllowAnyMethod();
                                      builder.AllowAnyOrigin();
                                  });
            });

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITodoListService, TodoListService>();
            services.AddScoped<ITodoItemsService, TodoItemsService>();
            services.AddScoped<ILabelService, LabelService>();
            services.AddScoped<ITodoListDalLayer, TodoListDalLayer>();
            services.AddScoped<ITodoItemsDalLayer, TodoItemsDalLayer>();
            services.AddScoped<ILabelDalLayer, LabelDalLayer>();
            services.AddScoped<IUserDalLayer, UserDalLayer>();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddSwaggerGen(p =>
            {
                p.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Adform Assignment API", Version = "v1" });
                p.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                p.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                p.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<HomeworkDBContext>();
                context.Database.EnsureCreated();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseCorrelationId(); // adds the correlation ID middleware
            app.UseRequestResponseLogging();
            app.ConfigureExceptionMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Adform Assignment API");
                c.RoutePrefix = string.Empty;
            });

            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseGraphQL().UsePlayground();
        }
    }
}
