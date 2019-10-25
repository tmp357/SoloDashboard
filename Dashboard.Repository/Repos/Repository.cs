using Dashboard.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Dashboard.Repository.Repos
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Expression<Func<T, bool>> GetDynamicQueryWithExpresionTrees(string propertyName, string val)
        {
            //x =>
            var param = Expression.Parameter(typeof(T), "x");
            //val ("Curry")
            var valExpression = Expression.Constant(val, typeof(string));
            //Field or Property Name
            var column = Expression.PropertyOrField(param, propertyName);
            //x.LastName == "Curry"
            BinaryExpression body = Expression.Equal(column, valExpression);
            //x => x.LastName == "Curry"
            var final = Expression.Lambda<Func<T, bool>>(body, param);
            //compiles the expression tree to a func delegate
            return final;
        }
    }
}
