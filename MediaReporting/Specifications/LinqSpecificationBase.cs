using System.Linq.Expressions;

namespace MediaReporting.Specifications
{
    public abstract class LinqSpecificationBasee<T> : ILinqSpecification<T> where T : class
    {
        public bool IsSatisfiedBy(T entity)
        {
            var predicate = ToExpression();
            return predicate(entity);
        }

        public abstract Func<T, bool> ToExpression();
    }
}
