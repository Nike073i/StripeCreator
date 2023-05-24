using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using NUnit.Framework;
using StripeCreator.WPF.Tests.Base;
using StripeCreator.WPF.Tests.Pages.WelcomePage;
using StripeCreator.WPF.Tests.Resources;
using System.Linq;
using Menu = StripeCreator.WPF.Tests.UiElements.Menu.Menu;
using MenuItem = StripeCreator.WPF.Tests.UiElements.Menu.MenuItem;

namespace StripeCreator.WPF.Tests.Pages.SchemePage
{
    internal class SchemePageNavigationTests : BaseTest
    {
        private Menu _menu;

        [Test]
        public void LoadPage_Success()
        {
            Assert.Multiple(() =>
            {
                Assert.That(CurrentWindow, Is.Not.Null);
                Assert.That(CurrentWindow.Title, Is.EqualTo(PageTitles.SchemePageTitle));
                Assert.That(CurrentWindow.IsEnabled, Is.True);
            });
        }

        [Test]
        public void ToMainMenuNavigation_Success()
        {
            CurrentWindow.Patterns.Window.Pattern.SetWindowVisualState(FlaUI.Core.Definitions.WindowVisualState.Maximized);
            var toMainMenuButton = _menu.GetMenuByIndex((int)SchemePageMenuItems.ToMainMenu);
            toMainMenuButton.UiElement.Click();

            Assert.Multiple(() =>
            {
                Assert.That(CurrentWindow, Is.Not.Null);
                Assert.That(CurrentWindow.Title, Is.EqualTo(PageTitles.WelcomePageTitle));
                Assert.That(CurrentWindow.IsEnabled, Is.True);
            });
        }

        [Test]
        public void SaveScheme_Success()
        {
            var saveSchemeButton = _menu.GetMenuByIndex((int)SchemePageMenuItems.SaveScheme);
            saveSchemeButton.UiElement.Click();

            var openDialogWindow = CurrentWindow.ModalWindows.First();

            Assert.Multiple(() =>
            {
                Assert.That(openDialogWindow, Is.Not.Null);
                Assert.That(openDialogWindow.Title, Is.EqualTo("Сохранение"));
                Assert.That(openDialogWindow.IsEnabled, Is.True);
            });
        }


        [Test]
        public void ExportScheme_Success()
        {
            var exportSchemeButton = _menu.GetMenuByIndex((int)SchemePageMenuItems.ExportScheme);
            exportSchemeButton.UiElement.Click();

            var openDialogWindow = CurrentWindow.ModalWindows.First();

            Assert.Multiple(() =>
            {
                Assert.That(openDialogWindow, Is.Not.Null);
                Assert.That(openDialogWindow.Title, Is.EqualTo("Сохранение"));
                Assert.That(openDialogWindow.IsEnabled, Is.True);
            });
        }

        [Test]
        public void CalculateMaterials_Success()
        {
            CurrentWindow.Patterns.Window.Pattern.SetWindowVisualState(FlaUI.Core.Definitions.WindowVisualState.Maximized);
            var calculateButton = _menu.GetMenuByIndex((int)SchemePageMenuItems.CalculateMaterials);
            calculateButton.UiElement.Click();

            var openDialogWindow = CurrentWindow.ModalWindows.First();
            Assert.Multiple(() =>
            {
                Assert.That(openDialogWindow, Is.Not.Null);
                Assert.That(openDialogWindow.Title.Contains(SchemePageUiElementSelectors.CalculateMaterialsWindowTitle));
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
            var toSchemePageMenuItem = menu.GetMenuByIndex((int)WelcomePageMenuItems.SchemePageAction);
            toSchemePageMenuItem.UiElement.Click();
            var openDialogWindow = CurrentWindow.ModalWindows.First();
            var pathPanel = openDialogWindow.FindFirstChild(cond => cond.ByClassName("ComboBoxEx32"));
            var pathComboBox = openDialogWindow.FindFirstChild(cond => cond.ByClassName("ComboBox"));
            var pathText = pathComboBox.FindFirstChild(cond => cond.ByClassName("Edit")).AsTextBox();
            pathText.Text = ResourcePaths.ExampleSchemePath;
            var openButton = openDialogWindow.FindFirstChild(cond => new AndCondition(cond.ByClassName("Button"), cond.ByName("Открыть")));
            openButton.Click();
        }

        protected override void DetectComponents()
        {
            base.DetectComponents();
            var schemePage = CurrentWindow.FindFirstChild(opt => opt.ByClassName(nameof(SchemePage)));
            var sideMenuControl = schemePage.FindFirstChild(cond => cond.ByClassName(nameof(SideIconMenuControl)));
            var scrollViewer = sideMenuControl.FindFirstChild();
            var actionMenuItems = scrollViewer.FindAllChildren(opt => opt.ByClassName(nameof(SideIconMenuItemControl)));
            var menuItems = scrollViewer.FindAllChildren().Select((item, index) => new MenuItem(item, index));
            _menu = new Menu(sideMenuControl, menuItems);
        }
    }
}
