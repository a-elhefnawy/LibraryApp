using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.BAL.Interfaces
{
    public interface ICrudOperations<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> Get(int id);
        Task<int> Add(T entity);
        int Update(T entity);
        int Delete(T entity);


    }
}
