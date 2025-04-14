using DAL.Repositories.Commons;
using System.Linq.Expressions;

namespace DAL.Repositories.Interfaces
{
        public interface IRepository<T> where T : class
        {
            /// <summary>
            /// Retrieve a collection of entities with optional filtering, inclusion, tracking, and paging.
            /// </summary>
            /// <param name="filter">An optional filter expression to apply to the query.</param>
            /// <param name="includeProperties">An optional comma-separated list of related properties to include.</param>
            /// <param name="tracked">A flag to specify if the entities should be tracked by the context.</param>
            /// <param name="paging">Optional paging information for result set pagination.</param>
            /// <returns>An asynchronous task that returns a collection of entities.</returns>
            Task<ICollection<T>> GetPagingAsync(
                Expression<Func<T, bool>>? filter = null,
                string? includeProperties = null,
                bool tracked = false,
                Expression<Func<T, object>>? orderBy = null,
                SortDirection sortDirection = SortDirection.Descending,
                int pageNumber = 1,
                int pageSize = 5
            );

            /// <summary>
            /// Retrieve a collection of entities with optional filtering, inclusion, tracking.
            /// </summary>
            /// <param name="filter">An optional filter expression to apply to the query.</param>
            /// <param name="includeProperties">An optional comma-separated list of related properties to include.</param>
            /// <param name="tracked">A flag to specify if the entities should be tracked by the context.</param>
            /// <returns></returns>
            Task<ICollection<T>> GetAllAsync(
                Expression<Func<T, bool>>? filter = null,
                string? includeProperties = null,
                Expression<Func<T, object>>? orderBy = null,
                SortDirection sortDirection = SortDirection.Ascending,
                bool tracked = false
            );

            /// <summary>
            /// Counts the number of entities that match the specified filter expression.
            /// </summary>
            /// <param name="filter">An optional filter expression to apply to the query.</param>
            /// <returns>A task representing the asynchronous operation, returning the count of matching entities.</returns>
            Task<int> CountAsync(Expression<Func<T, bool>>? filter = null);

            /// <summary>
            /// Retrieve a single entity based on a filter with optional inclusion and tracking.
            /// </summary>
            /// <param name="filter">A filter expression to apply to the query.</param>
            /// <param name="includeProperties">An optional comma-separated list of related properties to include.</param>
            /// <param name="tracked">A flag to specify if the entity should be tracked by the context.</param>
            /// <returns>An asynchronous task that returns a single entity or null if not found.</returns>
            Task<T?> GetAsync(
                Expression<Func<T, bool>> filter,
                string? includeProperties = null,
                bool tracked = false
            );

            /// <summary>
            /// Add a new entity to the repository.
            /// </summary>
            /// <param name="entity">The entity to add.</param>
            /// <returns>An asynchronous task to represent the operation completion.</returns>
            Task AddAsync(T entity);

            /// <summary>
            /// Add range of entities
            /// </summary>
            /// <param name="entities"></param>
            Task AddRange(IEnumerable<T> entities);

            /// <summary>
            /// Remove an entity from the repository.
            /// </summary>
            /// <param name="entities">The entity to remove.</param>
            /// <returns>An asynchronous task to represent the operation completion.</returns>
            Task RemoveAsync(params T[] entities);

            /// <summary>
            /// Remove range of entities
            /// </summary>
            /// <param name="entities"></param>
            Task RemoveRange(IEnumerable<T> entities);

            /// <summary>
            /// Update an entity in the repository.
            /// </summary>
            /// <param name="entity">The entity to update.</param>
            /// <returns>An asynchronous task to represent the operation completion.</returns>
            Task UpdateAsync(T entity);

            /// <summary>
            /// Update range of entities
            /// </summary>
            /// <param name="entities"></param>
            Task UpdateRange(IEnumerable<T> entities);

            /// <summary>
            /// Check if any entity in the repository matches the provided filter.
            /// </summary>
            /// <param name="filter">A filter expression to apply to the query.</param>
            /// <returns>An asynchronous task that returns true if any entity matches the filter, otherwise false.</returns>
            Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
        }

    
}
