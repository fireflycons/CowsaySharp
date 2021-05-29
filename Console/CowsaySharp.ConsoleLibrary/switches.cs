namespace CowsaySharp.ConsoleLibrary
{
    using System;
    using System.Linq;
    using System.Text;

    using CowsaySharp.Library;

    public static class Switches
    {
        public static void processSwitches(string[] args, string programDir, IBubbleChars bubbleChars)
        {
            var breakOut = false;
            var cowProcessing = false;
            var cowFileTested = false;
            var presetFaceSet = false;
            var isFiglet = false;

            var cowFileLocation = $"{programDir}\\cows";
            var cowSpecified = $"{cowFileLocation}\\default.cow";

            var message = new StringBuilder();

            var face = CowFaces.GetCowFace(CowFaces.FaceTypes.DefaultFace);

            var numberOfArguments = args.Length;
            var columnSize = 40;

            for (var i = 0; i < numberOfArguments; i++)
            {
                var argument = args[i].Contains('-') ? args[i].Remove(args[i].IndexOf('-'), 1) : "!";

                foreach (var arg in argument)
                {
                    switch (arg)
                    {
                        case 'W':
                            if (!cowProcessing)
                            {
                                cowProcessing = true;
                            }

                            columnSize = int.Parse(args[i + 1]);
                            i++;
                            break;
                        case 'n':

                            if (!cowProcessing)
                            {
                                cowProcessing = true;
                            }

                            isFiglet = true;

                            break;

                        case 'b':
                            if (!cowProcessing)
                            {
                                cowProcessing = true;
                            }

                            if (!presetFaceSet)
                            {
                                presetFaceSet = true;
                            }

                            face = CowFaces.GetCowFace(CowFaces.FaceTypes.Borg);

                            break;

                        case 'd':
                            if (!cowProcessing)
                            {
                                cowProcessing = true;
                            }

                            if (!presetFaceSet)
                            {
                                presetFaceSet = true;
                            }

                            face = CowFaces.GetCowFace(CowFaces.FaceTypes.Dead);

                            break;
                        case 'g':
                            if (!cowProcessing)
                            {
                                cowProcessing = true;
                            }

                            if (!presetFaceSet)
                            {
                                presetFaceSet = true;
                            }

                            face = CowFaces.GetCowFace(CowFaces.FaceTypes.Greedy);

                            break;
                        case 'p':
                            if (!cowProcessing)
                            {
                                cowProcessing = true;
                            }

                            if (!presetFaceSet)
                            {
                                presetFaceSet = true;
                            }

                            face = CowFaces.GetCowFace(CowFaces.FaceTypes.Paranoid);

                            break;

                        case 's':
                            if (!cowProcessing)
                            {
                                cowProcessing = true;
                            }

                            if (!presetFaceSet)
                            {
                                presetFaceSet = true;
                            }

                            face = CowFaces.GetCowFace(CowFaces.FaceTypes.Stoned);

                            break;

                        case 't':
                            if (!cowProcessing)
                            {
                                cowProcessing = true;
                            }

                            if (!presetFaceSet)
                            {
                                presetFaceSet = true;
                            }

                            face = CowFaces.GetCowFace(CowFaces.FaceTypes.Tired);

                            break;

                        case 'w':
                            if (!cowProcessing)
                            {
                                cowProcessing = true;
                            }

                            if (!presetFaceSet)
                            {
                                presetFaceSet = true;
                            }

                            face = CowFaces.GetCowFace(CowFaces.FaceTypes.Wired);

                            break;
                        case 'y':

                            if (!cowProcessing)
                            {
                                cowProcessing = true;
                            }

                            if (!presetFaceSet)
                            {
                                presetFaceSet = true;
                            }

                            face = CowFaces.GetCowFace(CowFaces.FaceTypes.Young);

                            break;

                        case 'e':
                            if (!cowProcessing)
                            {
                                cowProcessing = true;
                            }

                            if (!presetFaceSet)
                            {
                                face.Eyes = args[i + 1];
                            }

                            i++;
                            break;

                        case 'T':
                            if (!cowProcessing)
                            {
                                cowProcessing = true;
                            }

                            if (string.IsNullOrWhiteSpace(face.Tongue))
                            {
                                face.Tongue = args[i + 1];
                            }

                            i++;
                            break;

                        case 'f':
                            cowSpecified = args[i + 1];

                            var testCowFile = new TestCowFile(ref cowSpecified, cowFileLocation);

                            breakOut = testCowFile.BreakOut;
                            cowProcessing = testCowFile.CowProcessing;
                            cowFileTested = true;

                            i++;
                            break;

                        case 'h':
                            if (!cowProcessing)
                            {
                                Help.DisplayHelp();
                                breakOut = true;
                            }

                            break;

                        case 'l':
                            if (!cowProcessing)
                            {
                                ListCowfiles.ShowCowfiles(programDir, false);
                                breakOut = true;
                            }

                            break;

                        case 'L':

                            if (!cowProcessing)
                            {
                                ListCowfiles.ShowCowfiles(programDir, true);
                                breakOut = true;
                            }

                            break;

                        default:
                            if (!cowProcessing)
                            {
                                cowProcessing = true;
                            }

                            for (var j = i; j < numberOfArguments; j++)
                            {
                                message.Append(args[j] + " ");
                            }

                            i = numberOfArguments;
                            break;
                    }

                    if (breakOut)
                    {
                        break;
                    }
                }
            }

            if (!cowProcessing)
            {
                return;
            }

            if (!cowFileTested)
            {
                var testCowFile = new TestCowFile(ref cowSpecified, cowFileLocation);
                breakOut = testCowFile.BreakOut;
            }

            if (breakOut)
            {
                return;
            }

            var messageAsString = isFiglet ? message.ToString() : message.ToString().Trim();

            var speechBubbleReturned = SpeechBubble.ReturnSpeechBubble(
                messageAsString,
                bubbleChars,
                columnSize,
                isFiglet);
            var cowReturned = GetCow.ReturnCow(cowSpecified, bubbleChars, face);

            Console.WriteLine(speechBubbleReturned + Environment.NewLine + cowReturned);
        }
    }
}