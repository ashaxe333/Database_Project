using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace WindowsFormsApp1.DAO
{
    public interface IDAO<T> where T : class
    {
        void Save(T element, MySqlTransaction transaction);
        void Delete(int id);
        List<T> GetAll();
        T GetById(int id);
    }
}
