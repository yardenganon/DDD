using DDDProject.Domain;

namespace DDDProject.Providers;

public interface IProvider
{
    Task<IReadOnlyCollection<AdContent>> GetAdContents(long account);
}