using DataAcess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAcess.Business.Interfaces
{
    public interface ISortieDatabaseBusinessProvider
    {
        Task<List<SORTIE>> Find(long id);

        Task<SORTIE> Add(SORTIE obj);

        Task<SORTIE> Update(int id, SORTIE obj);

        Task<List<SORTIE>> Get();

        Task Remove(int id);
    }
}