namespace CowsaySharp.Library
{
    using System.Collections.Generic;

    public static class CowFaces
    {
        private static readonly Dictionary<FaceTypes, CowFace> FaceDictionary = new Dictionary<FaceTypes, CowFace>
                                                                                    {
                                                                                        { FaceTypes.DefaultFace, new CowFace("oo") },
                                                                                        { FaceTypes.Borg, new CowFace("==") },
                                                                                        { FaceTypes.Dead, new CowFace("xx", "U ") },
                                                                                        { FaceTypes.Greedy, new CowFace("$$") },
                                                                                        { FaceTypes.Paranoid, new CowFace("@@") },
                                                                                        { FaceTypes.Stoned, new CowFace("**", "U ") },
                                                                                        { FaceTypes.Tired, new CowFace("__") },
                                                                                        { FaceTypes.Wired, new CowFace("OO") },
                                                                                        { FaceTypes.Young, new CowFace("..") },
                                                                                    };

        public enum FaceTypes
        {
            DefaultFace,

            Borg,

            Dead,

            Greedy,

            Paranoid,

            Stoned,

            Tired,

            Wired,

            Young
        }

        public static CowFace GetCowFace(FaceTypes face)
        {
            FaceDictionary.TryGetValue(face, out var value);

            return value;
        }
    }
}