namespace CowsaySharp.Library
{
    using System;

    public class CowSay
    {
        private readonly SpeechBubble bubble;

        private readonly ICowFile cowFile;

        private readonly ICowFace face;

        /// <summary>
        /// Initializes a new instance of the <see cref="CowSay"/> class.
        /// </summary>
        /// <param name="cowFile">The cow file.</param>
        /// <param name="bubble">The bubble.</param>
        /// <param name="face">The face.</param>
        public CowSay(ICowFile cowFile, SpeechBubble bubble, ICowFace face)
        {
            this.face = face;
            this.bubble = bubble;
            this.cowFile = cowFile;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CowSay"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public CowSay(Options options)
        {
            this.face = options.Face;
            this.bubble = new SpeechBubble(options.Message, options.BubbleChars, options.Width, options.IsFiglet);
            this.cowFile = AbstractCowFile.GetCowFile(options.CowFile);
        }

        /// <summary>
        /// Gets the cow.
        /// </summary>
        /// <value>
        /// The cow.
        /// </value>
        public string Cow
        {
            get
            {
                var cow = this.cowFile.GetCow();
                var threeEyes = cow.ToString().Contains("($extra x 2)");

                cow.Replace("\\\\", "\\").Replace("$thoughts", this.bubble.BubbleChars.Bubble);

                if (threeEyes)
                {
                    var eyesForReplacement = new string(this.face.Eyes[0], 3);
                    cow.Replace("$eyes", eyesForReplacement).Replace("${eyes}", eyesForReplacement);
                }
                else
                {
                    var eyesForReplacement = this.face.Eyes;
                    cow.Replace("$eyes", eyesForReplacement).Replace("${eyes}", eyesForReplacement);
                }

                cow.Replace("$tongue", this.face.Tongue);

                if (cow.ToString().Substring(0, 1) == "\n")
                {
                    cow.Remove(0, 1);
                }

                return cow.ToString().TrimEnd();
            }
        }

        /// <summary>
        /// Gets the speech bubble.
        /// </summary>
        /// <value>
        /// The speech bubble.
        /// </value>
        public string SpeechBubble => this.bubble.ReturnSpeechBubble();

        /// <summary>
        /// Gets the speech bubble and cow.
        /// </summary>
        /// <value>
        /// The speech bubble and cow.
        /// </value>
        public string SpeechBubbleAndCow => this.SpeechBubble + Environment.NewLine + this.Cow;

        /// <summary>
        /// Renders the specified output action.
        /// </summary>
        /// <param name="outputAction">The output action.</param>
        public void Render(Action<string> outputAction)
        {
            outputAction(this.SpeechBubbleAndCow);
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.SpeechBubbleAndCow;
        }
    }
}