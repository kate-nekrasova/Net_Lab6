using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net_Lab6.interfaces
{
    public interface IFileLoader
    {
        Task<List<T>> LoadAsync<T>(string filePath);
    }
}
