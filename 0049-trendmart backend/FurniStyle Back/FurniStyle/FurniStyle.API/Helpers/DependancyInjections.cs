using FurniStyle.API.ErrorHandling;
using FurniStyle.Core.Entities.Identity;
using FurniStyle.Core.IRepositories;
using FurniStyle.Core.IServices;
using FurniStyle.Core.IServices.Caching;
using FurniStyle.Core.IServices.Categories;
using FurniStyle.Core.IServices.Furnis;
using FurniStyle.Core.IServices.Payments;
using FurniStyle.Core.IServices.Rooms;
using FurniStyle.Core.IServices.Toekn;
using FurniStyle.Core.IUnitOfWork;
using FurniStyle.Core.Mapping.Autentication;
using FurniStyle.Core.Mapping.Basket;
using FurniStyle.Core.Mapping.Categories;
using FurniStyle.Core.Mapping.Furnis;
using FurniStyle.Core.Mapping.Orders;
using FurniStyle.Core.Mapping.Rooms;
using FurniStyle.Repository;
using FurniStyle.Repository.Data.Context;
using FurniStyle.Repository.Identity.Context;
using FurniStyle.Repository.Repositories;
using FurniStyle.Repository.UnitOfWork;
using FurniStyle.Service.Services.Basket;
using FurniStyle.Service.Services.Caching;
using FurniStyle.Service.Services.Categories;
using FurniStyle.Service.Services.Furnis;
using FurniStyle.Service.Services.Orders;
using FurniStyle.Service.Services.Payments;
using FurniStyle.Service.Services.Rooms;
using FurniStyle.Service.Services.Token;
using FurniStyle.Service.Services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace EduTrack.API.Helpers
{
    public static class DependancyInjections
    {

        #region Functions that Calls the DependanyInjections Services 

        public static IServiceCollection AddDependancyInjections(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBuiltInServices();
            services.AddSwaggerServies();
            services.AddCORSServices();
            services.AddDbContextService(configuration);
            services.AddUserDefinedServices();
            services.AddAutoMapperService(configuration);
            services.AddConfigurationInvalidModelStateResponseService();
            services.AddRedisService(configuration);
            services.AddIdentityServices();
            services.AddAuthenticationService(configuration);


            return services;
        }

        #endregion

        #region Functions that add services 

        public static IServiceCollection AddBuiltInServices(this IServiceCollection services)
        {
            services.AddControllers();
            return services;
        }

        public static IServiceCollection AddSwaggerServies(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGenJwtAuth();
            return services;
        }

        public static IServiceCollection AddDbContextService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FurniStyleDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddDbContext<FurniStyleIdentityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityDefaultConnection"));
            });

            return services;
        }

        public static IServiceCollection AddCORSServices(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
            });
            });
            return services;

        }

        public static IServiceCollection AddUserDefinedServices(this IServiceCollection services)
        {
            services.AddScoped<IFurniService, FurniService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddAutoMapperService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(m => m.AddProfile(new FurniProfile(configuration)));
            services.AddAutoMapper(m => m.AddProfile(new CategoryProfile()));
            services.AddAutoMapper(m => m.AddProfile(new RoomProfile()));
            services.AddAutoMapper(m => m.AddProfile(new BasketProfile()));
            services.AddAutoMapper(m => m.AddProfile(new AuthenticationProfile()));
            services.AddAutoMapper(m => m.AddProfile(new OrderProfile(configuration)));

            return services;
        }

        public static IServiceCollection AddConfigurationInvalidModelStateResponseService(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                                                         .SelectMany(p => p.Value.Errors)
                                                         .Select(p => p.ErrorMessage);
                    var response = new ApiValidationErrorRsponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(response);
                };
            });

            return services;
        }

        public static void AddSwaggerGenJwtAuth(this IServiceCollection services)
        {
            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Team4Store",
                    Description = @"Team4Store is an ASP.NET Core API e-commerce platform for furniture," + "\n" +
                                   "allowing users to browse by category and room type, add items to their cart," + "\n" +
                                   "place orders, and complete secure payments. It features user authentication," + "\n" +
                                   "a seamless shopping experience, and an admin panel for managing furniture listings," + "\n" +
                                   "orders, and customers. 🚀",



                    Contact = new OpenApiContact
                    {
                        Name = "Hussein Adel",
                        Email = "husseinadelhhh@gmail.com",
                        Url = new Uri("https://linkedin.com/in/hussein-adel-b46783280")
                    }
                });

                // JWT Authentication Configuration
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' followed by your JWT token"
                };

                o.AddSecurityDefinition("Bearer", securityScheme);

                o.AddSecurityRequirement(new OpenApiSecurityRequirement{
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    }
                                },
                                new List<string>()
                            }
                 });

                // Enable XML Comments if available (for better documentation)
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
                if (File.Exists(xmlPath))
                {
                    o.IncludeXmlComments(xmlPath);
                }
            });
        }

        public static IServiceCollection AddRedisService(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("Redis");

            if (!string.IsNullOrEmpty(connection))
            {
                services.AddSingleton<IConnectionMultiplexer>(serviceProvider =>
                {
                    try
                    {
                        return ConnectionMultiplexer.Connect(connection);
                    }
                    catch (Exception ex)
                    {
                        var logger = serviceProvider.GetRequiredService<ILogger<IConnectionMultiplexer>>();
                        logger.LogError(ex, "Failed to connect to Redis. The app will continue without Redis caching.");
                        return null;
                    }
                });
            }
            return services;
        }


        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<FurniStyleIdentityDbContext>();

            return services;
        }

        public static IServiceCollection AddAuthenticationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorization(options =>
             {
                 options.AddPolicy("Admin", policy =>
                     policy.RequireRole("Admin"));
             });
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:Audience"],
                    ValidateLifetime= true,
                    RoleClaimType = ClaimTypes.Role,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
                };
            });
            return services;

        }



        #endregion
    }
}
