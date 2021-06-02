namespace CowsaySharp.ConsoleLibrary
{
    using System.Linq;
    using System.Text;

    using CowsaySharp.Library;

    /// <summary>
    /// Simple command line parser
    /// </summary>
    public static class Switches
    {
        /// <summary>
        /// Processes the switches.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="programDir">The program directory.</param>
        /// <param name="bubbleChars">The bubble chars.</param>
        /// <returns>An <see cref="Options"/> object, or <c>null</c> if nothing to do.</returns>
        public static Options ProcessSwitches(string[] args, string programDir, IBubbleChars bubbleChars)
        {
            var options = new Options
                              {
                                  BubbleChars = bubbleChars, Face = CowFaces.GetCowFace(CowFaces.FaceTypes.DefaultFace), CowFile = "default"
                              };
            var face = CowFaces.GetCowFace(CowFaces.FaceTypes.DefaultFace);
            var numberOfArguments = args.Length;

            for (var i = 0; i < numberOfArguments; i++)
            {
                var argument = args[i].Contains('-') ? args[i].Remove(args[i].IndexOf('-'), 1) : "!";

                foreach (var arg in argument)
                {
                    switch (arg)
                    {
                        case 'W':

                            options.Width = int.Parse(args[i + 1]);
                            i++;
                            break;

                        case 'n':

                            options.IsFiglet = true;
                            break;

                        case 'b':

                            options.Face = CowFaces.GetCowFace(CowFaces.FaceTypes.Borg);
                            break;

                        case 'd':

                            options.Face = CowFaces.GetCowFace(CowFaces.FaceTypes.Dead);
                            break;

                        case 'g':

                            options.Face = CowFaces.GetCowFace(CowFaces.FaceTypes.Greedy);
                            break;

                        case 'p':

                            options.Face = CowFaces.GetCowFace(CowFaces.FaceTypes.Paranoid);
                            break;

                        case 's':

                            options.Face = CowFaces.GetCowFace(CowFaces.FaceTypes.Stoned);
                            break;

                        case 't':

                            options.Face = CowFaces.GetCowFace(CowFaces.FaceTypes.Tired);
                            break;

                        case 'w':

                            options.Face = CowFaces.GetCowFace(CowFaces.FaceTypes.Wired);
                            break;

                        case 'y':

                            options.Face = CowFaces.GetCowFace(CowFaces.FaceTypes.Young);
                            break;

                        case 'e':

                            if (options.Face.GetType() != typeof(DefaultCowFace))
                            {
                                options.Face.Eyes = args[i + 1];
                            }

                            i++;
                            break;

                        case 'T':

                            if (string.IsNullOrWhiteSpace(face.Tongue))
                            {
                                options.Face.Tongue = args[i + 1];
                            }

                            i++;
                            break;

                        case 'f':
                            options.CowFile = args[i + 1];
                            i++;
                            break;

                        case 'h':

                            Help.DisplayHelp();
                            return null;

                        case 'l':

                            ListCowfiles.ShowCowfiles(programDir, false);
                            return null;

                        case 'L':

                            ListCowfiles.ShowCowfiles(programDir, true);
                            return null;

                        default:

                            var sb = new StringBuilder();
                            for (var j = i; j < numberOfArguments; j++)
                            {
                                sb.Append(args[j] + " ");
                            }

                            options.Message = options.IsFiglet ? sb.ToString() : sb.ToString().Trim();

                            i = numberOfArguments;
                            break;
                    }
                }
            }

            return options;
        }
    }
}