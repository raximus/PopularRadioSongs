using FluentValidation;

namespace PopularRadioSongs.Application.UseCases.Search.GetSearchResults
{
    public class GetSearchResultsQueryValidator : AbstractValidator<GetSearchResultsQuery>
    {
        public GetSearchResultsQueryValidator()
        {
            RuleFor(q => q.SearchValue)
                .NotEmpty().WithMessage("Search Value cant be empty")
                .MinimumLength(3).WithMessage("Search Value must have at least 3 characters");
        }
    }
}