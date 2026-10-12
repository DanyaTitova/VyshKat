namespace VyshKat.App.UI;

public interface IConsoleAdapter
{
    void WriteLine(string message);
    void Write(string message);
    string ReadLine();
}
