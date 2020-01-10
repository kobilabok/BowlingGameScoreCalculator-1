using System;
using System.Collections.Generic;
using System.Linq;

namespace BowlingGameScoreCalculator.Code
{
	/// <summary>
	/// This class validates some cases for the invalid input.
	/// </summary>
	public class ConsoleInputValidator
	{
		public void ValidateStringFormat(string consoleInput)
		{
			// Converts to upper case
			var input = consoleInput.ToUpper();

			var validChars = new List<char>() { 'X', '|', '-', '/', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
			var validStarter = new List<char>() { 'X', '-', '1', '2', '3', '4', '5', '6', '7', '8', '9' };



			// Cannot be NULL or Empty
			if (string.IsNullOrWhiteSpace(input))
			{
                throw new Exception("Cannot be blank or have white spaces. Please try again.");
			}

			// Should meet expected length 
			if (input.Length < 21 || input.Length > 32)
			{
                //return "Entered string is either too long or too short. Please try again.\n";
                throw new Exception("Entered string is either too long or too short. Please try again.");
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
                throw new Exception("The number of frames is not correct.");
            }

			// Should only contains defined valid characters
			foreach (char item in input)
			{
				if (!validChars.Contains(item))
				{
                    throw new Exception("Entered string contains invalid characters.");
                }
			}

            // Frame validations:
            var bonusFrameIndex = input.LastIndexOf("||", StringComparison.Ordinal);
            var regularFramesString = input.Substring(0, bonusFrameIndex);
            var bonusFrameString = input.Substring(bonusFrameIndex + 2);

            ValidateRegularFrame(regularFramesString);
            ValidateBonusFrame(bonusFrameString);
		}

		private void ValidateRegularFrame(string regularFrame)
        {
            // Frame cannot start with the Spare symbol
            var marks = regularFrame.Split('|');

            if (marks.Any(x => x.StartsWith("/")))
            {
                throw new Exception("Frame cannot start with '/' symbol.");
            }

            // Regular frame cannot have an 'X' and another symbol
            for (int i = 0; i < regularFrame.Length; i++)
            {
                if (marks.Any(x => x.Contains("X") && x.Length > 1))
                {
                    throw new Exception("Regular frame cannot contain an 'X' and another symbol.");
                }
            }

            // Regular frame sum cannot exceed ten points
            if (marks.Any(x => int.TryParse(x, out var value) && value > 10))
            {
                throw new Exception("Sum of regular frame cannot exceed 10 points.");
            }
        }

        private string ValidateBonusFrame(string bonusFrame)
        {
            // Bonus frame cannot start with bonus symbol
            if (bonusFrame.Length > 0)
            {
                if (bonusFrame[0] == '/')
                {
                    throw new Exception("Bonus frame cannot start with spare symbol.");
                }
            }

            // Bonus frame cannot start with Spare symbol

            //However, bonus frame can be greater than 10.
            //marks = regularFrames.Split('|');

            return string.Empty;
        }

        private void ValidateFirstCharacterIsntSpareSymbol()
        {

        }
	}
}
