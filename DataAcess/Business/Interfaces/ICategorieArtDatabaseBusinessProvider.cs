using DataAcess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAcess.Business.Interfaces
{
    public interface ICategorieArtDatabaseBusinessProvider
    {
        Task<IEnumerable<CATEGORIE_ART>> Find(long id, string nomCategorie);

        Task<CATEGORIE_ART> Save(CATEGORIE_ART obj);

        Task Remove(int id);
    }
}