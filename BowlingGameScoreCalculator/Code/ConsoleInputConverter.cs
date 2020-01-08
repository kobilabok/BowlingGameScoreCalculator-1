using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGameScoreCalculator.Code
{
    public class ConsoleInputConverter
    {
        /// <summary>
        /// This class takes string from the console and converts it into list of integers.
        /// </summary>
        /// 

        // Option #1: 
        //public List<int> ConvertString { get; set; }

        //public ConvertConsoleInput(string consoleInput)
        //{
        //    ConvertString = ConvertToPinsKnockedDown(consoleInput);
        //}

        //Option #2:
        readonly List<int> pins = new List<int>(21);

        public List<int> ConvertToPinsKnockedDown(string consoleInput)
        {

            // X|7/|9-|X|-8|8/|-6|X|X|X||81

            // X7/

            //var splitInput = consoleInput.Split('|').ToList();

            char[] chars = consoleInput.ToCharArray();


            // Option #1 or without it for Option #2
            // List<int> pins = new List<int>(21);

            for (int i = 0; i < chars.Length; i++)
            {
                switch (chars[i])
                {
                    case '-':
                        pins.Add(0);
                        break;
                    case '/':
                        pins.Add(10 - ConvertCharToInt(chars[i - 1]));
                        break;
                    case 'X':
                        pins.Add(10);
                        break;
                    case '|':
                        break;
                    default:
                        pins.Add(ConvertCharToInt(chars[i]));
                        break;
                }
            }
            return pins;
        }

        private int ConvertCharToInt(Char value) => (int)(Char.GetNumericValue(value));
    }
}
