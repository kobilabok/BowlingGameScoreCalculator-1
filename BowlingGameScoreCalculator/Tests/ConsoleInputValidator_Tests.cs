using BowlingGameScoreCalculator.Code;
using BowlingGameScoreCalculator.Exceptions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BowlingGameScoreCalculator.Tests
{
    [TestClass]
    public class ConsoleInputValidator_Tests
    {
        private readonly ConsoleInputValidator validator = new ConsoleInputValidator();

        #region Game String Tests
        [TestMethod]
        public void Validate_EmptyString_InvalidGameInputExceptionExpected()
        {
            var gameInput = "";

            Action act = () => validator.ValidateGameInputFormat(gameInput);

            act.Should().Throw<InvalidGameInputException>()
                .WithMessage("Game input cannot be blank or have white spaces. Please try again.");
        }

        [TestMethod]
        public void Validate_WhiteSpacesString_InvalidGameInputExceptionExpected()
        {
            var gameInput = "    ";

            Action act = () => validator.ValidateGameInputFormat(gameInput);

            act.Should().Throw<InvalidGameInputException>()
                .WithMessage("Game input cannot be blank or have white spaces. Please try again.");
        }

        [TestMethod]
        public void Validate_StringWithInvalidCharacter_InvalidGameInputExceptionExpected()
        {
            var gameInput = "--|S|--|--|--|--|--|--|--|--||";

            Action act = () => validator.ValidateGameInputFormat(gameInput);

            act.Should().Throw<InvalidGameInputException>()
                .WithMessage("Entered string contains invalid characters. Please check your string and try again.");
        }

        [TestMethod]
        public void Validate_StringCannotExceedElevenFrames_InvalidGameInputExceptionExpected()
        {
            var gameInput = "X|X|X|X|X|X|X|X|X|X|X|X|X||XX";

            Action act = () => validator.ValidateGameInputFormat(gameInput);

            act.Should().Throw<InvalidGameInputException>()
                .WithMessage("Entered string is in invalid format. Please check your string and try again.");
        }

        [TestMethod]
        public void Validate_ValidStringInLowerCase_NoExceptionExpected()
        {
            var gameInput = "x|x|x|x|x|x|x|x|x|x||xx";

            Action act = () => validator.ValidateGameInputFormat(gameInput);

            act.Should().NotThrow(because: "This frame is valid but in lower case. ");
        }

        [TestMethod]
        public void Validate_ValidString_NoExceptionExpected()
        {
            var gameInput = "--|7/|--|--|--|--|--|--|--|--||";

            Action act = () => validator.ValidateGameInputFormat(gameInput);

            act.Should().NotThrow(because: "String is in valid format. Please check your string and try again.");
        }

        #endregion

        #region Regular Frame Tests

        [TestMethod]
        public void Validate_RegularFrameCantStartWithSpareSymbol_InvalidGameInputExceptionExpected()
        {
            var gameInput = "/|--|--|--|--|--|--|--|--|--||";

            Action act = () => validator.ValidateGameInputFormat(gameInput);

            act.Should().Throw<InvalidGameInputException>()
                .WithMessage("Entered string starts with invalid character. Please check your string and try again.");
        }

        [TestMethod]
        public void Validate_RegularFrameCantStartWithBarSymbol_InvalidGameInputExceptionExpected()
        {
            var gameInput = "|--|--|--|--|--|--|--|--|--||";

            Action act = () => validator.ValidateGameInputFormat(gameInput);

            act.Should().Throw<InvalidGameInputException>()
                .WithMessage("Entered string starts with invalid character. Please check your string and try again.");
        }

        [TestMethod]
        public void Validate_RegularFrameCantHaveMoreThanOneStrikeSymbols_InvalidGameInputExceptionExpected()
        {
            var gameInput = "--|XX|--|--|--|--|--|--|--|--||";

            Action act = () => validator.ValidateGameInputFormat(gameInput);

            act.Should().Throw<InvalidGameInputException>()
                .WithMessage("Regular frame cannot contain an 'X' and another symbol. Please check your string and try again.");
        }

        [TestMethod]
        public void Validate_RegularFrameCantHaveOneCharacterIfItIsNotStrike_InvalidGameInputExceptionExpected()
        {
            var gameInput = "--|8|--|--|--|--|--|--|--|--||";

            Action act = () => validator.ValidateGameInputFormat(gameInput);

            act.Should().Throw<InvalidGameInputException>()
                .WithMessage("Frame is missing characters. Please check your string and try again.");
        }

        [TestMethod]
        public void Validate_RegularFrameScoreCantExceedTen_InvalidGameInputExceptionExpected()
        {
            var gameInput = "--|92|--|--|--|--|--|--|--|--||";

            Action act = () => validator.ValidateGameInputFormat(gameInput);

            act.Should().Throw<InvalidGameInputException>()
                .WithMessage("Sum of regular frame cannot exceed 10 points. Please check your string and try again.");
        }

        [TestMethod]
        public void Validate_RegularFrameCantHaveMoreThanOneSpareSymbol_InvalidGameInputExceptionException()
        {
            var gameInput = "--|//|--|--|--|--|--|--|--|--||";

            Action act = () => validator.ValidateGameInputFormat(gameInput);

            act.Should().Throw<InvalidGameInputException>()
                .WithMessage("Frame cannot start with '/' symbol. Please check your string and try again.");
        }

        #endregion

        #region Bonus Frame Tests

        [TestMethod]
        public void Validate_BonusFrameCanHaveTwoStrikes_NoExceptionExpected()
        {
            var gameInput = "--|--|--|--|--|--|--|--|--|X||XX";

            Action act = () => validator.ValidateGameInputFormat(gameInput);

            act.Should().NotThrow<InvalidGameInputException>();
        }

        [TestMethod]
        public void Validate_BonusFrameCantStartWithSpareSymbol_InvalidGameInputExceptionExpected()
        {
            var gameInput = "--|--|--|--|--|--|--|--|--|X||//";

            Action act = () => validator.ValidateGameInputFormat(gameInput);

            act.Should().Throw<InvalidGameInputException>()
                .WithMessage("Bonus frame cannot start with spare symbol. Please check your string and try again.");
        }

        [TestMethod]
        public void Validate_BonusFrameCantHaveMoreThanTwoCharacters_InvalidGameInputExceptionExpected()
        {
            var gameInput = "--|--|--|--|--|--|--|--|--|X||234";

            Action act = () => validator.ValidateGameInputFormat(gameInput);

            act.Should().Throw<InvalidGameInputException>()
                .WithMessage("Bonus frame cannot have more than two chatacters. Please check your string and try again.");
        }

        [TestMethod]
        public void Validate_BonusFrameSumCantExceedTenIfFirstBallIsNotStrike_InvalidGameInputExceptionExpected()
        {
            var gameInput = "--|--|--|--|--|--|--|--|--|X||38";

            Action act = () => validator.ValidateGameInputFormat(gameInput);

            act.Should().Throw<InvalidGameInputException>()
                .WithMessage("Bonus frame sum cannot exceed 10 points if first ball isn't a strike. Please check your string and try again.");
        }

        [TestMethod]
        public void Validate_BonusFrameMustHaveTwoBallsWhenTenthFrameIsStrike_InvalidGameInputExceptionExpected()
        {
            var gameInput = "--|--|--|--|--|--|--|--|--|X||";

            Action act = () => validator.ValidateGameInputFormat(gameInput);

            act.Should().Throw<InvalidGameInputException>()
                .WithMessage("Bonus frame needs two balls because 10th frame is a Strike. Please check your string and try again.");
        }

        [TestMethod]
        public void Validate_BonusFrameMustHaveOneBallWhenTenthFrameIsSpare_InvalidGameInputExceptionExpected()
        {
            var gameInput = "--|--|--|--|--|--|--|--|--|4/||";

            Action act = () => validator.ValidateGameInputFormat(gameInput);

            act.Should().Throw<InvalidGameInputException>()
                .WithMessage("Bonus frame needs one ball because 10th frame is a Spare. Please check your string and try again.");
        }

        [TestMethod]
        public void Validate_BonusFrameMustHaveNoBallsWhenTenthFrameNeitherStrikeOrSpare_InvalidGameInputExceptionExpected()
        {
            var gameInput = "--|--|--|--|--|--|--|--|--|4-||35";

            Action act = () => validator.ValidateGameInputFormat(gameInput);

            act.Should().Throw<InvalidGameInputException>()
                .WithMessage("Bonus frame cannot have balls because 10th frame neither Strike or Spare. Please check your string and try again.");
        }
    }
    #endregion
}
