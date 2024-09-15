using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen.Blazor;

namespace Client.Shared.Buttons;

public class DeleteButton: RadzenButton
{
    [Parameter] public Func<Task<bool>> CanDelete { get; set; } = default!;
    [Parameter] public Func<Task> Delete { get; set; } = default!;

    public override Task OnClick(MouseEventArgs args) => Delete();
    
    protected override async Task OnParametersSetAsync()
    {
        Visible = await CanDelete();
    }
}