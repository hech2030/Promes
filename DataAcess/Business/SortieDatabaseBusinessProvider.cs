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
    public class SortieDatabaseBusinessProvider : ISortieDatabaseBusinessProvider
    {
        private readonly SolarThermalEntities _dbContext;
        private readonly IArticleDatabaseBusinessProvider _articleDatabaseBusinessProvider;

        public SortieDatabaseBusinessProvider(SolarThermalEntities dbContext, IArticleDatabaseBusinessProvider articleDatabaseBusinessProvider)
        {
            _dbContext = dbContext;
            _articleDatabaseBusinessProvider = articleDatabaseBusinessProvider;
        }

        public Task<List<SORTIE>> Find(long id)
        {
            if (id > 0)
            {
                using (new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
                {
                    return _dbContext.SORTIE.Where(x => x.Id == id).ToListAsync();
                }
            }
            using (new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                return _dbContext.SORTIE.ToListAsync();
            }
        }

        public async Task<SORTIE> Add(SORTIE obj)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var art = obj.ARTICLE;
                art.quantite -= obj.quantite;
                if (art.quantite < 0)
                {
                    throw new Exception() { HelpLink = "La Quantité ne peut pas être négatif" };
                }
                art.prix = (art.prix + obj.prixDSortie) / 2;
                await _articleDatabaseBusinessProvider.Save(art);
                obj.ARTICLE = null;
                obj.dateSortie = DateTime.Now;
                var myObj = _dbContext.SORTIE.Add(obj);
                await _dbContext.SaveChangesAsync();
                transaction.Complete();
                return myObj;
            }
        }

        public async Task<SORTIE> Update(int id, SORTIE obj)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var myObj = _dbContext.SORTIE.FirstOrDefault(x => x.Id == id);
                obj = myObj;
                _dbContext.SORTIE.AddOrUpdate(obj);
                await _dbContext.SaveChangesAsync();
                transaction.Complete();
                return myObj;
            }
        }

        public Task<List<SORTIE>> Get()
        {
            return _dbContext.SORTIE.ToListAsync();
        }

        public async Task Remove(int id)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var myObj = _dbContext.SORTIE.Find(id);
                _dbContext.SORTIE.Remove(myObj);
                await _dbContext.SaveChangesAsync();
                transaction.Complete();
            }
        }
    }
}