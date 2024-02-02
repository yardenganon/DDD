using DDDProject.Domain.Abstractions;
using DDDProject.Providers.Factory;

namespace DDDProject.Features.CreateAdContent;

public class SyncAdContentsCommandHandler : ICommandHandler<SyncAdContentsCommand>
{
    private readonly IProvidersFactory _providerFactory;
    private readonly IAdContentRepository _adsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SyncAdContentsCommandHandler( IProvidersFactory providerFactory, IAdContentRepository adsRepository, IUnitOfWork unitOfWork)
    {
        _providerFactory = providerFactory;
        _adsRepository = adsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(SyncAdContentsCommand request, CancellationToken ct)
    {
        foreach (var provider in request.Providers)
        {
            var providerApi = _providerFactory.GetProviderApi(provider);

            foreach (var account in request.Accounts)
            {
                var providerAds = await providerApi.GetAdContents(account);
                
                var localAds = (await _adsRepository.GetAdContentsByAccountId(account, ct))
                    .ToDictionary(k => k.ProviderId, v => v);
                
                foreach (var providerAd in providerAds)
                {
                    var isNewAd = !localAds.ContainsKey(providerAd.ProviderId);

                    if (!isNewAd)
                    {
                        var isModifiedAd = !providerAd.Equals(localAds[providerAd.ProviderId]);

                        if (!isModifiedAd)
                        {
                            continue;
                        }
                        
                        _adsRepository.Update(providerAd);
                    }
                    else
                    {
                        _adsRepository.Add(providerAd);
                    }
                }
            }
        }
        
        await _unitOfWork.SaveChangesAsync(ct);
    }
}