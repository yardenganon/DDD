using DDDProject.Domain.Abstractions;

namespace DDDProject.Features.AutoReviewAdContents;

public record AutoReviewAdContentsCommand(
    DateTime NewAdsStart,
    DateTime OldAdsStart,
    long[] Accounts,
    long[] Campaigns,
    long[] AdGroups) : ICommand;