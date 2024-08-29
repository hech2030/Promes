using DataAcess.Business.Interfaces;
using DataAcess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAcess.Business
{
    public class AgenceDataBaseBusinessProvider : IAgenceDataBaseBusinessProvider
    {
        private readonly SolarThermalEntities _dbContext;

        public AgenceDataBaseBusinessProvider(SolarThermalEntities dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<AGENCE>> Find(long id, string ville)
        {
            var query = _dbContext.AGENCE.AsQueryable();
            if (id > 0)
            {
                query = query.Where(x => x.Id == id);
            }
            if (!string.IsNullOrEmpty(ville))
            {
                query = query.Where(x => x.ville.Contains(ville));
            }
            return query.ToListAsync();
        }

        public async Task<AGENCE> Save(AGENCE value)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var control = await Find(0, string.Empty);
                if (value.Id > 0)
                {
                    var CurrenctValue = control.Find(x => x.Id == value.Id); //WTH???
                    control.Remove(CurrenctValue); // WTH?
                }
                if (control.Exists(x => string.Equals(x.nom, value.nom, StringComparison.OrdinalIgnoreCase)))
                {
                    _dbContext.AGENCE.AddOrUpdate(value);
                    _dbContext.SaveChanges();
                    transaction.Complete();
                    return value;
                }
                else
                {
                    throw new Exception() { HelpLink = "Le nom de l'agence existe déjà" };
                }
            }
        }

        public Task<bool> Remove(long id)
        {
            try
            {
                using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
                {
                    var value = _dbContext.AGENCE.Find(id);
                    _dbContext.AGENCE.Remove(value);
                    _dbContext.SaveChanges();
                    transaction.Complete();
                    return Task.FromResult(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Task.FromResult(false);
            }
        }
    }
}