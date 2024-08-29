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
    public class EntreeDatabaseBusinessProvider : IEntreeDatabaseBusinessProvider
    {
        private readonly SolarThermalEntities _dbContext;
        private readonly IArticleDatabaseBusinessProvider _articleDatabaseBusinessProvider;

        public EntreeDatabaseBusinessProvider(SolarThermalEntities dbContext, IArticleDatabaseBusinessProvider articleDatabaseBusinessProvider)
        {
            _dbContext = dbContext;
            _articleDatabaseBusinessProvider = articleDatabaseBusinessProvider;
        }

        public Task<List<ENTREE>> Find(long id)
        {
            if (id > 0)
            {
                return _dbContext.ENTREE.Where(x => x.Id == id).ToListAsync();
            }
            return _dbContext.ENTREE.ToListAsync();
        }

        public async Task<ENTREE> Add(ENTREE obj)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var context = new SolarThermalEntities();
                var art = obj.ARTICLE;
                art.quantite += obj.quantite;
                art.prix = (art.prix + obj.prixDentree) / 2;
                await _articleDatabaseBusinessProvider.Save(art);
                obj.ARTICLE = null;
                obj.dateEntree = DateTime.Now;
                var myObj = context.ENTREE.Add(obj);
                await context.SaveChangesAsync();
                transaction.Complete();
                return myObj;
            }
        }

        public async Task<ENTREE> Update(int id, ENTREE obj)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var myObj = _dbContext.ENTREE.FirstOrDefault(x => x.Id == id);

                obj = myObj; // boh
                _dbContext.ENTREE.AddOrUpdate(obj);
                await _dbContext.SaveChangesAsync();
                transaction.Complete();
                return obj;
            }
        }

        public async Task Remove(int id)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var myObj = _dbContext.ENTREE.Find(id);
                _dbContext.ENTREE.Remove(myObj);
                await _dbContext.SaveChangesAsync();
                transaction.Complete();
            }
        }
    }
}