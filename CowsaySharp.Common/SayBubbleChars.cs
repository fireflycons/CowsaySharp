using CowsaySharp.Library;

namespace CowsaySharp.Common
{
    public class SayBubbleChars : BubbleChars
    {
        public SayBubbleChars()
        {
            UpLeft = DownRight = "/";
            UpRight = DownLeft = Bubble = "\\";
            Left = Right = "|";
            SmallLeft = "<";
            SmallRight = ">";
        }
    }
}