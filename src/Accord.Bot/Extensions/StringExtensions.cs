using System.Text.RegularExpressions;

namespace Accord.Bot.Extensions;

public static partial class StringExtensions
{
    [GeneratedRegex(@"```.*?```", RegexOptions.Singleline)]
    private static partial Regex CodeBlockRegex();

    [GeneratedRegex(@"`.*?`", RegexOptions.Singleline)]
    private static partial Regex InlineCodeRegex();
    private static readonly string[] SpecialCharacters = ["\\", "`", "|"];

    extension(string text)
    {
        public string SanitiseDiscordContent()
        {
            foreach (var character in SpecialCharacters)
            {
                text = text.Replace(character, $"\\{character}");
            }
            return text;
        }

        public string StripCodeBlocks()
        {
            text = CodeBlockRegex().Replace(text, string.Empty);
            return text;
        }
        public string StripInlineCode()
        {
            text = InlineCodeRegex().Replace(text, string.Empty);
            return text;
        }

        public string StripCode()
        {
            return text.StripCodeBlocks().StripInlineCode();
        }
    }
}