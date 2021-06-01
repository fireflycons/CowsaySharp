namespace CowsaySharp.Library
{
    using System.IO;
    using System.Text;

    /// <summary>
    /// <see cref="ICowFile"/> implementation that loads the cow definition from the file system.
    /// </summary>
    /// <seealso cref="CowsaySharp.Library.AbstractCowFile" />
    /// <seealso cref="CowsaySharp.Library.ICowFile" />
    public class CowFile : AbstractCowFile, ICowFile
    {
        private readonly string path;

        /// <summary>
        /// Initializes a new instance of the <see cref="CowFile"/> class.
        /// </summary>
        /// <param name="path">The path to the cow file.</param>
        public CowFile(string path)
        {
            this.path = path;
        }

        /// <summary>
        /// Gets the cow.
        /// </summary>
        /// <returns>A <see cref="StringBuilder"/> containing the cow file content.</returns>
        public StringBuilder GetCow()
        {
            using (var sr = new StreamReader(this.path))
            {
                return this.RemoveExtraCowLines(new StringBuilder(sr.ReadToEnd()));
            }
        }
    }
}