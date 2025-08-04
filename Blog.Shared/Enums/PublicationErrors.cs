using System.ComponentModel;

namespace Blog.Shared.Enums
{
    public enum PublicationErrors
    {
        [Description("'Title' can not be null or empty!")]
        Publication_Error_PublicationCanNotBeNullOrEmpty,

        [Description("'Title' must be at least 3 characters long!")]
        Publication_Error_PublicationTooShort,

        [Description("'Title' must be at most 100 characters long!")]
        Publication_Error_PublicationTooLong,

        [Description("'Title' contains invalid characters! Only letters and spaces are allowed.")]
        Publication_Error_PublicationInvalidCharacters
    }
}