namespace CowsaySharp.Library
{
    /// <summary>
    /// Interface describing cow's face
    /// </summary>
    public interface ICowFace
    {
        /// <summary>
        /// Gets or sets the eyes.
        /// </summary>
        /// <value>
        /// The eyes.
        /// </value>
        string Eyes { get; set; }

        /// <summary>
        /// Gets or sets the tongue.
        /// </summary>
        /// <value>
        /// The tongue.
        /// </value>
        string Tongue { get; set; }
    }
}