namespace CowsaySharp.Library
{
    using System.Collections.Generic;

    public static class CowFaces
    {
        private static readonly Dictionary<FaceTypes, ICowFace> FaceDictionary = new Dictionary<FaceTypes, ICowFace>
                                                                                    {
                                                                                        { FaceTypes.DefaultFace, new DefaultCowFace() },
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

        public static ICowFace GetCowFace(FaceTypes face)
        {
            FaceDictionary.TryGetValue(face, out var value);

            return value;
        }
    }
}