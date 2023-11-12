using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace ToDoListBlazorClient.Shared;

public partial class TextInputField : InputBase<string>
{
    [Parameter] public required Expression<Func<string>> ValidationFor { get; init; }
    [Parameter] public string? Id { get; init; }
    [Parameter] public string? Label { get; init; }

    [Parameter] public string Type { get; init; } = "text";

    protected override bool TryParseValueFromString(string? value, out string result, out string validationErrorMessage)
    {
        result = value ?? string.Empty;
        validationErrorMessage = string.Empty;
        return true;
    }

    private string ValidationClass()
    {
        var fieldIdentifier = FieldIdentifier.Create(ValidationFor);
        return EditContext.GetValidationMessages(fieldIdentifier).Any() ? "is-invalid" : string.Empty;
    }
}