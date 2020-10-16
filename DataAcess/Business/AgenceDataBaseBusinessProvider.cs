using DataAcess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Transactions;

namespace DataAcess.Business
{
    public class AgenceDataBaseBusinessProvider
    {
        private static SolarThermalEntities Context = new SolarThermalEntities();
        private readonly DbSet<AGENCE> DataAccessProvider = Context.AGENCE;
        /// <summary>
        /// lock object
        /// </summary>
        private static readonly object Lck = new object();

        /// <summary>
        /// instance of DataBaseBusinessProvider
        /// </summary>
        private static AgenceDataBaseBusinessProvider instance;

        /// <summary>
        /// Gets Instance.
        /// </summary>
        public static AgenceDataBaseBusinessProvider Instance

        {
            get
            {
                lock (Lck)
                {
                    return instance ?? (instance = new AgenceDataBaseBusinessProvider());
                }
            }
        }

        public IList<AGENCE> Find(long id, string ville)
        {

            var query = DataAccessProvider.AsQueryable();
            if (id > 0)
            {
                query = query.Where(x => x.Id == id);
            }
            if (!string.IsNullOrEmpty(ville))
            {
                query = query.Where(x => x.ville.Contains(ville));
            }
            return query.ToList();
        }

        public AGENCE Save(AGENCE value)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                DataAccessProvider.AddOrUpdate(value);
                Context.SaveChanges();
                transaction.Complete();
                return value;
            }
        }


        public bool Remove(long id)
        {
            try
            {
                using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
                {
                    var value = DataAccessProvider.Find(id);
                    DataAccessProvider.Remove(value);
                    Context.SaveChanges();
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
