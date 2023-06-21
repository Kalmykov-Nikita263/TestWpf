using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using TestWpf.Infrastructure.HostBuilders;

namespace TestWpf;

public partial class App : Application
{
    public IHost AppHost { get; private set; }

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

    #if DEBUG
        AllocConsole();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput())
        {
            AutoFlush = true
        });
    #endif

        AppHost = ApplicationHostBuilder.CreateDefaultBuilder(e.Args).Build();

        var layout = AppHost.Services.GetRequiredService<Layout>();
        layout.Show();

        await AppHost.StartAsync();
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        base.OnExit(e);

        await AppHost?.StopAsync();
        AppHost?.Dispose();
        FreeConsole();
    }

    [LibraryImport("kernel32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool AllocConsole();

    [LibraryImport("kernel32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool FreeConsole();
}