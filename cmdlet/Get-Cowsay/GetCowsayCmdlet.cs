namespace CowsaySharp.GetCowsay
{
    using System;
    using System.IO;
    using System.Management.Automation;
    using System.Reflection;

    using CowsaySharp.Common;
    using CowsaySharp.ConsoleLibrary;
    using CowsaySharp.GetCowsay.Containers;
    using CowsaySharp.Library;

    [Cmdlet(VerbsCommon.Get, "Cowsay")]
    [OutputType(typeof(Cowsay))]
    public class GetCowsayCmdlet : Cmdlet
    {
        private int _wrapcolumn = 40;

        bool breakOut;

        IBubbleChars bubbleChars;

        string cowFileLocation;

        string cowSpecified;

        CowFace face;

        string moduleDirectory;

        [Parameter]
        [Alias("f")]
        public string cowfile { get; set; }

        [Parameter]
        [Alias("e")]
        public string eyes { get; set; }

        [Parameter]
        [Alias("n")]
        public SwitchParameter figlet { get; set; }

        [Parameter]
        [Alias("l")]
        public SwitchParameter list { get; set; }

        [Parameter(ValueFromPipeline = true, Position = 0)]
        public string message { get; set; }

        [Parameter]
        [ValidateSet("borg", "dead", "greedy", "paranoid", "stoned", "tired", "wired", "young")]
        [Alias("m")]
        public string mode { get; set; }

        [Parameter]
        public SwitchParameter think { get; set; }

        [Parameter]
        [Alias("T")]
        public string tongue { get; set; }

        [Parameter]
        [Alias("W")]
        public int wrapcolumn
        {
            get
            {
                return this._wrapcolumn;
            }

            set
            {
                this._wrapcolumn = value;
            }
        }

        protected override void BeginProcessing()
        {
            this.moduleDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            this.cowFileLocation = $"{this.moduleDirectory}\\cows";
            this.cowSpecified = $"{this.cowFileLocation}\\default.cow";
            this.face = new CowFace();

            if (!string.IsNullOrEmpty(this.mode))
            {
                switch (this.mode)
                {
                    case "borg":
                        this.face = CowFaces.GetCowFace(CowFaces.cowFaces.borg);
                        break;
                    case "dead":
                        this.face = CowFaces.GetCowFace(CowFaces.cowFaces.dead);
                        break;
                    case "greedy":
                        this.face = CowFaces.GetCowFace(CowFaces.cowFaces.greedy);
                        break;
                    case "paranoid":
                        this.face = CowFaces.GetCowFace(CowFaces.cowFaces.paranoid);
                        break;
                    case "stoned":
                        this.face = CowFaces.GetCowFace(CowFaces.cowFaces.stoned);
                        break;
                    case "tired":
                        this.face = CowFaces.GetCowFace(CowFaces.cowFaces.tired);
                        break;
                    case "wired":
                        this.face = CowFaces.GetCowFace(CowFaces.cowFaces.wired);
                        break;
                    case "young":
                        this.face = CowFaces.GetCowFace(CowFaces.cowFaces.young);
                        break;
                }
            }

            if (!string.IsNullOrEmpty(this.eyes) && string.IsNullOrWhiteSpace(this.face.Eyes))
                this.face = new CowFace(this.eyes);

            if (string.IsNullOrEmpty(this.face.Eyes)) this.face = CowFaces.GetCowFace(CowFaces.cowFaces.defaultFace);

            if (!string.IsNullOrEmpty(this.tongue) && string.IsNullOrWhiteSpace(this.face.Tongue))
                this.face.Tongue = this.tongue;

            if (!string.IsNullOrEmpty(this.cowfile))
            {
                this.cowSpecified = this.cowfile;
                TestCowFile testCowFile = new TestCowFile(ref this.cowSpecified, this.cowFileLocation);
                this.breakOut = testCowFile.breakOut;
            }
            else
            {
                TestCowFile testCowFile = new TestCowFile(ref this.cowSpecified, this.cowFileLocation);
                this.breakOut = testCowFile.breakOut;
            }

            if (this.wrapcolumn < 10 | this.wrapcolumn > 76)
                this.ThrowTerminatingError(
                    new ErrorRecord(
                        new ArgumentOutOfRangeException(
                            nameof(this.wrapcolumn),
                            "Cannot specify a size smaller than 10 characters or larger than 76 characters"),
                        "E1",
                        ErrorCategory.LimitsExceeded,
                        this));

            if (this.think)
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
            if (this.list)
            {
                Console.WriteLine();
                ListCowfiles.ShowCowfiles(this.moduleDirectory, list: true);
                Console.WriteLine();
                this.breakOut = true;
            }
            else if (string.IsNullOrWhiteSpace(this.message))
                Console.WriteLine();
            else if (!this.breakOut) this.WriteObject(this.BuildCowsay());
        }

        private Cowsay BuildCowsay()
        {
            string SpeechBubbleReturned = SpeechBubble.ReturnSpeechBubble(
                this.message,
                this.bubbleChars,
                this.wrapcolumn,
                this.figlet);
            string CowReturned = GetCow.ReturnCow(this.cowSpecified, this.bubbleChars, this.face);
            return new Cowsay(CowReturned, SpeechBubbleReturned);
        }
    }
}