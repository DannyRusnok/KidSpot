using KidSpot.Application.Common;
using KidSpot.Application.Reviews.Commands;
using KidSpot.Domain.Repositories;

namespace KidSpot.Application.Reviews.Queries;

public class GetReviewsByPlaceHandler
{
    private readonly IReviewRepository _reviewRepository;

    public GetReviewsByPlaceHandler(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public async Task<ApiResponse<IReadOnlyList<ReviewDto>>> HandleAsync(
        Guid placeId,
        CancellationToken cancellationToken = default)
    {
        var reviews = await _reviewRepository.GetByPlaceIdAsync(placeId, cancellationToken);

        var dtos = reviews.Select(r => new ReviewDto(
            r.Id, r.PlaceId, r.UserId,
            r.User.Name, r.User.AvatarUrl,
            r.Rating, r.Text, r.CreatedAt
        )).ToList();

        return ApiResponse<IReadOnlyList<ReviewDto>>.Success(dtos);
    }
}
