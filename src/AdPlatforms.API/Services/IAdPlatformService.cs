namespace AdPlatforms.API.Services;

public interface IAdPlatformService
{
    public void LoadFromFile(string fileContent);
    public IEnumerable<string> FindByLocation(string location);
}
