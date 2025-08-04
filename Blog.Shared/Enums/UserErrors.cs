using System.ComponentModel;

namespace Blog.Shared.Enums
{
    public enum UserErrors
    {
        [Description("'Email' can not be null or empty!")]
        User_Error_EmailCanNotBeNullOrEmpty,

        [Description("'Email' invalid format!")]
        User_Error_InvalidEmailFormat,

        [Description("'Email' can not be less 4 letters!")]
        User_Error_EmailLenghtLessFour,

        [Description("'Name' can not be null or empty!")]
        User_Error_NameCanNotBeNullOrEmpty,

        [Description("'Name' must be at least 3 characters long!")]
        User_Error_NameTooShort,

        [Description("'Name' must be at most 100 characters long!")]
        User_Error_NameTooLong,

        [Description("'Name' contains invalid characters! Only letters and spaces are allowed.")]
        User_Error_NameInvalidCharacters
    }
}