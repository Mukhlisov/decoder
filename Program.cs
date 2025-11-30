namespace decoder;

class Program
{
    static void Main(string[] args)
    {
        if (!TryHandleMessageFilePath(args, out var messageFilePath))
            Console.WriteLine(Decoder.Helper.HowToUse);

        using var message = new StreamReader(messageFilePath);
        var decodedMessage = Decoder.Decode(message);
        Console.WriteLine(decodedMessage);
    }

    private static bool TryHandleMessageFilePath(string[] args, out string messageFilePath)
    {
        messageFilePath = args.Length == 0 
            ? Path.Combine(AppContext.BaseDirectory, "message.txt") 
            : args[0];
        return File.Exists(messageFilePath);
    }
}