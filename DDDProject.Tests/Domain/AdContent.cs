using DDDProject.Domain;
using DDDProject.Domain.Enums;

namespace DDDProject.Tests.Domain;

public class AdContent
{
    [Test]
    public void Equals_SameOrder_ShouldReturnTrue()
    {
        // Arrange
        var ad1 = ResponsiveSearchAd.Create(
            long.MaxValue,
            new Campaign(long.MaxValue, long.MaxValue, "UKSearch", Enumerable.Empty<Country>()),
            User.Create("User", "1234"),
            EAdContentStatus.Enabled,
            DateTime.UtcNow,
            new() { "headline1", "headline2", "headline3", "headline4" },
            new() { "description1", "description2", "description3", "description4" },
            "path1",
            "path2");
        
        var ad2 = ResponsiveSearchAd.Create(
            long.MinValue,
            new Campaign(long.MaxValue, long.MaxValue, "UKSearch", Enumerable.Empty<Country>()),
            User.Create("User", "1234"),
            EAdContentStatus.Paused,
            DateTime.UtcNow,
            new() { "headline1", "headline2", "headline3", "headline4" },
            new() { "description1", "description2", "description3", "description4" },
            "path1",
            "path2");
        
        // Act
        var isEqual = ad1.Equals(ad2);

        // Assert
        Assert.True(isEqual);
    }
    
    [Test]
    public void Equals_NotSameOrder_ShouldReturnTrue()
    {
        // Arrange
        var ad1 = ResponsiveSearchAd.Create(
            long.MaxValue,
            new Campaign(long.MaxValue, long.MaxValue, "UKSearch", Enumerable.Empty<Country>()),
            User.Create("User", "1234"),
            EAdContentStatus.Paused,
            DateTime.UtcNow,
            new() { "headline1", "headline4", "headline2", "headline3" },
            new() {  "description3", "description2", "description4", "description1" },
            "path1",
            "path2");
        
        var ad2 = ResponsiveSearchAd.Create(
            long.MaxValue,
            new Campaign(long.MaxValue, long.MaxValue, "UKSearch", Enumerable.Empty<Country>()),
            User.Create("User", "1234"),
            EAdContentStatus.Paused,
            DateTime.UtcNow,
            new() { "headline1", "headline2", "headline3", "headline4" },
            new() { "description1", "description2", "description3", "description4" },
            "path1",
            "path2");
        
        // Act
        var isEqual = ad1.Equals(ad2);

        // Assert
        Assert.True(isEqual);
    }
    
    [Test]
    public void Equals_NotSameContent_ShouldReturnFalse()
    {
        // Arrange
        var ad1 = ResponsiveSearchAd.Create(
            long.MaxValue,
            new Campaign(long.MaxValue, long.MaxValue, "UKSearch", Enumerable.Empty<Country>()),
            User.Create("User", "1234"),
            EAdContentStatus.Deleted,
            DateTime.UtcNow,
            new() { "headline1", "headline2", "headline3", "headline4" },
            new() { "description1", "description2", "description3", "description4" },
            "path1",
            "path2");
        
        var ad2 = ResponsiveSearchAd.Create(
            long.MaxValue,
            new Campaign(long.MaxValue, long.MaxValue, "UKSearch", Enumerable.Empty<Country>()),
            User.Create("User", "1234"),
            EAdContentStatus.Paused,
            DateTime.UtcNow,
            new() { "headline1", "headline2", "headline3", "headline4" },
            new() { "description1", "description2", "description3", "descriptionBad4" },
            "path1",
            "path2");
        
        // Act
        var isEqual = ad1.Equals(ad2);

        // Assert
        Assert.False(isEqual);
    }
    
    [Test]
    public void Equals_NotSamePaths_ShouldReturnFalse()
    {
        // Arrange
        var ad1 = ResponsiveSearchAd.Create(
            long.MaxValue,
            new Campaign(long.MaxValue, long.MaxValue, "UKSearch", Enumerable.Empty<Country>()),
            User.Create("User", "1234"),
            EAdContentStatus.Paused,
            DateTime.UtcNow,
            new() { "headline1", "headline2", "headline3", "headline4" },
            new() { "description1", "description2", "description3", "description4" },
            "path1",
            "path2");
        
        var ad2 = ResponsiveSearchAd.Create(
            long.MaxValue,
            new Campaign(long.MaxValue, long.MaxValue, "UKSearch", Enumerable.Empty<Country>()),
            User.Create("User", "1234"),
            EAdContentStatus.Enabled,
            DateTime.UtcNow,
            new() { "headline1", "headline2", "headline3", "headline4" },
            new() { "description1", "description2", "description3", "description4" },
            "path11",
            "path2");
        
        // Act
        var isEqual = ad1.Equals(ad2);

        // Assert
        Assert.False(isEqual);
    }
}