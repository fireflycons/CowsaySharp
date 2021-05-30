namespace CowsaySharp.GetCowsay
{
    using System;
    using System.IO;
    using System.Management.Automation;
    using System.Reflection;

    using CowsaySharp.ConsoleLibrary;
    using CowsaySharp.Library;

    [Cmdlet(VerbsCommon.Get, "Cowsay")]
    [OutputType(typeof(Cowsay))]
    public class GetCowsayCmdlet : Cmdlet
    {
        bool breakOut;

        IBubbleChars bubbleChars;

        string cowFileLocation;

        string cowSpecified;

        CowFace face;

        string moduleDirectory;

        [Parameter]
        [Alias("f")]
        public string Cowfile { get; set; }

        [Parameter]
        [Alias("e")]
        public string Eyes { get; set; }

        [Parameter]
        [Alias("n")]
        public SwitchParameter Figlet { get; set; }

        [Parameter]
        [Alias("l")]
        public SwitchParameter List { get; set; }

        [Parameter(ValueFromPipeline = true, Position = 0)]
        public string Message { get; set; }

        [Parameter]
        [ValidateSet("borg", "dead", "greedy", "paranoid", "stoned", "tired", "wired", "young")]
        [Alias("m")]
        public string Mode { get; set; }

        [Parameter]
        public SwitchParameter Think { get; set; }

        [Parameter]
        [Alias("T")]
        public string Tongue { get; set; }

        [Parameter]
        [Alias("W")]
        public int Wrapcolumn { get; set; } = 40;

        protected override void BeginProcessing()
        {
            this.moduleDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            this.cowFileLocation = $"{this.moduleDirectory}\\cows";
            this.cowSpecified = $"{this.cowFileLocation}\\default.cow";
            this.face = new CowFace();

            if (!string.IsNullOrEmpty(this.Mode))
            {
                switch (this.Mode)
                {
                    case "borg":
                        this.face = CowFaces.GetCowFace(CowFaces.FaceTypes.Borg);
                        break;
                    case "dead":
                        this.face = CowFaces.GetCowFace(CowFaces.FaceTypes.Dead);
                        break;
                    case "greedy":
                        this.face = CowFaces.GetCowFace(CowFaces.FaceTypes.Greedy);
                        break;
                    case "paranoid":
                        this.face = CowFaces.GetCowFace(CowFaces.FaceTypes.Paranoid);
                        break;
                    case "stoned":
                        this.face = CowFaces.GetCowFace(CowFaces.FaceTypes.Stoned);
                        break;
                    case "tired":
                        this.face = CowFaces.GetCowFace(CowFaces.FaceTypes.Tired);
                        break;
                    case "wired":
                        this.face = CowFaces.GetCowFace(CowFaces.FaceTypes.Wired);
                        break;
                    case "young":
                        this.face = CowFaces.GetCowFace(CowFaces.FaceTypes.Young);
                        break;
                }
            }

            if (!string.IsNullOrEmpty(this.Eyes) && string.IsNullOrWhiteSpace(this.face.Eyes))
            {
                this.face = new CowFace(this.Eyes);
            }

            if (string.IsNullOrEmpty(this.face.Eyes))
            {
                this.face = CowFaces.GetCowFace(CowFaces.FaceTypes.DefaultFace);
            }

            if (!string.IsNullOrEmpty(this.Tongue) && string.IsNullOrWhiteSpace(this.face.Tongue))
            {
                this.face.Tongue = this.Tongue;
            }

            if (!string.IsNullOrEmpty(this.Cowfile))
            {
                this.cowSpecified = this.Cowfile;
                var testCowFile = new TestCowFile(ref this.cowSpecified, this.cowFileLocation);
                this.breakOut = testCowFile.BreakOut;
            }
            else
            {
                var testCowFile = new TestCowFile(ref this.cowSpecified, this.cowFileLocation);
                this.breakOut = testCowFile.BreakOut;
            }

            if (this.Wrapcolumn < 10 | this.Wrapcolumn > 76)
            {
                this.ThrowTerminatingError(
                    new ErrorRecord(
                        new ArgumentOutOfRangeException(
                            nameof(this.Wrapcolumn),
                            "Cannot specify a size smaller than 10 characters or larger than 76 characters"),
                        "E1",
                        ErrorCategory.LimitsExceeded,
                        this));
            }

            if (this.Think)
            {
                this.bubbleChars = new ThinkBubbleChars();
            }
            else
            {
                this.bubbleChars = new SayBubbleChars();
            }
        }

        protected override void ProcessRecord()
        {
            if (this.List)
            {
                Console.WriteLine();
                ListCowfiles.ShowCowfiles(this.moduleDirectory, list: true);
                Console.WriteLine();
                this.breakOut = true;
            }
            else if (string.IsNullOrWhiteSpace(this.Message))
            {
                Console.WriteLine();
            }
            else if (!this.breakOut)
            {
                this.WriteObject(this.BuildCowsay());
            }
        }

        private Cowsay BuildCowsay()
        {
            return new Cowsay(
                GetCow.ReturnCow(this.cowSpecified, this.bubbleChars, this.face),
                SpeechBubble.ReturnSpeechBubble(this.Message, this.bubbleChars, this.Wrapcolumn, this.Figlet));
        }
    }
}