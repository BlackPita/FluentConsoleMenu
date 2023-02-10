namespace FluentConsoleMenu.Interfaces
{
    public interface ICanSetMenuProperties : ICanSetMenuItem
    {
        ICanSetMenuProperties WithCoordinates(int x, int y);
        ICanSetMenuProperties WithColors(ConsoleColor foregroundColor, ConsoleColor backgroundColor);
        ICanSetMenuProperties MaximumEntriesToDisplay(int max);

    }
}
