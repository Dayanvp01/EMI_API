using FluentValidation;

namespace EMI_API.Utils
{
    public class ValidationFilters <T> : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();

            if (validator is null)
            {
                return await next(context);
            }
            var inputToValidate = context.Arguments.OfType<T>().FirstOrDefault();

            if (inputToValidate is null)
            {
                return TypedResults.Problem("No pudo ser encontrada la entidad a validar");
            }

            var validationResult = await validator.ValidateAsync(inputToValidate);

            if (!validationResult.IsValid)
            {
                return TypedResults.ValidationProblem(validationResult.ToDictionary());
            }

            return await next(context);
        }

    }
}
