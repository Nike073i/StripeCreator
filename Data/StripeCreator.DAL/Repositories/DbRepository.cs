using Microsoft.EntityFrameworkCore;
using StripeCreator.Core.Models;
using StripeCreator.Core.Repositories;
using StripeCreator.DAL.Exceptions;
using StripeCreator.DAL.Mappers;
using StripeCreator.DAL.Models;

namespace StripeCreator.DAL.Repositories
{
    /// <summary>
    /// Базовая реализация репозитория БД
    /// </summary>
    /// <typeparam name="TDbEntity">Тип хранимой сущности</typeparam>
    /// <typeparam name="TEntity">Тип доменной сущности</typeparam>
    public abstract class DbRepository<TDbEntity, TEntity> : IRepository<TEntity>
    where TDbEntity : DbEntity where TEntity : Entity
    {
        #region Private fields

        /// <summary>
        /// Набор данных хранимых сущностей
        /// </summary>
        protected readonly DbSet<TDbEntity> Set;

        #endregion

        #region Protected fields

        /// <summary>
        /// Контекст базы данных
        /// </summary>
        protected readonly StripeCreatorDb DbContext;

        /// <summary>
        /// Преобразователь хранимых сущностей и доменных сущностей
        /// </summary>
        protected readonly IDbMapper<TDbEntity, TEntity> Mapper;

        /// <summary>
        /// Набор данных хранимых сущностей с возможностью включения в запрос дополнительной логики
        /// </summary>
        protected virtual IQueryable<TDbEntity> Items => Set;

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        /// <param name="mapper">Преобразователь данных</param>
        protected DbRepository(StripeCreatorDb context, IDbMapper<TDbEntity, TEntity> mapper)
        {
            DbContext = context;
            Set = context.Set<TDbEntity>();
            Mapper = mapper;
        }

        #endregion

        #region Interface implementations

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await Items.Select(dmModel => Mapper.MapToDomainModel(dmModel))
                                                                            .ToListAsync();

        public async Task<IEnumerable<TEntity>> GetAsync(int skip, int take)
        {
            if (skip < 0)
                throw new ArgumentOutOfRangeException(nameof(skip), "Количество пропускаемых элементов не может быть < 0");
            if (take <= 0)
                throw new ArgumentOutOfRangeException(nameof(take), "Количество запрашиваемых элементов не может быть <= 0");
            return await Items.Skip(skip)
                            .Take(take)
                            .Select(dmModel => Mapper.MapToDomainModel(dmModel))
                            .ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            var storedEntity = await Items.FirstOrDefaultAsync(x => x.Id == id);
            return storedEntity != null ? Mapper.MapToDomainModel(storedEntity) : null;
        }

        public async Task<Guid> RemoveAsync(Guid id)
        {
            var entity = await Items.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
                throw new EntityNotFoundException($"Сущность с Id {id} не найдена");
            DbContext.Entry(entity).State = EntityState.Deleted;
            await DbContext.SaveChangesAsync();
            return id;
        }

        public async Task<TEntity> SaveAsync(TEntity entity)
        {
            var storedEntity = await Items.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (storedEntity == null)
            {
                storedEntity = Mapper.CreateDbModel(entity);
                await Set.AddAsync(storedEntity);
            }
            else
            {
                Mapper.UpdateDbModel(entity, ref storedEntity);
                Set.Update(storedEntity);
            }
            await DbContext.SaveChangesAsync();
            return Mapper.MapToDomainModel(storedEntity);
        }

        #endregion
    }
}