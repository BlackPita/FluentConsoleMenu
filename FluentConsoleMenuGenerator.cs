namespace FluentConsoleMenu;

using Models;
using Interfaces;
using System;

public sealed class FluentConsoleMenuGenerator : ICanSetTitle, ICanSetMenuItem, ICanSetMenuProperties, ICanShowMenu
{

    private readonly Menu menu;
    private string selected;

    private FluentConsoleMenuGenerator()
    {
        menu = new Menu();
        selected = string.Empty;
    }

    public static ICanSetTitle CreateBuilder()
    {
        return new FluentConsoleMenuGenerator();
    }

    public ICanSetMenuProperties WithTitle(string title)
    {
        menu.Title = title;
        return this;
    }


    ICanSetMenuProperties ICanSetMenuProperties.WithCoordinates(int x, int y)
    {
        menu.X = x;
        menu.Y = y;
        return this;
    }

    public ICanSetMenuItem WithMenuItem(string text, string? code = null)
    {
        menu.MenuItems.Add(new MenuItem(text , code == null ? text : code));
        return this;
    }

    ICanSetMenuProperties ICanSetMenuProperties.WithColors(ConsoleColor foregroundColor, ConsoleColor backgroundColor)
    {
        menu.ForegroundColor = foregroundColor;
        menu.BackgroundColor = backgroundColor;
        return this;
    }

    ICanSetMenuProperties ICanSetMenuProperties.MaximumEntriesToDisplay(int max)
    {
        menu.MaximumToDisplay = max;
        return this;
    }

    public string ShowMenu()
    {
        InitializeScreen();

        DisplayTitle();
        
        DisplayMenuItems();

        GetSelection();

        CleanUp();

        return selected;
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

        Console.SetCursorPosition(menu.X, menu.Y);
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(menu.Title);

        Console.ForegroundColor = foreGroundSave;
        Console.BackgroundColor = backGroundSave;
    }
    private void DisplayMenuItems()
    {
        for (var i = 0; i <= menu.MaximumDisplayed-1; i++)
        {
            DisplayMenuItem(i, menu.MenuItems[i], selected: i == 0);
        }
    }
    private void DisplayMenuItem(int i, MenuItem menuItem, bool selected = false)
    {
        var x = menu.X + menu.OffsetX;
        var y = menu.Y + menu.OffsetY + i;

        var backGroundSave = Console.BackgroundColor;
        var foreGroundSave = Console.ForegroundColor;
        var maxMenuItemLength = menu.MenuItems.Aggregate((max, cur) => max.Text.Length > cur.Text.Length ? max : cur).Text.Length;

        Console.SetCursorPosition(x, y);
        Console.WriteLine(new string(' ', maxMenuItemLength + 2));

        Console.SetCursorPosition(x, y);

        Console.ForegroundColor = selected ? menu.BackgroundColor : menu.ForegroundColor;
        Console.BackgroundColor = selected ? menu.ForegroundColor : menu.BackgroundColor;
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
                    if (selected < menu.MenuItems.Count - 1)
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
                    break;
            }

            if (selected < menu.MaximumToDisplay - 1)
            {
                for (var i = 0; i < menu.MaximumDisplayed; i++)
                {
                    DisplayMenuItem(i, menu.MenuItems[i], i == selected);
                }
            }
            else
            {
                for (var i = 0; i < menu.MaximumDisplayed - 1; i++)
                {
                    DisplayMenuItem(i, menu.MenuItems[selected - (menu.MaximumDisplayed - 1) + i], false);
                }
                DisplayMenuItem(menu.MaximumDisplayed - 1, menu.MenuItems[selected], true);
            }

        }
        while (key != ConsoleKey.Enter && key != ConsoleKey.Escape) ;

        this.selected = selected == -1 ? string.Empty : menu.MenuItems[selected].Code;
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