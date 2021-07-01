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
        private readonly DbSet<ARTICLE> DataAccessProvider = Context.ARTICLE;

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

        public IList<ARTICLE> Find(long id, long magasinId, string designation)
        {

            var query = DataAccessProvider.AsQueryable();
            if (id > 0)
            {
                query = query.Where(x => x.Id == id);
            }
            if (!string.IsNullOrEmpty(designation))
            {
                query = query.Where(x => x.designation.Contains(designation));
            }
            if (magasinId > 0)
            {
                query = query.Where(x => x.MAGASINId == magasinId);
            }
            return query.Include(a => a.MAGASIN).Include(a => a.CATEGORIE_ART).ToList();
        }

        public ARTICLE Save(ARTICLE value)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var control = Find(0, 0, string.Empty);
                if (value.Id > 0)
                {
                    var CurrenctValue = control.Where(x => x.Id == value.Id).FirstOrDefault();
                    control.Remove(CurrenctValue);
                }
                if (control.Where(x => x.designation.Equals(value.designation)).Count() == 0)
                {
                    DataAccessProvider.AddOrUpdate(value);
                    Context.SaveChanges();
                    transaction.Complete();
                    return value;
                }
                else
                {
                    throw new Exception() { HelpLink = "Le désignation de l'article existe déjà" };
                }
            }
        }


        public bool Remove(long id)
        {
            try
            {
                using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
                {
                    var value = DataAccessProvider.Find(id);
                    DataAccessProvider.Remove(value);
                    Context.SaveChanges();
                    transaction.Complete();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

    }
}
