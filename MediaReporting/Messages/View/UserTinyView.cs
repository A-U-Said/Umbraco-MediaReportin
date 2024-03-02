using Umbraco.Cms.Core.Models.Membership;


namespace MediaReporting.Messages.View
{
    public class UserTinyView
    {
        public UserTinyView()
        {
        }

        public object? Id { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string[]? Avatars { get; set; }
        public UserState UserState { get; set; }
    }
}
