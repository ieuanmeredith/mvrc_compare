
namespace MVRC_Compare.Shared.Models
{
    public class MvrcCase
    {
        public string FilePath { get; set; } = default!;

        public IList<string> Images { get; set; } = default!;

        public string GetCaseName()
        {
            return FilePath.Split("\\").Last();
        }

        public IList<string> GetImageSets()
        {
            List<string> sets = [];

            var name = GetCaseName();

            foreach (var img in Images)
            {
                var setName = img.Split('\\').Last().Substring(name.Length + 1);
                setName = setName.Remove(setName.Length - 8);

                if (sets.Contains(setName))
                {
                    continue;
                }

                sets.Add(setName);
            }

            return sets;
        }
    }
}
