using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using Teal_Way_RPG.GameData;

namespace Teal_Way_RPG
{
    public class Utils
    {
        private static Random random = new Random();
        public static ConsoleColor DefaultForeColor = ConsoleColor.Gray;
        public static ConsoleColor DefaultBackColor = ConsoleColor.Black;
        public static object Text;
        public static object Input;

        /// <summary>
        /// Writes text with line break
        /// </summary>
        public static void CW(string text)
        {
            Console.WriteLine(text);
        }


        public static void CW(object textAsObject)
        {
            Console.WriteLine(textAsObject.ToString());
        }

        /// <summary>
        /// Writes text WITHOUT line break
        /// </summary>
        public static void Cw(string text)
        {
            Console.Write(text);
        }

        /// <summary>
        /// Writes text FROM new line. Can add extra line.
        /// </summary>
        public static void CWL(string text)
        {
            Console.Write(Environment.NewLine + text);
        }

        /// <summary>
        /// Reads input.
        /// </summary>
        public static string CR()
        {
            return Console.ReadLine();
        }

        /// <summary>
        /// Reads first input character.
        /// </summary>
        public static string CK()
        {
            return Console.ReadKey().KeyChar.ToString();
        }

        /// <summary>
        /// Reads first input character and returns it in LOWER case
        /// </summary>
        public static string CKL()
        {
            return Console.ReadKey().KeyChar.ToString().ToLower();
        }

        /// <summary>
        /// Places empty line with line break. Based on CW.
        /// </summary>
        public static void CWE()
        {
            CW("");
        }

        /// <summary>
        /// Writes dummy text 'Press any key'. Based on CW.
        /// </summary>
        public static void PKC()
        {
            CW("Press any key to continue...");
        }

        /// <summary>
        /// Clears console
        /// </summary>
        public static void Clear()
        {
            Console.Clear();
        }

        public static StringBuilder SB(object text)
        {
            return new StringBuilder(text.ToString());
        }

        public static void Sleep(int seconds)
        {
            int timespan = seconds * 1000;
            Thread.Sleep(timespan);
        }

        public static int GetRandom(int max)
        {
            return random.Next(max);
        }

        public static int GetRandom(int min, int max)
        {
            return random.Next(min, max);
        }

        public static void SetColorsToDefault()
        {
            Console.ForegroundColor = DefaultForeColor;
            Console.BackgroundColor = DefaultBackColor;
        }

        public static void SetColorsTo(ConsoleColor colorFore = ConsoleColor.Gray,
            ConsoleColor colorBack = ConsoleColor.Black)
        {
            Console.ForegroundColor = colorFore;
            Console.BackgroundColor = colorBack;
        }

        public static void DoNothing()
        {

        }

        public static bool BoolReverser(ref bool boolean)
        {
            if (boolean)
            {
                boolean = false;
                return boolean;
            }
            else
            {
                boolean = true;
                return boolean;
            }
        }

        public static string StringTypeCutter(string @string, char delimiter)
        {
            var lastIndex = @string.LastIndexOf(delimiter);
            return @string.Substring(lastIndex + 1);
        }

        public static string DataCloser(int symbolNumber)
        {
            string lineCloser = "-";
            //Cw("-");
            for (var i = 0; i < symbolNumber - 1; i++)
            {
                //Cw("-");
                //lineCloser = lineCloser.Insert(i, lineCloser);
                lineCloser = lineCloser + "-";
            }

            return lineCloser;
        }

        public static void WrongInput()
        {
            CW("Wrong input detected. Please, try again!");
            CR();
        }

