namespace CowsaySharp.Library
{
    using System.IO;

    public static class ValidateFile
    {
        public static bool validate(string file)
        {
            try
            {
                File.GetAttributes(file);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}