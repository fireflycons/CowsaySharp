namespace CowsaySharp.Library
{
    public class ThinkBubbleChars : BubbleChars
    {
        public ThinkBubbleChars()
        {
            this.UpLeft = this.DownLeft = this.Left = this.SmallLeft = "(";
            this.UpRight = this.DownRight = this.Right = this.SmallRight = ")";
            this.Bubble = "o";
        }
    }
}