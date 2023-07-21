using LibraryApp.BAL.Interfaces;
using LibraryApp.DAL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.BAL.Repositories
{
    public class CrudOperations<T> : ICrudOperations<T> where T : class
    {
        private readonly LibraryContext db;

        public CrudOperations(LibraryContext db)
        {
            this.db = db;
        }
        public async Task<int> Add(T entity)
        {
            await db.Set<T>().AddAsync(entity);
            return await db.SaveChangesAsync();
        }

        public int Delete(T entity)
        {
             db.Set<T>().Remove(entity);
            return db.SaveChanges();
        }

        public async Task<T> Get(int Id)
        {
            return await db.Set<T>().FindAsync(Id);
        }

        public async Task<List<T>> GetAll()
        {
            if(typeof(T).Name is "BorrowingOP")
            {
                return await db.BorrowingOPs.Include(p=>p.Book).Include(p=>p.Borrower).ToListAsync() as List<T>;
            }
            return await db.Set<T>().ToListAsync();
        }

        public int Update(T entity)
        {
            db.Set<T>().Update(entity);
            return db.SaveChanges();
        }
    }
}
