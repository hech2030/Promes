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
    public class ArticleDatabaseBusinessProvider : IArticleDatabaseBusinessProvider
    {
        private readonly SolarThermalEntities _dbContext;

        public ArticleDatabaseBusinessProvider(SolarThermalEntities dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<ARTICLE>> Find(long id, long magasinId, string designation)
        {
            var query = _dbContext.ARTICLE.AsQueryable();
            if (id > 0)
            {
                query = query.Where(x => x.Id == id);
            }
            if (!string.IsNullOrEmpty(designation))
            {
                query = query.Where(x => x.designation.Contains(designation));
            }
            if (magasinId > 0)
            {
                query = query.Where(x => x.MAGASINId == magasinId);
            }
            return await query.Include(a => a.MAGASIN)
                        .Include(a => a.CATEGORIE_ART).ToListAsync();
        }

        public async Task<ARTICLE> Save(ARTICLE value)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var control = await Find(0, 0, string.Empty);
                if (value.Id > 0)
                {
                    var CurrenctValue = control.FirstOrDefault(x => x.Id == value.Id);
                    control.Remove(CurrenctValue);
                }
                if (control.Any(x => x.designation.Equals(value.designation)))
                {
                    _dbContext.ARTICLE.AddOrUpdate(value);
                    _dbContext.SaveChanges();
                    transaction.Complete();
                    return value;
                }
                else
                {
                    throw new Exception() { HelpLink = "Le désignation de l'article existe déjà" };
                }
            }
        }

        public async Task<bool> Remove(long id)
        {
            try
            {
                using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
                {
                    var value = await _dbContext.ARTICLE.FindAsync(id);
                    _dbContext.ARTICLE.Remove(value);
                    _dbContext.SaveChanges();
                    transaction.Complete();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}