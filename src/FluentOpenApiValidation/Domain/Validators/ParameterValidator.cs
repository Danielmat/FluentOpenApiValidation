using System.Text.Json;

namespace FluentOpenApiValidation
{
    internal sealed class ParameterValidator : BaseValidator
    {
        protected override void ValidateInternal(ValidationContext context)
        {
            var parameters = context.Document.Parameters;
            if (parameters is not { Count: > 0 }) return;

            var methodElement = GetMethodElement(context);
            if (methodElement is null) return;

            if (parameters is not null)
                ValidateParameters(context, parameters, methodElement.Value);
        }

        private static JsonElement? GetMethodElement(ValidationContext context) =>
            context.Document.Element
                .TryGetPath(
                    $"{JsonPaths.Paths}.{context.Document.Path}.{context.Document.Method.Method.ToLower()}",
                    out var element)
                ? element
                : null;

        private static void ValidateParameters(ValidationContext context, IEnumerable<Parameter> parameters, JsonElement methodElement)
        {
            JsonElement? parametersElement = methodElement.TryGetPath(JsonPaths.Parameters, out var element) ? element : null;

            if (parametersElement is null)
            {
                context.AddError("INVALID_PARAMETERS", "Parameters not found");
                return;
            }

            foreach (var parameter in parameters)
            {
                if (!ValidateParameter(parametersElement, parameter, parameter.Type))
                {
                    context.AddError(
                        "INVALID_PARAMETER",
                        $"Parameter or type '{parameter.Name}' not found");
                }
            }
        }

        private static bool ValidateParameter(JsonElement? element, Parameter parameter, PropertyType type) =>
            element?.EnumerateArray()
                .Any(p => p.TryGetPath(JsonPaths.Name, out var nameElement) && nameElement.GetString().Equals(parameter.Name, StringComparison.InvariantCultureIgnoreCase) &&
                          p.TryGetPath(JsonPaths.Required, out var required) && required.GetBoolean() == parameter.Required &&
                          p.TryGetPath(JsonPaths.In, out var inParam) && inParam.GetString().Equals(parameter.InParameter.ToDescriptionString(), StringComparison.InvariantCultureIgnoreCase) &&
                          p.TryGetPath(JsonPaths.Schema, out var schema) &&
                          p.TryGetPath($"{JsonPaths.Schema}.{JsonPaths.Type}", out var parameterType) &&
                            parameterType.GetString() == type.ToDescriptionString()
                          )
                 ?? false;
    }
}