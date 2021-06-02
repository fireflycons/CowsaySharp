namespace CowsaySharp.Library
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    using CowsaySharp.Library.cows;

    /// <summary>
    /// <see cref="ICowFile"/> implementation that loads the cow definition from embedded resources.
    /// </summary>
    /// <seealso cref="CowsaySharp.Library.AbstractCowFile" />
    /// <seealso cref="CowsaySharp.Library.ICowFile" />
    public class CowResource : AbstractCowFile, ICowFile
    {
        private static readonly Assembly ThisAssembly = Assembly.GetExecutingAssembly();

        private readonly string resourceName;

        /// <summary>
        /// Initializes a new instance of the <see cref="CowResource"/> class.
        /// </summary>
        /// <param name="resourceName">Name of the cow resource.</param>
        public CowResource(string resourceName)
        {
            this.resourceName = resourceName;
        }

        /// <summary>
        /// Lists the cow resources stored as embedded resources in this library.
        /// </summary>
        /// <returns>List of cow names.</returns>
        public static IEnumerable<string> ListCowResources()
        {
            var ns = typeof(ICowLocator).Namespace;

            return ThisAssembly.GetManifestResourceNames()
                .Select(r => r.Substring(ns.Length + 1, r.Length - ns.Length - 5));
        }

        /// <summary>
        /// Gets the cow file from embedded resources.
        /// </summary>
        /// <returns>A <see cref="StringBuilder"/> containing the cow file content.</returns>
        public StringBuilder GetCow()
        {
            using (var sr = new StreamReader(ThisAssembly.GetManifestResourceStream(this.resourceName)))
            {
                return this.RemoveExtraCowLines(new StringBuilder(sr.ReadToEnd()));
            }
        }
    }
}