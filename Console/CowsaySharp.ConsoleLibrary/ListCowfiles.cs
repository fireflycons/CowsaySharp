namespace CowsaySharp.ConsoleLibrary
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using CowsaySharp.Library;

    public static class ListCowfiles
    {
        private const string CowSearchPattern = "*.cow";

        private const string CowsFolder = "cows";

        private static string CowFilesDirectory { get; set; }

        public static void ShowCowfiles(string directory, bool list)
        {
            CowFilesDirectory = $"{directory}\\{CowsFolder}";

            if (!ValidateDirectory.validate(CowFilesDirectory))
                throw new ArgumentException("Cow Files Path is not valid or not accessible", CowFilesDirectory);

            IList<string> cowfiles = Directory.GetFiles(CowFilesDirectory, CowSearchPattern);
            for (var i = 0; i < cowfiles.Count; i++)
            {
                var cowfile = Path.GetFileNameWithoutExtension(cowfiles[i]);
                cowfiles[i] = cowfile;
            }

            Console.WriteLine($"Cow files in {CowFilesDirectory}:");

            if (list)
                listInColumnsDown(cowfiles);
            else listInBunch(cowfiles);
        }

        private static void listInBunch(IEnumerable<string> cowfiles)
        {
            var bunchBuilder = new StringBuilder();
            foreach (var file in cowfiles) bunchBuilder.Append($"{file} ");

            Console.WriteLine(bunchBuilder.ToString().Trim());
        }

        private static void listInColumnsDown(IList<string> cowfiles)
        {
            var returnList = new List<string>();
            var fullList = new StringBuilder();
            const int NumberOfColumns = 3;
            var columnSize = (short)cowfiles.Max(s => s.Length) + 2;
            var numberOfFiles = cowfiles.Count;
            var numberOfLines = (numberOfFiles - (numberOfFiles % NumberOfColumns)) / NumberOfColumns + 1;

            for (int currentIndexOfFile = 0, currentRowOfColulmn = 0, currentColumn = 0;
                 currentColumn < NumberOfColumns && currentIndexOfFile < numberOfFiles;
                 currentIndexOfFile++, currentRowOfColulmn++)
            {
                var sb = new StringBuilder();
                var file = cowfiles[currentIndexOfFile];
                var toAppend = string.Format($"{{0,-{columnSize}}}", file);

                if (currentColumn == 0)
                {
                    sb.Append(toAppend);
                    returnList.Add(sb.ToString());
                }
                else
                {
                    sb.Append(returnList[currentRowOfColulmn - 1]);
                    sb.Append(toAppend);
                    returnList[currentRowOfColulmn - 1] = sb.ToString();
                }

                if (currentRowOfColulmn == numberOfLines - 1 && currentColumn == 0)
                {
                    currentColumn++;
                    currentRowOfColulmn = 0;
                }
                else if (currentRowOfColulmn == numberOfLines)
                {
                    currentColumn++;
                    currentRowOfColulmn = 0;
                }
            }

            foreach (var item in returnList)
            {
                fullList.AppendLine(item);
            }

            Console.WriteLine(fullList.ToString().Trim());
        }
    }
}