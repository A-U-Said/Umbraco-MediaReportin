using System.Linq.Expressions;

namespace MediaReporting.Specifications
{
    public abstract class SpecificationBase<T> : ISpecificationBase<T> where T : class
    {
        public bool IsSatisfiedBy(T entity)
        {
            var predicate = ToExpression().Compile();
            return predicate(entity);
        }

        public abstract Expression<Func<T, bool>> ToExpression();
    }
}
