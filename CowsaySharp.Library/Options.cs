namespace CowsaySharp.Library
{
    /// <summary>
    /// Options as returned by command line processors, or created by library callee.
    /// </summary>
    public class Options
    {
        /// <summary>
        /// Gets or sets the bubble chars (say or think).
        /// </summary>
        /// <value>
        /// The bubble chars.
        /// </value>
        public IBubbleChars BubbleChars { get; set; }

        /// <summary>
        /// Gets or sets the name of the cow file to use.
        /// </summary>
        /// <value>
        /// The cow file.
        /// </value>
        public string CowFile { get; set; }

        /// <summary>
        /// Gets or sets the face.
        /// </summary>
        /// <value>
        /// The face.
        /// </value>
        public ICowFace Face { get; set; } = CowFaces.GetCowFace(CowFaces.FaceTypes.DefaultFace);

        /// <summary>
        /// Gets or sets a value indicating whether this instance is a figlet.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is a figlet; otherwise, <c>false</c>.
        /// </value>
        public bool IsFiglet { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the column width of the message.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public int Width { get; set; } = 40;
    }
}