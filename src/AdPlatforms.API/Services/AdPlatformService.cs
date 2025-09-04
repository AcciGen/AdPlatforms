namespace AdPlatforms.API.Services;

public class AdPlatformService : IAdPlatformService
{
    private Dictionary<string, HashSet<string>> _locationToPlatforms = new();

    public void LoadFromFile(string fileContent)
    {
        var newData = new Dictionary<string, HashSet<string>>();
        var lines = fileContent.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

        for (int line = 0; line < lines.Length; line++)
        {
            var parts = lines[line].Split(':', 2);
            if (parts.Length != 2) continue;

            var name = parts[0].Trim();
            if (string.IsNullOrWhiteSpace(name)) continue;

            var locations = parts[1].Split(',', StringSplitOptions.RemoveEmptyEntries);

            for (int l = 0; l < locations.Length; l++)
            {
                var normalizedLocation = locations[l].Trim();
                if (string.IsNullOrWhiteSpace(normalizedLocation)) continue;

                if (!newData.ContainsKey(normalizedLocation))
                    newData[normalizedLocation] = new HashSet<string>();

                newData[normalizedLocation].Add(name);

                //var segments = normalizedLocation.Split('/', StringSplitOptions.RemoveEmptyEntries);

                //for (int s = 1; s <= segments.Length; s++)
                //{
                //    var prefix = "/" + string.Join("/", segments.Take(s));

                //    if (!newData.ContainsKey(prefix))
                //        newData[prefix] = new HashSet<string>();

                //    newData[prefix].Add(name);
                //}
            }
        }
        _locationToPlatforms = newData;
    }


    public IEnumerable<string> FindByLocation(string location)
    {
        var result = new HashSet<string>();

        var segments = location.Split('/', StringSplitOptions.RemoveEmptyEntries);

        for (int s = 1; s <= segments.Length; s++)
        {
            var prefix = "/" + string.Join("/", segments.Take(s));

            if (_locationToPlatforms.TryGetValue(prefix, out var platforms))
            {
                foreach (var platform in platforms)
                    result.Add(platform);
            }
        }

        return result;
    }
}
