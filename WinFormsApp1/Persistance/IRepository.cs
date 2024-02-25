using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.Persistance
{
    public interface IRepository<T>
    {
        void Move();
        T Get(int id);
        void Remove(int id);
        void Append(T t);
        void Rename(int id, string newName);
    }
}
