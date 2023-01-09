using System;
using System.Windows;

namespace StripeCreator.WPF
{
    public partial class App
    {
        #region Public properties

        public static string CurrentDirectory => Environment.CurrentDirectory;

        #endregion

        #region Override lifecycles

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            IoC.CreateHost(e.Args, CurrentDirectory, "appsettings.json");
            await IoC.StartAsync();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            await IoC.StopAsync();
        }

        #endregion
    }
}
