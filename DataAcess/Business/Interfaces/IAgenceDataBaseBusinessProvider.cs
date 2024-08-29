using DataAcess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAcess.Business.Interfaces
{
    public interface IAgenceDataBaseBusinessProvider
    {
        Task<List<AGENCE>> Find(long id, string ville);

        Task<AGENCE> Save(AGENCE value);

        Task<bool> Remove(long id);
    }
}