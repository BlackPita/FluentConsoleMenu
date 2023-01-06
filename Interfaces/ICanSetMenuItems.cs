namespace FluentConsoleMenu.Interfaces;

public interface ICanSetMenuItems : ICanShowMenu
{
    public ICanSetMenuItems WithMenuItem(string menuItem);
    
}