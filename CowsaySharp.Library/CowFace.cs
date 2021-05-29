namespace CowsaySharp.Library
{
    using System;

    public class CowFace
    {
        private string eyes;

        private bool ThreeEyes;

        private string tongue;

        public CowFace()
            : this(null, null, false)
        {
        }

        public CowFace(string cowEyes)
            : this(cowEyes, null, false)
        {
        }

        public CowFace(string cowEyes, string cowTongue)
            : this(cowEyes, cowTongue, false)
        {
        }

        public CowFace(string cowEyes, string cowTongue, bool threeEyes)
        {
            this.Eyes = cowEyes;
            this.Tongue = cowTongue;
            this.ThreeEyes = threeEyes;
        }

        public string Eyes
        {
            get
            {
                return this.eyes;
            }

            set
            {
                if (this.ThreeEyes) this.eyes = value?.Substring(0, 3);
                else this.eyes = value?.Substring(0, 2);
            }
        }

        public string Tongue
        {
            get
            {
                return this.tongue;
            }

            set
            {
                if (string.IsNullOrEmpty(value)) this.tongue = "  ";
                else this.tongue = value.Substring(0, 2);
            }
        }
    }
}