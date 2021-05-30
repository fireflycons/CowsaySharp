namespace CowsaySharp.Library
{
    public class SayBubbleChars : BubbleChars
    {
        public SayBubbleChars()
        {
            this.UpLeft = this.DownRight = "/";
            this.UpRight = this.DownLeft = this.Bubble = "\\";
            this.Left = this.Right = "|";
            this.SmallLeft = "<";
            this.SmallRight = ">";
        }
    }
}