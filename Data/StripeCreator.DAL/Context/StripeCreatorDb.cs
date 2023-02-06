using Microsoft.EntityFrameworkCore;
using StripeCreator.DAL.Models;

namespace StripeCreator.DAL
{
    public class StripeCreatorDb : DbContext
    {
        #region Constructors

        /// <summary>
        /// Конструктор с опциями подключения к БД
        /// </summary>
        /// <param name="options">Опции подключения к БД</param>
        public StripeCreatorDb(DbContextOptions<StripeCreatorDb> options) : base(options) { }

        #endregion

        #region Overrides

        /// <summary>
        /// Перегруз для установки конфигураций моделей
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DbThread>();
            modelBuilder.Entity<DbCloth>();
        }

        #endregion
    }
}