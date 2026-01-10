using System.Windows;
using CManager.Presentation.GuiApp.ViewModels;
using CManager.Presentation.GuiApp.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CManager.Presentation.GuiApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private IHost _host;

    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                //Service/Repository
                //Vyer och Viewmodels
                services.AddSingleton<MainViewModel>();
                services.AddSingleton<MainWindow>();

                services.AddTransient<ActivitiesViewModel>();
                services.AddTransient<ActivitiesView>();

                services.AddTransient<AddActivityViewModel>();
                services.AddTransient<AddActivityView>();

            })
                .Build();
     }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainwindow = _host.Services.GetRequiredService<MainWindow>();
        mainwindow.Show();
    }
}

