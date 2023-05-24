using NUnit.Framework;
using StripeCreator.WPF.Tests.Base;
using StripeCreator.WPF.Tests.Pages.WelcomePage;
using System.Linq;
using Menu = StripeCreator.WPF.Tests.UiElements.Menu.Menu;
using MenuItem = StripeCreator.WPF.Tests.UiElements.Menu.MenuItem;

namespace StripeCreator.WPF.Tests.Pages.CommunityPage
{
    internal class CommunityPageNavigationTests : BaseTest
    {
        private Menu _menu;

        [Test]
        public void LoadPage_Success()
        {
            Assert.Multiple(() =>
            {
                Assert.That(CurrentWindow, Is.Not.Null);
                Assert.That(CurrentWindow.Title, Is.EqualTo(PageTitles.CommunityPageTitle));
                Assert.That(CurrentWindow.IsEnabled, Is.True);
            });
        }

        [Test]
        public void ToMainMenuNavigation_Success()
        {
            var toMainMenuButton = _menu.GetMenuByIndex((int)CommunityPageMenuItems.ToMainMenu);
            toMainMenuButton.UiElement.Click();

            Assert.Multiple(() =>
            {
                Assert.That(CurrentWindow, Is.Not.Null);
                Assert.That(CurrentWindow.Title, Is.EqualTo(PageTitles.WelcomePageTitle));
                Assert.That(CurrentWindow.IsEnabled, Is.True);
            });
        }

        [Test]
        public void CreateNewMarket_Success()
        {
            var createButton = _menu.GetMenuByIndex((int)CommunityPageMenuItems.Add);
            createButton.UiElement.Click();

            var openDialogWindow = CurrentWindow.ModalWindows.First();
            Assert.Multiple(() =>
            {
                Assert.That(openDialogWindow, Is.Not.Null);
                Assert.That(openDialogWindow.Title.Contains(CommunityPageUiElementSelectors.CreateMarketWindowTitle));
                Assert.That(openDialogWindow.IsEnabled, Is.True);
            });
        }

        [Test]
        public void PublishNewPost_Success()
        {
            var postButton = _menu.GetMenuByIndex((int)CommunityPageMenuItems.Post);
            postButton.UiElement.Click();
            var openDialogWindow = CurrentWindow.ModalWindows.First();
            Assert.Multiple(() =>
            {
                Assert.That(openDialogWindow, Is.Not.Null);
                Assert.That(openDialogWindow.Title.Contains(CommunityPageUiElementSelectors.PublishMessageWindowTitle));
                Assert.That(openDialogWindow.IsEnabled, Is.True);
            });
        }

        protected override void Preparation()
        {
            base.Preparation();
            var welcomePage = CurrentWindow.FindFirstChild(opt => opt.ByClassName(nameof(WelcomePage)));
            var actionMenuControl = welcomePage.FindFirstChild(opt => opt.ByClassName(nameof(ActionMenuControl)));
            var actionMenuItems = actionMenuControl.FindAllChildren(opt => opt.ByClassName(nameof(ActionMenuItemControl)));
            var menuItems = actionMenuItems.Select((item, index) => new MenuItem(item, index));
            var menu = new Menu(actionMenuControl, menuItems);
            var toCommunityPageMenuItem = menu.GetMenuByIndex((int)WelcomePageMenuItems.CommunityPage);
            toCommunityPageMenuItem.UiElement.Click();
        }

        protected override void DetectComponents()
        {
            base.DetectComponents();
            var communityPage = CurrentWindow.FindFirstChild(opt => opt.ByClassName(nameof(CommunityPage)));
            var sideMenuControl = communityPage.FindFirstChild(cond => cond.ByClassName(nameof(SideIconMenuControl)));
            var _scrollViewer = sideMenuControl.FindFirstChild();
            var actionMenuItems = _scrollViewer.FindAllChildren(opt => opt.ByClassName(nameof(SideIconMenuItemControl)));
            var menuItems = _scrollViewer.FindAllChildren().Select((item, index) => new MenuItem(item, index));
            _menu = new Menu(sideMenuControl, menuItems);
        }
    }
}
