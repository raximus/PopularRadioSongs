namespace PopularRadioSongs.Application.Results
{
    public record ValidationFailure(IDictionary<string, string[]> ValidationErrors) : IFailure;
}