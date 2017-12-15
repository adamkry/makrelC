using makrelC.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace makrelC.Data
{
    public class Repository
    {
        private JsonDb _db;
        public JsonDb Db => _db ?? (_db = new JsonDb(@"F:\custom\makrelC\makrelC\Data\Json"));

        private Dictionary<Type, object> data = new Dictionary<Type, object>();
        
        public IEnumerable<T> FindAll<T>(Func<T, bool> filter = null)
            where T : IEntity
        {            
            var source = GetSource<T>();
            return filter != null 
                ? source.Where(filter) 
                : source;
        }

        public T Find<T>(Func<T, bool> filter = null)
            where T : IEntity
        {
            var source = GetSource<T>();
            return filter != null 
                ? source.FirstOrDefault(filter) 
                : default(T);
        }
        
        private List<T> GetSource<T>()
            where T : IEntity
        {
            if (!data.ContainsKey(typeof(T)))
            {
                data[typeof(T)] = Db.Get<T>();
            }
            var source = data[typeof(T)] as List<T>;
            return source ?? new List<T>();
        }

        public void Insert<T>(T entity)
            where T : IEntity
        {
            entity.Id = GetNewId<T>();
            var source = data[typeof(T)] as List<T>;
            source.Add(entity);
            Db.Save(source);
        }

        public void InsertAll<T>(List<T> entities)
            where T : IEntity
        {
            int startId = GetNewId<T>();
            entities.ForEach(e => e.Id = (startId++));
            var source = data[typeof(T)] as List<T>;
            source.AddRange(entities);
            Db.Save(source);
        }

        public void UpdateAll<T>()
        {
            var source = data[typeof(T)] as List<T>;
            Db.Save(source);
        }

        protected int GetNewId<T>()
            where T : IEntity
        {
            var all = FindAll<T>().ToList();
            return all.Count > 0 ? all.Max(e => e.Id) + 1 : 1;
        }
    }
}
