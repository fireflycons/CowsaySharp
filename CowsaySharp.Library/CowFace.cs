namespace CowsaySharp.Library
{
    /// <summary>
    /// Defines the cow's face
    /// </summary>
    /// <seealso cref="CowsaySharp.Library.ICowFace" />
    public class CowFace : ICowFace
    {
        private readonly bool threeEyes;

        private string eyes;

        private string tongue;

        /// <summary>
        /// Initializes a new instance of the <see cref="CowFace"/> class.
        /// </summary>
        public CowFace()
            : this(null, null, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CowFace"/> class.
        /// </summary>
        /// <param name="cowEyes">The cow eyes.</param>
        public CowFace(string cowEyes)
            : this(cowEyes, null, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CowFace"/> class.
        /// </summary>
        /// <param name="cowEyes">The cow eyes.</param>
        /// <param name="cowTongue">The cow tongue.</param>
        public CowFace(string cowEyes, string cowTongue)
            : this(cowEyes, cowTongue, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CowFace"/> class.
        /// </summary>
        /// <param name="cowEyes">The cow eyes.</param>
        /// <param name="cowTongue">The cow tongue.</param>
        /// <param name="threeEyes">if set to <c>true</c> [three eyes].</param>
        public CowFace(string cowEyes, string cowTongue, bool threeEyes)
        {
            this.Eyes = cowEyes;
            this.Tongue = cowTongue;
            this.threeEyes = threeEyes;
        }

        /// <summary>
        /// Gets or sets the eyes.
        /// </summary>
        /// <value>
        /// The eyes.
        /// </value>
        public string Eyes
        {
            get => this.eyes;

            set => this.eyes = value?.Substring(0, this.threeEyes ? 3 : 2);
        }

        /// <summary>
        /// Gets or sets the tongue.
        /// </summary>
        /// <value>
        /// The tongue.
        /// </value>
        public string Tongue
        {
            get => this.tongue;

            set => this.tongue = string.IsNullOrEmpty(value) ? "  " : value.Substring(0, 2);
        }
    }
}