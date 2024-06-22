using System;
using System.IO;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ChronOTP.Core.Abstractions.Repositories;
using ChronOTP.Core.Abstractions.Utils;
using ChronOTP.DesktopApplication.Helpers;
using ChronOTP.DesktopApplication.ViewModels;
using ChronOTP.Infrastructure.Repositories.InMemory;
using ChronOTP.Infrastructure.Repositories.Sqlite;
using ChronOTP.Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MainWindow = ChronOTP.DesktopApplication.Views.MainWindow;
using NewProfileWindow = ChronOTP.DesktopApplication.Views.NewProfileWindow;

namespace ChronOTP.Desktop;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var services = new ServiceCollection();

        var currentDir = AppContext.BaseDirectory;
        
        services
            .AddDbContext<SqliteDbContext>(options =>
            {
                options.UseSqlite($"Data Source={Path.Join(currentDir, "chronotp.db")}");
            })
            .AddTransient<ITotpProfileRepository, SqliteTotpProfileRepository>()
            // .AddSingleton<ITotpProfileRepository, InMemoryTotpProfileRepository>()
            .AddTransient<ITotpGenerator, TotpGenerator>()
            .AddTransient<NewProfileWindowViewModel>()
            .AddTransient<NewProfileWindow>()
            .AddTransient<MainWindowViewModel>()
            .AddSingleton<MainWindow>();

        DependencyHelper.Initialize(services);

        var dbContext = DependencyHelper.GetRequiredService<SqliteDbContext>();
        
        dbContext.Database.OpenConnection();
        dbContext.Database.EnsureCreated();
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = DependencyHelper.GetRequiredService<MainWindow>();
        }

        base.OnFrameworkInitializationCompleted();
    }
}