using FluentConsoleMenu.Interfaces;

namespace FluentConsoleMenu.Models
{
    public class Menu
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int OffsetX { get; set; }
        public int OffsetY { get; set; }
        public ConsoleColor ForegroundColor { get; set; }
        public ConsoleColor BackgroundColor { get; set; }
        public int MaximumToDisplay { get; set; }
        public int MaximumDisplayed => MaximumToDisplay > MenuItems.Count ? MenuItems.Count : MaximumToDisplay;

        public List<string> MenuItems { get; set; }

        public Menu()
        {
            Title = string.Empty;
            Description = string.Empty;
            X = 0;
            Y = 0;
            OffsetX = 1;
            OffsetY = 1;
            ForegroundColor = ConsoleColor.White;
            BackgroundColor = ConsoleColor.Black;
            MaximumToDisplay = 5;
            MenuItems = new List<string>();

        }
            
    }
}
