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
            var cowResources = CowResource.ListCowResources();

            Console.WriteLine("Built-in cow files:");

            if (list)
            {
                listInColumnsDown(cowResources.ToList());
            }
            else
            {
                listInBunch(cowResources);
            }

            CowFilesDirectory = $"{directory}\\{CowsFolder}";
            if (!ValidateDirectory.validate(CowFilesDirectory))
            {
                Console.WriteLine("Cow Files Path is not valid or not accessible");
                return;
            }

            var cowfiles = Directory.EnumerateFiles(CowFilesDirectory, CowSearchPattern)
                .Select(Path.GetFileNameWithoutExtension).ToList();

            if (!cowfiles.Any())
            {
                return;
            }

            Console.WriteLine($"Cow files in {CowFilesDirectory}:");

            if (list)
            {
                listInColumnsDown(cowfiles);
            }
            else
            {
                listInBunch(cowfiles);
            }
        }

        private static void listInBunch(IEnumerable<string> cowfiles)
        {
            var bunchBuilder = new StringBuilder();
            foreach (var file in cowfiles)
            {
                bunchBuilder.Append($"{file} ");
            }

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

            for (int currentIndexOfFile = 0, currentRowOfColumn = 0, currentColumn = 0;
                 currentColumn < NumberOfColumns && currentIndexOfFile < numberOfFiles;
                 currentIndexOfFile++, currentRowOfColumn++)
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
                    sb.Append(returnList[currentRowOfColumn - 1]);
                    sb.Append(toAppend);
                    returnList[currentRowOfColumn - 1] = sb.ToString();
                }

                if (currentRowOfColumn == numberOfLines - 1 && currentColumn == 0)
                {
                    currentColumn++;
                    currentRowOfColumn = 0;
                }
                else if (currentRowOfColumn == numberOfLines)
                {
                    currentColumn++;
                    currentRowOfColumn = 0;
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