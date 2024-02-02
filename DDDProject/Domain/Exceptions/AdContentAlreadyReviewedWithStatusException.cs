using DDDProject.Domain.Enums;

namespace DDDProject.Domain.Exceptions;

public class AdContentAlreadyReviewedWithStatusException : Exception
{
    public AdContentAlreadyReviewedWithStatusException(Guid id, EReviewStatus status) : 
        base($"Ad Content {id} is already reviewed with status {status}") {}
}