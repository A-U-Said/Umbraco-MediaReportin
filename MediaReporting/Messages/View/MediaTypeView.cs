using Umbraco.Cms.Core.Models.Entities;
using Umbraco.Cms.Core.Models;

namespace MediaReporting.Messages.View
{
    public class MediaTypeView
    {
        public MediaTypeView()
        { 
        }

        public MediaTypeView(IMediaType mediaType)
        {
            Id = mediaType.Id;
            Alias = mediaType.Alias;
            Name = mediaType.Name;
            Description = mediaType.Description;
            Icon = mediaType.Icon;
            AllowedAsRoot = mediaType.AllowedAsRoot;
            IsContainer = mediaType.IsContainer;
            IsElement = mediaType.IsElement;
        }


        public int Id { get; set; }
        public string? Name { get; set; }
        public string Alias { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public bool AllowedAsRoot { get; set; }
        public bool IsContainer { get; set; }
        public bool IsElement { get; set; }
    }
}
