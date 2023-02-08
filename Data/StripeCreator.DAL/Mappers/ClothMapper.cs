using StripeCreator.Core.Models;
using StripeCreator.DAL.Models;
using StripeCreator.Stripe.Models;

namespace StripeCreator.DAL.Mappers
{
    /// <summary>
    /// Преобразователь хранимых <see cref="DbCloth"/> и доменных <see cref="Cloth"/> сущностей 
    /// </summary>
    public class ClothMapper : IDbMapper<DbCloth, Cloth>
    {
        #region Interface implementations

        public DbCloth CreateDbModel(Cloth domainModel)
        {
            string name = domainModel.Name;
            decimal price = domainModel.Price;
            string manufacturer = domainModel.Manufacturer;
            var color = domainModel.Color.HexValue;
            var type = domainModel.Type;
            int count = domainModel.Count;
            return new DbCloth(name, price, manufacturer, color, type, count);
        }

        public void UpdateDbModel(Cloth domainModel, ref DbCloth dbModel)
        {
            dbModel.Name = domainModel.Name;
            dbModel.Price = domainModel.Price;
            dbModel.Manufacturer = domainModel.Manufacturer;
            dbModel.ColorHex = domainModel.Color.HexValue;
            dbModel.Type = domainModel.Type;
            dbModel.Count = domainModel.Count;
        }

        public Cloth MapToDomainModel(DbCloth dbModel)
        {
            string name = dbModel.Name;
            decimal price = dbModel.Price;
            string manufacturer = dbModel.Manufacturer;
            var color = new Color(dbModel.ColorHex);
            var type = dbModel.Type;
            int count = dbModel.Count;
            var id = dbModel.Id;
            return new Cloth(name, price, manufacturer, color, type, count, id);
        }

        #endregion
    }
}