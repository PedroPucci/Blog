using System;

namespace Blog.Shared.Logging
{
    public static class LogMessages
    {
        //User
        public static string InvalidEmail() => "Message: This email exist.";
        public static string InvalidUserInputs() => "Message: Invalid inputs to User.";
        public static string NullOrEmptyUserEmail() => "Message: The Email field is null, empty, or whitespace.";
        public static string UpdatingErrorUser(Exception ex) => $"Message: Error updating User: {ex.Message}";
        public static string UpdatingSuccessUser() => "Message: Successfully updated User.";
        public static string AddingUserError(Exception ex) => $"Message: Error adding a new User: {ex.Message}";
        public static string AddingUserSuccess() => "Message: Successfully added a new User.";

        //Publication
        public static string InvalidPublicationInputs() => "Message: Invalid inputs to Publication.";
        public static string AddingPublicationError(Exception ex) => $"Message: Error adding a new Publication: {ex.Message}";
        public static string AddingPublicationSuccess() => "Message: Successfully added a new Publication.";
        public static string UpdatingErrorPublication(Exception ex) => $"Message: Error updating Publication: {ex.Message}";
        public static string UpdatingSuccessPublication() => "Message: Successfully updated Publication.";
        public static string GetPublicationSuccess() => "Message: Get with success Publication.";
        public static string GetPublicationError(Exception ex) => "Message: Get with error Publication.";
        public static string DeletePublicationError(Exception ex) => $"Message: Error to delete a Publication: {ex.Message}";
        public static string DeletePublicationSuccess() => "Message: Delete with success Publication.";
    }
}