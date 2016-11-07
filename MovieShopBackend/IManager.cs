using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieShopBackend
{
    public interface IManager <T>
    {
        
        T Create(T t);

        List<T> ReadAll();

        T ReadOne(int id);

        T Update(T t);

        bool Delete(int id);

    }
}
