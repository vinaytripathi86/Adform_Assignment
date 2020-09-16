using System.Collections.Generic;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Homework_Adform.TodoAPI.Filters
{
    /// <summary>
    /// Add required header parameters.
    /// </summary>
    public class AddRequiredHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Custom-Correlation-Id",
                In = ParameterLocation.Header,
                Description = "correlation id",
                Required = false
            });
        }
    }
}
