namespace CowsaySharp.Library
{
    using System.Text;

    public static class GetCow
    {
        public static string ReturnCow(ICowFile cowFile, IBubbleChars bubbles, CowFace face)
        {
            var cow = cowFile.GetCow();
            var threeEyes = cow.ToString().Contains("($extra x 2)");

            cow.Replace("\\\\", "\\").Replace("$thoughts", bubbles.Bubble);

            if (threeEyes)
            {
                var eyesForReplacement = new string(face.Eyes[0], 3);
                cow.Replace("$eyes", eyesForReplacement).Replace("${eyes}", eyesForReplacement);
            }
            else
            {
                var eyesForReplacement = face.Eyes;
                cow.Replace("$eyes", eyesForReplacement).Replace("${eyes}", eyesForReplacement);
            }

            cow.Replace("$tongue", face.Tongue);

            if (cow.ToString().Substring(0, 1) == "\n")
            {
                cow.Remove(0, 1);
            }

            return cow.ToString().TrimEnd();
        }
    }
}