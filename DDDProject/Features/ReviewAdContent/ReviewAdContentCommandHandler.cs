using DDDProject.Domain;
using DDDProject.Domain.Abstractions;
using DDDProject.Domain.Enums;
using DDDProject.Domain.Exceptions;

namespace DDDProject.Features.ReviewAdContent;

public class ReviewAdContentCommandHandler : ICommandHandler<ReviewAdContentCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IAdContentRepository _contentRepository;
    private readonly ITimeProvider _timeProvider;
    private readonly IUnitOfWork _unitOfWork;

    public ReviewAdContentCommandHandler(IUserRepository userRepository, IAdContentRepository contentRepository, IUnitOfWork unitOfWork, ITimeProvider timeProvider)
    {
        _userRepository = userRepository;
        _contentRepository = contentRepository;
        _timeProvider = timeProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ReviewAdContentCommand command, CancellationToken cancellationToken)
    {
        var reviewer = await _userRepository.GetUserById(command.ReviewerId, cancellationToken);

        if (reviewer is null)
        {
            throw new UserNotFoundException(command.ReviewerId);
        }

        var adContent = await _contentRepository.GetAdContentById(command.AdContentId, cancellationToken);

        var comment = Comment.Create(reviewer.Id, _timeProvider.Now(), command.Comment);
        
        ChangeReviewStatus(adContent, command.Status, reviewer, comment);
        
        _contentRepository.Update(adContent);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private void ChangeReviewStatus(AdContent adContent, EReviewStatus status, User reviewer, Comment? comment)
    {
        switch (status)
        {
            case EReviewStatus.Approved:
                adContent.Approve(reviewer, _timeProvider.Now(), comment);
                break;
            case EReviewStatus.Pending:
                break;
            case EReviewStatus.Declined:
                adContent.Decline(reviewer, _timeProvider.Now(), comment);
                break;
            case EReviewStatus.Reviewed:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(status), status, null);
        };
    }
}