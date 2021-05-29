﻿namespace CowsaySharp.ConsoleLibrary
{
    using System;
    using System.IO;

    using CowsaySharp.Common;

    public class TestCowFile
    {
        public bool breakOut;

        public bool cowProcessing;

        public TestCowFile(ref string cowSpecified, string cowFileLocation)
        {
            string directory;

            if (cowSpecified.Contains("\\"))
            {
                string cowFile = cowSpecified.Substring(cowSpecified.LastIndexOf('\\') + 1);

                if (cowSpecified.Substring(0, 1) == "\\" && cowSpecified.Substring(0, 2) != "\\\\")
                {
                    directory =
                        $"{Directory.GetCurrentDirectory()}{cowSpecified.Substring(0, cowSpecified.IndexOf(cowFile))}";
                    cowSpecified = $"{directory}{cowFile}";
                }
                else
                    directory = cowSpecified.Substring(0, cowSpecified.LastIndexOf('\\'));

                if (!ValidateDirectory.validate(directory))
                {
                    Console.WriteLine(
                        $"The directory you specified is either invalid or cannot be accessed:\n{directory}");
                    this.breakOut = true;
                    this.cowProcessing = false;
                }

                if (cowFile.Length == 0 && !this.breakOut)
                {
                    Console.WriteLine($"You specified a directory but did not specify a Cow File.");
                    this.breakOut = true;
                    this.cowProcessing = false;
                }
                else if (!cowFile.EndsWith(".cow"))
                    cowSpecified += ".cow";
            }
            else
            {
                cowSpecified = $"{cowFileLocation}\\{cowSpecified}";

                if (!cowSpecified.EndsWith(".cow"))
                    cowSpecified = $"{cowSpecified}.cow";
            }

            if (!this.breakOut && !ValidateFile.validate(cowSpecified))
            {
                Console.WriteLine($"The Cow File you specified does not exist or cannot be accessed:\n{cowSpecified}");
                this.breakOut = true;
                this.cowProcessing = false;
            }
        }
    }
}