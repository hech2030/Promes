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
    public class ReceptionDatabaseBusinessProvider
    {
        /// <summary>
        /// lock object
        /// </summary>
        private static readonly object Lck = new object();

        /// <summary>
        /// instance of DataBaseBusinessProvider
        /// </summary>
        private static ReceptionDatabaseBusinessProvider instance;

        /// <summary>
        /// Gets Instance.
        /// </summary>
        public static ReceptionDatabaseBusinessProvider Instance

        {
            get
            {
                lock (Lck)
                {
                    return instance ?? (instance = new ReceptionDatabaseBusinessProvider());
                }
            }
        }

        public IEnumerable<RECEPTION> Find(long id)
        {
            if (id > 0)
            {
                using (new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
                {
                    return new SolarThermalEntities().RECEPTION.Where(x => x.Id == id).ToList();
                }
            }
            else
            {
                using (new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
                {
                    return new SolarThermalEntities().RECEPTION.ToList();
                }
            }
        }

        public IEnumerable<RECEPTION> Get()
        {
            return new SolarThermalEntities().RECEPTION.ToList();
        }

        public RECEPTION Add(RECEPTION obj)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var context = new SolarThermalEntities();
                var myObj = context.RECEPTION.Add(obj);
                context.SaveChanges();
                transaction.Complete();
                return myObj;
            }
        }

        public RECEPTION Update(int id, RECEPTION obj)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var context = new SolarThermalEntities();
                var myObj = context.RECEPTION.FirstOrDefault(x => x.Id == id);
                myObj = obj;
                // a tester sinon :  context.RECEPTION.AddOrUpdate<RECEPTION>(obj);
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
                var myObj = context.RECEPTION.Find(id);
                context.RECEPTION.Remove(myObj);
                context.SaveChanges();
                transaction.Complete();
            }
        }
    }
}
