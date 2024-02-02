using DDDProject.Domain.Abstractions;
using DDDProject.Domain.Enums;
using DDDProject.Domain.Events;
using DDDProject.Domain.Exceptions;

namespace DDDProject.Domain;

public abstract class AdContent : Entity
{
    public Guid Id {get; protected set;}
    public long ProviderId { get; protected set; }
    public Campaign Campaign { get; protected set; }
    public long AdGroupId { get; protected set; }
    public long ProviderAdGroupId { get; protected set; }
    public long MarketingUnitId { get; protected set; }
    public long ProviderMarketingUnitId { get; protected set; }
    public User Creator {get; protected set;}
    public User? Reviewer { get; protected set; }
    public DateTime? ReviewedAt { get; protected set; }
    public DateTime CreatedAt {get; protected set;}
    public DateTime? ModifiedAt { get; protected set; } = null;
    public DateTime? ContentModifiedAt { get; private set; } = null;
    private List<Comment> Comments { get; } = new();
    public IReadOnlyCollection<Comment> GetComments => Comments.AsReadOnly();
    public EReviewStatus ReviewStatus { get; protected set; } = EReviewStatus.Pending;
    public Guid? ReferencedAd { get; protected set; }
    public EAdContentStatus Status { get; protected set; } = EAdContentStatus.Paused;
    
    public void Enable(DateTime now)
    {
        Status = EAdContentStatus.Enabled;
        RaiseEvent(new AdContentEnabledDomainEvent(Id, now));
    }
    public void Pause(DateTime now)
    {
        Status = EAdContentStatus.Paused;
        RaiseEvent(new AdContentPausedDomainEvent(Id, now));
    }
    public void Delete(DateTime now)
    {
        Status = EAdContentStatus.Deleted;
        RaiseEvent(new AdContentDeletedDomainEvent(Id, now));
    }
    public void AutoApprove(DateTime now, Guid referenceAd)
    {
        ReviewStatus = EReviewStatus.Approved;
        ModifiedAt = ReviewedAt = now;
        ReferencedAd = referenceAd;

        RaiseEvent(new AdContentAutoApprovedDomainEvent(Id, ReviewedAt.Value, ReferencedAd.Value));
    }
    public void AutoPause(DateTime now)
    {
        ReviewStatus = EReviewStatus.Approved;
        ModifiedAt = ReviewedAt = now;

        RaiseEvent(new AdContentAutoPausedDomainEvent(Id, ReviewedAt.Value));
    }
    public void Approve(User reviewer, DateTime now, Comment? comment) => SetReviewStatus(reviewer, EReviewStatus.Approved, now, comment);
    public void Decline(User reviewer, DateTime now, Comment? comment) => SetReviewStatus(reviewer, EReviewStatus.Declined, now, comment);
    private void SetReviewStatus(User reviewer, EReviewStatus status, DateTime now, Comment? comment)
    {
        if (ReviewStatus == status)
        {
            throw new AdContentAlreadyReviewedWithStatusException(Id, ReviewStatus);
        }
        
        Reviewer = reviewer;
        ReviewStatus = status;
        ModifiedAt = ReviewedAt = now;
        
        if (comment is not null)
        {
            Comments.Add(comment);
        }
        
        RaiseEvent(new AdContentReviewedDomainEvent(Id, ReviewedAt.Value, ReviewStatus, reviewer.Id));
    }
}