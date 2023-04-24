using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using NUnit.Framework;
using StripeCreator.WPF.Tests.Base;
using StripeCreator.WPF.Tests.Pages.WelcomePage;
using System.Linq;
using MenuItemUiHelper = StripeCreator.WPF.Tests.UiElements.Menu.MenuItem;
using MenuUiHelper = StripeCreator.WPF.Tests.UiElements.Menu.Menu;

namespace StripeCreator.WPF.Tests.Pages.DataStorePage
{
    internal class DataStorePageNavigationTests : BaseTest
    {
        private MenuUiHelper _menu;
        private AutomationElement _dataManagementGroupBox;

        [Test]
        public void LoadPage_Success()
        {
            Assert.Multiple(() =>
            {
                Assert.That(CurrentWindow, Is.Not.Null);
                Assert.That(CurrentWindow.Title, Is.EqualTo(PageTitles.DataPageTitle));
                Assert.That(CurrentWindow.IsEnabled, Is.True);
            });
        }

        [Test]
        public void ShowThreadData_Success()
        {
            var showThreadMenuItem = _menu.GetMenuByIndex((int)DataStorePageMenuItems.Threads);
            showThreadMenuItem.UiElement.Click();

            Assert.Multiple(() =>
            {
                Assert.That(CurrentWindow, Is.Not.Null);
                Assert.That(CurrentWindow.Title, Is.EqualTo(PageTitles.DataPageTitle));
                Assert.That(CurrentWindow.IsEnabled, Is.True);
                Assert.That(_dataManagementGroupBox.Name, Is.EqualTo(DataStorePageUiElementSelectors.ThreadDataGroupBoxName));
            });
        }

        [Test]
        public void ShowClothData_Success()
        {
            var showClothMenuItem = _menu.GetMenuByIndex((int)DataStorePageMenuItems.Cloths);
            showClothMenuItem.UiElement.Click();

            Assert.Multiple(() =>
            {
                Assert.That(CurrentWindow, Is.Not.Null);
                Assert.That(CurrentWindow.Title, Is.EqualTo(PageTitles.DataPageTitle));
                Assert.That(CurrentWindow.IsEnabled, Is.True);
                Assert.That(_dataManagementGroupBox.Name, Is.EqualTo(DataStorePageUiElementSelectors.ClothDataGroupBoxName));
            });
        }

        [Test]
        public void ShowClientData_Success()
        {
            var showClientMenuItem = _menu.GetMenuByIndex((int)DataStorePageMenuItems.Clients);
            showClientMenuItem.UiElement.Click();

            Assert.Multiple(() =>
            {
                Assert.That(CurrentWindow, Is.Not.Null);
                Assert.That(CurrentWindow.Title, Is.EqualTo(PageTitles.DataPageTitle));
                Assert.That(CurrentWindow.IsEnabled, Is.True);
                Assert.That(_dataManagementGroupBox.Name, Is.EqualTo(DataStorePageUiElementSelectors.ClientDataGroupBoxName));
            });
        }

        [Test]
        public void ShowProductData_Success()
        {
            var showProductMenuItem = _menu.GetMenuByIndex((int)DataStorePageMenuItems.Products);
            showProductMenuItem.UiElement.Click();

            Assert.Multiple(() =>
            {
                Assert.That(CurrentWindow, Is.Not.Null);
                Assert.That(CurrentWindow.Title, Is.EqualTo(PageTitles.DataPageTitle));
                Assert.That(CurrentWindow.IsEnabled, Is.True);
                Assert.That(_dataManagementGroupBox.Name, Is.EqualTo(DataStorePageUiElementSelectors.ProductDataGroupBoxName));
            });
        }

        [Test]
        public void ToMainMenuNavigation_Success()
        {
            var toMainMenuButton = _menu.GetMenuByIndex((int)DataStorePageMenuItems.ToMainMenu);
            toMainMenuButton.UiElement.Click();

            Assert.Multiple(() =>
            {
                Assert.That(CurrentWindow, Is.Not.Null);
                Assert.That(CurrentWindow.Title, Is.EqualTo(PageTitles.WelcomePageTitle));
                Assert.That(CurrentWindow.IsEnabled, Is.True);
            });
        }

        [Test]
        public void CreateNewEntity_Success()
        {
            var addButton = _dataManagementGroupBox.FindFirstChild(cond => new AndCondition(
                cond.ByClassName(nameof(Button)),
                cond.ByHelpText(DataStorePageUiElementSelectors.AddButtonHelpText)));
            addButton.Click();

            Assert.Multiple(() =>
            {
                Assert.That(CurrentWindow, Is.Not.Null);
                Assert.That(CurrentWindow.Title.Contains(DataStorePageUiElementSelectors.FormationWindowTitlePrefix));
                Assert.That(CurrentWindow.IsEnabled, Is.True);
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
            var toDataPageMenuItem = menu.GetMenuByIndex((int)WelcomePageMenuItems.DataPage);
            toDataPageMenuItem.UiElement.Click();
        }

        protected override void DetectComponents()
        {
            base.DetectComponents();
            var dataStorePage = CurrentWindow.FindFirstChild(opt => opt.ByClassName(nameof(DataStorePage)));
            var sideMenuControl = dataStorePage.FindFirstChild(cond => cond.ByClassName(nameof(SideIconMenuControl)));
            var scrollViewer = sideMenuControl.FindFirstChild();
            var actionMenuItems = scrollViewer.FindAllChildren(opt => opt.ByClassName(nameof(SideIconMenuItemControl)));
            var menuItems = scrollViewer.FindAllChildren().Select((item, index) => new MenuItemUiHelper(item, index));
            _menu = new MenuUiHelper(sideMenuControl, menuItems);
            _dataManagementGroupBox = dataStorePage.FindFirstChild(cond =>
                cond.ByHelpText(DataStorePageUiElementSelectors.DataManagementGroupBoxHelpText));
        }
    }
}
