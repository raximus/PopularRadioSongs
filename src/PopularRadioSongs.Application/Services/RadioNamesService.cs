using PopularRadioSongs.Application.Contracts;
using System.Collections.Frozen;

namespace PopularRadioSongs.Application.Services
{
    public class RadioNamesService : IRadioNamesService
    {
        private readonly FrozenDictionary<int, string> _radioNames;

        public RadioNamesService(IEnumerable<IRadioStation> radioStations)
        {
            _radioNames = radioStations.ToFrozenDictionary(r => r.Id, r => r.Name);
        }

        public bool ConfirmRadioExist(int radioId)
        {
            return _radioNames.ContainsKey(radioId);
        }

        public string GetRadioName(int radioId)
        {
            if (_radioNames.TryGetValue(radioId, out var radioName))
            {
                return radioName;
            }

            return "Radio " + radioId;
        }

        public List<KeyValuePair<int, string>> GetRadioStationNames()
        {
            return _radioNames.ToList();
        }
    }
}