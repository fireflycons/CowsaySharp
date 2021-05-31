namespace CowsaySharp.Library
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    using CowsaySharp.Library.cows;

    public class CowResource : AbstractCowFile, ICowFile
    {
        private static readonly Assembly ThisAssembly = Assembly.GetExecutingAssembly();

        private readonly string resouceName;

        public CowResource(string resouceName)
        {
            this.resouceName = resouceName;
        }

        public StringBuilder GetCow()
        {
            using (var sr =
                new StreamReader(ThisAssembly.GetManifestResourceStream(this.resouceName)))
            {
                return this.removeExtraCowLines(new StringBuilder(sr.ReadToEnd()));
            }
        }

        public static IEnumerable<string> ListCowResources()
        {
            var ns = typeof(IResourceLocator).Namespace;

            return ThisAssembly.GetManifestResourceNames().Select(r => r.Substring(ns.Length + 1, r.Length - ns.Length - 5));
        }
    }
}