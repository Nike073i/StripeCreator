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

        #region Public properties

        public DbSet<DbClient> Clients { get; set; }
        public DbSet<DbOrder> Orders { get; set; }
        public DbSet<DbProduct> Products { get; set; }
        public DbSet<DbCloth> Cloths { get; set; }
        public DbSet<DbThread> Threads { get; set; }

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
            modelBuilder.Entity<DbProduct>();

            #region Сущность клиента

            // Связь клиента с заказом
            modelBuilder.Entity<DbClient>()
                .HasMany(client => client.Orders)
                .WithOne(order => order.Client)
                .HasForeignKey(order => order.ClientId);

            #endregion

            #region Сущность заказа

            // Связь заказа с клиентом
            modelBuilder.Entity<DbOrder>()
                .HasOne(order => order.Client)
                .WithMany(client => client.Orders)
                .HasForeignKey(order => order.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Связь заказа с заказ-продукцией
            modelBuilder.Entity<DbOrder>()
                .HasMany(order => order.Products)
                .WithOne(orderProduct => orderProduct.DbOrder)
                .HasForeignKey(orderProduct => orderProduct.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion

            #region Сущность связки заказа и продукции (М:М)

            // Связь заказа-продукции с заказом
            modelBuilder.Entity<DbOrderProduct>()
                .HasKey(x => new { x.OrderId, x.ProductId });

            modelBuilder.Entity<DbOrderProduct>()
                .HasOne(x => x.DbOrder)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.OrderId);

            modelBuilder.Entity<DbOrderProduct>()
                .HasOne(x => x.DbProduct)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion
        }

        #endregion
    }
}