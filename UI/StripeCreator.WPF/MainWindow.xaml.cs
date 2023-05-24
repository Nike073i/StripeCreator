using System.Windows;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Внутренняя логика окна <see cref="MainWindow"/>
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        public MainWindow() => InitializeComponent();

        /// <summary>
        /// Метод центровки окна при изменении его размеров
        /// </summary>
        /// <param name="sizeInfo">Данные по размерам окна</param>
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            var prevHeight = sizeInfo.PreviousSize.Height;
            if (sizeInfo.HeightChanged && prevHeight > 0)
                Top += (sizeInfo.PreviousSize.Height - sizeInfo.NewSize.Height) / 2;
            var prevWidth = sizeInfo.PreviousSize.Width;
            if (sizeInfo.WidthChanged && prevWidth > 0)
                Left += (sizeInfo.PreviousSize.Width - sizeInfo.NewSize.Width) / 2;
        }
    }
}
