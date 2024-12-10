using System.Globalization;
using System.Text.RegularExpressions;

namespace PopularRadioSongs.Core.Common
{
    public static class StringsHelper
    {
        private static readonly Dictionary<string, string> DiacriticsMap = new Dictionary<string, string>()
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
                { "ü", "u" },

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
        private static readonly Regex DiacriticsRegex = new Regex(string.Join("|", DiacriticsMap.Keys), RegexOptions.Compiled);
        private static readonly Regex LetterNumberRegex = new Regex("[^0-9a-z]", RegexOptions.Compiled);

        public static string StandardizeString(string text)
        {
            var textInfo = CultureInfo.InvariantCulture.TextInfo;

            return textInfo.ToTitleCase(text.Trim().ToLower());
        }

        public static string LookupString(string text)
        {
            text = DiacriticsRegex.Replace(text.ToLower(), m => DiacriticsMap[m.Value]);

            text = text.Replace("the ", string.Empty);

            return LetterNumberRegex.Replace(text, string.Empty);
        }
    }
}