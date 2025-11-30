namespace decoder.encodings;

public static class Encodings
{
    private static readonly string FileName = Path.Combine(AppContext.BaseDirectory, "Encodings.txt");
    private const char Separator = ':';

    public static Dictionary<string, string> ReadEncodings()
    {
        var encodings = new Dictionary<string, string>();
        try
        {
            if (!File.Exists(FileName))
                throw new FileNotFoundException(FileName);
            foreach (var line in File.ReadLines(FileName))
                encodings.AddEncoding(line);
            return encodings;
        }
        catch (Exception)
        {
            Console.WriteLine("Error occured while reading encodings");
            throw;
        }
    }

    private static (string key, string value) GetEncoding(string line)
    {
        var separatedLine = line.Split(Separator);
        return (separatedLine[0], separatedLine[1]);
    }

    private static void AddEncoding(this Dictionary<string, string> encodings, string line)
    {
        var encoding = GetEncoding(line);
        encodings.Add(encoding.key, encoding.value);
    }
}