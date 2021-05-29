namespace CowsaySharp.Common
{
    using System.IO;

    public static class ValidateDirectory
    {
        public static bool validate(string directory)
        {
            try
            {
                Directory.GetAccessControl(directory);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}