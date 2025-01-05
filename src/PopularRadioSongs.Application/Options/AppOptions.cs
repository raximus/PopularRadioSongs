using System.ComponentModel.DataAnnotations;

namespace PopularRadioSongs.Application.Options
{
    public class AppOptions
    {
        [Range(0, 59)]
        public int PlaybacksImporterStartMinute { get; set; }
        public bool ManualImportOnProduction { get; set; }
    }
}