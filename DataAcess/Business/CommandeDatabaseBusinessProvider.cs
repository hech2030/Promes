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
    public class CommandeDatabaseBusinessProvider
    {

        private static SolarThermalEntities Context = new SolarThermalEntities();
        private readonly DbSet<COMMANDE> DataAccessProvider = Context.COMMANDE;
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

        public IList<COMMANDE> Find(long id, FOURNISSEUR fournisseur, DateTime? date)
        {

            var query = DataAccessProvider.AsQueryable();
            if (id > 0)
            {
                query = query.Where(x => x.Id == id);
            }
            if (fournisseur != null)
            {
                query = query.Where(x => x.FOURNISSEURId == fournisseur.Id);
            }
            if (date != null)
            {
                query = query.Where(x => x.dateCOMMANDE == date);
            }
            return query.Include(a => a.FOURNISSEUR).ToList();
        }

        public COMMANDE Save(COMMANDE value)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                value.FOURNISSEURId = value.FOURNISSEUR.Id;
                value.FOURNISSEUR = null;
                foreach (var item in value.LIGNE_COMMANDE)
                {
                    item.ARTICLEId = item.ARTICLE.Id;
                    item.ARTICLE = null;
                }
                value.etat = "Envoyé";
                value.dateCOMMANDE = DateTime.Now;
                DataAccessProvider.AddOrUpdate(value);
                Context.SaveChanges();
                transaction.Complete();
                return value;
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
