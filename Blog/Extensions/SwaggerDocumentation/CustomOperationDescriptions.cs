using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Blog.Exrensions.SwaggerDocumentation
{
    public class CustomOperationDescriptions : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context?.ApiDescription?.HttpMethod is null || context.ApiDescription.RelativePath is null)
                return;

            var routeHandlers = new Dictionary<string, Action>
                {
                    { "user", () => HandleUserOperations(operation, context) },
                    { "publication", () => HandlePublicationsOperations(operation, context) }
                };

            foreach (var routeHandler in routeHandlers)
            {
                if (context.ApiDescription.RelativePath.Contains(routeHandler.Key))
                {
                    routeHandler.Value.Invoke();
                    break;
                }
            }
        }

        private void HandleUserOperations(OpenApiOperation operation, OperationFilterContext context)
        {
            var method = context.ApiDescription.HttpMethod;
            var path = context.ApiDescription.RelativePath?.ToLower() ?? string.Empty;

            if (method == "POST")
            {
                operation.Summary = "Create a new User";
                operation.Description = "This endpoint allows you to create a new User by providing the necessary details.";
                AddResponses(operation, "200", "The User was successfully created.");
            }
            else if (method == "PUT")
            {
                operation.Summary = "Update an existing User";
                operation.Description = "This endpoint allows you to update an existing User by providing the necessary details.";
                AddResponses(operation, "200", "The User was successfully updated.");
            }
            else if (method == "DELETE")
            {
                operation.Summary = "Delete an existing User";
                operation.Description = "This endpoint allows you to delete an existing User by providing the ID.";
                AddResponses(operation, "200", "The User was successfully deleted.");
                AddResponses(operation, "404", "User not found. Please verify the ID.");
            }
            else if (method == "GET")
            {
                operation.Summary = "Retrieve all Users";
                operation.Description = "This endpoint allows you to retrieve details of all existing Users.";
                AddResponses(operation, "200", "All User details were successfully retrieved.");
            }
        }

        private void HandlePublicationsOperations(OpenApiOperation operation, OperationFilterContext context)
        {
            var method = context.ApiDescription.HttpMethod;
            var path = context.ApiDescription.RelativePath?.ToLower() ?? string.Empty;

            if (method == "POST")
            {
                operation.Summary = "Create a new Publication";
                operation.Description = "This endpoint allows you to create a new Publication by providing the necessary details.";
                AddResponses(operation, "200", "The Publication was successfully created.");
            }
            else if (method == "PUT")
            {
                operation.Summary = "Update an existing Publication";
                operation.Description = "This endpoint allows you to update an existing Publication by providing the necessary details.";
                AddResponses(operation, "200", "The Publication was successfully updated.");
            }
            else if (method == "DELETE")
            {
                operation.Summary = "Delete an existing Publication";
                operation.Description = "This endpoint allows you to delete an existing Publication by providing the ID.";
                AddResponses(operation, "200", "The Publication was successfully deleted.");
                AddResponses(operation, "404", "Publication not found. Please verify the ID.");
            }
            else if (method == "GET")
            {
                operation.Summary = "Retrieve all Publications";
                operation.Description = "This endpoint allows you to retrieve details of all existing Publications.";
                AddResponses(operation, "200", "All Publication details were successfully retrieved.");
            }
        }

        private void AddResponses(OpenApiOperation operation, string statusCode, string description)
        {
            if (!operation.Responses.ContainsKey(statusCode))
            {
                operation.Responses.Add(statusCode, new OpenApiResponse { Description = description });
            }
        }
    }
}