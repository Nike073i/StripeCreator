using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using NUnit.Framework;
using NUnit.Framework.Internal;
using StripeCreator.WPF.Tests.Base;
using StripeCreator.WPF.Tests.Pages.WelcomePage;
using System;
using System.IO;
using System.Linq;
using System.Resources;
using System.Windows.Controls;
using Button = System.Windows.Controls.Button;
using MenuItemUiHelper = StripeCreator.WPF.Tests.UiElements.Menu.MenuItem;
using MenuUiHelper = StripeCreator.WPF.Tests.UiElements.Menu.Menu;

namespace StripeCreator.WPF.Tests.Pages.ImageProcessingPageTests
{
    internal class ImageProcessingPageNavigationTests : BaseTest
    {
        private AutomationElement _processingOptionsGroupBox;

        [Test]
        public void LoadPage_Success()
        {
            Assert.Multiple(() =>
            {
                Assert.That(CurrentWindow, Is.Not.Null);
                Assert.That(CurrentWindow.Title, Is.EqualTo(PageTitles.ImageProcessingPageTitle));
                Assert.That(CurrentWindow.IsEnabled, Is.True);
            });
        }

        [Test]
        public void ToWelcomePageNavigation_Success()
        {
            var toMainMenuButton = _processingOptionsGroupBox.FindFirstChild(cond =>
                new AndCondition(cond.ByClassName(nameof(Button)),
                cond.ByName(ImageProcessingPageUiElementSelectors.ToMainMenuButtonName)));
            toMainMenuButton.Click();

            Assert.Multiple(() =>
            {
                Assert.That(CurrentWindow, Is.Not.Null);
                Assert.That(CurrentWindow.Title, Is.EqualTo(PageTitles.WelcomePageTitle));
                Assert.That(CurrentWindow.IsEnabled, Is.True);
            });
        }

        [Test]
        public void SelectImageForProcessingNavigation_Success()
        {
            var selectImageButton = _processingOptionsGroupBox.FindFirstChild(cond =>
                new AndCondition(cond.ByClassName(nameof(Button)),
                cond.ByName(ImageProcessingPageUiElementSelectors.SelectImageButtonName)));
            selectImageButton.Click();

            var openDialogWindow = CurrentWindow.ModalWindows.First();

            Assert.Multiple(() =>
            {
                Assert.That(openDialogWindow, Is.Not.Null);
                Assert.That(openDialogWindow.Title, Is.EqualTo("Открытие"));
                Assert.That(openDialogWindow.IsEnabled, Is.True);
            });
        }

        [Test]
        public void ToProcessWithoutImage_Fail()
        {
            var toProcessButton = _processingOptionsGroupBox.FindFirstChild(cond =>
                new AndCondition(cond.ByClassName(nameof(Button)),
                cond.ByName(ImageProcessingPageUiElementSelectors.ToProcessImageButtonName)));
            
            Assert.Multiple(() =>
            {
                Assert.That(CurrentWindow, Is.Not.Null);
                Assert.That(toProcessButton.IsEnabled, Is.False);
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
            var toImageProcessingMenuItem = menu.GetMenuByIndex((int)WelcomePageMenuItems.ImageProcessingPage);
            toImageProcessingMenuItem.UiElement.Click();
        }

        protected override void DetectComponents()
        {
            base.DetectComponents();
            var imageProcessingPage = CurrentWindow.FindFirstChild(opt => opt.ByClassName(nameof(ImageProcessingPage)));
            _processingOptionsGroupBox = imageProcessingPage.FindFirstChild(opt =>
                new AndCondition(opt.ByClassName(nameof(GroupBox)),
                opt.ByName(ImageProcessingPageUiElementSelectors.ProcessingOptionsGroupBoxTitle)));
        }
    }
}
