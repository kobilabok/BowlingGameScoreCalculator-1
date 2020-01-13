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
            var bonusFrame = gameInput.Substring(bonusFrameIndex + 2);

            // Validate regular frame
            ValidateRegularFrame(regularFrameString);

            // Validate bonus frame
            ValidateBonusFrame(bonusFrame, regularFrameString);
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
        private void ValidateRegularFrame(string regularFrameString)
        {
            string[] frameArray = SplitRegularFrameString(regularFrameString);

            // Regular frame cannot start with the Spare symbol
            if (frameArray.Any(x => x.StartsWith("/")))
            {
                throw new InvalidGameInputException("Frame cannot start with '/' symbol. Please check your string and try again.");
            }

            // Regular frame must be two characters long if it's not a Strike
            if (frameArray.Any(x => !x.Contains("X") && x.Length != 2))
            {
                throw new InvalidGameInputException("Frame is missing characters. Please check your string and try again.");
            }

            // Regular frame can be only one character if it's a Strike
            if (frameArray.Any(x => x.Contains("X") && x.Length > 1))
            {
                throw new InvalidGameInputException("Regular frame cannot contain an 'X' and another symbol. Please check your string and try again.");
            }

            // Regular frame sum cannot exceed ten points
            foreach (var frame in frameArray)
            {
                ValidateRegularFrameTotal(frame);
            }
        }
        private string[] SplitRegularFrameString(string regularFrameString)
        {
            return regularFrameString.Split('|');
        }
        private void ValidateRegularFrameTotal(string regularFrameString)
        {
            // Regular frame consists of one or two characters. ex. [ "12", "11"]
            if (!int.TryParse(regularFrameString[0].ToString(), out int firstBall))
            {
                return;
            }
            if (!int.TryParse(regularFrameString[1].ToString(), out int secondBall))
            {
                return;
            }

            // Regular frame sum cannot be greater than ten
            if (firstBall + secondBall > 10)
            {
                throw new InvalidGameInputException("Sum of regular frame cannot exceed 10 points. Please check your string and try again.");
            }
        }
        private void ValidateBonusFrame(string bonusFrame, string regularFrameString)
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

            ValidateBonusFrameTotal(bonusFrame);

            ValidateBonusFrameLengthBasedOfTenthFrame(bonusFrame, regularFrameString);
        }
        private void ValidateBonusFrameLengthBasedOfTenthFrame(string bonusFrame, string regularFrameString)
        {
            var frameArray = SplitRegularFrameString(regularFrameString);
            var tenthFrame = frameArray[9];

            // Bonus frame must have two balls when tenth frame is a Strike
            if (tenthFrame == "X" && bonusFrame.Length != 2)
            {
                throw new InvalidGameInputException("Bonus frame needs two balls because 10th frame is a Strike. Please check your string and try again.");
            }

            // Bonus frame must have one ball when tenth frame is a Spare
            if (tenthFrame.Contains('/') && bonusFrame.Length != 1)
            {
                throw new InvalidGameInputException("Bonus frame needs one ball because 10th frame is a Spare. Please check your string and try again.");
            }

            // Bonus frame can't have any balls if tenth frame isn't Strike or Spare.
            if ((tenthFrame != "X" && !tenthFrame.Contains('/')) && bonusFrame.Length > 0)
            {
                throw new InvalidGameInputException("Bonus frame cannot have balls because 10th frame neither Strike or Spare. Please check your string and try again.");
            }
        }
        private void ValidateBonusFrameTotal(string bonusFrame)
        {
            if (bonusFrame.Length > 0)
            {
                //Bonus frame sum cannot be greater than ten if first ball isn't a Strike
                if (bonusFrame[0] != 'X')
                {
                    if (!int.TryParse(bonusFrame[0].ToString(), out int firstBall))
                    {
                        return;
                    }

                    int secondBall = 0;
                    if (bonusFrame.Length > 1)
                    {
                        if (!int.TryParse(bonusFrame[1].ToString(), out secondBall))
                        {
                            return;
                        }
                    }

                    if (firstBall + secondBall > 10)
                    {
                        throw new InvalidGameInputException("Bonus frame sum cannot exceed 10 points if first ball isn't a strike. Please check your string and try again.");
                    }
                }
            }
        }
    }
}
