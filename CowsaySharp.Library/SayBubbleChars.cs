namespace CowsaySharp.Library
{
    /// <summary>
    /// Describes characters that make up the borders of a speech bubble.
    /// </summary>
    /// <seealso cref="CowsaySharp.Library.BubbleChars" />
    public class SayBubbleChars : BubbleChars
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SayBubbleChars"/> class.
        /// </summary>
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