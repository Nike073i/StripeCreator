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
            try
            {
                IoC.CreateHost(e.Args, CurrentDirectory, "appsettings.json");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка запуска приложения", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(-1);
            }
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
            if (isMinSize)
            {
                Current.MainWindow.MinWidth = width;
                Current.MainWindow.MinHeight = height;
            }
            Current.MainWindow.Height = height;
            Current.MainWindow.Width = width;
        }

        #endregion
    }
}
