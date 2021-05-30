namespace CowsaySharp.Think
{
    using System.IO;
    using System.Reflection;

    using CowsaySharp.ConsoleLibrary;
    using CowsaySharp.Library;

    class Program
    {
        static void Main(string[] args)
        {
            Switches.processSwitches(
                args,
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                new ThinkBubbleChars());
        }
    }
}