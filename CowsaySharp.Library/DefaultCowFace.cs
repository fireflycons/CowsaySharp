namespace CowsaySharp.Library
{
    public class DefaultCowFace : ICowFace
    {
        private string eyes = "oo";

        private string tongue = "  ";

        public string Eyes
        {
            get => this.eyes;
            set => this.eyes = value;
        }

        public string Tongue
        {
            get => this.tongue;
            set => this.tongue = value;
        }
    }
}