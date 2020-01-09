using System;
using System.Collections.Generic;
using System.Linq;

namespace BowlingGameScoreCalculator.Code
{
    /// <summary>
    /// This class validates some invalid cases.
    /// </summary>
	public class ConsoleInputValidator
	{
		public string ValidateStringFormat(string consoleInput)
		{
			// Converts to upper case
			var input = consoleInput.ToUpper();

            var validChars = new List<char>() { 'X', '|', '-', '/', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
			var validStarter = new List<char>() { 'X', '-', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

			// Cannot be NULL or Empty
			if (string.IsNullOrWhiteSpace(input))
			{
				return "Cannot be blank or have white spaces. Please try again.\n";
			}

			// Should meet expected length 
			if (input.Length < 21 || input.Length > 32)
			{
				return "Entered string is either too long or too short. Please try again.\n";
			}

            // Should contains eleven '|' symbols
            int countBarSymbols = 0;
			for (int i = 0; i < input.Length; i++)
			{
				if (input[i] == '|')
				{
					countBarSymbols++;
				}
			}

			if (countBarSymbols != 11)
			{
				return "Invalid input. Please Try again.\n";
			}

			// Should only contains defined valid characters
			foreach (char item in input)
			{
				if (!validChars.Contains(item))
				{
					return "Entered string contains invalid characters.\n";
				}
			}

			// Should start with one of the defined allowed characters
			if (!validStarter.Contains(input[0]))
			{
				return "Entered string can't start with this character.\n";
			}


            // Frame can only have one Spare symbol in one frame including the bonus frame
            var marks = input.Split('|');
            if (marks.Any(x => x.Contains("//")))
            {
                return "Entered string contains two '/' symbols in one frame.\n";
            }


            // Frame can only have one Strike symbol.This should still be valid for the bonus frame.
            var bonusFrameIndex = input.LastIndexOf("||", StringComparison.Ordinal);
            var regularFrames = input.Substring(0, bonusFrameIndex);

            marks = regularFrames.Split('|');

			for (int i = 0; i < input.Length; i++)
			{
				if (marks.Any(x => x.Contains("XX")))
				{
					return "Entered string contains two 'X' in one frame.\n";
				}
			}

            // Frame sum can not exceed ten points in one frame. However, bonus frame can be greater than 10.
            marks = regularFrames.Split('|');

            if (marks.Any(x => int.TryParse(x, out var value) && value > 10))
			{
				return "Sum of one frame can not exceed 10 points.\n";
			}

			return string.Empty;
		}
	}
}
