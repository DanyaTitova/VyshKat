namespace VyshKat.App.UI;

public class SystemConsoleAdapter : IConsoleAdapter
{
    public void WriteLine(string message) => Console.WriteLine(message);
    public void Write(string message) => Console.Write(message);
    public string ReadLine() => Console.ReadLine() ?? string.Empty;
}
