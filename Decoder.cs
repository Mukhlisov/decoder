using System.Text;

namespace decoder;

public class Decoder
{
    private static readonly Lazy<Dictionary<string, string>> _encodings = new (encodings.Encodings.ReadEncodings);
    private static Dictionary<string, string> Encodings => _encodings.Value;

    public static class Helper
    {
        public const string HowToUse = """
                                       Как использовать:
                                            dotnet decoder.dll /path/to/encodede-message или ./decoder
                                            Если файл с сообщением не был указан то возьмется файл из текущей директории (имя файла по умолчанию - message.txt)
                                       """;
    }

    public static string Decode(StreamReader message)
    {
        var result = new StringBuilder();
        return result.ToString();
    }
}