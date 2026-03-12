using KidSpot.Application.Common;
using KidSpot.Domain.Entities;
using KidSpot.Domain.Repositories;

namespace KidSpot.Application.Reviews.Commands;

public record AddReviewCommand(int Rating, string Text);

public record ReviewDto(Guid Id, Guid PlaceId, Guid UserId, string UserName, string? UserAvatarUrl, int Rating, string Text, DateTime CreatedAt);

public class AddReviewHandler
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IPlaceRepository _placeRepository;
    private readonly IUserRepository _userRepository;

    public AddReviewHandler(
        IReviewRepository reviewRepository,
        IPlaceRepository placeRepository,
        IUserRepository userRepository)
    {
        _reviewRepository = reviewRepository;
        _placeRepository = placeRepository;
        _userRepository = userRepository;
    }

    public async Task<ApiResponse<ReviewDto>> HandleAsync(
        Guid placeId,
        Guid userId,
        AddReviewCommand command,
        CancellationToken cancellationToken = default)
    {
        var place = await _placeRepository.GetByIdAsync(placeId, cancellationToken);
        if (place is null)
            return ApiResponse<ReviewDto>.Failure("NOT_FOUND", $"Place with id '{placeId}' was not found.");

        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
        if (user is null)
            return ApiResponse<ReviewDto>.Failure("NOT_FOUND", "User not found.");

        Review review;
        try
        {
            review = Review.Create(placeId, userId, command.Rating, command.Text);
        }
        catch (ArgumentException ex)
        {
            return ApiResponse<ReviewDto>.Failure("VALIDATION_ERROR", ex.Message);
        }

        await _reviewRepository.AddAsync(review, cancellationToken);

        // Recalculate average rating
        place.RecalculateAverageRating();
        await _placeRepository.UpdateAsync(place, cancellationToken);

        return ApiResponse<ReviewDto>.Success(new ReviewDto(
            review.Id, review.PlaceId, review.UserId,
            user.Name, user.AvatarUrl,
            review.Rating, review.Text, review.CreatedAt));
    }
}
