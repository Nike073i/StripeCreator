using FlaUI.Core.AutomationElements;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StripeCreator.WPF.Tests.UiElements.Menu
{
    internal class Menu
    {
        private List<MenuItem> _items;

        public AutomationElement UiElement { get; }
        public IEnumerable<MenuItem> Items => _items;

        public Menu(AutomationElement uiElement, IEnumerable<MenuItem> items)
        {
            UiElement = uiElement;
            _items = items.ToList();
        }

        public MenuItem GetMenuByIndex(int index)
        {
            if (index >= _items.Count)
                throw new ArgumentOutOfRangeException(nameof(index));
            return _items[index];
        }
    }
}
