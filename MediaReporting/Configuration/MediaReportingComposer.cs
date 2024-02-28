using MediaReporting.Helpers;
using Umbraco.Cms.Core.Composing;

namespace MediaReporting.Configuration
{
    public class MediaReportingComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddSingleton<IMediaReportHelper, MediaReportHelper>();
        }
    }
}
