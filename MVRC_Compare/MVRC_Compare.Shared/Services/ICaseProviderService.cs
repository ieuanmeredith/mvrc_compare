using MVRC_Compare.Shared.Models;

namespace MVRC_Compare.Shared.Services;

public interface ICaseProviderService
{
    public Task<MvrcCase?> LoadCaseAsync();
}
