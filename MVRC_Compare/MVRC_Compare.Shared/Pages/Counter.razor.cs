using Microsoft.AspNetCore.Components;
using MVRC_Compare.Shared.Services;

namespace MVRC_Compare.Shared.Pages;

public partial class Counter : ComponentBase
{
    private int currentCount = 0;

    private ICaseProviderService caseProviderService;

    public Counter(ICaseProviderService caseProviderService)
    {
        this.caseProviderService = caseProviderService;
    }

    private void IncrementCount()
    {
        currentCount++;
    }

    private async Task AddCase()
    {
        await caseProviderService.LoadCaseAsync();
    }
}
