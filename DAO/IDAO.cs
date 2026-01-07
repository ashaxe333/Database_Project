using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.DAO
{
    public interface IDAO<T> where T : class
    {
        void Save(T element);
        void Delete(int id);
        List<T> GetAll();
        T GetById(int id);
    }
}
