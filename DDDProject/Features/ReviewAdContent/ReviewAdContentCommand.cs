using DDDProject.Domain.Abstractions;
using DDDProject.Domain.Enums;

namespace DDDProject.Features.ReviewAdContent;
public record ReviewAdContentCommand(Guid ReviewerId, Guid AdContentId, EReviewStatus Status, string? Comment) : ICommand {}