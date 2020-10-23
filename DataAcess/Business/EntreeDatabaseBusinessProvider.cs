using System;
using DataAcess.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using IsolationLevel = System.Transactions.IsolationLevel;
using System.Data.Entity.Migrations;
using System.Runtime.Remoting.Contexts;

namespace DataAcess.Business
{
    public class EntreeDatabaseBusinessProvider
    {
        /// <summary>
        /// lock object
        /// </summary>
        private static readonly object Lck = new object();

        /// <summary>
        /// instance of DataBaseBusinessProvider
        /// </summary>
        private static EntreeDatabaseBusinessProvider instance;

        /// <summary>
        /// Gets Instance.
        /// </summary>
        public static EntreeDatabaseBusinessProvider Instance

        {
            get
            {
                lock (Lck)
                {
                    return instance ?? (instance = new EntreeDatabaseBusinessProvider());
                }
            }
        }

        public IEnumerable<ENTREE> Get()
        {
            return new SolarThermalEntities().ENTREE.ToList();
        }

        public IEnumerable<ENTREE> Find(long id)
        {
            if (id > 0)
            {
                using (new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
                {
                    return new SolarThermalEntities().ENTREE.Where(x => x.Id == id).ToList();
                }
            }
            else
            {
                using (new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
                {
                    return new SolarThermalEntities().ENTREE.ToList();
                }
            }
        }

        public ENTREE Add(ENTREE obj)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var context = new SolarThermalEntities();
                var art = obj.ARTICLE;
                art.quantite += obj.quantite;
                art.prix = (art.prix + obj.prixDentree) / 2;
                ArticleDatabaseBusinessProvider.Instance.Save(art);
                obj.ARTICLE = null;
                obj.dateEntree = DateTime.Now;
                var myObj = context.ENTREE.Add(obj);
                context.SaveChanges();
                transaction.Complete();
                return myObj;
            }
        }

        public ENTREE Update(int id, ENTREE obj)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var context = new SolarThermalEntities();
                var myObj = context.ENTREE.FirstOrDefault(x => x.Id == id);
                myObj = obj;
                // a tester sinon :  context.ENTREE.AddOrUpdate<CATEGORIE_ART>(obj);
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
                var myObj = context.ENTREE.Find(id);
                context.ENTREE.Remove(myObj);
                context.SaveChanges();
                transaction.Complete();
            }
        }
    }
}
