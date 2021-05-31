namespace CowsaySharp.ConsoleLibrary
{
    using System;

    internal static class Help
    {
        public static void DisplayHelp()
        {
            var help = $@"CowsaySharp, (c) 2016 Terry Trent

  The original idea of cowsay come from:
    [Tony Monroe](http://www.nog.net/~tony/)
    [cowsay](https://github.com/schacon/cowsay)

Usage: cowsay [-bdgpstwy] [-h] [-e eyes] [-f cowfile] 
          [-l] [-L] [-n] [-T tongue] [-W wrapcolumn] [message]";

            Console.WriteLine(help);
        }
    }
}