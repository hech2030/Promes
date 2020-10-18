using System;
using DataAcess.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using IsolationLevel = System.Transactions.IsolationLevel;
using System.Data.Entity.Migrations;

namespace DataAcess.Business
{
    public class MagasinDatabaseBusinessProvider
    {
        /// <summary>
        /// lock object
        /// </summary>
        private static readonly object Lck = new object();

        /// <summary>
        /// instance of DataBaseBusinessProvider
        /// </summary>
        private static MagasinDatabaseBusinessProvider instance;

        /// <summary>
        /// Gets Instance.
        /// </summary>
        public static MagasinDatabaseBusinessProvider Instance
        {
            get
            {
                lock (Lck)
                {
                    return instance ?? (instance = new MagasinDatabaseBusinessProvider());
                }
            }
        }

        public IEnumerable<MAGASIN> Find(long id)
        {
            if (id > 0)
            {
                using (new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
                {
                    return new SolarThermalEntities().MAGASIN.Where(x => x.Id == id).ToList();
                }
            }
            else
            {
                using (new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
                {
                    return new SolarThermalEntities().MAGASIN.ToList();
                }
            }
        }

        public IEnumerable<MAGASIN> Get()
        {
            return new SolarThermalEntities().MAGASIN.ToList();
        }

        public MAGASIN Add(MAGASIN obj)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var context = new SolarThermalEntities();
                var myObj = context.MAGASIN.Add(obj);
                context.SaveChanges();
                transaction.Complete();
                return myObj;
            }
        }

        public MAGASIN Update(int id, MAGASIN obj)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var context = new SolarThermalEntities();
                var myObj = context.MAGASIN.FirstOrDefault(x => x.Id == id);
                myObj = obj;
                // a tester sinon :  context.MAGASIN.AddOrUpdate<MAGASIN>(obj);
                context.SaveChanges();
                transaction.Complete();
                return myObj;
            }
        }

        public void Remove(int id)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var context = new SolarThermalEntities();
                var myObj = context.MAGASIN.Find(id);
                context.MAGASIN.Remove(myObj);
                context.SaveChanges();
                transaction.Complete();
            }
        }
    }
}
