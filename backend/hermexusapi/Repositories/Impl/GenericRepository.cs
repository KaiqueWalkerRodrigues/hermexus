using hermexusapi.Models.Context;
using Microsoft.EntityFrameworkCore;
using hermexusapi.Models.Base;

namespace hermexusapi.Repositories.Impl
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected MySQLContext _context;
        private DbSet<T> _dataset;

        public GenericRepository(MySQLContext context)
        {
            _context = context;
            _dataset = _context.Set<T>();
        }
        public T Create(T item)
        {
            _context.Add(item);
            _context.SaveChanges();
            return item;
        }
        public List<T> FindAll()
        {
            return _dataset.ToList();
        }
        public T FindById(long id)
        {
            return _dataset.Find(id);
        }
        public virtual T Update(T item)
        {
            var existinItem = _dataset.Find(item.Id);
            if (existinItem == null)
                return null;
            _context.Entry(existinItem).CurrentValues.SetValues(item);
            _context.SaveChanges();
            return item;
        }
        public bool Delete(long id)
        {
            var existingItem = _dataset.Find(id);
 
            if (existingItem == null) return false;

            existingItem.Deleted_at = DateTime.Now;

            _context.Entry(existingItem).State = EntityState.Modified;
            _context.SaveChanges();

            return true;
        }
        public bool Exists(long id)
        {
            return _dataset.Any(e => e.Id == id);
        }

        public List<T> FindWithPagedSearch(string query)
        {
            //return _dataset.FromSqlRaw(query).ToList();
            return [.. _dataset.FromSqlRaw(query)];
        }

        public int GetCount(string query)
        {
            using var connection = _context.Database.GetDbConnection();
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = query;

            var result = command.ExecuteScalar();
            return Convert.ToInt32(result);
        }
    }
}
