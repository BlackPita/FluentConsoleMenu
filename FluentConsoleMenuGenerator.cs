namespace FluentConsoleMenu;

using Models;
using Interfaces;
using System;

public class FluentConsoleMenuGenerator : ICanSetTitle, ICanSetCoordinates, ICanSetColors , ICanSetMaximumEntriesToDisplay, ICanSetMenuItems
{

    private readonly Menu _menu;
    private string _selected;

    private FluentConsoleMenuGenerator()
    {
        this._menu = new Menu();
        this._selected = string.Empty;
    }

    public static ICanSetTitle CreateBuilder()
    {
        return new FluentConsoleMenuGenerator();
    }

    public ICanSetCoordinates WithTitle(string title)
    {
        this._menu.Title = title;
        return this;
    }

    public ICanSetColors WithCoordinates(int x, int y)
    {
        this._menu.X = x;
        this._menu.Y = y;
        return this;
    }
    public ICanSetMenuItems WithMenuItem(string code, string text)
    {
        this._menu.MenuItems.Add(new MenuItem(code,text));
        return this;
    }

    public ICanSetMaximumEntriesToDisplay WithColors(ConsoleColor foregroundColor, ConsoleColor backgroundColor)
    {
        this._menu.ForegroundColor = foregroundColor;
        this._menu.BackgroundColor = backgroundColor;
        return this;
    }

    public ICanSetMenuItems MaximumEntriesToDisplay(int max)
    {
        this._menu.MaximumToDisplay = max;
        return this;
    }

    public string ShowMenu()
    {
        InitializeScreen();

        DisplayTitle();
        
        DisplayMenuItems();

        GetSelection();

        CleanUp();

        return _selected;
    }
    private static void InitializeScreen()
    {
        Console.CursorVisible = false;
        Console.Clear();
    }
    private void DisplayTitle()
    {
        ConsoleColor backGroundSave = Console.BackgroundColor;
        ConsoleColor foreGroundSave = Console.ForegroundColor;

        int consoleWidth = Console.WindowWidth;

        Console.SetCursorPosition(_menu.X, _menu.Y);
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(_menu.Title);

        Console.ForegroundColor = foreGroundSave;
        Console.BackgroundColor = backGroundSave;
    }
    private void DisplayMenuItems()
    {
        for (var i = 0; i <= _menu.MaximumDisplayed-1; i++)
        {
            DisplayMenuItem(i, _menu.MenuItems[i], selected: i == 0);
        }
    }

    private void DisplayMenuItem(int i, MenuItem menuItem, bool selected = false)
    {
        var x = _menu.X + _menu.OffsetX;
        var y = _menu.Y + _menu.OffsetY + i;

        var backGroundSave = Console.BackgroundColor;
        var foreGroundSave = Console.ForegroundColor;
        var maxMenuItemLength = _menu.MenuItems.Aggregate((max, cur) => max.Text.Length > cur.Text.Length ? max : cur).Text.Length;

        Console.SetCursorPosition(x, y);
        Console.WriteLine(new string(' ', maxMenuItemLength + 2));

        Console.SetCursorPosition(x, y);

        Console.ForegroundColor = selected ? _menu.BackgroundColor : _menu.ForegroundColor;
        Console.BackgroundColor = selected ? _menu.ForegroundColor : _menu.BackgroundColor;
        Console.WriteLine($" {menuItem.Text} ");

        Console.ForegroundColor = foreGroundSave;
        Console.BackgroundColor = backGroundSave;
    }

    private void GetSelection()
    {
        ConsoleKey key;

        var selected = 0;

        do
        {
            key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (selected != 0)
                    {
                        selected--;
                    }

                    break;
                case ConsoleKey.DownArrow:
                    if (selected < _menu.MenuItems.Count - 1)
                    {
                        selected += 1;
                    }
                    break;
                case ConsoleKey.Escape:
                    selected = -1;
                    break;
                case ConsoleKey.Enter:
                    break;
                default:
                    selected = -1;
                    break;
            }

            if (selected < _menu.MaximumToDisplay - 1)
            {
                for (var i = 0; i < _menu.MaximumDisplayed; i++)
                {
                    DisplayMenuItem(i, _menu.MenuItems[i], i == selected);
                }
            }
            else
            {
                for (var i = 0; i < _menu.MaximumDisplayed - 1; i++)
                {
                    DisplayMenuItem(i, _menu.MenuItems[selected - (_menu.MaximumDisplayed - 1) + i], false);
                }
                DisplayMenuItem(_menu.MaximumDisplayed - 1, _menu.MenuItems[selected], true);
            }

        }
        while (key != ConsoleKey.Enter && key != ConsoleKey.Escape) ;

        _selected = selected == -1 ? string.Empty : _menu.MenuItems[selected].Code;
    }
    private static void CleanUp()
    {
        Console.CursorVisible = true;
    }

    private static void ShowDebug(string variable, object value)
    {
        Console.SetCursorPosition(1, 1);
        Console.WriteLine($"{variable}: {value}");

    }
}