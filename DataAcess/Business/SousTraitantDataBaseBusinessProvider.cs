using DataAcess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Transactions;

namespace DataAcess.Business
{
    public class SousTraitantDataBaseBusinessProvider
    {
        private static SolarThermalEntities Context = new SolarThermalEntities();
        private readonly DbSet<SOUS_TRAITANT> DataAccessProvider = Context.SOUS_TRAITANT;
        /// <summary>
        /// lock object
        /// </summary>
        private static readonly object Lck = new object();

        /// <summary>
        /// instance of DataBaseBusinessProvider
        /// </summary>
        private static SousTraitantDataBaseBusinessProvider instance;

        /// <summary>
        /// Gets Instance.
        /// </summary>
        public static SousTraitantDataBaseBusinessProvider Instance

        {
            get
            {
                lock (Lck)
                {
                    return instance ?? (instance = new SousTraitantDataBaseBusinessProvider());
                }
            }
        }

        public IList<SOUS_TRAITANT> Find(long id, string ville)
        {

            var query = DataAccessProvider.AsQueryable();
            if (id > 0)
            {
                query = query.Where(x => x.Id == id);
            }
            if (!string.IsNullOrEmpty(ville))
            {
                query = query.Where(x => x.ville.Contains(ville));
            }
            return query.ToList();
        }

        public SOUS_TRAITANT Save(SOUS_TRAITANT value)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var control = Find(0, string.Empty);
                if (value.Id > 0)
                {
                    var CurrenctValue = control.Where(x => x.Id == value.Id).FirstOrDefault();
                    control.Remove(CurrenctValue);
                }
                if (control.Where(x => x.nom.Equals(value.nom)).Count() == 0)
                {
                    DataAccessProvider.AddOrUpdate(value);
                    Context.SaveChanges();
                    transaction.Complete();
                    return value;
                }
                else
                {
                    throw new Exception() { HelpLink = "Le nom de Sous-traitant existe déjà" };
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
