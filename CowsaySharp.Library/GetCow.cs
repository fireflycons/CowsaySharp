namespace CowsaySharp.Library
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public static class GetCow
    {
        private static StringBuilder cow;

        public static string ReturnCow(string cowFile, IBubbleChars bubbles, CowFace face)
        {
            var sr = new StreamReader(cowFile);
            cow = new StringBuilder(sr.ReadToEnd());
            var threeEyes = cow.ToString().Contains("($extra x 2)");

            cow = removeExtraCowLines(cow);

            cow.Replace("\\\\", "\\");

            cow.Replace("$thoughts", bubbles.Bubble);
            if (threeEyes)
            {
                var eyesForReplacement = new string(face.Eyes[0], 3);
                cow.Replace("$eyes", eyesForReplacement);
                cow.Replace("${eyes}", eyesForReplacement);
            }
            else
            {
                var eyesForReplacement = face.Eyes;
                cow.Replace("$eyes", eyesForReplacement);
                cow.Replace("${eyes}", eyesForReplacement);
            }

            cow.Replace("$tongue", face.Tongue);

            if (cow.ToString().Substring(0, 1) == "\n")
            {
                cow.Remove(0, 1);
            }

            return cow.ToString().TrimEnd();
        }

        private static StringBuilder removeExtraCowLines(StringBuilder cowBuilder)
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
    }
}