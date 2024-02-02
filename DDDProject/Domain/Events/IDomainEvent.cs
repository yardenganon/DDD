using DDDProject.Domain.Enums;

namespace DDDProject.Domain.Events;

public interface IDomainEvent {}

public record AdContentCreatedDomainEvent(long ProviderId, DateTime CreatedAt) : IDomainEvent;
public record AdContentEnabledDomainEvent(Guid Id, DateTime Now) : IDomainEvent;
public record AdContentPausedDomainEvent(Guid Id, DateTime Now) : IDomainEvent;
public record AdContentDeletedDomainEvent(Guid Id, DateTime DeletedAt) : IDomainEvent;
public record AdContentReviewedDomainEvent(Guid Id, DateTime ReviewedAt, EReviewStatus Status, Guid ReviewerId) : IDomainEvent;
public record AdContentAutoApprovedDomainEvent(Guid Id, DateTime ReviewedAt, Guid ReferencedAd) : IDomainEvent;
public record AdContentAutoPausedDomainEvent(Guid Id, DateTime ReviewedAt) : IDomainEvent;
public record AdContentModifiedDomainEvent(Guid Id, DateTime ModifiedAt, string OldState, string NewState) : IDomainEvent;
