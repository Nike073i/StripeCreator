using FlaUI.Core.AutomationElements;
using NUnit.Framework;
using StripeCreator.WPF.Tests.Base;
using System.Linq;

namespace StripeCreator.WPF.Tests.Pages
{
    internal class WelcomePageActions
    {
        public static readonly string ToImageProcessingPageActionText = "Обработать изображение";
        public static readonly string ToDataPageActionText = "Справочники";
        public static readonly string ToOrderPageActionText = "Заказы";
        public static readonly string ToReportPageActionText = "Отчеты";
        public static readonly string ToCommunityPageActionText = "Сообщество";
        public static readonly string ToSchemePageActionText = "Загрузить схему";
    }

    internal class WelcomePageTests : BaseTest
    {
        private AutomationElement _actionMenuControl;
        private AutomationElement[] _menuItems;

        [Test]
        public void ToImageProcessingNavigation_Success()
        {
            var y = _menuItems.First(menuItem =>
            {
                /// Я пока хз как достать текст и сравнить по нему. Может добавить какое нибудь доп-свойство и по нему искать...
                var buttonElement = menuItem.FindFirstChild();
                var button = buttonElement.AsButton();
                return true;
            });

            var x = 5;
        }

        protected override void DetectComponents()
        {
            base.DetectComponents();
            var window = CurrentWindow;
            var welcomePage = window.FindFirstChild(opt => opt.ByClassName(nameof(WelcomePage)));
            _actionMenuControl = welcomePage.FindFirstChild(opt => opt.ByClassName(nameof(ActionMenuControl)));
            _menuItems = _actionMenuControl.FindAllChildren(opt => opt.ByClassName(nameof(ActionMenuItemControl)));
        }
    }
}
