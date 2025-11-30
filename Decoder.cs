using System.Text;

namespace decoder;

public class Decoder
{
    private static readonly Lazy<Dictionary<string, string>> _encodings = new (encodings.Encodings.ReadEncodings);
    private static Dictionary<string, string> Encodings => _encodings.Value;

    public static string Decode(StreamReader message)
    {
        var result = new StringBuilder();
        var buffer = new char[16];
        while (!message.EndOfStream)
        {
            var read = message.Read(buffer, 0, buffer.Length);
            var chunk = new string(buffer, 0, read);
            var usefulLetters = GetUsefulLetters(chunk);
            usefulLetters.PlaceCorrectly();
            result.Append(ProcessDecode(usefulLetters));
        }
        return result.ToString();
    }

    private static string ProcessDecode(StringBuilder usefulLetters)
    {
        var result = new StringBuilder();
        for (var i = 0; i < usefulLetters.Length; i += 2)
        {
            var encodedLetter = new string(new[] { usefulLetters[i], usefulLetters[i + 1] });
            if (Encodings.TryGetValue(encodedLetter, out var decodedLetter))
                result.Append(decodedLetter);
            else
                result.Append('?');
        }
        return result.ToString();
    }

    private static StringBuilder GetUsefulLetters(string chunk)
    {
        var usefulLetters = new StringBuilder(8);
        usefulLetters.Append(chunk.Substring(0, 2));
        usefulLetters.Append(chunk.Substring(6, 1));
        usefulLetters.Append(chunk.Substring(8, 5));
        return usefulLetters;
    }


    public static class Helper
    {
        public const string HowToUse = """
                                       Как использовать:
                                            dotnet decoder.dll /path/to/encodede-message или ./decoder /path/to/encoded-message
                                            Если файл с сообщением не был указан то возьмется файл из текущей директории (имя файла по умолчанию - message.txt)
                                       """;
    }
}

public static class LettersExtensions
{
    public static void PlaceCorrectly(this StringBuilder usefulLetters)
    {
        (usefulLetters[1], usefulLetters[4]) = (usefulLetters[4], usefulLetters[1]);
        (usefulLetters[4], usefulLetters[6]) = (usefulLetters[6], usefulLetters[4]);
        (usefulLetters[5], usefulLetters[7]) = (usefulLetters[7], usefulLetters[5]);
        (usefulLetters[2], usefulLetters[6]) = (usefulLetters[6], usefulLetters[2]);
        (usefulLetters[2], usefulLetters[3]) = (usefulLetters[3], usefulLetters[2]);
        (usefulLetters[4], usefulLetters[5]) = (usefulLetters[5], usefulLetters[4]);
    }
}