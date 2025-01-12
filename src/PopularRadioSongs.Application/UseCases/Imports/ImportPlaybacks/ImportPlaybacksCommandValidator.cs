using FluentValidation;

namespace PopularRadioSongs.Application.UseCases.Imports.ImportPlaybacks
{
    public class ImportPlaybacksCommandValidator : AbstractValidator<ImportPlaybacksCommand>
    {
        public ImportPlaybacksCommandValidator()
        {
            RuleFor(c => c.HoursRange)
                .InclusiveBetween(1, 24).WithMessage("Hours must be between 1 and 24");
        }
    }
}