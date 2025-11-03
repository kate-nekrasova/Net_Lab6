using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net_Lab6.interfaces
{
    public interface IFileSaver
    {
        Task SaveAsync<T>(string filePath, List<T> data);
    }
}
