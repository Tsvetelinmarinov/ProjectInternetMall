using EMall.Data.DatabaseContext;
using EMall.Data.EntityModels;
using EMall.Data.Exceptions;
using EMall.Data.Repositories.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using static EMall.Data.EntityModels.Common.DbConstants.BaseRepositoryRelated;

namespace EMall.Data.Repositories.Base
{
    /// <summary>
    ///  Provides base CRUD operations for all repositories.
    /// </summary>
    public abstract class RepositoryBase<TEntity>
        : IBaseRepository<TEntity>
        where TEntity : class
    {
        #region Protected(Child-Only) Properties

        protected ApplicationDbContext DbContext { get; private set; }
        protected DbSet<TEntity> CurrentSet { get; private set; }

        #endregion

        #region Constructor

        protected RepositoryBase(ApplicationDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.CurrentSet = this.DbContext.Set<TEntity>();
        }

        #endregion


        /// <summary>
        ///  Retrieves all the entities from the database
        ///  whithout tracking it for changes.
        /// </summary>
        /// <returns>
        ///  <see cref="IQueryable{TEntity}"/> with all untracked entites.
        /// </returns>
        public async Task<IQueryable<TEntity>> AllAsNoTracking()
            => this.CurrentSet
                   .AsNoTracking()
                   .AsQueryable();

        /// <summary>
        ///  Adds an entity/record to the database.
        /// </summary>
        /// <param name="entity">
        ///  The entity to be added.
        /// </param>
        public async Task AddAsync(TEntity entity)
        {
            _ = await this.CurrentSet.AddAsync(entity);
            _ = await this.DbContext.SaveChangesAsync();
        }

        /// <summary>
        ///  Deletes entity/record form the
        ///  database by its ID.
        ///  <strong>NOTE: Every delete is SoftDelete native.</strong>
        /// </summary>
        /// <param name="id">
        ///  The ID of the entity/record to be deleted.
        /// </param>
        /// <returns>
        ///  True if deleted - otherwise False.
        /// </returns>
        public async Task DeleteAsync(int id)
        {
            var entity = await this.CurrentSet
                .FindAsync([id])
                ?? throw new DbException(NoSuchEntity);

            // Soft delete on products.
            if (typeof(TEntity).IsAssignableFrom(typeof(Product)))
            {
                var productEntity = entity as Product
                    ?? throw new ArgumentNullException("productEntity");

                productEntity.IsDeleted = true;
                _ = await this.DbContext.SaveChangesAsync();
            }

            // Normal deleting for all other entities.
            var removedEntityEntry = this.CurrentSet.Remove(entity);
            _ = await this.DbContext.SaveChangesAsync();
        }

        /// <summary>
        ///  Retrieves entity from the database
        ///  by its ID.
        /// </summary>
        /// <param name="id">
        ///  The ID of the record/entity.
        /// </param>
        /// <returns>
        ///  The record/entity with the specified ID.
        /// </returns>
        public async Task<TEntity> FindByIdAsync(int id)
            => await this.CurrentSet.FindAsync([id])
               ?? throw new DbException(NoSuchEntity);
    }
}