        /// <summary>
        /// Used for defining Wrong Input validation with specified data.
        /// </summary>
        /// <param name="textAsObject">Custom object-wrapped message of choice. Wrap will be auto-deconstructed.</param>
        /// <param name="keys">Valid keys of choice.</param>
        /// <returns></returns>
        public static string WrongInput(object textAsObject, params object[] keys)
        {
            //int iterator = 0;
            StringBuilder result;
            do
            {
                //if (iterator > 0)
                //{
                //    Input = UtilsExtra.WrongInputParser(textAsObject.ToString(), keys).Item2;
                //}
                Clear();
                CW("Wrong input detected. Please, try again!");
                CKL();
                //iterator = 1;
                result = SB(UtilsExtra.WrongInputParser(textAsObject.ToString(), keys));
            } while (result.ToString() == "");

            Clear();
            return result.ToString();
        }

        public static bool TryCatch(string methodSignature, object obj)
        {
            int result = 0;
            var type = obj.GetType();
            switch (type.ToString())
            {
                case "System.Int32":
                    try
                    {
                        Convert.ToInt32(obj);
                    }
                    catch
                    {
                        CW("Method \"" + methodSignature + "\" tried to parse to Int32 object \"" + obj + $"\", but it wasn't {type} type.");
                        CW("It was intended that type has to be another type.");
                        CW("This is a debug message. Nothing was lost, but consider it as a warning.");
                        CWL("Press any key to acknowledge this message and continue...");
                        CR();
                        Clear();
                        result = 1;
                    }
                    break;
                case "System.String":
                    try
                    {
                        obj.ToString();
                    }
                    catch
                    {
                        CW("Method \"" + methodSignature + "\" tried to parse to String object \"" + obj + $"\", but it wasn't {type} type.");
                        CW("It was intended that type has to be another type.");
                        CW("This is a debug message. Nothing was lost, but consider it as a warning.");
                        CWL("Press any key to acknowledge this message and continue...");
                        CR();
                        Clear();
                        result = 1;
                    }
                    break;
                case "System.Char":
                    try
                    {
                        Char.Parse(obj.ToString());
                    }
                    catch
                    {
                        CW("Method \"" + methodSignature + "\" tried to parse to Char object \"" + obj + $"\", but it wasn't {type} type.");
                        CW("It was intended that type has to be another type.");
                        CW("This is a debug message. Nothing was lost, but consider it as a warning.");
                        CWL("Press any key to acknowledge this message and continue...");
                        CR();
                        Clear();
                        result = 1;
                    }
                    break;
            }

            if (result == 0)
            {
                return false;
            }
            
            return true;
        }
    }

    public class UtilsExtra : Utils
    {
        public static int GetMostElementsNumber()
        {
            int number = Artifacts.ArtifactList.Count;
            if (number > Currencies.CurrencyList.Count)
                DoNothing();
            else
                number = Currencies.CurrencyList.Count;

            if (number > Effects.EffectList.Count)
                DoNothing();
            else
                number = Effects.EffectList.Count;

            if (number > Locations.LocationList.Count)
                DoNothing();
            else
                number = Locations.LocationList.Count;

            if (number > Monsters.MonsterList.Count)
                DoNothing();
            else
                number = Monsters.MonsterList.Count;

            if (number > Potions.PotionList.Count)
                DoNothing();
            else
                number = Potions.PotionList.Count;

            if (number > Towns.TownList.Count)
                DoNothing();
            else
                number = Towns.TownList.Count;

            //if (number > Currencies.CurrencyList.Count) /TODO quests
            //    DoNothing();
            //else
            //    number = Currencies.CurrencyList.Count;

            return number;
        }

        public static string TryParseStringList(List<string> list, int iterator, string baseIdFormat = "")
        {
            var output = "";
            //for (var i = 0; i < iterator; i++)
            //{
                try
                {
                    output = list[iterator];
                }
                catch
                {
                    return DataCloser(baseIdFormat.Length);
            }
            //}

            return output;
        }

        public static string WrongInputParser(string text, object[] keys)
        {
            Clear();
            CW(text);
            Input = CKL();
            if (keys.Any(key => Input.ToString() == key.ToString()))
            {
                return Input.ToString();
            }
            return "";
        }
    }
}
