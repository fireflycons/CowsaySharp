namespace CowsaySharp.Library
{
    public class CowFace
    {
        private readonly bool threeEyes;

        private string eyes;

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
            this.threeEyes = threeEyes;
        }

        public string Eyes
        {
            get => this.eyes;

            set => this.eyes = value?.Substring(0, this.threeEyes ? 3 : 2);
        }

        public string Tongue
        {
            get => this.tongue;

            set => this.tongue = string.IsNullOrEmpty(value) ? "  " : value.Substring(0, 2);
        }
    }
}