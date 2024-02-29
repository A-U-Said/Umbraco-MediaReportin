using System.Linq.Expressions;

namespace MediaReporting.Specifications
{
    public interface ILinqSpecification<T> where T : class
    {
        bool IsSatisfiedBy(T entity);
        Func<T, bool> ToExpression();
    }
}
