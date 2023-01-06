namespace FluentConsoleMenu.Interfaces;

public interface ICanSetMaximumEntriesToDisplay : ICanSetMenuItems
{
    public ICanSetMenuItems MaximumEntriesToDisplay(int max);

}