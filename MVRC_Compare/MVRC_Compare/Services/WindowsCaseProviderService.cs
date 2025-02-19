using CommunityToolkit.Maui.Storage;
using MVRC_Compare.Shared.Models;
using MVRC_Compare.Shared.Services;
using System.Threading;

namespace MVRC_Compare.Services;

public class WindowsCaseProviderService : ICaseProviderService
{
    public const string REPORTMOD = "_report";
    public const string PICMOD = "PICs";

    private readonly IFolderPicker _folderPicker;

    public WindowsCaseProviderService(
        IFolderPicker folderPicker)
    {
        _folderPicker = folderPicker;
    }

    public async Task<MvrcCase?> LoadCaseAsync()
    {
        var result = await _folderPicker.PickAsync();

        if(!result.IsSuccessful)
        {
            return null;
        }

        var path = result.Folder!.Path;
        var pathParts = path.Split('\\');
        var caseName = pathParts.Last();
        var dirs = Directory.GetDirectories(path);
        var reportDir = $"{path}\\{caseName}{REPORTMOD}";

        if (!Directory.Exists(reportDir))
        {
            return null;
        }

        var reportPicDir = $"{reportDir}\\{PICMOD}";

        if (!Directory.Exists(reportPicDir))
        {
            return null;
        }

        var images = Directory.GetFiles(reportPicDir);

        return new MvrcCase
        {
            FilePath = path,
            Images = images.ToList()
        };
    }
}
