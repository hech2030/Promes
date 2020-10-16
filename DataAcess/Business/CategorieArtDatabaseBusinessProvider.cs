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
    public class CategorieArtDatabaseBusinessProvider
    {
        /// <summary>
        /// lock object
        /// </summary>
        private static readonly object Lck = new object();

        /// <summary>
        /// instance of DataBaseBusinessProvider
        /// </summary>
        private static CategorieArtDatabaseBusinessProvider instance;

        /// <summary>
        /// Gets Instance.
        /// </summary>
        public static CategorieArtDatabaseBusinessProvider Instance

        {
            get
            {
                lock (Lck)
                {
                    return instance ?? (instance = new CategorieArtDatabaseBusinessProvider());
                }
            }
        }

        public IEnumerable<CATEGORIE_ART> Find(long id)
        {
            if (id > 0)
            {
                using (new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
                {
                    return new SolarThermalEntities().CATEGORIE_ART.Where(x => x.Id == id).ToList();
                }
            }
            else
            {
                using (new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
                {
                    return new SolarThermalEntities().CATEGORIE_ART.ToList();
                }
            }
        }

        public CATEGORIE_ART Add(CATEGORIE_ART obj)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var context = new SolarThermalEntities();
                var myObj = context.CATEGORIE_ART.Add(obj);
                context.SaveChanges();
                transaction.Complete();
                return myObj;
            }
        }

        public CATEGORIE_ART Update(int id, CATEGORIE_ART obj)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var context = new SolarThermalEntities();
                var myObj = context.CATEGORIE_ART.FirstOrDefault(x => x.Id == id);
                myObj = obj;
                // a tester sinon :  context.CATEGORIE_ART.AddOrUpdate<CATEGORIE_ART>(obj);
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
                var myObj = context.CATEGORIE_ART.Find(id);
                context.CATEGORIE_ART.Remove(myObj);
                context.SaveChanges();
                transaction.Complete();
            }
        }
    }
}
