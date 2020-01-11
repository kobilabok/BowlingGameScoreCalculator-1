using BowlingGameScoreCalculator.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BowlingGameScoreCalculator.Code
{
    /// <summary>
    /// This class validates game string format.
    /// </summary>
    public class ConsoleInputValidator
    {
        public void ValidateGameInputFormat(string consoleInput)
        {
            // Converts to upper case
            var gameInput = consoleInput.ToUpper();

            // String cannot be NULL or Empty
            if (string.IsNullOrWhiteSpace(gameInput))
            {
                throw new InvalidGameInputException("Game input cannot be blank or have white spaces. Please try again.");
            }

            // Validate that game string only contains valid characters 
            ValidateInputCharacters(gameInput);

            // Validate number of frames in game string
            ValidateNumberOfFrames(gameInput);

            // Validate first character in game string
            ValidateFirstCharacter(gameInput);

            // Separate regular and bonus frames
            var bonusFrameIndex = gameInput.LastIndexOf("||", StringComparison.Ordinal);
            var regularFrameString = gameInput.Substring(0, bonusFrameIndex);
            var bonusFrameString = gameInput.Substring(bonusFrameIndex + 2);

            // Validate regular frame
            ValidateRegularFrame(regularFrameString);

            // Validate bonus frame
            ValidateBonusFrame(bonusFrameString);
        }

        private void ValidateInputCharacters(string gameInput)
        {
            // Game string could only consist of the defined valid characters
            var validCharacters = new List<char>() { 'X', '|', '-', '/', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            foreach (char character in gameInput)
            {
                if (!validCharacters.Contains(character))
                {
                    throw new InvalidGameInputException("Entered string contains invalid characters. Please check your string and try again.");
                }
            }
        }

        private void ValidateFirstCharacter(string gameInput)
        {
            // Game string could only start with one of the defined valid characters
            var validStarter = new List<char>() { 'X', '-', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            if (!validStarter.Contains(gameInput[0]))
            {
                throw new InvalidGameInputException("Entered string starts with invalid character. Please check your string and try again.");
            }
        }

        private void ValidateNumberOfFrames(string gameInput)
        {
            // Game string should contain eleven '|' symbols, else it's invalid string.
            int barSymbolsCount = 0;

            for (int i = 0; i < gameInput.Length; i++)
            {
                if (gameInput[i] == '|')
                {
                    barSymbolsCount++;
                }
            }

            if (barSymbolsCount != 11)
            {
                throw new InvalidGameInputException("Entered string is in invalid format. Please check your string and try again.");
            }
        }

        private void ValidateRegularFrame(string regularFrame)
        {
            // Regular frame cannot start with the Spare symbol
            // marksArray [ "12", "11", "X", "4/", "-8"]
            var marksArray = regularFrame.Split('|');

            if (marksArray.Any(x => x.StartsWith("/")))
            {
                throw new InvalidGameInputException("Frame cannot start with '/' symbol. Please check your string and try again.");
            }

            if (marksArray.Any(x => !x.Contains("X") && x.Length != 2))
            {
                throw new InvalidGameInputException("Frame is missing characters. Please check your string and try again.");
            }

            if (marksArray.Any(x => x.Contains("X") && x.Length > 1))
            {
                throw new InvalidGameInputException("Regular frame cannot contain an 'X' and another symbol. Please check your string and try again.");
            }

            // Regular frame sum cannot exceed ten points
            foreach (var frame in marksArray) 
            {
                ValidateRegularFrameTotal(frame);
            }
        }

        private void ValidateRegularFrameTotal(string regularFrame)
        {
            // Regular frame consists of one or two characters. ex. [ "12", "11", "X", "4/", "-8"]
            if (!int.TryParse(regularFrame[0].ToString(), out int firstBall))
            {
                return;
            }
            if (!int.TryParse(regularFrame[1].ToString(), out int secondBall))
            {
                return;
            }

            // Regular frame sum cannot be greater than ten
            if (firstBall + secondBall > 10)
            {
                throw new InvalidGameInputException("Sum of regular frame cannot exceed 10 points. Please check your string and try again.");
            }
        }

        private string ValidateBonusFrame(string bonusFrame)
        {
            // Bonus frame cannot start with a spare symbol
            if (bonusFrame.Length > 0)
            {
                if (bonusFrame[0] == '/')
                {
                    throw new InvalidGameInputException("Bonus frame cannot start with spare symbol. Please check your string and try again.");
                }
            }

            // Bonus frame cannot be longer than two characters
            if (bonusFrame.Length > 2)
            {
                throw new InvalidGameInputException("Bonus frame cannot have more than two chatacters. Please check your string and try again.");
            }

            return string.Empty;
        }
    }
}
