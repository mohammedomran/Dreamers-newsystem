using Dreamers.Ui.Interfaces;
using Dreamers.Ui.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Dreamers.Ui.Repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private AppDbContext _context;
        private DbSet<T> table;
        public GenericRepo()
        {
            this._context = new AppDbContext();
            table = _context.Set<T>();
        }
        public GenericRepo(AppDbContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }
        public T GetById(object id)
        {
            return table.Find(id);
        }
        public void Insert(T obj)
        {
            table.Add(obj);
        }
        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
