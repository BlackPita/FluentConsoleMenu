using FluentConsoleMenu.Interfaces;

namespace FluentConsoleMenu.Models
{
    public class Menu
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public int OffsetX { get; set; } = 1;
        public int OffsetY { get; set; } = 1;
        public ConsoleColor ForegroundColor { get; set; } = ConsoleColor.White;
        public ConsoleColor BackgroundColor { get; set; } = ConsoleColor.Black;
        public int MaximumToDisplay { get; set; } = 5;
        public int MaximumDisplayed => MaximumToDisplay > MenuItems.Count ? MenuItems.Count : MaximumToDisplay;
        public List<MenuItem> MenuItems { get; set; }= new List<MenuItem>();
    }
}
