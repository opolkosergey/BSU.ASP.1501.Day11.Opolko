using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public interface IRepository<T> where T : IComparable<T>, IEquatable<T>
    {
        List<T> LoadBooks();
        void Save(IEnumerable<T> books);
    }
}
