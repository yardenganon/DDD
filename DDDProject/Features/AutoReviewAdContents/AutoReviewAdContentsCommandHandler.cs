using DDDProject.Domain;
using DDDProject.Domain.Abstractions;
using DDDProject.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace DDDProject.Features.AutoReviewAdContents;

public class AutoReviewAdContentsCommandHandler : ICommandHandler<AutoReviewAdContentsCommand>
{
    private readonly IAdContentRepository _adContentRepository;
    private readonly ITimeProvider _timeProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly DbSet<AdContent> _adContentReadOnly;

    public AutoReviewAdContentsCommandHandler(IAdContentRepository adContentRepository, IUnitOfWork unitOfWork, DbSet<AdContent> adContentReadOnly, ITimeProvider timeProvider)
    {
        _adContentRepository = adContentRepository;
        _unitOfWork = unitOfWork;
        _adContentReadOnly = adContentReadOnly;
        _timeProvider = timeProvider;
    }

    public async Task Handle(AutoReviewAdContentsCommand request, CancellationToken cancellationToken)
    {
        List<AdContent> approvedAds = await GetApprovedAds(request.OldAdsStart, request.NewAdsStart, request.Campaigns, request.AdGroups);
        List<AdContent> pendingAds = await _adContentRepository.GetPendingAds(request.NewAdsStart);
        var timeNow = _timeProvider.Now();

        foreach (var pendingAd in pendingAds)
        {
            foreach (var approvedAd in approvedAds)
            {
                if (!pendingAd.Equals(approvedAd))
                {
                    continue;
                }

                pendingAd.AutoApprove(timeNow, approvedAd.Id);

                _adContentRepository.Update(pendingAd);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                break;
            }
        }
    }

    private async Task<List<AdContent>> GetApprovedAds(DateTime from, DateTime to, long[] campaigns, long[] adGroups)
    {
        return await _adContentReadOnly.Where(
            ad => ad.ModifiedAt >= from && ad.ModifiedAt <= to &&
                  !campaigns.Any() || campaigns.Contains(ad.Campaign.Id) &&
                  !adGroups.Any() || adGroups.Contains(ad.AdGroupId) &&
                  ad.ReviewStatus == EReviewStatus.Approved)
            .ToListAsync();
    }
}