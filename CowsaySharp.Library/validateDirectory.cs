namespace CowsaySharp.Library
{
    using System.IO;

    public static class ValidateDirectory
    {
        public static bool validate(string directory)
        {
            try
            {
                Directory.GetCreationTime(directory);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}