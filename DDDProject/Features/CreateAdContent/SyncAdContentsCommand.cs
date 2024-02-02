using DDDProject.Domain.Abstractions;
using DDDProject.Domain.Enums;

namespace DDDProject.Features.CreateAdContent;

public record SyncAdContentsCommand(
    EProvider[] Providers,
    long[] Accounts,
    long[] Campaigns,
    long[] AdGroups) : ICommand;