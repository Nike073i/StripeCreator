using NUnit.Framework;
using StripeCreator.WPF.Tests.Base;
using StripeCreator.WPF.Tests.Pages.WelcomePage;
using System.Linq;
using MenuItemUiHelper = StripeCreator.WPF.Tests.UiElements.Menu.MenuItem;
using MenuUiHelper = StripeCreator.WPF.Tests.UiElements.Menu.Menu;

namespace StripeCreator.WPF.Tests.Pages.DataStorePage
{
    internal class DataStorePageUiElementSelectors
    {

    }

    internal class DataStorePageNavigationTests : BaseTest
    {
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

        // Должна быть проверка 6 кнопок: Нитки, Ткани, Клиенты, Продукты, В меню и Создать пользователя

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

            // Находим SideMenuControl
            // Находим клиентскую GB

            //_menu = imageProcessingPage.FindFirstChild(opt =>
            //    new AndCondition(opt.ByClassName(nameof(SideIconMenuControl)),
            //    opt.ByName(ImageProcessingPageUiElementSelectors.ProcessingOptionsGroupBoxTitle)));
        }
    }
}
