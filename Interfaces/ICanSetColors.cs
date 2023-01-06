namespace FluentConsoleMenu.Interfaces;

public interface ICanSetColors : ICanSetMaximumToDisplay
{
    public ICanSetMaximumToDisplay WithColors(ConsoleColor foregroundColor, ConsoleColor backgroundColor);

}