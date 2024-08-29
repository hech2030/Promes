using DataAcess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAcess.Business.Interfaces
{
    public interface IArticleDatabaseBusinessProvider
    {
        Task<ICollection<ARTICLE>> Find(long id, long magasinId, string designation);

        Task<ARTICLE> Save(ARTICLE value);

        Task<bool> Remove(long id);
    }
}