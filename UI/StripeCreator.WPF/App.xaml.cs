using System;
using System.Windows;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Основной класс приложения
    /// </summary>
    public partial class App
    {
        #region Public properties

        /// <summary>
        /// Текущая рабочая папка
        /// </summary>
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

        #region Window managements

        /// <summary>
        /// Установка размеров окна
        /// </summary>
        /// <param name="width">Ширина окна</param>
        /// <param name="height">Высота окна</param>
        /// <param name="isMinSize">Установить <paramref name="width"/> и <paramref name="height"/> в качестве минимальных размеров</param>
        public static void SetWindowSize(int width, int height, bool isMinSize = true)
        {
            Current.MainWindow.Height = height;
            Current.MainWindow.Width = width;
            if (isMinSize)
            {
                Current.MainWindow.MinWidth = width;
                Current.MainWindow.MinHeight = height;
            }
        }

        #endregion
    }
}
