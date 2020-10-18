using System;
using DataAcess.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using IsolationLevel = System.Transactions.IsolationLevel;
using System.Data.Entity.Migrations;
using System.Data.Entity;

namespace DataAcess.Business
{
    public class ArticleDatabaseBusinessProvider
    {
        private static SolarThermalEntities Context = new SolarThermalEntities();
        /// <summary>
        /// lock object
        /// </summary>
        private static readonly object Lck = new object();

        /// <summary>
        /// instance of DataBaseBusinessProvider
        /// </summary>
        private static ArticleDatabaseBusinessProvider instance;

        /// <summary>
        /// Gets Instance.
        /// </summary>
        public static ArticleDatabaseBusinessProvider Instance

        {
            get
            {
                lock (Lck)
                {
                    return instance ?? (instance = new ArticleDatabaseBusinessProvider());
                }
            }
        }

        public IEnumerable<ARTICLE> Find(long id)
        {
            if (id > 0)
            {
                using (new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
                {
                    return Context.ARTICLE.Where(x => x.Id == id && x.isDeleted == 0).ToList();
                }
            }
            else
            {
                using (new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
                {
                    return Context.ARTICLE.Where(x => x.isDeleted == 0).ToList();
                }
            }
        }

        public ARTICLE Add(ARTICLE obj)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var myObj = Context.ARTICLE.AsNoTracking().SingleOrDefault(x => x.designation == obj.designation);
                if (myObj != null)
                {
                    Context.ARTICLE.Attach(myObj);
                    obj.Id = myObj.Id;
                    myObj = obj;
                }
                else
                {
                    myObj = Context.ARTICLE.Add(obj);
                }
                var entree = AddEntree(myObj);
                Context.SaveChanges();
                transaction.Complete();
                return obj;
            }
        }

        public ARTICLE Update(int id, ARTICLE obj)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var myObj = Context.ARTICLE.SingleOrDefault(x => x.Id == id);
                Context.ARTICLE.Attach(myObj);
                if (myObj != null)
                {
                    long oldQuantity = (long)myObj.quantite;
                    myObj = obj;
                    if (obj.quantite > 0)
                    {
                        var entree = AddEntree(myObj);
                    }
                    else
                    {
                        var sortie = AddSortie(myObj);
                    }
                    myObj.quantite = oldQuantity + obj.quantite;
                }
                Context.SaveChanges();
                transaction.Complete();
                return myObj;
            }
        }

        public void Remove(int id)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var myObj = Context.ARTICLE.Find(id);
                myObj.isDeleted = 1;
                var sortie = AddSortie(myObj);
                myObj.quantite = 0;
                Context.SaveChanges();
                transaction.Complete();
            }
        }

        public ENTREE AddEntree(ARTICLE myObj)
        {
            ENTREE entree = new ENTREE();
            entree.dateEntree = DateTime.UtcNow;
            entree.prixDentree = myObj.prix;
            entree.quantite = myObj.quantite;
            entree.numEntree = DateTime.UtcNow.Ticks;
            entree.ARTICLEId = myObj.Id;
            return Context.ENTREE.Add(entree);
        }
        public SORTIE AddSortie(ARTICLE myObj)
        {
            SORTIE sortie = new SORTIE();
            sortie.dateSortie = DateTime.UtcNow;
            sortie.prixDSortie = myObj.prix;
            sortie.quantite = myObj.quantite;
            sortie.numSortie = DateTime.UtcNow.Ticks;
            sortie.ARTICLEId = myObj.Id;
            return Context.SORTIE.Add(sortie);
        }
    }
}
