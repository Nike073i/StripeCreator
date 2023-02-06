using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StripeCreator.DAL.Models
{
    /// <summary>
    /// Базовый класс хранимой сущности
    /// </summary>
    public abstract class DbEntity
    {
        #region Public properties

        /// <summary>
        /// Идентификатор хранимой сущности
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid? Id { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор по умолчанию для EFC
        /// </summary>
        protected DbEntity(Guid? id = null) => Id = id;

        #endregion
    }
}