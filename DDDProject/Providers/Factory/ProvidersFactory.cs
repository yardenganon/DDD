using DDDProject.Domain.Enums;

namespace DDDProject.Providers.Factory;

public class ProvidersFactory : IProvidersFactory
{
    public IProvider GetProviderApi(EProvider provider) => provider switch
    {
        EProvider.Google => new GoogleProvider(),
        EProvider.Bing => new BingProvider(),
        _ => throw new ArgumentOutOfRangeException(nameof(provider), provider, null)
    };
}