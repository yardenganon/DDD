using DDDProject.Domain.Enums;
using DDDProject.Domain.Events;

namespace DDDProject.Domain;

public class ResponsiveSearchAd : AdContent
{
    public static ResponsiveSearchAd Create(
        long providerId,
        Campaign campaign,
        User creator,
        EAdContentStatus status,
        DateTime now,
        List<string> headlines,
        List<string> descriptions,
        string? path1 = null,
        string? path2 = null)
    {
        var ad = new ResponsiveSearchAd(providerId, campaign, creator, status, now, headlines, descriptions, path1, path2);
        ad.RaiseEvent(new AdContentCreatedDomainEvent(providerId, now));
        return ad;
    }

    private ResponsiveSearchAd() {} // for ef core
    private ResponsiveSearchAd(
        long providerId,
        Campaign campaign,
        User creator,
        EAdContentStatus status,
        DateTime now,
        List<string>? headlines,
        List<string>? descriptions,
        string? path1 = null, string? path2 = null)
    {
        Id = Guid.NewGuid();
        ProviderId = providerId;
        Campaign = campaign;
        ReviewStatus = EReviewStatus.Pending;
        Status = status;
        CreatedAt = now;
        Creator = creator;
        Headlines = headlines ?? new List<string>();
        Descriptions = descriptions ?? new List<string>();
        Path1 = path1;
        Path2 = path2;
    }

    public List<string> Headlines {get; private set;} = new();
    public List<string> Descriptions {get; private set;} = new();
    public string? Path1 { get; private set; } = string.Empty;
    public string? Path2 { get; private set; } = string.Empty;

    public void ModifyAdContent(
        IEnumerable<string> descriptions,
        IEnumerable<string> headlines,
        string? path1,
        string? path2,
        DateTime now)
    {
        var oldAdContentState = AdContentString;
        Headlines = headlines.ToList();
        Descriptions = descriptions.ToList();
        Path1 = path1;
        Path2 = path2;
        
        RaiseEvent(new AdContentModifiedDomainEvent(Id, now, oldAdContentState, AdContentString));
    }

    private string AdContentString =>
        $"Headlines: {string.Join(",", Headlines.OrderDescending())}, " +
        $"Descriptions: {string.Join(",", Descriptions.OrderDescending())}, " +
        $"Path1: {Path1}, Path2: {Path2}"; 
    
    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }
        var ad = (ResponsiveSearchAd)obj;
        return AreDescriptionsEqual(ad.Descriptions) &&
               AreHeadlinesEqual(ad.Headlines) &&
               ArePathsEqual(ad.Path1, ad.Path2);
    }

    private bool AreDescriptionsEqual(IReadOnlyCollection<string> descriptions) =>
        descriptions.Count == Descriptions.Count && descriptions.OrderDescending().SequenceEqual(Descriptions.OrderDescending());
    
    private bool AreHeadlinesEqual(IReadOnlyCollection<string> headlines) =>
        headlines.Count == Headlines.Count && headlines.OrderDescending().SequenceEqual(Headlines.OrderDescending());

    private bool ArePathsEqual(string? path1, string? path2) =>
        path1 == Path1 && path2 == Path2;
}