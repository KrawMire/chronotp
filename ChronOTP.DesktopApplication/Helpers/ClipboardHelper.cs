using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input.Platform;

namespace ChronOTP.DesktopApplication.Helpers;

public class ClipboardHelper
{
    public static IClipboard GetClipboard()
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime { MainWindow: { } window })
        {
            return window.Clipboard!;
        }

        throw new InvalidOperationException("Cannot get clipboard on non-desktop system");
    }
}