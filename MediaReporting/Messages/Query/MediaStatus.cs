using System.ComponentModel;

namespace MediaReporting.Messages.Query
{
    public enum MediaStatus : byte
    {
        [Description("Active")]
        Active = 0,

        [Description("Deleted")]
        Deleted = 1
    }
}
