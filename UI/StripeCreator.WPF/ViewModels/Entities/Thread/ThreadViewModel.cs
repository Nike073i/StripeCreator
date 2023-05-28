using StripeCreator.Stripe.Extensions;
using System.Collections.Generic;
using Thread = StripeCreator.Stripe.Models.Thread;

namespace StripeCreator.WPF
{
    /// <summary>
    /// ViewModel доменной сущности <see cref="Thread"/>
    /// </summary>
    public class ThreadViewModel : EntityViewModel<Thread>
    {
        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="thread">Доменная модель нити</param>
        public ThreadViewModel(Thread thread) : base(thread) { }

        #endregion

        #region Interface implementations 

        public override EntityInfoViewModel GetData
        {
            get
            {
                var data = new List<EntityInfoValueViewModel>
                {
                    new("Название", Entity.Name),
                    new("Стоимость", Entity.Price.ToString()),
                    new("Производитель", Entity.Manufacturer),
                    new("Код цвета", Entity.Color.HexValue),
                    new("Тип", Entity.Type.ConverToString()),
                    new("Вес", Entity.Weight.ToString())
                };

                return new EntityInfoViewModel(data);
            }
        }

        #endregion
    }
}
