using AdPlatforms.API.Services;

namespace AdPlatforms.Tests;

public class AdPlatformServiceTests
{
    private readonly AdPlatformService _service = new AdPlatformService();

    [Fact]
    public void LoadFromFile_ShouldLoadSimpleFile()
    {
        // Arrange
        string fileContent = "Yandex:/ru\nLocalAds:/ru/msk";

        // Act
        _service.LoadFromFile(fileContent);

        // Assert
        var resultRu = _service.FindByLocation("/ru");
        var resultMsk = _service.FindByLocation("/ru/msk");

        Assert.Contains("Yandex", resultRu);
        Assert.Contains("Yandex", resultMsk);
        Assert.Contains("LocalAds", resultMsk);
    }

    [Fact]
    public void LoadFromFile_ShouldIgnoreInvalidLines()
    {
        // Arrange
        string fileContent = "InvalidLine\nAnotherBadLine\nYandex:/ru";

        // Act
        _service.LoadFromFile(fileContent);

        // Assert
        var result = _service.FindByLocation("/ru");
        Assert.Single(result);
        Assert.Contains("Yandex", result);
    }

    [Fact]
    public void GetPlatformsByLocation_ShouldReturnEmpty()
    {
        // Arrange
        string fileContent = "Yandex:/ru";
        _service.LoadFromFile(fileContent);

        // Act
        var result = _service.FindByLocation("/us");

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void GetPlatformsByLocation_ShouldHandleSeveralLocations()
    {
        // Arrange
        string fileContent = "Yandex:/ru\nRegional:/ru/svrd\nLocal:/ru/svrd/revda\nSuspicious:/uz";
        _service.LoadFromFile(fileContent);

        // Act
        var result = _service.FindByLocation("/ru/svrd/revda");

        // Assert
        Assert.Contains("Yandex", result);
        Assert.Contains("Regional", result);
        Assert.Contains("Local", result);
        Assert.DoesNotContain("Suspicious", result);
    }
}