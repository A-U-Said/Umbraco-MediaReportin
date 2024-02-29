using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;


namespace MediaReporting.Specifications
{
    public class MediaSizeSpecification : LinqSpecificationBasee<IMedia>
    {
        private readonly int? _minimumSize;

        public MediaSizeSpecification(int? minimumSize)
        {
            _minimumSize = minimumSize;
        }

        // This CANNOT be used in IQuery expressions
        //
        // The reason that we cannot check for the value of the filesize is because it exists in the "Properties"
        // property on the IContent base interface and is not a property on the ContentDto, or indeed any DTO.
        // It is not mapped by the NPOCO ORM and therefore isn't available in IQueries.
        // It appears to be mapped through a seperate database call and map.
        // It cannot be included like in Entity Framework, no can an easy join be done using NPocoSqlExtensions
        // due to the PropertyDataDto being an internal protected class.
        // Custom queries using pure SQL can be written and parsed as seen in MediaQueries, but it would appear that the
        // Umbraco team are trying to limit access to the properties outside of internal methods at any chance possible,
        // and it is therefore better to simply filter the results of the query using a LINQ Where clause.
        // MediaQueries has been left for now in case any methods to retrieve property data using ORM is made available.
        public override Func<IMedia, bool> ToExpression()
        {
            return content =>
                    (!_minimumSize.HasValue || (
                        content.HasProperty(Constants.Conventions.Media.Bytes) &&
                        _minimumSize <= content.GetValue<int>(Constants.Conventions.Media.Bytes, null, null, false)
                    ));
        }
    }
}
