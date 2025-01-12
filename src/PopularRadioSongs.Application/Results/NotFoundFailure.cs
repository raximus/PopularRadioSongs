namespace PopularRadioSongs.Application.Results
{
    public record NotFoundFailure(string Message) : IFailure;
}