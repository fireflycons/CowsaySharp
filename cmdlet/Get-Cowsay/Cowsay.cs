namespace CowsaySharp.GetCowsay.Containers
{
    using System;

    class Cowsay
    {
        public Cowsay(string cow, string speechBubble)
        {
            this.Speech_Bubble = speechBubble;
            this.Cow = cow;
            this.SpeechBubbleAndCow = $"{this.Speech_Bubble}{Environment.NewLine}{this.Cow}";
        }

        public string Cow { get; set; }

        public string Speech_Bubble { get; set; }

        public string SpeechBubbleAndCow { get; set; }
    }
}