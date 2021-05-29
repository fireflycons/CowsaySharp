namespace CowsaySharp.Library
{
    /// <summary>
    /// Characters that will be used to generate the Speech Bubble
    /// </summary>
    public interface IBubbleChars
    {
        char BottomLine { get; }

        string Bubble { get; }

        string DownLeft { get; }

        string DownRight { get; }

        string Left { get; }

        string Right { get; }

        string SmallLeft { get; }

        string SmallRight { get; }

        char TopLine { get; }

        string UpLeft { get; }

        string UpRight { get; }
    }
}