using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DataContext _dataContext;

        // Access to the corresponding table for the entity
        private readonly DbSet<T> _dbSet;

        public Repository(DataContext dataContext)
        {
            _dataContext = dataContext;
            _dbSet = _dataContext.Set<T>();
        }

        public async Task AddRecordAsync(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateException dbUpdateEx)
            {
                throw new DbUpdateException("There was an error while attempting to add the record.", dbUpdateEx);
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while adding the record.");
            }
        }

        public async Task<bool> DeleteRecordAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                return false;
            }
            _dataContext.Remove(entity);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<T>> GetAllRecordsAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetRecordByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task UpdateRecordAsync(T entity)
        {
            try
            {
                _dataContext.Update(entity);
                await _dataContext.SaveChangesAsync();
            }
            // Handle concurrency exceptions
            catch (DbUpdateConcurrencyException dbConExc)
            {
                throw new DbUpdateConcurrencyException("This record was updated by another process.", dbConExc);
            }
            catch (DbUpdateException dbUpdateEx)
            {
                throw new DbUpdateException("There was an error while attempting to update the record.", dbUpdateEx);
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while updating the record.");
            }
        }
    }
}
