namespace CowsaySharp.Library
{
    /// <summary>
    /// Describes characters that make up the borders of a thought bubble.
    /// </summary>
    /// <seealso cref="CowsaySharp.Library.BubbleChars" />
    public class ThinkBubbleChars : BubbleChars
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThinkBubbleChars"/> class.
        /// </summary>
        public ThinkBubbleChars()
        {
            this.UpLeft = this.DownLeft = this.Left = this.SmallLeft = "(";
            this.UpRight = this.DownRight = this.Right = this.SmallRight = ")";
            this.Bubble = "o";
        }
    }
}