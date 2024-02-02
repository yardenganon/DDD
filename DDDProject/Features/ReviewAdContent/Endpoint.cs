using DDDProject.Domain.Enums;
using MediatR;

namespace DDDProject.Features.ReviewAdContent;

public static class Endpoint
{
    public static void AddEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPut("api/ad-contents", async (ApproveAdContentRequest request, ISender sender) =>
        {
            await sender.Send(new ReviewAdContentCommand(request.ReviewerId, request.AdContentId, request.Status, request.Comment));
        });
    }
}

public record ApproveAdContentRequest(Guid ReviewerId, Guid AdContentId, EReviewStatus Status, string? Comment);