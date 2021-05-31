namespace CowsaySharp.Library
{
    using System.IO;
    using System.Text;

    public class CowFile : AbstractCowFile, ICowFile
    {
        private readonly string path;

        public CowFile(string path)
        {
            this.path = path;
        }

        public StringBuilder GetCow()
        {
            using (var sr = new StreamReader(this.path))
            {
                return this.removeExtraCowLines(new StringBuilder(sr.ReadToEnd()));
            }
        }
    }
}