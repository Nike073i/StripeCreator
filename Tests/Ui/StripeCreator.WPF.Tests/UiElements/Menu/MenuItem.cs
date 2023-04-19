using FlaUI.Core.AutomationElements;

namespace StripeCreator.WPF.Tests.UiElements.Menu
{
    internal class MenuItem
    {
        public AutomationElement UiElement { get; }
        public int ItemIndex { get; }

        public MenuItem(AutomationElement uiElement, int itemIndex)
        {
            UiElement = uiElement;
            ItemIndex = itemIndex;
        }
    }
}
