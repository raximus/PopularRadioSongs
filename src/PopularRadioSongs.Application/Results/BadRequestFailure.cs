namespace PopularRadioSongs.Application.Results
{
    public record BadRequestFailure(string Message) : IFailure;
}