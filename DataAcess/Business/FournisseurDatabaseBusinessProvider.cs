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
    public class FournisseurDatabaseBusinessProvider
    {
        private static SolarThermalEntities Context = new SolarThermalEntities();
        private readonly DbSet<FOURNISSEUR> DataAccessProvider = Context.FOURNISSEUR;
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

        public IEnumerable<FOURNISSEUR> Find(long id, string nomFournissuer)
        {
            var query = DataAccessProvider.AsQueryable();
            if (id > 0)
            {
                query = query.Where(x => x.Id == id);
            }
            if (!string.IsNullOrEmpty(nomFournissuer))
            {
                query = query.Where(x => x.NomF == nomFournissuer);
            }
            return query.ToList();
        }

        public FOURNISSEUR Save(FOURNISSEUR obj)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var currentList = DataAccessProvider.ToList();
                if (obj.Id > 0)
                {
                    currentList.Remove(currentList.Where(x => x.Id == obj.Id).FirstOrDefault());
                }
                if (currentList.Where(x => x.NomF.Equals(obj.NomF)).Count() > 0)
                {
                    throw new Exception() { HelpLink = "Ce nom de fournisseur existe déjà" };
                }
                else
                {
                    DataAccessProvider.AddOrUpdate(obj);
                    Context.SaveChanges();
                    transaction.Complete();
                    return obj;
                }
            }
        }

        public void Remove(long id)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var obj = DataAccessProvider.Find(id);
                DataAccessProvider.Remove(obj);
                Context.SaveChanges();
                transaction.Complete();
            }
        }
    }
}
