using Microsoft.AspNetCore.Components;
using MVRC_Compare.Shared.Services;
using Toolbelt.Blazor.HotKeys2;

namespace MVRC_Compare.Shared.Pages;

public partial class Home : ComponentBase, IAsyncDisposable
{
    private readonly HotKeys _hotKeys;
    private HotKeysContext? _hotKeysContext;

    public readonly ICaseProviderService caseProviderService;
    public readonly ICompareState compareState;

    public Home(
        ICaseProviderService caseProviderService,
        ICompareState compareState,
        HotKeys hotKeys)
    {
        this.caseProviderService = caseProviderService;
        this.compareState = compareState;
        _hotKeys = hotKeys;
    }

    public async ValueTask DisposeAsync() 
    {
        if (_hotKeysContext != null)
        {
            await _hotKeysContext.DisposeAsync();
        }
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            _hotKeysContext = _hotKeys.CreateContext()
              .Add(Code.ArrowRight, NextImage)
              .Add(Code.ArrowLeft, PreviousImage)
              .Add(Code.ArrowDown, NextCase)
              .Add(Code.ArrowUp, PreviousCase);
        }
    }

    private async Task AddCase()
    {
        var mvrcCase = await caseProviderService.LoadCaseAsync();

        if (mvrcCase is not null)
        {
            compareState.AddCase(mvrcCase);
        }
    }

    private string GetImage()
    {
        var selectedCase = compareState.GetSelectedCase();
        if (string.IsNullOrEmpty(selectedCase))
        {
            return string.Empty;
        }

        var mvrcCase = compareState.GetCases().First(x => x.GetCaseName() == selectedCase);

        var selectedImageSet = compareState.GetSelectedImageSet();
        if (string.IsNullOrEmpty(selectedImageSet))
        {
            var defaultImageSet = mvrcCase.GetImageSets().First();
            compareState.SetSelectedImageSet(defaultImageSet);
            compareState.SetSelectedImageSetIndex(defaultImageSet, 0);
            return GetBase64ImageFromPath(mvrcCase.Images.First(x => x.Contains(defaultImageSet)));
        }

        var selectedImageSetIndex = compareState.GetSelectedImageSetIndex(selectedImageSet);
        if (selectedImageSetIndex == -1)
        {
            compareState.SetSelectedImageSetIndex(selectedImageSet, 0);
            return GetBase64ImageFromPath(mvrcCase.Images.First(x => x.Contains(selectedImageSet)));
        }

        return GetBase64ImageFromPath(mvrcCase.Images.Where(x => x.Contains(selectedImageSet)).ToArray()[selectedImageSetIndex]);
    }

    private string GetImageSetThumbnail(string imageSet)
    {
        return GetBase64ImageFromPath(compareState.GetCases().First().Images.First(x => x.Contains(imageSet)));
    }

    private string GetBase64ImageFromPath(string path)
    {
        byte[] imageArray = File.ReadAllBytes(path);
        string base64ImageRepresentation = Convert.ToBase64String(imageArray);

        return base64ImageRepresentation;
    }

    private void SelectImageSet(string imageSet)
    {
        compareState.SetSelectedImageSet(imageSet);
        StateHasChanged();
    }

    private void NextImage()
    {
        var currentImageSet = compareState.GetSelectedImageSet();
        var currentIndex = compareState.GetSelectedImageSetIndex(currentImageSet);

        compareState.SetSelectedImageSetIndex(currentImageSet, currentIndex + 1);
    }

    private void PreviousImage()
    {
        var currentImageSet = compareState.GetSelectedImageSet();
        var currentIndex = compareState.GetSelectedImageSetIndex(currentImageSet);

        compareState.SetSelectedImageSetIndex(currentImageSet, currentIndex - 1);
    }

    private void NextCase()
    {
        var cases = compareState.GetCases();
        var currentCase = cases.First(x => x.GetCaseName() == compareState.GetSelectedCase());

        var nextCaseIndex = cases.IndexOf(currentCase) + 1;

        if(cases.Count <= nextCaseIndex)
        {
            compareState.SetSelectedCase(cases[0].GetCaseName());
            return;
        }

        compareState.SetSelectedCase(cases[nextCaseIndex].GetCaseName());
        return;
    }

    private void PreviousCase()
    {
        var cases = compareState.GetCases();
        var currentCase = cases.First(x => x.GetCaseName() == compareState.GetSelectedCase());

        var previousCaseIndex = cases.IndexOf(currentCase) - 1;

        if (previousCaseIndex < 0)
        {
            compareState.SetSelectedCase(cases[cases.Count -1].GetCaseName());
            return;
        }

        compareState.SetSelectedCase(cases[previousCaseIndex].GetCaseName());
        return;
    }
}
