using DataAcess.Business.Interfaces;
using DataAcess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using IsolationLevel = System.Transactions.IsolationLevel;

namespace DataAcess.Business
{
    public class CategorieArtDatabaseBusinessProvider : ICategorieArtDatabaseBusinessProvider
    {
        private readonly SolarThermalEntities _dbContext;

        public CategorieArtDatabaseBusinessProvider(SolarThermalEntities dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CATEGORIE_ART>> Find(long id, string nomCategorie)
        {
            ICollection<ARTICLE> articles = new List<ARTICLE>();
            var query = _dbContext.CATEGORIE_ART.AsQueryable();
            if (id > 0)
            {
                articles = await _dbContext.ARTICLE.AsQueryable().Where(x => x.CATEGORIE_ARTId == id).ToListAsync();
                query = query.Where(x => x.Id == id);
            }
            if (!string.IsNullOrEmpty(nomCategorie))
            {
                query = query.Where(x => x.nomCate == nomCategorie);
            }
            var result = query.ToList();
            if (articles.Count > 0)
            {
                result[0].ARTICLE = articles;
            }
            return result;
        }

        public async Task<CATEGORIE_ART> Save(CATEGORIE_ART obj)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var currentList = _dbContext.CATEGORIE_ART.ToList();
                if (obj.Id > 0)
                {
                    currentList.Remove(currentList.Find(x => x.Id == obj.Id));
                }
                if (currentList.Exists(x => x.nomCate.Equals(obj.nomCate)))
                {
                    throw new Exception() { HelpLink = "Ce nom de catégorie existe déjà" };
                }
                else
                {
                    _dbContext.CATEGORIE_ART.AddOrUpdate(obj);
                    await _dbContext.SaveChangesAsync();
                    transaction.Complete();
                    return obj;
                }
            }
        }

        public async Task Remove(int id)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var obj = _dbContext.CATEGORIE_ART.Find(id);
                if (obj.ARTICLE.Count > 0)
                {
                    throw new Exception() { HelpLink = "Cette catégorie contient des articles" };
                }
                else
                {
                    _dbContext.CATEGORIE_ART.Remove(obj);
                    await _dbContext.SaveChangesAsync();
                }
                transaction.Complete();
            }
        }
    }
}