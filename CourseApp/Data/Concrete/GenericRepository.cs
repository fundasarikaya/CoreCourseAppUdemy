using CourseApp.Data.Abstract;
using CourseApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApp.Data.Concrete
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private DataContext _context;
        public GenericRepository(DataContext context)
        {
            _context = context;
        }
        public virtual void Delete(int id)
        {
            _context.Remove<TEntity>(Get(id)); //hangi entity olacagını bilmiyoruz o yuzden genel olsun diye tentity
            _context.SaveChanges();
        }

        public virtual TEntity Get(int id)
        {
            //artık buraya hangi sınıfı gonderirsen onunla ilgili işlemler yapılacak
            return _context.Set<TEntity>().Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        public virtual void Insert(TEntity entity)
        {
            _context.Add<TEntity>(entity);
            _context.SaveChanges();
        }

        public virtual void Update(TEntity entity)
        {
            _context.Update<TEntity>(entity);
            _context.SaveChanges(); //startup kısmına hangi entitylerle ugraşacaksak onları ekledik
        }
    }
}
