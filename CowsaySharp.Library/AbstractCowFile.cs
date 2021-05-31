namespace CowsaySharp.Library
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public abstract class AbstractCowFile
    {
        protected StringBuilder removeExtraCowLines(StringBuilder cowBuilder)
        {
            var cowString = cowBuilder.ToString();
            var cowToReturn = new StringBuilder();
            var cowList = new List<string>();

            while (cowString.Length > 0)
            {
                var sub = cowString.Substring(0, cowString.IndexOf("\n", StringComparison.Ordinal));

                cowList.Add(sub);

                cowBuilder.Remove(0, cowString.IndexOf("\n", StringComparison.Ordinal) + 1);
                cowString = cowBuilder.ToString();
            }

            foreach (var line in cowList.Where(
                line => !(line.StartsWith("#") || line.StartsWith("$") || line.StartsWith("EOC"))))
            {
                cowToReturn.Append(line + Environment.NewLine);
            }

            return cowToReturn;
        }
        
        public static ICowFile GetCowFile(string name)
        {
            var resources = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            var cowResource = resources
                .FirstOrDefault(n => n.EndsWith($"{name}.cow", StringComparison.OrdinalIgnoreCase));

            if (cowResource != null)
            {
                return new CowResource(cowResource);
            }

            var cowFile = Path.Combine(
                Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),
                "cows",
                $"{name}.cow");

            try
            {
                File.GetAccessControl(cowFile);
                return new CowFile(cowFile);
            }
            catch
            {
                throw new FileNotFoundException($"Cannot locate {name}.cow in resources or filesystem.");
            }
        }
    }
}