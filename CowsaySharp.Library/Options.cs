namespace CowsaySharp.Library
{
    using System;
    using System.Text;

    public class Options
    {
        public string CowFile { get; set; }

        public bool IsFiglet { get; set; }

        public int Width { get; set; } = 40;

        public ICowFace Face { get; set; } = CowFaces.GetCowFace(CowFaces.FaceTypes.DefaultFace);

        public string Message { get; set; }

        public IBubbleChars BubbleChars { get; set; }
    }
}