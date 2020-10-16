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
    public class FournisseurDatabaseBusinessProvider
    {
        /// <summary>
        /// lock object
        /// </summary>
        private static readonly object Lck = new object();

        /// <summary>
        /// instance of DataBaseBusinessProvider
        /// </summary>
        private static FournisseurDatabaseBusinessProvider instance;

        /// <summary>
        /// Gets Instance.
        /// </summary>
        public static FournisseurDatabaseBusinessProvider Instance

        {
            get
            {
                lock (Lck)
                {
                    return instance ?? (instance = new FournisseurDatabaseBusinessProvider());
                }
            }
        }

        public IEnumerable<FOURNISSEUR> Find(long id)
        {
            if (id > 0)
            {
                using (new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
                {
                    return new SolarThermalEntities().FOURNISSEUR.Where(x => x.Id == id).ToList();
                }
            }
            else
            {
                using (new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
                {
                    return new SolarThermalEntities().FOURNISSEUR.ToList();
                }
            }
        }

        public FOURNISSEUR Add(FOURNISSEUR obj)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var context = new SolarThermalEntities();
                var myObj = context.FOURNISSEUR.Add(obj);
                context.SaveChanges();
                transaction.Complete();
                return myObj;
            }
        }

        public FOURNISSEUR Update(int id, FOURNISSEUR obj)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var context = new SolarThermalEntities();
                var myObj = context.FOURNISSEUR.FirstOrDefault(x => x.Id == id);
                myObj = obj;
                // a tester sinon :  context.FOURNISSEUR.AddOrUpdate<CATEGORIE_ART>(obj);
                context.SaveChanges();
                transaction.Complete();
                return myObj;
            }
        }

        public IEnumerable<FOURNISSEUR> Get()
        {
            return new SolarThermalEntities().FOURNISSEUR.ToList();
        }

        public void Remove(int id)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var context = new SolarThermalEntities();
                var myObj = context.FOURNISSEUR.Find(id);
                context.FOURNISSEUR.Remove(myObj);
                context.SaveChanges();
                transaction.Complete();
            }
        }
    }
}
