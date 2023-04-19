using NUnit.Framework;
using StripeCreator.WPF.Tests.Base;
using StripeCreator.WPF.Tests.UiElements.Menu;
using System.Linq;

namespace StripeCreator.WPF.Tests.Pages
{

    internal class WelcomePageTests : BaseTest
    {
        private Menu _menu;

        [Test]
        public void ToImageProcessingNavigation_Success()
        {
            var toImageProcessingMenuItem = _menu.GetMenuByIndex((int)WelcomePageMenuItems.ImageProcessingPage);
            toImageProcessingMenuItem.UiElement.Click();
            var x = 5;
        }

        protected override void DetectComponents()
        {
            base.DetectComponents();
            var window = CurrentWindow;
            var welcomePage = window.FindFirstChild(opt => opt.ByClassName(nameof(WelcomePage)));
            var actionMenuControl = welcomePage.FindFirstChild(opt => opt.ByClassName(nameof(ActionMenuControl)));
            var actionMenuItems = actionMenuControl.FindAllChildren(opt => opt.ByClassName(nameof(ActionMenuItemControl)));
            var menuItems = actionMenuItems.Select((item, index) => new MenuItem(item, index));
            _menu = new Menu(actionMenuControl, menuItems);
        }
    }
}
