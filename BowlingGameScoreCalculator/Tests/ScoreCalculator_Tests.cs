using BowlingGameScoreCalculator.Code;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BowlingGameScoreCalculator.Tests
{
    [TestClass]
    public class ScoreCalculator_Tests
    {
        [TestMethod]
        public void RollAllZerosGame_ExpectedScore_Zero()
        {
            var gameInput = "--|--|--|--|--|--|--|--|--|--||";
            int gameScore = ConvertStringAndCalculateTotal(gameInput);

            gameScore.Should().Be(0);
        }

        [TestMethod]
        public void RollMissAndSpareInSameFrame_ExpectedScore_50()
        {
            var gameInput = "-/|X|X|--|--|--|--|--|--|--||";
            int gameScore = ConvertStringAndCalculateTotal(gameInput);

            gameScore.Should().Be(50);
        }

        [TestMethod]
        public void RollAllOnes_ExpectedScore_20()
        {
            var gameInput = "11|11|11|11|11|11|11|11|11|11||";
            int gameScore = ConvertStringAndCalculateTotal(gameInput);

            gameScore.Should().Be(20);
        }

        [TestMethod]
        public void RollAllStrike_ExpectedScore_300()
        {
            var gameInput = "X|X|X|X|X|X|X|X|X|X||XX";

            // Validate game string
            new ConsoleInputValidator().ValidateGameInputFormat(gameInput);

            // Convert game string
            var convertor = new ConsoleInputConverter();
            var convertedGameInput = convertor.ConvertToPinsKnockedDown(gameInput);

            // Calculate game total score
            var gameScore = new ScoreCalculator(convertedGameInput).CalculateScore();

            gameScore.Should().Be(300);

            // # 2
            gameInput = "9-|9-|9-|9-|9-|9-|9-|9-|9-|9-||";
            convertedGameInput = convertor.ConvertToPinsKnockedDown(gameInput);
            // Calculate game total score
            gameScore = new ScoreCalculator(convertedGameInput).CalculateScore();

            gameScore.Should().Be(90);
        }

        [TestMethod]
        public void RollAllNineAndMiss_ExpectedScore_90()
        {
            var gameInput = "9-|9-|9-|9-|9-|9-|9-|9-|9-|9-||";
            int gameScore = ConvertStringAndCalculateTotal(gameInput);

            gameScore.Should().Be(90);
        }

        [TestMethod]
        public void RollAllFivesAndSpare_ExpectedScore_150()
        {
            var gameInput = "5/|5/|5/|5/|5/|5/|5/|5/|5/|5/||5";

            int gameScore = ConvertStringAndCalculateTotal(gameInput);

            gameScore.Should().Be(150);
        }

        [TestMethod]
        public void RollRandomPins_WithBonus_ExpectedScore_167()
        {
            var gameInput = "X|7/|9-|X|-8|8/|-6|X|X|X||81";
            int gameScore = ConvertStringAndCalculateTotal(gameInput);

            gameScore.Should().Be(167);
        }

        [TestMethod]
        public void RollRandomPins_WithoutBonus_ExpectedScore_120()
        {
            var gameInput = "5-|7/|9-|X|-8|8/|-6|X|X|5-||";
            int gameScore = ConvertStringAndCalculateTotal(gameInput);

            gameScore.Should().Be(120);
        }

        [TestMethod]
        public void RollRandomPins_WithStrikeBonus_ExpectedScore_180()
        {
            var gameInput = "X|7/|9-|X|-8|8/|-6|X|X|X||XX";

            int gameScore = ConvertStringAndCalculateTotal(gameInput);

            gameScore.Should().Be(180);
        }

        // Helper method
        private int ConvertStringAndCalculateTotal(string gameInput)
        {
            // Validate game string
            new ConsoleInputValidator().ValidateGameInputFormat(gameInput);

            // Convert game string
            var convertedGameInput = new ConsoleInputConverter().ConvertToPinsKnockedDown(gameInput);

            // Calculate game total score
            var gameScore = new ScoreCalculator(convertedGameInput).CalculateScore();

            return gameScore;
        }
    }
}
