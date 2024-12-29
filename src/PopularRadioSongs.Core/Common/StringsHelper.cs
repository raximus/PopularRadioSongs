using System.Globalization;
using System.Text.RegularExpressions;

namespace PopularRadioSongs.Core.Common
{
    public static partial class StringsHelper
    {
        private static readonly Dictionary<string, string> _diacriticsMap = new Dictionary<string, string>()
            {
                { "ą", "a" },
                { "ć", "c" },
                { "ę", "e" },
                { "ł", "l" },
                { "ń", "n" },
                { "ó", "o" },
                { "ś", "s" },
                { "ż", "z" },
                { "ź", "z" },

                { "á", "a" },
                { "é", "e" },
                { "í", "i" },
                { "ú", "u" },
                { "ñ", "n" },

                { "à", "a" },
                { "è", "e" },
                { "ì", "i" },
                { "ò", "o" },
                { "ù", "u" },

                { "â", "a" },
                { "ê", "e" },
                { "î", "i" },
                { "ô", "o" },
                { "û", "u" },
                { "ç", "c" },

                { "ä", "a" },
                { "ë", "e" },
                { "ï", "i" },
                { "ö", "o" },
                { "ü", "u" },
                { "ß", "b" },
                { "å", "a" }
            };
        private static readonly Regex _diacriticsRegex = new Regex(string.Join("|", _diacriticsMap.Keys), RegexOptions.Compiled);

        [GeneratedRegex("[^0-9a-z]")]
        private static partial Regex LetterNumberRegex();

        public static string StandardizeString(string text)
        {
            var textInfo = CultureInfo.InvariantCulture.TextInfo;

            return textInfo.ToTitleCase(text.Trim().ToLower());
        }

        public static string LookupString(string text)
        {
            text = _diacriticsRegex.Replace(text.ToLower(), m => _diacriticsMap[m.Value]);

            text = text.Replace("the ", string.Empty);

            return LetterNumberRegex().Replace(text, string.Empty);
        }
    }
}