namespace FluentConsoleMenu.Interfaces
{
    public interface ICanSetMenuItem : ICanShowMenu
    {
        public ICanSetMenuItem WithMenuItem(string text, string? code = null);

    }
}