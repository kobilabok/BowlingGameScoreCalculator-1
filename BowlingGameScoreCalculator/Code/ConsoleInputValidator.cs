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
                throw new InvalidInputException("Game input cannot be blank or have white spaces. Please try again.");
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

        private static void ValidateInputCharacters(string gameInput)
        {
            var validCharacters = new List<char>() { 'X', '|', '-', '/', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            foreach (char character in gameInput)
            {
                if (!validCharacters.Contains(character))
                {
                    throw new InvalidInputException("Entered string contains invalid characters. Please check your string and try again.");
                }
            }
        }

        private static void ValidateFirstCharacter(string gameInput)
        {
            // Should only contains defined valid characters
            var validStarter = new List<char>() { 'X', '-', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            if (!validStarter.Contains(gameInput[0]))
            {
                throw new InvalidInputException("Entered string starts with invalid character. Please check your string and try again.");
            }
        }

        private static void ValidateNumberOfFrames(string gameInput)
        {
            // Game string should contains eleven '|' symbols
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
                throw new InvalidInputException("Entered string is in invalid format. Please check your string and try again.");
            }
        }

        private void ValidateRegularFrame(string regularFrame)
        {
            // Frame cannot start with the Spare symbol
            // marksArray [ "12", "11", "X", "4/", "-8"]
            var marksArray = regularFrame.Split('|');

            if (marksArray.Any(x => x.StartsWith("/")))
            {
                throw new InvalidInputException("Frame cannot start with '/' symbol. Please check your string and try again.");
            }

            if (marksArray.Any(x => !x.Contains("X") && x.Length != 2))
            {
                throw new InvalidInputException("Frame is missing characters. Please check your string and try again.");
            }

            if (marksArray.Any(x => x.Contains("X") && x.Length > 1))
            {
                throw new InvalidInputException("Regular frame cannot contain an 'X' and another symbol. Please check your string and try again.");
            }

            // Regular frame sum cannot exceed ten points
            foreach (var frame in marksArray) 
            {
                ValidateFrameTotal(frame);
            }
        }

        private void ValidateFrameTotal(string frame)
        {
            // Frame consists of one or two characters. ex. [ "12", "11", "X", "4/", "-8"]
            if (!int.TryParse(frame[0].ToString(), out int firstBall))
            {
                return;
            }
            if (!int.TryParse(frame[1].ToString(), out int secondBall))
            {
                return;
            }
            if (firstBall + secondBall > 10)
            {
                throw new InvalidInputException("Sum of regular frame cannot exceed 10 points. Please check your string and try again.");
            }
        }

        private string ValidateBonusFrame(string bonusFrame)
        {
            // Bonus frame cannot start with a spare symbol
            if (bonusFrame.Length > 0)
            {
                if (bonusFrame[0] == '/')
                {
                    throw new InvalidInputException("Bonus frame cannot start with spare symbol. Please check your string and try again.");
                }
            }

            // Bonus frame cannot be longer than two characters
            if (bonusFrame.Length > 2)
            {
                throw new InvalidInputException("Bonus frame cannot have more than two chatacters. Please check your string and try again.");
            }

            return string.Empty;
        }
    }
}
