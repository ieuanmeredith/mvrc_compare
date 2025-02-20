using Microsoft.AspNetCore.Components;
using MVRC_Compare.Shared.Models;
using MVRC_Compare.Shared.Services;

namespace MVRC_Compare.Shared.Pages;

public partial class Counter : ComponentBase
{
    private int currentCount = 0;

    private ICaseProviderService caseProviderService;

    public IList<MvrcCase> cases = [];

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
        var mvrcCase = await caseProviderService.LoadCaseAsync();

        if(mvrcCase is not null)
        {
            cases.Add(mvrcCase);
            // StateHasChanged();
        }
    }
}
