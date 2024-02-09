using Aspose.OCR.Cloud.CommandLineTool.API;
using Aspose.OCR.Cloud.SDK.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Aspose.OCR.Cloud.CommandLineTool
{
    public static class ArgParser
    {

        public static void ParseArgs(string[] args)
        {
            if (args.Length == 0) { Console.WriteLine(GetHelpMessage(Actions.GetHelp)); return; };
            Actions action = ParseAction(args[0]);
            if (Array.IndexOf(args, "help") != -1)
            {
                Console.WriteLine(GetHelpMessage(action));
            }
            else
            {
                switch (action)
                {
                    case Actions.GetHelp:
                        Console.WriteLine(GetHelpMessage(action));
                        break;
                    case Actions.Configure:
                        Config.Configure(args);
                        break;
                    case Actions.RecognizeImage:
                        if (Array.IndexOf(args, "help") != -1)
                        {
                            Console.WriteLine(GetHelpMessage(action));
                        }
                        else
                        {
                            string srcFilePath = GetFilePathFromArgs(args, "-f");
                            string dstFilePath = GetFilePathFromArgs(args, "-o");
                            bool makeBinarization = GetBoolArgumentByFlagFromArgs(args, "-makeBinarization", true);
                            bool makeSkewCorection = GetBoolArgumentByFlagFromArgs(args, "-makeSkewCorrection");
                            bool makeUpsampling = GetBoolArgumentByFlagFromArgs(args, "-makeUpsampling");
                            Language language = GetEnumValueFromArgs<Language>(args);
                            DsrMode dsrMode = GetEnumValueFromArgs<DsrMode>(args);
                            ResultType resultType = GetEnumValueFromArgs<ResultType>(args);
                            RecognizeImage.Run(srcFilePath, 
                                dstFilePath, 
                                language, 
                                makeBinarization, 
                                makeSkewCorection, 
                                makeUpsampling,
                                dsrMode,
                                resultType);
                        }
                        break;
                    default:
                        Console.WriteLine(GetHelpMessage(action));
                        break;
                };
            }
        }


        /// <summary>
        /// Parses action from first argument
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static Actions ParseAction(string value)
        {
            return value switch
            {
                "config" => Actions.Configure,
                "recognizeimage" => Actions.RecognizeImage,
                _ => Actions.Undefined
            };
        }


        public static string GetHelpMessage(Actions action)
        {
            string defaultHelpMessage = "Aspose.OCR.Cloud.CommandLineTool v.2024.01.0\n" +
                "Usage:\n" +
                "1. Configurre credentials at first usage: \'Aspose.OCR.Cloud.CommandLineTool config -clientid <your clinet id> -secret <your client secret>\'\n" +
                "2. Use recognition action: \'Aspose.OCR.Cloud.CommandLineTool recognizeimage -f <Path to your image> -o <Path to the output file> -lang <Recognition language>\'\n" +
                "For more information about actions please type \'Aspose.OCR.Cloud.CommandLineTool <action> help\'";
            string recognizeImageHelpMessage = "Action recognize image help:" +
                "Usage: \'Aspose.OCR.Cloud.CommandLineTool recognizeimage -f <Path to your image> -o <Path to the output file> -lang <Recognition language> <Other settings>\'" +
                "Settings reference:\n" +
                "-f\t\t\t\t\tPath to image file to recognize\n" +
                "-o\t\t\t\t\tPath to output file\n" +
                "-lang\t\t\t\tRecognition language from the following list (English as default): English, German, French, Italian, Spanish, Portuguese, Polish, Slovene, Slovak, Netherlands, Lithuanian, " +
                "Latvian, Danish, Norwegian, Finnish, Serbian, Croatian, Czech, Swedish, Estonian, Romanian, Chinese, Russian, Arabic, Hindi, Ukrainan, Bengali, Tibetan, Thai," +
                " Urdu, Turkish, Korean, Indonesian, Hebrew, Javanese, Greek, Japanese, Persian, Albanian, Latin, Vietnamese, Uzbek, Georgian, Bulgarian, Azerbaijani, Kazah, Macedonian, Belorussian, HWT_eng\n" +
                "-makeBinarization\tImage preprocessing option, true by default. Accepted values: true, false\n" +
                "-makeSkewCorrection\tImage preprocessing option, false by default. Accepted values: true, false\n" +
                "-makeUpsampling\tImage preprocessing option, false by default. Accepted values: true, false\n" +
                "-dsrMode\t\t\tDocument structure recognition mode from the following list (DsrPlusDetector as default): DsrNoFilter, DsrAndFilter, NoDsrNoFilter, TextDetector, DsrPlusDetector, PolygonalTextDetector" +
                "-resultType\t\tOutput data type from the following list (Text as default): Text, Pdf, Hocr";

            return action switch
            {
                Actions.Undefined => "Action argument undefined. Available actions: config, recognizeimage, help. Type \'Aspose.OCR.Cloud.CommandLineTool <action> help\' to get help",
                Actions.GetHelp => defaultHelpMessage,
                Actions.RecognizeImage => recognizeImageHelpMessage,
                _ => defaultHelpMessage
            };
        }




        /// <summary>
        /// Return file path argument value
        /// </summary>
        /// <param name="args"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static string GetFilePathFromArgs(string[] args, string flag)
        {
            if (args.Length == 0) throw new Exception("No arguments provided");
            if (string.IsNullOrEmpty(flag)) throw new Exception("Target flag undefined");

            try
            {
                int outFileFlagIndex = Array.IndexOf(args, flag);
                string srcFilePath = outFileFlagIndex != -1
                    ? args[outFileFlagIndex + 1]
                    : throw new Exception($"Argument \"{flag}\" defined incorrectly");
                if (!IsFilePath(srcFilePath))
                    throw new Exception("File path provided is not an path");
                return srcFilePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to parse source file path: {ex}");
                throw;
            }
        }


        /// <summary>
        /// Checks is file path look like path
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static bool IsFilePath(string input)
        {
            string pattern = @"^(?:[a-zA-Z]:\\|\/|(?:\.{1,2}\/))*(?:[^\/:*?<>|]+(?:\\|\/))*[^\/:*?<>|]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }



        private static bool GetBoolArgumentByFlagFromArgs(string[] args, string flag, bool defaultValue = false)
        {
            try
            {
                if (string.IsNullOrEmpty(flag)) throw new Exception($"Target flag is not passed correctly");
                int flagIndex = Array.IndexOf(args, flag);
                string flagValue = flagIndex != -1
                    ? args[flagIndex + 1]
                    : defaultValue.ToString();

                if (flagValue.StartsWith('-')) return true;
                if (bool.TryParse(flagValue, out bool flagValueParsed))
                {
                    return flagValueParsed;
                }
                else
                {
                    throw new Exception($"'{flagValue}' is not a valid value for flag '{flag}'");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to parse Boolean argument: {ex}");
                throw;
            }

        }


        /// <summary>
        /// Returns value of generic Enum type parsed from args or default
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static T GetEnumValueFromArgs<T>(string[] args)
        {
            try
            {
                string flag = "";
                string defaultStringValue = "";
                if (typeof(T) == typeof(DsrMode))
                {
                    flag = "-dsrMode";
                    defaultStringValue = "DsrPlusDetector";
                }
                else if (typeof(T) == typeof(Language))
                {
                    flag = "-lang";
                    defaultStringValue = "English";
                }
                else if (typeof(T) == typeof(ResultType))
                {
                    flag = "-resultType";
                    defaultStringValue = "Text";
                }
                else
                {
                    throw new Exception("Wrong generic type");
                };


                int flagIndex = Array.IndexOf(args, flag);
                string stringValue = flagIndex != -1
                    ? args[flagIndex + 1]
                    : defaultStringValue;

                if (Enum.TryParse(typeof(T), stringValue, true, out object enumValue))
                {
                    T enumResult = (T)enumValue;
                    return enumResult;
                }
                else
                {
                    throw new Exception($"'{stringValue}' is not a valid value.");
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to parse enum value: {ex}");
                throw;
            }
        }

    }
}
