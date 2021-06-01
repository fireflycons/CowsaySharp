namespace CowsaySharp.Library
{
    /// <summary>
    /// The default face. This has its own type to make it easier to test for whether the default face is set during argument processing
    /// </summary>
    /// <seealso cref="CowsaySharp.Library.ICowFace" />
    public class DefaultCowFace : ICowFace
    {
        private string eyes = "oo";

        private string tongue = "  ";

        /// <summary>
        /// Gets or sets the eyes.
        /// </summary>
        /// <value>
        /// The eyes.
        /// </value>
        public string Eyes
        {
            get => this.eyes;
            set => this.eyes = value;
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
            set => this.tongue = value;
        }
    }
}