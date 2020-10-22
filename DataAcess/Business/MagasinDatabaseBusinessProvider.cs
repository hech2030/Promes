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
    public class MagasinDatabaseBusinessProvider
    {
        private static SolarThermalEntities Context = new SolarThermalEntities();
        private readonly DbSet<MAGASIN> DataAccessProvider = Context.MAGASIN;
        /// <summary>
        /// lock object
        /// </summary>
        private static readonly object Lck = new object();

        /// <summary>
        /// instance of DataBaseBusinessProvider
        /// </summary>
        private static MagasinDatabaseBusinessProvider instance;

        /// <summary>
        /// Gets Instance.
        /// </summary>
        public static MagasinDatabaseBusinessProvider Instance
        {
            get
            {
                lock (Lck)
                {
                    return instance ?? (instance = new MagasinDatabaseBusinessProvider());
                }
            }
        }

        public IList<MAGASIN> Find(long id, string nomMagasin)
        {

            var query = DataAccessProvider.AsQueryable();
            if (id > 0)
            {
                query = query.Where(x => x.Id == id);
            }
            if (!string.IsNullOrEmpty(nomMagasin))
            {
                query = query.Where(x => x.nomMagasin.Contains(nomMagasin));
            }
            return query.ToList();
        }

        public MAGASIN Save(MAGASIN value)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var control = Find(0, string.Empty);
                if (value.Id > 0)
                {
                    var CurrenctValue = control.Where(x => x.Id == value.Id).FirstOrDefault();
                    control.Remove(CurrenctValue);
                }
                if (control.Where(x => x.nomMagasin.Equals(value.nomMagasin)).Count() == 0)
                {
                    DataAccessProvider.AddOrUpdate(value);
                    Context.SaveChanges();
                    transaction.Complete();
                    return value;
                }
                else
                {
                    throw new Exception() { HelpLink = "Le nom du magasin existe déjà" };
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
