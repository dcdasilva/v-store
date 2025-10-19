using System.ComponentModel.DataAnnotations;

namespace VStoreApi.Infrastructure.Endpoints.Filters;

public class ValidationFilter<T> : IEndpointFilter
{
  public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
  {
    var model = context.Arguments.OfType<T>().FirstOrDefault();
    if (model == null) return Results.BadRequest("Corpo da Requisição Inválido");

    var validationResults = new List<ValidationResult>();
    var validationContext = new ValidationContext(model);

    if (!Validator.TryValidateObject(model, validationContext, validationResults, true))
    {
      var errors = validationResults
        .GroupBy(v => v.MemberNames.FirstOrDefault() ?? "")
        .ToDictionary(g => g.Key, g => g.Select(v => v.ErrorMessage).ToArray());

      return Results.ValidationProblem(errors!);
    }

    return await next(context);
  }
}