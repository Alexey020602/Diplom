using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen.Blazor;

namespace Client.Shared.Buttons;

public class DeleteButton: RadzenButton
{
    [Parameter] public RenderFragment? CanNotDeleteContent { get; set; }
    [Parameter] public RenderFragment? CanDeleteContent { get; set; }
    [Parameter] public string? CanNotDeleteText { get; set; }
    [Parameter] public string? CanDeleteText { get; set; }
    [Parameter] public bool CanDelete { get; set; }
    [Parameter] public Func<Task> Delete { get; set; } = default!;
    public override Task OnClick(MouseEventArgs args) => CanDelete ? Delete()  : Task.CompletedTask;

    protected override void OnParametersSet()
    {
        Text = CanDelete ? CanDeleteText : CanNotDeleteText;
        ChildContent = CanDelete ? CanDeleteContent : CanNotDeleteContent;
    }
}