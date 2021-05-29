namespace CowsaySharp.Common
{
    using System.IO;

    static public class ValidateFile
    {
        static public bool validate(string file)
        {
            try
            {
                var fileAccess = File.GetAccessControl(file);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}