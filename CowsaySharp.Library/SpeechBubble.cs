namespace CowsaySharp.Library
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class SpeechBubble
    {
        private readonly bool figlet;

        private int? maxLineLength;

        private string message;

        public SpeechBubble(string message, IBubbleChars bubbleChars, int? maxLineLength, bool figlet)
        {
            this.figlet = figlet;
            this.maxLineLength = maxLineLength;
            this.BubbleChars = bubbleChars;
            this.message = message;
        }

        public IBubbleChars BubbleChars { get; }

        public string ReturnSpeechBubble()
        {
            char[] splitChar = { ' ', (char)10, (char)13 };
            var messageAsList = new List<string>();

            if (!this.maxLineLength.HasValue)
            {
                this.maxLineLength = 40;
            }
            else if (this.maxLineLength > 76 || this.maxLineLength < 10)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(this.maxLineLength),
                    "Cannot specify a size smaller than 10 characters or larger than 76 characters");
            }

            if (this.figlet)
            {
                messageAsList = SplitFigletToLinesAsList(this.message);
            }
            else if (this.message.Length > this.maxLineLength)
            {
                messageAsList = SplitToLinesAsList(this.message, splitChar, (int)this.maxLineLength);
            }
            else if (this.message.Length < this.maxLineLength && this.message.IndexOfAny(splitChar) != -1)
            {
                messageAsList = SplitToLinesAsListShort(this.message);
            }

            if (this.message.Length > this.maxLineLength || messageAsList.Count > 1)
            {
                this.message = createLargeWordBubble(messageAsList, this.BubbleChars);
            }
            else
            {
                this.message = createSmallWordBubble(this.message, this.BubbleChars);
            }

            return this.message;
        }

        private static string createLargeWordBubble(IReadOnlyList<string> list, IBubbleChars bubbles)
        {
            var bubbleBuilder = new StringBuilder();
            var longestLineInList = list.Max(s => s.Length);
            var lengthOfTopAndBottomLinesInBubble = longestLineInList + 2;
            var topBubbleLine = $" {repeatCharacter(bubbles.TopLine, lengthOfTopAndBottomLinesInBubble)}";
            var bottomBubbleLine = $" {repeatCharacter(bubbles.BottomLine, lengthOfTopAndBottomLinesInBubble)}";
            var firstLineInMessageSpaces = repeatCharacter(' ', longestLineInList - list[0].Length + 1);
            var lastLineInMessageSpaces = repeatCharacter(' ', longestLineInList - list[list.Count - 1].Length + 1);

            bubbleBuilder.AppendLine(topBubbleLine);
            bubbleBuilder.AppendLine($"{bubbles.UpLeft} {list[0]}{firstLineInMessageSpaces}{bubbles.UpRight}");
            for (var i = 1; i < list.Count - 1; i++)
            {
                var numberofspaces = longestLineInList - list[i].Length;
                var spacesInLine = repeatCharacter(' ', numberofspaces + 1);

                bubbleBuilder.AppendLine($"{bubbles.Left} {list[i]}{spacesInLine}{bubbles.Right}");
            }

            bubbleBuilder.AppendLine(
                $"{bubbles.DownLeft} {list[list.Count - 1]}{lastLineInMessageSpaces}{bubbles.DownRight}");
            bubbleBuilder.AppendLine(bottomBubbleLine);

            return bubbleBuilder.ToString();
        }

        private static string createSmallWordBubble(string message, IBubbleChars bubbles)
        {
            var lengthOfMessage = message.Length;
            var lengthOfTopAndBottomLinesInBubble = lengthOfMessage + 2;
            var topBubbleLine = repeatCharacter(bubbles.TopLine, lengthOfTopAndBottomLinesInBubble);
            var bottomBubbleLine = repeatCharacter(bubbles.BottomLine, lengthOfTopAndBottomLinesInBubble);

            return
                $" {topBubbleLine} \r\n{bubbles.SmallLeft} {message.Trim()} {bubbles.SmallRight}\r\n {bottomBubbleLine}";
        }

        private static string repeatCharacter(char character, int numberOfUnderscores)
        {
            return new string(character, numberOfUnderscores);
        }

        private static List<string> SplitFigletToLinesAsList(string text)
        {
            var listToReturn = new List<string>();
            char[] newLines = { (char)10, (char)13 };
            var sb = new StringBuilder(text);

            while (sb.Length > 0)
            {
                try
                {
                    var indexOfFirstNewLine = sb.ToString().IndexOfAny(newLines);
                    listToReturn.Add(sb.ToString().Substring(0, indexOfFirstNewLine));
                    sb.Remove(0, indexOfFirstNewLine + 2);
                }
                catch
                {
                    listToReturn.Add(sb.ToString().Substring(0));
                    sb.Clear();
                }
            }

            return listToReturn;
        }

        private static List<string> SplitToLinesAsList(string message, char[] splitOnCharacters, int maxStringLength)
        {
            var listToReturn = new List<string>();
            var messageSb = new StringBuilder(message);
            char[] newLineCharacters = { (char)10, (char)13 };
            const int Index = 0;

            while (messageSb.ToString().Length > Index)
            {
                int splitAt;
                if (Index + maxStringLength <= messageSb.ToString().Length)
                {
                    var thisLine = messageSb.ToString().Substring(Index, maxStringLength);
                    if (thisLine.IndexOfAny(newLineCharacters) != -1)
                    {
                        if (thisLine.StartsWith(((char)10).ToString()) || thisLine.StartsWith(((char)13).ToString()))
                        {
                            splitAt = 1;
                        }
                        else
                        {
                            splitAt = thisLine.LastIndexOfAny(newLineCharacters);
                        }
                    }
                    else
                    {
                        splitAt = thisLine.LastIndexOf(' ');
                    }
                }
                else
                {
                    splitAt = messageSb.ToString().Length - Index;
                }

                splitAt = splitAt == -1 ? maxStringLength : splitAt;

                listToReturn.Add(messageSb.ToString().Substring(Index, splitAt).Trim());
                messageSb.Remove(Index, splitAt);
            }

            return listToReturn;
        }

        private static List<string> SplitToLinesAsListShort(string message)
        {
            var listToReturn = new List<string>();
            var sb = new StringBuilder(message);
            char[] splitChars = { (char)10, (char)13 };

            const int StartingIndex = 0;
            var lengthLeft = sb.ToString().Length;

            while (lengthLeft != 0)
            {
                var lengthIndex = sb.ToString().IndexOfAny(splitChars) != -1
                                      ? sb.ToString().IndexOfAny(splitChars)
                                      : sb.ToString().Length;

                listToReturn.Add(sb.ToString().Substring(StartingIndex, lengthIndex));
                if (sb.ToString().Length == lengthIndex)
                {
                    sb.Remove(StartingIndex, lengthIndex);
                }
                else
                {
                    var newLineChar = sb.ToString().Substring(lengthIndex, 2);
                    if (newLineChar == Environment.NewLine)
                    {
                        sb.Remove(StartingIndex, lengthIndex + 2);
                    }
                    else
                    {
                        sb.Remove(StartingIndex, lengthIndex + 1);
                    }
                }

                lengthLeft = sb.ToString().Length;
            }

            return listToReturn;
        }
    }
}