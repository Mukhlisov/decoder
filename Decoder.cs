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
            var correctlyPlacedLetters = PlaceCorrectly(usefulLetters);
            result.Append(ProcessDecode(correctlyPlacedLetters));
        }
        return result.ToString();
    }

    private static string ProcessDecode(char[] encodedSymbols)
    {
        var result = new StringBuilder();
        for (var i = 0; i < encodedSymbols.Length; i += 2)
        {
            var encodedLetter = new string(new[] { encodedSymbols[i], encodedSymbols[i + 1] });
            if (Encodings.TryGetValue(encodedLetter, out var decodedLetter))
                result.Append(decodedLetter);
        }
        return result.ToString();
    }

    private static char[] GetUsefulLetters(string chunk) => 
        new [] { chunk[0], chunk[1], chunk[6], chunk[8], chunk[9], chunk[10], chunk[11], chunk[12] };

    private static char[] PlaceCorrectly(char[] usefulLetters) =>
        new[]
        {
            usefulLetters[0],
            usefulLetters[4],
            usefulLetters[3],
            usefulLetters[1],
            usefulLetters[7],
            usefulLetters[6],
            usefulLetters[2],
            usefulLetters[5],
        };


    public static class Helper
    {
        public const string HowToUse = """
                                       Как использовать:
                                            dotnet decoder.dll /path/to/encodede-message или ./decoder /path/to/encoded-message
                                            Если файл с сообщением не был указан то возьмется файл из текущей директории (имя файла по умолчанию - message.txt)
                                       """;
    }
}