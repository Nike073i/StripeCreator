using NUnit.Framework;
using StripeCreator.WPF.Tests.Base;
using System.Linq;
using MenuItemUiHelper = StripeCreator.WPF.Tests.UiElements.Menu.MenuItem;
using MenuUiHelper = StripeCreator.WPF.Tests.UiElements.Menu.Menu;

namespace StripeCreator.WPF.Tests.Pages.WelcomePage
{
    internal class WelcomePageNavigationTests : BaseTest
    {
        private MenuUiHelper _menu;

        [Test]
        public void LoadPage_Success()
        {
            Assert.Multiple(() =>
            {
                Assert.That(CurrentWindow, Is.Not.Null);
                Assert.That(CurrentWindow.Title, Is.EqualTo(PageTitles.WelcomePageTitle));
                Assert.That(CurrentWindow.IsEnabled, Is.True);
            });
        }

        [Test]
        public void ToImageProcessingPageNavigation_Success()
        {
            var toImageProcessingMenuItem = _menu.GetMenuByIndex((int)WelcomePageMenuItems.ImageProcessingPage);
            toImageProcessingMenuItem.UiElement.Click();

            Assert.Multiple(() =>
            {
                Assert.That(CurrentWindow, Is.Not.Null);
                Assert.That(CurrentWindow.Title, Is.EqualTo(PageTitles.ImageProcessingPageTitle));
                Assert.That(CurrentWindow.IsEnabled, Is.True);
            });
        }

        [Test]
        public void ToDataPageNavigation_Success()
        {
            var toDataPageMenuItem = _menu.GetMenuByIndex((int)WelcomePageMenuItems.DataPage);
            toDataPageMenuItem.UiElement.Click();

            Assert.Multiple(() =>
            {
                Assert.That(CurrentWindow, Is.Not.Null);
                Assert.That(CurrentWindow.Title, Is.EqualTo(PageTitles.DataPageTitle));
                Assert.That(CurrentWindow.IsEnabled, Is.True);
            });
        }

        [Test]
        public void ToOrderPageNavigation_Success()
        {
            var toOrderPageMenuItem = _menu.GetMenuByIndex((int)WelcomePageMenuItems.OrderPage);
            toOrderPageMenuItem.UiElement.Click();

            Assert.Multiple(() =>
            {
                Assert.That(CurrentWindow, Is.Not.Null);
                Assert.That(CurrentWindow.Title, Is.EqualTo(PageTitles.OrderPageTitle));
                Assert.That(CurrentWindow.IsEnabled, Is.True);
            });
        }

        [Test]
        public void ToReportPageNavigation_Success()
        {
            var toReportPageMenuItem = _menu.GetMenuByIndex((int)WelcomePageMenuItems.ReportPage);
            toReportPageMenuItem.UiElement.Click();

            Assert.Multiple(() =>
            {
                Assert.That(CurrentWindow, Is.Not.Null);
                Assert.That(CurrentWindow.Title, Is.EqualTo(PageTitles.ReportPageTitle));
                Assert.That(CurrentWindow.IsEnabled, Is.True);
            });
        }

        [Test]
        public void ToCommunityPageNavigation_Success()
        {
            var toCommunityPageMenuItem = _menu.GetMenuByIndex((int)WelcomePageMenuItems.CommunityPage);
            toCommunityPageMenuItem.UiElement.Click();

            Assert.Multiple(() =>
            {
                Assert.That(CurrentWindow, Is.Not.Null);
                Assert.That(CurrentWindow.Title, Is.EqualTo(PageTitles.CommunityPageTitle));
                Assert.That(CurrentWindow.IsEnabled, Is.True);
            });
        }

        [Test]
        public void ToSchemePageNavigation_Success()
        {
            var toSchemePageMenuItem = _menu.GetMenuByIndex((int)WelcomePageMenuItems.SchemePageAction);
            toSchemePageMenuItem.UiElement.Click();

            var openDialogWindow = CurrentWindow.ModalWindows.First();

            Assert.Multiple(() =>
            {
                Assert.That(openDialogWindow, Is.Not.Null);
                Assert.That(openDialogWindow.Title, Is.EqualTo("Открытие"));
                Assert.That(openDialogWindow.IsEnabled, Is.True);
            });
        }

        protected override void DetectComponents()
        {
            base.DetectComponents();
            var welcomePage = CurrentWindow.FindFirstChild(opt => opt.ByClassName(nameof(WelcomePage)));
            var actionMenuControl = welcomePage.FindFirstChild(opt => opt.ByClassName(nameof(ActionMenuControl)));
            var actionMenuItems = actionMenuControl.FindAllChildren(opt => opt.ByClassName(nameof(ActionMenuItemControl)));
            var menuItems = actionMenuItems.Select((item, index) => new MenuItemUiHelper(item, index));
            _menu = new MenuUiHelper(actionMenuControl, menuItems);
        }
    }
}
