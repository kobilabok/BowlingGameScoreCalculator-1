using BowlingGameScoreCalculator.Code;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGameScoreCalculator.Tests
{
    [TestClass]
    public class TenPinScoreCalculator_Tests
    {
        [TestMethod]
        public void RollGutterGame_ExpectedScore_Zero()
        {
            var stringInput = "--|--|--|--|--|--|--|--|--|--||";

            var convertInput = new ConsoleInputConverter();
            var game = new TenPinScoreCalculator(convertInput.ConvertToPinsKnockedDown(stringInput));

            game.CalculateScore().Should().Be(0);
        }

        [TestMethod]
        public void RollAllOnes_ExpectedScore_20()
        {
            var stringInput = "11|11|11|11|11|11|11|11|11|11||";

            var convertInput = new ConsoleInputConverter();
            var game = new TenPinScoreCalculator(convertInput.ConvertToPinsKnockedDown(stringInput));

            game.CalculateScore().Should().Be(20);
        }

        [TestMethod]
        public void RollAllStrike_ExpectedScore_300()
        {
            var stringInput = "X|X|X|X|X|X|X|X|X|X||XX";

            var convertInput = new ConsoleInputConverter();
            var game = new TenPinScoreCalculator(convertInput.ConvertToPinsKnockedDown(stringInput));

            game.CalculateScore().Should().Be(300);
        }

        [TestMethod]
        public void RollAllNineAndMiss_ExpectedScore_90()
        {
            var stringInput = "9-|9-|9-|9-|9-|9-|9-|9-|9-|9-||";

            var convertInput = new ConsoleInputConverter();
            var game = new TenPinScoreCalculator(convertInput.ConvertToPinsKnockedDown(stringInput));

            game.CalculateScore().Should().Be(90);
        }

        [TestMethod]
        public void RollAllSpareFives_ExpectedScore_150()
        {
            var stringInput = "5/|5/|5/|5/|5/|5/|5/|5/|5/|5/||5";

            var convertInput = new ConsoleInputConverter();
            var game = new TenPinScoreCalculator(convertInput.ConvertToPinsKnockedDown(stringInput));

            game.CalculateScore().Should().Be(150);
        }

        [TestMethod]
        public void RollRandomPins_WithBonus_ExpectedScore_167()
        {
            var stringInput = "X|7/|9-|X|-8|8/|-6|X|X|X||81";

            var convertInput = new ConsoleInputConverter();
            var game = new TenPinScoreCalculator(convertInput.ConvertToPinsKnockedDown(stringInput));

            game.CalculateScore().Should().Be(167);
        }

        [TestMethod]
        public void RollRandomPins_WithoutBonus_ExpectedScore_120()
        {
            var stringInput = "5-|7/|9-|X|-8|8/|-6|X|X|5-||";

            var convertInput = new ConsoleInputConverter();
            var game = new TenPinScoreCalculator(convertInput.ConvertToPinsKnockedDown(stringInput));

            game.CalculateScore().Should().Be(120);
        }

        [TestMethod]
        public void RollRandomPins_WithStrikeBonus_ExpectedScore_180()
        {
            var stringInput = "X|7/|9-|X|-8|8/|-6|X|X|X||XX";

            var convertInput = new ConsoleInputConverter();
            var game = new TenPinScoreCalculator(convertInput.ConvertToPinsKnockedDown(stringInput));

            game.CalculateScore().Should().Be(180);
        }
    }
}
