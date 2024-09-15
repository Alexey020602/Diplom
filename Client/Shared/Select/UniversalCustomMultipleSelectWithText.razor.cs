using Client.Services.Api.BaseApi;
using Microsoft.AspNetCore.Components;
using Model;

namespace Client.Shared.Select;

public class UniversalCustomMultipleSelectWithText<TItem, TApi> : ComponentBase
    where TItem : class, ISelectionWithTextElement
    where TApi : class, IReadApi<TItem>
{
}