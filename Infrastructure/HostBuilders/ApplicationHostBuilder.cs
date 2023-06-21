using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using TestWpf.Domain;
using TestWpf.Domain.Repsitory.Abstractions;
using TestWpf.Domain.Repsitory.EntityFramework;
using TestWpf.Infrastructure.Base;
using TestWpf.Infrastructure.Services;
using TestWpf.ViewModels;

namespace TestWpf.Infrastructure.HostBuilders;

public class ApplicationHostBuilder
{
    public static IHostBuilder CreateDefaultBuilder(string[] args)
        => Host.CreateDefaultBuilder(args)
        .ConfigureLogging((context, logging) =>
        {
            if (context.HostingEnvironment.IsDevelopment())
            {
                logging.ClearProviders()
                .SetMinimumLevel(LogLevel.Debug)
                .AddConsole();
            }
        })
        .ConfigureAppConfiguration(config =>
        {
            config.AddUserSecrets(Assembly.GetExecutingAssembly(), true);
        })
        .ConfigureServices((context, services) =>
        {
            var builder = new SqlConnectionStringBuilder(context.Configuration.GetConnectionString("DefaultConnection"));

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseLazyLoadingProxies()
                .UseSqlServer(builder.ConnectionString, output => output.MigrationsAssembly("TestWpf"));
            });

            services.AddTransient<IAsssetsRepository, EFAssetsRepository>();
            services.AddTransient<IInventoriesRepository, EFInventoriesRepository>();
            services.AddTransient<DataManager>();

            services.AddSingleton(provider => new Layout
            {
                DataContext = provider.GetRequiredService<AuthorizationViewModel>()
            });

            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<HomeViewModel>();
            services.AddSingleton<AuthorizationViewModel>();

            services.AddSingleton<Func<Type, ViewModelBase>>(
                provider => viewModelType => (ViewModelBase) provider.GetRequiredService(viewModelType)
            );

            services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        });
}