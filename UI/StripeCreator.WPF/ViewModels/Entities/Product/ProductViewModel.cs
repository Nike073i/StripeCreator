using StripeCreator.Business.Models;
using System.Collections.Generic;

namespace StripeCreator.WPF
{
    /// <summary>
    /// ViewModel доменной сущности <see cref="Product"/>
    /// </summary>
    public class ProductViewModel : EntityViewModel<Product>
    {
        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="product">Доменная модель продукта</param>
        public ProductViewModel(Product product) : base(product) { }

        #endregion

        #region Interface implementations 

        public override EntityInfoViewModel GetData
        {
            get
            {
                var data = new List<EntityInfoValueViewModel>
                {
                    new("Название", Entity.Name),
                    new("Описание", Entity.Description),
                    new("Стоимость", Entity.Price.ToString()),
                };
                return new EntityInfoViewModel(data);
            }
        }

        #endregion
    }
}
