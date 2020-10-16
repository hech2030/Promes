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
    public class SortieDatabaseBusinessProvider
    {
        /// <summary>
        /// lock object
        /// </summary>
        private static readonly object Lck = new object();

        /// <summary>
        /// instance of DataBaseBusinessProvider
        /// </summary>
        private static SortieDatabaseBusinessProvider instance;

        /// <summary>
        /// Gets Instance.
        /// </summary>
        public static SortieDatabaseBusinessProvider Instance

        {
            get
            {
                lock (Lck)
                {
                    return instance ?? (instance = new SortieDatabaseBusinessProvider());
                }
            }
        }

        public IEnumerable<SORTIE> Find(long id)
        {
            if (id > 0)
            {
                using (new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
                {
                    return new SolarThermalEntities().SORTIE.Where(x => x.Id == id).ToList();
                }
            }
            else
            {
                using (new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
                {
                    return new SolarThermalEntities().SORTIE.ToList();
                }
            }
        }

        public SORTIE Add(SORTIE obj)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var context = new SolarThermalEntities();
                var myObj = context.SORTIE.Add(obj);
                context.SaveChanges();
                transaction.Complete();
                return myObj;
            }
        }

        public SORTIE Update(int id, SORTIE obj)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var context = new SolarThermalEntities();
                var myObj = context.SORTIE.FirstOrDefault(x => x.Id == id);
                myObj = obj;
                // a tester sinon :  context.SORTIE.AddOrUpdate<SORTIE>(obj);
                context.SaveChanges();
                transaction.Complete();
                return myObj;
            }
        }

        public IEnumerable<SORTIE> Get()
        {
            return new SolarThermalEntities().SORTIE.ToList();
        }

        public void Remove(int id)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var context = new SolarThermalEntities();
                var myObj = context.SORTIE.Find(id);
                context.SORTIE.Remove(myObj);
                context.SaveChanges();
                transaction.Complete();
            }
        }
    }
}
