using System;
using System.Collections.Generic;

namespace BowlingGameScoreCalculator.Code
{
    public class ConsoleInputConverter
    {
        /// <summary>
        /// This class takes game string in valid format and converts it into a list of integers.
        /// </summary>
        readonly List<int> pins = new List<int>();

        public List<int> ConvertToPinsKnockedDown(string gameInput)
        {
            char[] chararters = gameInput.ToCharArray();

            for (int i = 0; i < chararters.Length; i++)
            {
                switch (chararters[i])
                {
                    case '|':
                        break;
                    case '-':
                        pins.Add(0);
                        break;
                    case 'X':
                        pins.Add(10);
                        break;
                    case '/':
                        pins.Add(10 - ConvertCharToInt(chararters[i - 1]));
                        break;                   
                    default:
                        pins.Add(ConvertCharToInt(chararters[i]));
                        break;
                }
            }
            return pins;
        }

        private int ConvertCharToInt(Char value) => (int)(Char.GetNumericValue(value == '-' ? '0' : value));
    }
}
