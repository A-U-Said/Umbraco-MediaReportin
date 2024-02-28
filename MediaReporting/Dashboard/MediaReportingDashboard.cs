using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Dashboards;

namespace MediaReporting.Dashboard
{
    [Weight(10)]
    public class MediaReportingDashboard : IDashboard
    {
        public string Alias => "mediaReportingDashboard";

        public string[] Sections => new[]
        {
            Constants.Applications.Media,
        };

        public string View => "/App_Plugins/MediaReporting/MediaReporting.html";

        public IAccessRule[] AccessRules => new IAccessRule[]
        {
            new AccessRule { Type = AccessRuleType.Grant, Value = Constants.Security.AdminGroupAlias }
        };

    }
}