using FluentConsoleMenu.Models;

namespace FluentConsoleMenu;

using Interfaces;
using System;

public class FluentConsoleMenuGenerator : ICanSetTitle, ICanSetCoordinates, ICanSetColors , ICanSetMaximumToDisplay, ICanSetMenuItems
{

    private readonly Menu _menu;

    private FluentConsoleMenuGenerator()
    {
        this._menu = new Menu();
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

    public ICanSetMaximumToDisplay WithColors(ConsoleColor foregroundColor, ConsoleColor backgroundColor)
    {
        this._menu.ForegroundColor = foregroundColor;
        this._menu.BackgroundColor = backgroundColor;
        return this;
    }

    public ICanSetMenuItems MaximumToDisplay(int max)
    {
        this._menu.MaximumToDisplay = max;
        return this;
    }

    public string ShowMenu()
    {
        ConsoleKey key;

        InitializeScreen();
        DisplayTitle();
        DisplayMenuItems();

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
                    if (selected < _menu.MenuItems.Count -1 )
                    {
                        selected += 1;
                    }
                    break;
            }

            Console.SetCursorPosition(10, 10);
            Console.WriteLine($"selected: {selected} _menu.MaximumToDisplay: {_menu.MaximumToDisplay}");


            if (selected < _menu.MaximumToDisplay - 1)
            {
                for (var i = 0; i < _menu.MaximumToDisplay; i++)
                {

                    if (i == selected)
                    {
                        DisplayMenuItem(i, _menu.MenuItems[i], ConsoleColor.Black, ConsoleColor.White);
                    }
                    else
                    {
                        DisplayMenuItem(i, _menu.MenuItems[i], ConsoleColor.White, ConsoleColor.Black);
                    }
                }
            }
            else
            {
                for (var i = 0; i < _menu.MaximumToDisplay-1; i++)
                {
                    DisplayMenuItem(i, _menu.MenuItems[selected - (_menu.MaximumToDisplay-1) + i], ConsoleColor.White, ConsoleColor.Black);
                }
                
                DisplayMenuItem(_menu.MaximumToDisplay-1 , _menu.MenuItems[selected], ConsoleColor.Black, ConsoleColor.White);
            }

        }
        while (key != ConsoleKey.Enter);

        CleanUp();

        return _menu.MenuItems[selected];
    }


    public ICanSetMenuItems WithMenuItem(string menuItem)
    {
        this._menu.MenuItems.Add(menuItem);
        return this;
    }

    private void InitializeScreen()
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

        for (var i = 0; i <= _menu.MaximumToDisplay-1; i++)
        {
            if (i == 0)
            {
                DisplayMenuItem(i, _menu.MenuItems[i], ConsoleColor.Black, ConsoleColor.White);
            }
            else
            {
                DisplayMenuItem(i, _menu.MenuItems[i], ConsoleColor.White, ConsoleColor.Black);
            }
        }

    }

    private void DisplayMenuItem(int i,string menuItem, ConsoleColor fgColor, ConsoleColor bgColor)
    {
        var x = _menu.X + _menu.OffsetX;
        var y = _menu.Y + _menu.OffsetY + i;

        var backGroundSave = Console.BackgroundColor;
        var foreGroundSave = Console.ForegroundColor;
        var maxMenuItemLength = _menu.MenuItems.Aggregate((max, cur) => max.Length > cur.Length ? max : cur).Length;

        Console.SetCursorPosition(x, y);
        Console.WriteLine(new string(' ', maxMenuItemLength + 2));

        Console.SetCursorPosition(x, y);
        Console.BackgroundColor = bgColor;
        Console.ForegroundColor = fgColor;
        Console.WriteLine($" {menuItem} ");

        Console.ForegroundColor = foreGroundSave;
        Console.BackgroundColor = backGroundSave;
    }

    private void CleanUp()
    {
        Console.CursorVisible = true;
    }
}