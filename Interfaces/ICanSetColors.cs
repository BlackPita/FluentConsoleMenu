namespace FluentConsoleMenu.Interfaces;

public interface ICanSetColors : ICanSetMaximumEntriesToDisplay
{
    public ICanSetMaximumEntriesToDisplay WithColors(ConsoleColor foregroundColor, ConsoleColor backgroundColor);

}