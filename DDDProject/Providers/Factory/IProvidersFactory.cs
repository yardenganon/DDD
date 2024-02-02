using DDDProject.Domain.Enums;

namespace DDDProject.Providers.Factory;

public interface IProvidersFactory
{
    IProvider GetProviderApi(EProvider provider);
}