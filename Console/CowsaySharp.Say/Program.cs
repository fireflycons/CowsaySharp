namespace CowsaySharp.Say
{
    using System.IO;
    using System.Reflection;

    using CowsaySharp.Common;
    using CowsaySharp.ConsoleLibrary;

    class Program
    {
        static void Main(string[] args)
        {
            Switches.processSwitches(
                args,
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                new SayBubbleChars());
        }
    }
}