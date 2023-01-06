namespace FluentConsoleMenu.Interfaces;

public interface ICanSetCoordinates : ICanSetColors
{
    public ICanSetColors WithCoordinates(int x , int y);
}