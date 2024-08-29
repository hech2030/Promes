using DataAcess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAcess.Business.Interfaces
{
    public interface IEntreeDatabaseBusinessProvider
    {
        Task<List<ENTREE>> Find(long id);

        Task<ENTREE> Update(int id, ENTREE obj);

        Task Remove(int id);

        Task<ENTREE> Add(ENTREE obj);
    }
}