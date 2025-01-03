namespace PopularRadioSongs.Application.Contracts
{
    public interface IRadioNamesService
    {
        bool ConfirmRadioExist(int radioId);
        string GetRadioName(int radioId);
        List<KeyValuePair<int, string>> GetRadioStationNames();
    }
}