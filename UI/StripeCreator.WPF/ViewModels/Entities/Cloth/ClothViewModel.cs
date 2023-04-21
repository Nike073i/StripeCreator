using StripeCreator.Stripe.Models;
using System.Collections.Generic;

namespace StripeCreator.WPF
{
    /// <summary>
    /// ViewModel доменной сущности <see cref="Cloth"/>
    /// </summary>
    public class ClothViewModel : EntityViewModel<Cloth>
    {
        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="cloth">Доменная модель ткани</param>
        public ClothViewModel(Cloth cloth) : base(cloth) { }

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
                    new("Тип", Entity.Type.ToString()),
                    new("Каунт", Entity.Count.ToString())
                };

                return new EntityInfoViewModel(data);
            }
        }

        #endregion
    }
}
