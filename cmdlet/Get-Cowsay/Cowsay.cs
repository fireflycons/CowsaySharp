namespace CowsaySharp.GetCowsay
{
    using System;

    internal class Cowsay
    {
        public Cowsay(string cow, string speechBubble)
        {
            this.SpeechBubble = speechBubble;
            this.Cow = cow;
            this.SpeechBubbleAndCow = $"{this.SpeechBubble}{Environment.NewLine}{this.Cow}";
        }

        public string Cow { get; }

        public string SpeechBubble { get; }

        public string SpeechBubbleAndCow { get; }
    }
}