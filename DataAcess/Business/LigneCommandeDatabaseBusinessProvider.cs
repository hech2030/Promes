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
    public class LigneCommandeDatabaseBusinessProvider
    {
        /// <summary>
        /// lock object
        /// </summary>
        private static readonly object Lck = new object();

        /// <summary>
        /// instance of DataBaseBusinessProvider
        /// </summary>
        private static LigneCommandeDatabaseBusinessProvider instance;

        /// <summary>
        /// Gets Instance.
        /// </summary>
        public static LigneCommandeDatabaseBusinessProvider Instance

        {
            get
            {
                lock (Lck)
                {
                    return instance ?? (instance = new LigneCommandeDatabaseBusinessProvider());
                }
            }
        }

        public IEnumerable<LIGNE_COMMANDE> Find(long id)
        {
            if (id > 0)
            {
                using (new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
                {
                    return new SolarThermalEntities().LIGNE_COMMANDE.Where(x => x.Id == id).ToList();
                }
            }
            else
            {
                using (new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
                {
                    return new SolarThermalEntities().LIGNE_COMMANDE.ToList();
                }
            }
        }

        public LIGNE_COMMANDE Add(LIGNE_COMMANDE obj)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var context = new SolarThermalEntities();
                var myObj = context.LIGNE_COMMANDE.Add(obj);
                context.SaveChanges();
                transaction.Complete();
                return myObj;
            }
        }

        public LIGNE_COMMANDE Update(int id, LIGNE_COMMANDE obj)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var context = new SolarThermalEntities();
                var myObj = context.LIGNE_COMMANDE.FirstOrDefault(x => x.Id == id);
                myObj = obj;
                // a tester sinon :  context.LIGNE_COMMANDE.AddOrUpdate<LIGNE_COMMANDE>(obj);
                context.SaveChanges();
                transaction.Complete();
                return myObj;
            }
        }

        public IEnumerable<LIGNE_COMMANDE> Get()
        {
            return new SolarThermalEntities().LIGNE_COMMANDE.ToList();
        }

        public void Remove(int id)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var context = new SolarThermalEntities();
                var myObj = context.LIGNE_COMMANDE.Find(id);
                context.LIGNE_COMMANDE.Remove(myObj);
                context.SaveChanges();
                transaction.Complete();
            }
        }
    }
}
