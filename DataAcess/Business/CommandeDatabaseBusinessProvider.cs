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
    public class CommandeDatabaseBusinessProvider
    {
        /// <summary>
        /// lock object
        /// </summary>
        private static readonly object Lck = new object();

        /// <summary>
        /// instance of DataBaseBusinessProvider
        /// </summary>
        private static CommandeDatabaseBusinessProvider instance;

        /// <summary>
        /// Gets Instance.
        /// </summary>
        public static CommandeDatabaseBusinessProvider Instance

        {
            get
            {
                lock (Lck)
                {
                    return instance ?? (instance = new CommandeDatabaseBusinessProvider());
                }
            }
        }

        public IEnumerable<COMMANDE> Get()
        {
            return new SolarThermalEntities().COMMANDE.ToList();
        }

        public COMMANDE Add(COMMANDE obj)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var context = new SolarThermalEntities();
                var myObj = context.COMMANDE.Add(obj);
                context.SaveChanges();
                transaction.Complete();
                return myObj;
            }
        }

        public COMMANDE Update(int id, COMMANDE obj)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var context = new SolarThermalEntities();
                var myObj = context.COMMANDE.FirstOrDefault(x => x.Id == id);
                myObj = obj;
                // a tester sinon :  context.COMMANDE.AddOrUpdate<COMMANDE>(obj);
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
                var myObj = context.COMMANDE.Find(id);
                context.COMMANDE.Remove(myObj);
                context.SaveChanges();
                transaction.Complete();
            }
        }
    }
}
