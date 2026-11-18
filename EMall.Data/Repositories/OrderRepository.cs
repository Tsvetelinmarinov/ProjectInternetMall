using EMall.Data.DatabaseContext;
using EMall.Data.EntityModels;
using EMall.Data.Exceptions;
using EMall.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using static EMall.Data.EntityModels.Common.DbConstants.OrderRelated;

namespace EMall.Data.Repositories
{
    /// <summary>
    ///  Provides CRUD operations for Order entity.
    /// </summary>
    public class OrderRepository(ApplicationDbContext context) : IOrderRepository
    {
        // DbContext for communicating with the database.
        private readonly ApplicationDbContext _context = context;

        /// <summary>
        ///  Retrieves all the orders.
        /// </summary>
        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            var orderEntities =  await this._context
                .Orders
                .AsNoTracking()
                .OrderBy((order) => order.Status)
                .ThenByDescending((order) => order.TotalPrice)
                .ToListAsync()
                 ?? throw new DbException(NoOrdersFromDb);

            return orderEntities;
        }

        /// <summary>
        ///  Adds an entity/record to the database.
        /// </summary>
        /// <param name="entity">
        ///  The entity to be added.
        /// </param>
        public async Task AddAsync(Order entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///  Deletes entity/record of form the
        ///  database by its ID.
        /// </summary>
        /// <param name="id">
        ///  The ID of the entity/record to be deleted.
        /// </param>
        /// <returns>
        ///  True if deleted - otherwise False.
        /// </returns>
        public async Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///  Updates existing record/entity in the database.
        /// </summary>
        /// <param name="id">
        ///  The ID of the entity to be updated.
        /// </param>
        public async Task UpdateAsync(int id)
        {
            throw new NotImplementedException();
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
        public async Task<Order> FindByIdAsync(int id)
        {
            var orderEntity = await this._context
                .Orders
                .FindAsync([id])
                ?? throw new DbException(NoOrderFromDb);

            return orderEntity;
        }
    }
}