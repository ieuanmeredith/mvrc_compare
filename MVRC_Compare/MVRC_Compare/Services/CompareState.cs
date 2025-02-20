using MVRC_Compare.Shared.Models;
using MVRC_Compare.Shared.Services;

namespace MVRC_Compare.Services;

public class CompareState : ICompareState
{
    private List<MvrcCase> mvrcCases = [];

    private string selectedImageSet = string.Empty;

    private string selectedCase = string.Empty;

    private List<KeyValuePair<string, int>> imageSetIndexes = new();

    public void AddCase(MvrcCase mvrcCase)
    {
        mvrcCases.Add(mvrcCase);

        if (string.IsNullOrEmpty(selectedCase))
        {
            selectedCase = mvrcCase.GetCaseName();
        }
    }

    public bool RemoveCase(MvrcCase mvrcCase)
    {
        return mvrcCases.Remove(mvrcCase);
    }

    public IList<MvrcCase> GetCases()
    {
        return mvrcCases;
    }

    public IList<string> GetCaseNames()
    {
        return mvrcCases.Select(x => x.GetCaseName()).ToList();
    }

    public string GetSelectedImageSet()
    {
        return selectedImageSet;
    }

    public void SetSelectedImageSet(string imageSet)
    {
        selectedImageSet = imageSet;

        if(!imageSetIndexes.Any(x => x.Key == imageSet))
        {
            imageSetIndexes.Add(new KeyValuePair<string, int>(imageSet, 0));
        }
    }

    public string GetSelectedCase()
    {
        return selectedCase;
    }

    public void SetSelectedCase(string mvrcCase)
    {
        selectedCase = mvrcCase;
    }

    public int GetSelectedImageSetIndex(string imageSet)
    {
        if (!imageSetIndexes.Any(x => x.Key == imageSet)) {
            return -1;
        }

        return imageSetIndexes.First(x => x.Key == imageSet).Value;
    }

    public void SetSelectedImageSetIndex(string imageSet, int index)
    {
        var current = imageSetIndexes.First(x => x.Key == imageSet);
        var images = mvrcCases.First(x => x.GetCaseName() == selectedCase).Images;
        var imagesInCurrentSet = images.Where(x => x.Contains(imageSet));

        imageSetIndexes.Remove(current);

        if (index < 0)
        {
            imageSetIndexes.Add(new KeyValuePair<string, int>(imageSet, imagesInCurrentSet.Count() - 1));
            return;
        }

        if (imagesInCurrentSet.Count() <= index)
        {
            imageSetIndexes.Add(new KeyValuePair<string, int>(imageSet, 0));
            return;
        }

        imageSetIndexes.Add(new KeyValuePair<string, int>(imageSet, index));
        return;
    }
}
