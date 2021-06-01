namespace CowsaySharp.Library
{
    using System.Text;

    /// <summary>
    /// Interface describing how to load a cow file.
    /// </summary>
    public interface ICowFile
    {
        /// <summary>
        /// Gets the cow.
        /// </summary>
        /// <returns>A <see cref="StringBuilder"/> containing the cow file content.</returns>
        StringBuilder GetCow();
    }
}