using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace ToDoListBlazorClient.Shared;

public partial class TextInputField : InputBase<string>
{
    [Parameter, EditorRequired] public Expression<Func<string>> ValidationFor { get; set; } = default!;
    [Parameter] public string? Id { get; set; }
    [Parameter] public string? Label { get; set; }

    protected override bool TryParseValueFromString(string? value, out string result, out string validationErrorMessage)
    {
        result = value;
        validationErrorMessage = null;
        return true;
    }
}