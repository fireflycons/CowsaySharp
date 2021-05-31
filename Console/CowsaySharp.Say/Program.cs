namespace CowsaySharp.Say
{
    using System;
    using System.IO;
    using System.Reflection;

    using CowsaySharp.ConsoleLibrary;
    using CowsaySharp.Library;

    class Program
    {
        static void Main(string[] args)
        {
            var options = Switches.ProcessSwitches(
                args,
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                new SayBubbleChars());

            if (options == null)
            {
                return;
            }

            var cow = new CowSay(options);
            cow.Render(Console.WriteLine);
        }
    }
}