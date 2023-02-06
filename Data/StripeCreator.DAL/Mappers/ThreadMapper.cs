using StripeCreator.Core.Models;
using StripeCreator.DAL.Models;
using Thread = StripeCreator.Stripe.Models.Thread;

namespace StripeCreator.DAL.Mappers
{
    /// <summary>
    /// преобразователя хранимых <see cref="DbThread"/> и доменных <see cref="Thread"/> сущностей 
    /// </summary>
    public class ThreadMapper : IDbMapper<DbThread, Thread>
    {
        #region Interface implementations

        public DbThread CreateDbModel(Thread domainModel)
        {
            string name = domainModel.Name;
            decimal price = domainModel.Price;
            string manufacturer = domainModel.Manufacturer;
            var color = domainModel.Color.HexValue;
            var type = domainModel.Type;
            int weight = domainModel.Weight;
            return new DbThread(name, price, manufacturer, color, type, weight);
        }

        public void UpdateDbModel(Thread domainModel, ref DbThread dbModel)
        {
            dbModel.Name = domainModel.Name;
            dbModel.Price = domainModel.Price;
            dbModel.Manufacturer = domainModel.Manufacturer;
            dbModel.ColorHex = domainModel.Color.HexValue;
            dbModel.Type = domainModel.Type;
            dbModel.Weight = domainModel.Weight;
        }

        public Thread MapToDomainModel(DbThread dbModel)
        {
            string name = dbModel.Name;
            decimal price = dbModel.Price;
            string manufacturer = dbModel.Manufacturer;
            var color = new Color(dbModel.ColorHex);
            var type = dbModel.Type;
            int weight = dbModel.Weight;
            var id = dbModel.Id;
            return new Thread(name, price, manufacturer, color, type, weight, id);
        }

        #endregion
    }
}