using BowlingGameScoreCalculator.Code;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BowlingGameScoreCalculator.Tests
{
    [TestClass]
    public class ScoreCalculator_Tests
    {
        [TestMethod]
        public void RollGutterGame_ExpectedScore_Zero()
        {
            var stringInput = "--|--|--|--|--|--|--|--|--|--||";

            var game = new ScoreCalculator(stringInput);

            game.CalculateScore().Should().Be(0);
        }

        [TestMethod]
        public void RollMissAndSpareInSameFrame_ExpectedScore_50()
        {
            var stringInput = "-/|X|X|--|--|--|--|--|--|--||";

            var game = new ScoreCalculator(stringInput);

            game.CalculateScore().Should().Be(50);
        }

        [TestMethod]
        public void RollAllOnes_ExpectedScore_20()
        {
            var stringInput = "11|11|11|11|11|11|11|11|11|11||";

            var game = new ScoreCalculator(stringInput);

            game.CalculateScore().Should().Be(20);
        }

        [TestMethod]
        public void RollAllStrike_ExpectedScore_300()
        {
            var stringInput = "X|X|X|X|X|X|X|X|X|X||XX";

            var game = new ScoreCalculator(stringInput);

            game.CalculateScore().Should().Be(300);
        }

        [TestMethod]
        public void RollAllNineAndMiss_ExpectedScore_90()
        {
            var stringInput = "9-|9-|9-|9-|9-|9-|9-|9-|9-|9-||";

            var game = new ScoreCalculator(stringInput);

            game.CalculateScore().Should().Be(90);
        }

        [TestMethod]
        public void RollAllSpareFives_ExpectedScore_150()
        {
            var stringInput = "5/|5/|5/|5/|5/|5/|5/|5/|5/|5/||5";

            var game = new ScoreCalculator(stringInput);

            game.CalculateScore().Should().Be(150);
        }

        [TestMethod]
        public void RollRandomPins_WithBonus_ExpectedScore_167()
        {
            var stringInput = "X|7/|9-|X|-8|8/|-6|X|X|X||81";

            var game = new ScoreCalculator(stringInput);

            game.CalculateScore().Should().Be(167);
        }

        [TestMethod]
        public void RollRandomPins_WithoutBonus_ExpectedScore_120()
        {
            var stringInput = "5-|7/|9-|X|-8|8/|-6|X|X|5-||";

            var game = new ScoreCalculator(stringInput);

            game.CalculateScore().Should().Be(120);
        }

        [TestMethod]
        public void RollRandomPins_WithStrikeBonus_ExpectedScore_180()
        {
            var stringInput = "X|7/|9-|X|-8|8/|-6|X|X|X||XX";

            var game = new ScoreCalculator(stringInput);

            game.CalculateScore().Should().Be(180);
        }
    }
}
