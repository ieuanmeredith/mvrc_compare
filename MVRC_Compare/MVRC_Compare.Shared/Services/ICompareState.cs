using MVRC_Compare.Shared.Models;

namespace MVRC_Compare.Shared.Services;

public interface ICompareState
{
    public void AddCase(MvrcCase mvrcCase);

    public bool RemoveCase(MvrcCase mvrcCase);

    public IList<MvrcCase> GetCases();

    public IList<string> GetCaseNames();

    public string GetSelectedImageSet();
    public void SetSelectedImageSet(string imageSet);

    public string GetSelectedCase();
    public void SetSelectedCase(string mvrcCase);

    public int GetSelectedImageSetIndex(string imageSet);
    public void SetSelectedImageSetIndex(string imageSet, int index);
}
