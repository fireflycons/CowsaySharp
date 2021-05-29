namespace CowsaySharp.Library
{
    public class BubbleChars : IBubbleChars
    {
        public BubbleChars()
        {
            this.TopLine = '_';
            this.BottomLine = '-';
        }

        public char BottomLine { get; set; }

        public string Bubble { get; set; }

        public string DownLeft { get; set; }

        public string DownRight { get; set; }

        public string Left { get; set; }

        public string Right { get; set; }

        public string SmallLeft { get; set; }

        public string SmallRight { get; set; }

        public char TopLine { get; set; }

        public string UpLeft { get; set; }

        public string UpRight { get; set; }
    }
}