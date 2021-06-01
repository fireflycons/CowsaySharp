namespace CowsaySharp.GetCowsay
{
    using System;
    using System.IO;
    using System.Management.Automation;
    using System.Reflection;

    using CowsaySharp.ConsoleLibrary;
    using CowsaySharp.Library;

    [Cmdlet(VerbsCommon.Get, "Cowsay")]
    [OutputType(typeof(CowSay))]
    public class GetCowsayCmdlet : Cmdlet
    {
        bool breakOut;

        string cowFileLocation;

        string moduleDirectory;

        private Options options =
            new Options { Face = new DefaultCowFace(), BubbleChars = new SayBubbleChars(), Width = 40 };

        [Parameter]
        [Alias("f")]
        public string Cowfile
        {
            get => this.options.CowFile;
            set => this.options.CowFile = value;
        }

        [Parameter]
        [Alias("e")]
        public string Eyes
        {
            get => this.options.Face.Eyes; 
            set => this.options.Face.Eyes = value;
        }

        [Parameter]
        [Alias("n")]
        public SwitchParameter Figlet
        {
            get => this.options.IsFiglet; 
            set => this.options.IsFiglet = value;
        }

        [Parameter]
        [Alias("l")]
        public SwitchParameter List { get; set; }

        [Parameter(ValueFromPipeline = true, Position = 0)]
        public string Message
        {
            get => this.options.Message; 
            set => this.options.Message = value;
        }

        [Parameter]
        [ValidateSet("Borg", "Dead", "Greedy", "Paranoid", "Stoned", "Tired", "Wired", "Young")]
        [Alias("m")]
        public string Mode
        {
            get => this.options.Face.ToString();
            set
            {
                if (Enum.TryParse<CowFaces.FaceTypes>(value, true, out var enumValue))
                {
                    this.options.Face = CowFaces.GetCowFace(enumValue);
                }
            }
        }

        [Parameter]
        public SwitchParameter Think
        {
            get => this.options.BubbleChars.GetType() == typeof(ThinkBubbleChars); 
            set => this.options.BubbleChars = value ? (IBubbleChars)new ThinkBubbleChars() : new SayBubbleChars();
        }

        [Parameter]
        [Alias("T")]
        public string Tongue
        {
            get => this.options.Face.Tongue; 
            set => this.options.Face.Tongue = value;
        }

        [Parameter]
        [Alias("W")]
        [ValidateRange(10, 76)]
        public int Wrapcolumn
        {
            get => this.options.Width; 
            set => this.options.Width = value;
        }

        /// <summary>
        /// Processes the record.
        /// </summary>
        protected override void ProcessRecord()
        {
            if (this.List)
            {
                Console.WriteLine();
                ListCowfiles.ShowCowfiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), listInColumns: true);
                Console.WriteLine();
                return;
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

        /// <summary>
        /// Builds the cowsay.
        /// </summary>
        /// <returns>New <see cref="CowSay"/></returns>
        private CowSay BuildCowsay()
        {
            return new CowSay(this.options);
        }
    }
}