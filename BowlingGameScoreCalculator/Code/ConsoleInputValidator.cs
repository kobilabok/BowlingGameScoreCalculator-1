using BowlingGameScoreCalculator.Exceptions;
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

            // String cannot be NULL or Empty
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new InvalidInputException("Cannot be blank or have white spaces. Please try again.");
            }

            // String should meet expected length 
            if (input.Length < 21 || input.Length > 32)
            {
                throw new InvalidInputException("Entered string is either too long or too short. Please try again.");
            }

            ValidateInputCharacters(input);

            ValidateNumberOfFrames(input);

            ValidateFirstCharacter(input);

            // Frame validations:
            var bonusFrameIndex = input.LastIndexOf("||", StringComparison.Ordinal);
            var regularFramesString = input.Substring(0, bonusFrameIndex);
            var bonusFrameString = input.Substring(bonusFrameIndex + 2);

            ValidateRegularFrame(regularFramesString);

            ValidateBonusFrame(bonusFrameString);
        }

        private static void ValidateInputCharacters(string input)
        {
            var validChars = new List<char>() { 'X', '|', '-', '/', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            foreach (char item in input)
            {
                if (!validChars.Contains(item))
                {
                    throw new InvalidInputException("Entered string contains invalid characters.");
                }
            }
        }

        private static void ValidateFirstCharacter(string input)
        {
            // Should only contains defined valid characters
            var validStarter = new List<char>() { 'X', '-', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            if (!validStarter.Contains(input[0]))
            {
                throw new InvalidInputException("Entered string starts with invalid character.");
            }
        }

        private static void ValidateNumberOfFrames(string input)
        {
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
                throw new InvalidInputException("The number of frames is not correct.");
            }
        }

        private void ValidateRegularFrame(string regularFrame)
        {
            // Frame cannot start with the Spare symbol
            var marks = regularFrame.Split('|');

            if (marks.Any(x => x.StartsWith("/")))
            {
                throw new InvalidInputException("Frame cannot start with '/' symbol.");
            }

            if (marks.Any(x => !x.Contains("X") && x.Length != 2))
            {
                throw new InvalidInputException("Frame is missing characters.");
            }

            if (marks.Any(x => x.Contains("X") && x.Length > 1))
            {
                throw new InvalidInputException("Regular frame cannot contain an 'X' and another symbol.");
            }

            // Regular frame sum cannot exceed ten points
            if (marks.Any(x => int.TryParse(x, out var value) && value > 10))
            {
                throw new InvalidInputException("Sum of regular frame cannot exceed 10 points.");
            }
        }

        private string ValidateBonusFrame(string bonusFrame)
        {
            // Bonus frame cannot start with bonus symbol
            if (bonusFrame.Length > 0)
            {
                if (bonusFrame[0] == '/')
                {
                    throw new InvalidInputException("Bonus frame cannot start with spare symbol.");
                }
            }

            return string.Empty;
        }
    }
}
