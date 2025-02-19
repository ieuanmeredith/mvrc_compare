
namespace MVRC_Compare.Shared.Models
{
    public class MvrcCase
    {
        public string FilePath { get; set; } = default!;

        public IList<string> Images { get; set; } = default!;
    }
}
