using EduTrack.API.Helpers;
using FurniStyle.API.ErrorHandling;
using FurniStyle.API.ErrorHandling.Middleware;
using FurniStyle.Core.IServices.Categories;
using FurniStyle.Core.IServices.Furnis;
using FurniStyle.Core.IServices.Rooms;
using FurniStyle.Core.IUnitOfWork;
using FurniStyle.Core.Mapping.Categories;
using FurniStyle.Core.Mapping.Furnis;
using FurniStyle.Core.Mapping.Rooms;
using FurniStyle.Repository.Data.Context;
using FurniStyle.Repository.Data.DataSeeding;
using FurniStyle.Repository.UnitOfWork;
using FurniStyle.Service.Services.Categories;
using FurniStyle.Service.Services.Furnis;
using FurniStyle.Service.Services.Rooms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurniStyle.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            #region Adding The DependancyInjections Services and Other Services

            var builder = WebApplication.CreateBuilder(args);

            #region Register LoggerService

            builder.Services.AddLogging();
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();



            #endregion
            builder.Services.AddDependancyInjections(builder.Configuration);

            #endregion

            #region Adding Middlewares

            var app = builder.Build();
            await app.ConfigureMiddlewaresAsync();


            #endregion

            app.Run();
        }
    }

}
