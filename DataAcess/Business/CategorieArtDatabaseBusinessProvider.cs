using System;
using DataAcess.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using IsolationLevel = System.Transactions.IsolationLevel;
using System.Data.Entity.Migrations;
using System.Data.Entity;
using System.Runtime.InteropServices;

namespace DataAcess.Business
{
    public class CategorieArtDatabaseBusinessProvider
    {
        private static SolarThermalEntities Context = new SolarThermalEntities();
        private readonly DbSet<CATEGORIE_ART> DataAccessProvider = Context.CATEGORIE_ART;
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

        public IEnumerable<CATEGORIE_ART> Find(long id, string nomCategorie)
        {
            List<ARTICLE> articles = new List<ARTICLE>();
            var query = DataAccessProvider.AsQueryable();
            if (id > 0)
            {
                articles =  Context.ARTICLE.AsQueryable().Where(x => x.CATEGORIE_ARTId == id).ToList();
                query = query.Where(x => x.Id == id);
            }
            if (!string.IsNullOrEmpty(nomCategorie))
            {
                query = query.Where(x => x.nomCate == nomCategorie);
            }
            var result = query.ToList();
            //var test = from a in query
            //           join art in Context.ARTICLE on a.Id equals art.CATEGORIE_ARTId
            //           select new CATEGORIE_ART
            //           {
            //               Id = a.Id,
            //               ARTICLE = art
            //           };
            if (articles.Count > 0)
            {
                result[0].ARTICLE = articles;
            }
            return result;
        }

        public CATEGORIE_ART Save(CATEGORIE_ART obj)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var currentList = DataAccessProvider.ToList();
                if(obj.Id>0)
                {
                    currentList.Remove(currentList.Where(x => x.Id == obj.Id).FirstOrDefault());
                }
                if (currentList.Where(x => x.nomCate.Equals(obj.nomCate)).Count() > 0)
                {
                    throw new Exception() { HelpLink = "Ce nom de catégorie existe déjà" };
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

        public void Remove(int id)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {

                var obj = DataAccessProvider.Find(id);
                if (obj.ARTICLE.Count > 0)
                {
                    throw new Exception() { HelpLink = "Cette catégorie contient des articles" };
                }
                else
                {
                    DataAccessProvider.Remove(obj);
                    Context.SaveChanges();
                }
                transaction.Complete();
            }
        }
    }
}
