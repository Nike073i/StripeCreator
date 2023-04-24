using NUnit.Framework;
using StripeCreator.WPF.Tests.Base;
using StripeCreator.WPF.Tests.Pages.WelcomePage;
using System.Linq;
using MenuItemUiHelper = StripeCreator.WPF.Tests.UiElements.Menu.MenuItem;
using MenuUiHelper = StripeCreator.WPF.Tests.UiElements.Menu.Menu;

namespace StripeCreator.WPF.Tests.Pages.ReportPage
{
    internal class ReportPageNavigationTests : BaseTest
    {
        private MenuUiHelper _menu;

        [Test]
        public void LoadPage_Success()
        {
            Assert.Multiple(() =>
            {
                Assert.That(CurrentWindow, Is.Not.Null);
                Assert.That(CurrentWindow.Title, Is.EqualTo(PageTitles.ReportPageTitle));
                Assert.That(CurrentWindow.IsEnabled, Is.True);
            });
        }

        [Test]
        public void ToMainMenuNavigation_Success()
        {
            var toMainMenuButton = _menu.GetMenuByIndex((int)ReportPageMenuItems.ToMainMenu);
            toMainMenuButton.UiElement.Click();

            Assert.Multiple(() =>
            {
                Assert.That(CurrentWindow, Is.Not.Null);
                Assert.That(CurrentWindow.Title, Is.EqualTo(PageTitles.WelcomePageTitle));
                Assert.That(CurrentWindow.IsEnabled, Is.True);
            });
        }

        [Test]
        public void Report_Success()
        {
            var createButton = _menu.GetMenuByIndex((int)ReportPageMenuItems.CreateReport);
            createButton.UiElement.Click();

            var openDialogWindow = CurrentWindow.ModalWindows.First();

            Assert.Multiple(() =>
            {
                Assert.That(openDialogWindow, Is.Not.Null);
                Assert.That(openDialogWindow.Title, Is.EqualTo("Сохранение"));
                Assert.That(openDialogWindow.IsEnabled, Is.True);
            });
        }

        protected override void Preparation()
        {
            base.Preparation();
            var welcomePage = CurrentWindow.FindFirstChild(opt => opt.ByClassName(nameof(WelcomePage)));
            var actionMenuControl = welcomePage.FindFirstChild(opt => opt.ByClassName(nameof(ActionMenuControl)));
            var actionMenuItems = actionMenuControl.FindAllChildren(opt => opt.ByClassName(nameof(ActionMenuItemControl)));
            var menuItems = actionMenuItems.Select((item, index) => new MenuItemUiHelper(item, index));
            var menu = new MenuUiHelper(actionMenuControl, menuItems);
            var toDataPageMenuItem = menu.GetMenuByIndex((int)WelcomePageMenuItems.ReportPage);
            toDataPageMenuItem.UiElement.Click();
        }

        protected override void DetectComponents()
        {
            base.DetectComponents();
            var reportPage = CurrentWindow.FindFirstChild(opt => opt.ByClassName(nameof(ReportPage)));
            var mainGroupBox = reportPage.FindFirstChild();
            var actionMenuControl = mainGroupBox.FindFirstChild(cond => cond.ByClassName(nameof(ActionMenuControl)));
            var actionMenuItems = actionMenuControl.FindAllChildren(opt => opt.ByClassName(nameof(ActionMenuItemControl)));
            var menuItems = actionMenuItems.Select((item, index) => new MenuItemUiHelper(item, index));
            _menu = new MenuUiHelper(actionMenuControl, menuItems);
        }
    }
}
