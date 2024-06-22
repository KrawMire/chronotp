using System;
using Avalonia;
using Avalonia.ReactiveUI;

namespace ChronOTP.Desktop;

sealed class Program
{
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);
    
    public static AppBuilder BuildAvaloniaApp()
    {
        var builder = AppBuilder
            .Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI();
        
        return builder;
    }
}