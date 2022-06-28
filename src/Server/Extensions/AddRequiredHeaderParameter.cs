using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace ErpDashboard.Server.Extensions
{
    public class AddRequiredHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Company",
                In = ParameterLocation.Header,
                Required = false
            });
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Actor",
                In = ParameterLocation.Header,
                Required = false
            });
        }
    }
}
