using AutoMapper;
using Csla.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SurgeonPortal.Api.Configuration;
using SurgeonPortal.Api.Extensions;
using SurgeonPortal.Api.Identity;
using SurgeonPortal.DataAccess;
using SurgeonPortal.DataAccess.Contracts;
using SurgeonPortal.DataAccess.Contracts.Identity;
using SurgeonPortal.Library;
using SurgeonPortal.Library.Contracts;
using SurgeonPortal.Models;
using System.Text;
using System.Text.Json;
using Ytg.AspNetCore.Configuration;
using Ytg.AspNetCore.Filters;
using Ytg.AspNetCore.Helpers;
using Ytg.AspNetCore.Middleware;
using Ytg.AspNetCore.Models;
using Ytg.AspNetCore.Swagger;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;
using Ytg.Framework.IoC;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.Api
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
            services.AddHttpContextAccessor();

            services.Configure<TokensConfiguration>(Configuration.GetSection(ConfigurationSections.Tokens));

            services.AddCsla();

            new CslaConfiguration()
                .DataPortal().AutoCloneOnUpdate(false);

            services.AddCors(options =>
                options.AddPolicy("cors", policy => policy
                    .WithOrigins("http://localhost:4200",
                                 "https://localhost:5001")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .SetPreflightMaxAge(TimeSpan.FromDays(30))));

            services.AddOptions();

            services
                 .AddControllers(options =>
                 {
                     options.Filters.Add(typeof(AppInsightsExceptionFilter));
                 })
                .AddControllersAsServices()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.Converters.Add(new UtcDateTimeConverter());
                    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                    options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.WriteIndented = true;
                });

            services.AddApiVersioning(
                options =>
                {
                    options.ReportApiVersions = true;
                    options.UseApiBehavior = false;
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.AssumeDefaultVersionWhenUnspecified = true;
                });

            ConfigureSwagger(services);

            var sqlConfig = new SqlConfiguration(Configuration.GetConnectionString("DefaultConnection"));
            services.AddTransient<ISqlConnectionManager>(provider => new SqlServerConnectionManager(sqlConfig));
            services.AddSingleton<AutoMapper.IConfigurationProvider>(GetAutoMapperConfiguration());
            services.AddTransient<IPagination, Pagination>();
            services.AddTransient<IMapper, Mapper>();
            services.AddTransient<IIdentityProvider, HttpContextIdentityProvider>();
            services.AddTransient<IAbsIdentityProvider, HttpContextIdentityProvider>();
            services.AddTransient<IAbsoluteUriProvider, AbsoluteUriProvider>();

            services.RegisterByConvention<LibraryConventionProvider, LibraryConventionResolver>();
            services.RegisterByConvention<DataAccessConventionProvider, DataAccessConventionResolver>();

            var tokenConfig = Configuration.GetSection(ConfigurationSections.Tokens).Get<TokensConfiguration>();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(config =>
                {
                    config.RequireHttpsMetadata = false;
                    config.SaveToken = true;

                    
                    config.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidIssuer = tokenConfig.Issuer,
                            ValidAudience = tokenConfig.Issuer,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfig.Key)),
                        };
                });

            services.AddAuthorization(options =>
            {
                var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
                defaultAuthorizationPolicyBuilder = defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
                options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseObjectActivator(app.ApplicationServices);

            app.UseCors("cors");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                var siteConfig = Configuration.GetSection(ConfigurationSections.Site).Get<SiteConfiguration>();
                var routePrefix = "";
                if (!string.IsNullOrWhiteSpace(siteConfig.VirtualDirectoryPath))
                {
                    routePrefix = siteConfig.VirtualDirectoryPath;

                    if (!routePrefix.StartsWith("/"))
                    {
                        routePrefix = "/" + routePrefix;
                    }
                    if (routePrefix.EndsWith("/"))
                    {
                        routePrefix = routePrefix.Substring(0, routePrefix.Length - 1);
                    }
                }

                c.SwaggerEndpoint($"{routePrefix}/swagger/v1/swagger.json", "SurgeonPortal API v1.0");

                
                c.SwaggerEndpointWithVirtualDirectory(siteConfig?.VirtualDirectoryPath, "SurgeonPortal API v1.0");
                 c.RoutePrefix = routePrefix;
            });

            app.UseCslaAuthenticationWrapper();

            app.UseYtgExceptionHandling();

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapDefaultControllerRoute().RequireAuthorization();
            });

            Configuration.ConfigureCsla();

            app.UseCsla();
        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SurgeonPortal API", Version = "v1" });
                c.CustomSchemaIds(i => i.FullName);
 
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Please enter JWT with Bearer into field",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
                });
                c.EnableAnnotations();
            });
        }

        private MapperConfiguration GetAutoMapperConfiguration()
        {
            return new MapperConfiguration(config =>
            {
                config.AddProfile<ConventionAutoMapperProfile<LibraryConventionProvider, ModelConventionResolver>>();
            });
        }

        private class ApiExplorerGroupPerVersionConvention : IControllerModelConvention
        {
            public void Apply(ControllerModel controller)
            {
                var controllerNamespace = controller.ControllerType.Namespace; // e.g. "Controllers.v1"
                var apiVersion = controllerNamespace?.Split('.').Last().ToLower();

                controller.ApiExplorer.GroupName = apiVersion;
            }
        }

        public class ModelConventionMapperBase
        {
            public void Map<TProvider, TResolver>(Profile profile)
                where TProvider : IConventionProvider, new()
                where TResolver : IConventionResolver, new()
            {
                var provider = new TProvider();
                var resolver = new TResolver();

                foreach (var serviceType in provider.GetServiceTypes())
                {
                    var implementationType = resolver.GetImplementationType(serviceType);

                    if (implementationType == null)
                        continue;

                    profile.CreateMap(serviceType, implementationType);
                }
            }
        }
    }
}
