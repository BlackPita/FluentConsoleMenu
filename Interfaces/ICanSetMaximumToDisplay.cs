namespace FluentConsoleMenu.Interfaces;

public interface ICanSetMaximumToDisplay : ICanSetMenuItems
{
    public ICanSetMenuItems MaximumToDisplay(int max);

}