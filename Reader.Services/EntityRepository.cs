namespace Reader.Services
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using MongoDB.Driver;
    using MongoDB.Driver.Linq;
    using Reader.Data;

    
    internal sealed class EntityRepository<T> : IQueryable<T>
        where T: EntityBase, new()
    {
        #region Fields

        private readonly string Name = string.Format("{0}s", typeof(T).Name);

        #endregion

        #region Properties

        private MongoCollection<T> Collection
        {
            get
            {
                return Configuration.Database.GetCollection<T>(this.Name, WriteConcern.Acknowledged);
            }
        }

        #endregion

        #region Constructor(s)

        public EntityRepository()
        {
            this.Collection.EnsureIndex((new T() as EntityBase).index_keys);
        }

        #endregion

        #region IQueryable<T> Implementation

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return this.Collection.AsQueryable<T>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Collection.AsQueryable().GetEnumerator();
        }

        Type IQueryable.ElementType
        {
            get { return this.Collection.AsQueryable<T>().ElementType; }
        }

        Expression IQueryable.Expression
        {
            get { return this.Collection.AsQueryable<T>().Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return this.Collection.AsQueryable<T>().Provider; }
        }

        #endregion

        internal T GetById(string id)
        {
            return this.SingleOrDefault<T>(acct => acct.id == id);
        }

        internal bool Insert(T entity)
        {
            try
            {
                return this.Collection.Insert<T>(entity).Ok;
            }
            catch
            {
                return false;
            }
        }

        internal bool Save(T entity)
        {
            try
            {
                return this.Collection.Save<T>(entity).Ok;
            }
            catch
            {
                return false;
            }
        }
    }
}