namespace CowsaySharp.ConsoleLibrary
{
    using System;
    using System.IO;

    using CowsaySharp.Library;

    public class TestCowFile
    {
        public TestCowFile(ref string cowSpecified, string cowFileLocation)
        {
            if (cowSpecified.Contains("\\"))
            {
                var cowFile = cowSpecified.Substring(cowSpecified.LastIndexOf('\\') + 1);

                string directory;
                if (cowSpecified.Substring(0, 1) == "\\" && cowSpecified.Substring(0, 2) != "\\\\")
                {
                    directory =
                        $"{Directory.GetCurrentDirectory()}{cowSpecified.Substring(0, cowSpecified.IndexOf(cowFile, StringComparison.Ordinal))}";
                    cowSpecified = $"{directory}{cowFile}";
                }
                else
                {
                    directory = cowSpecified.Substring(0, cowSpecified.LastIndexOf('\\'));
                }

                if (!ValidateDirectory.validate(directory))
                {
                    Console.WriteLine(
                        $"The directory you specified is either invalid or cannot be accessed:\n{directory}");
                    this.BreakOut = true;
                    this.CowProcessing = false;
                }

                if (cowFile.Length == 0 && !this.BreakOut)
                {
                    Console.WriteLine("You specified a directory but did not specify a Cow File.");
                    this.BreakOut = true;
                    this.CowProcessing = false;
                }
                else if (!cowFile.EndsWith(".cow"))
                {
                    cowSpecified += ".cow";
                }
            }
            else
            {
                cowSpecified = $"{cowFileLocation}\\{cowSpecified}";

                if (!cowSpecified.EndsWith(".cow"))
                {
                    cowSpecified = $"{cowSpecified}.cow";
                }
            }

            if (!this.BreakOut && !ValidateFile.validate(cowSpecified))
            {
                Console.WriteLine($"The Cow File you specified does not exist or cannot be accessed:\n{cowSpecified}");
                this.BreakOut = true;
                this.CowProcessing = false;
            }
        }

        public bool BreakOut { get; }

        public bool CowProcessing { get; }
    }
}