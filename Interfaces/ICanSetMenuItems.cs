using FluentConsoleMenu.Models;

namespace FluentConsoleMenu.Interfaces;

public interface ICanSetMenuItems : ICanShowMenu
{
    public ICanSetMenuItems WithMenuItem(string code, string text);
    
}