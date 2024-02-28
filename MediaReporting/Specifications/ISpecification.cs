using System.Linq.Expressions;

namespace MediaReporting.Specifications
{
    public interface ISpecificationBase<T> where T : class
    {
        bool IsSatisfiedBy(T entity);
        Expression<Func<T, bool>> ToExpression();
    }
}
