using System;
using System.Collections.Generic;

namespace BowlingGameScoreCalculator.Code
{
    public class ConsoleInputConverter
    {
        /// <summary>
        /// This class takes string in a valid format from the console and converts it into a list of integers.
        /// </summary>
    
        readonly List<int> pins = new List<int>(21);

        public List<int> ConvertToPinsKnockedDown(string consoleInput)
        {

            char[] chars = consoleInput.ToCharArray();

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
