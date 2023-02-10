namespace FluentConsoleMenu.Models
{
    public class MenuItem
    {
        public string Text { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;

        public MenuItem(string code, string text)
        {
            Code = code;
            Text = text;
        }

    }
}
