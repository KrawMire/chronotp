using System;
using Microsoft.Extensions.DependencyInjection;

namespace ChronOTP.DesktopApplication.Helpers;

public static class DependencyHelper
{
    private static IServiceProvider? _provider;

    public static void Initialize(IServiceCollection services)
    {
        _provider = services.BuildServiceProvider();
    }

    public static T GetRequiredService<T>() where T : notnull
    {
        if (_provider is null)
        {
            throw new NullReferenceException("ServiceProvider was not defined");
        }
        
        return _provider.GetRequiredService<T>();
    }
}