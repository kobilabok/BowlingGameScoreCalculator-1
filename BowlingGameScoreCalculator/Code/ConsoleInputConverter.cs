using System;
using System.Collections.Generic;

namespace BowlingGameScoreCalculator.Code
{
    /// <summary>
    /// This class takes game string in valid format and converts it into a list of integers.
    /// </summary>
    ///
    public class ConsoleInputConverter
    {
        //readonly List<int> pins = new List<int>();

        public List<int> ConvertToPinsKnockedDown(string gameInput)
        {
            List<int> pins = new List<int>();

            char[] characters = gameInput.ToUpper().ToCharArray();


            for (int i = 0; i < characters.Length; i++)
            {
                switch (characters[i])
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
                        pins.Add(10 - ConvertCharToInt(characters[i - 1]));
                        break;                   
                    default:
                        pins.Add(ConvertCharToInt(characters[i]));
                        break;
                }
            }
            return pins;
        }

        private int ConvertCharToInt(Char value)
        {
            return (int)(Char.GetNumericValue(value == '-' ? '0' : value));
        }

        // List of pins would look something like this when converting a game string:

        // " X | 7/ | 9- | X | -8 | 8/ | -6 | X | X | X || 81 "

        // 10, 7, 3, 9, 0, 10, 0, 8, 8, 2, 0, 6, 10, 10, 10, 8, 1
    }
}
