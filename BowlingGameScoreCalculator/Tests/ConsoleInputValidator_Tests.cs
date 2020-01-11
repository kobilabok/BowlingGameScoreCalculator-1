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
        public void Validate_EmptyString_InvalidInputExceptionExpected()
        {
            var gameInput = "";

            Action act = () => validator.ValidateGameInputFormat(gameInput);

            act.Should().Throw<InvalidInputException>()
                .WithMessage("Game input cannot be blank or have white spaces. Please try again.");
        }

        [TestMethod]
        public void Validate_WhiteSpacesString_InvalidInputExceptionExpected()
        {
            var gameInput = "    ";

            Action act = () => validator.ValidateGameInputFormat(gameInput);

            act.Should().Throw<InvalidInputException>()
                .WithMessage("Game input cannot be blank or have white spaces. Please try again.");
        }

        [TestMethod]
        public void Validate_AllowedRangeWithInvalidCharacterString_InvalidInputExceptionExpected()
        {
            var gameInput = "--|S|--|--|--|--|--|--|--|--||";

            Action act = () => validator.ValidateGameInputFormat(gameInput);

            act.Should().Throw<InvalidInputException>()
                .WithMessage("Entered string contains invalid characters. Please check your string and try again.");
        }

        [TestMethod]
        public void Validate_StringCannotExceedElevenFrames_InvalidInputExceptionExpected()
        {
            var gameInput = "X|X|X|X|X|X|X|X|X|X|X|X|X||XX";

            Action act = () => validator.ValidateGameInputFormat(gameInput);

            act.Should().Throw<InvalidInputException>()
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
        public void Validate_RegularFrameCantStartWithSpareSymbol_InvalidInputExceptionExpected()
        {
            var gameInput = "/|--|--|--|--|--|--|--|--|--||";

            Action act = () => validator.ValidateGameInputFormat(gameInput);

            act.Should().Throw<InvalidInputException>()
                .WithMessage("Entered string starts with invalid character. Please check your string and try again.");
        }

        [TestMethod]
        public void Validate_RegularFrameCantStartWithBarSymbol_InvalidInputExceptionExpected()
        {
            var gameInput = "|--|--|--|--|--|--|--|--|--||";

            Action act = () => validator.ValidateGameInputFormat(gameInput);

            act.Should().Throw<InvalidInputException>()
                .WithMessage("Entered string starts with invalid character. Please check your string and try again.");
        }

        [TestMethod]
        public void Validate_RegularFrameCantHaveMoreThanOneStrikeSymbols_InvalidInputExceptionExpected()
        {
            var gameInput = "--|XX|--|--|--|--|--|--|--|--||";

            Action act = () => validator.ValidateGameInputFormat(gameInput);

            act.Should().Throw<InvalidInputException>()
                .WithMessage("Regular frame cannot contain an 'X' and another symbol. Please check your string and try again.");
        }

        [TestMethod]
        public void Validate_RegularFrameCantHaveOneCharacterIfItIsNotStrike_InvalidInputExceptionExpected()
        {
            var gameInput = "--|8|--|--|--|--|--|--|--|--||";

            Action act = () => validator.ValidateGameInputFormat(gameInput);

            act.Should().Throw<InvalidInputException>()
                .WithMessage("Frame is missing characters. Please check your string and try again.");
        }

        [TestMethod]
        public void Validate_RegularFrameScoreCantExceedTen_InvalidInputExceptionExpected()
        {
            var gameInput = "--|99|--|--|--|--|--|--|--|--||";

            Action act = () => validator.ValidateGameInputFormat(gameInput);

            act.Should().Throw<InvalidInputException>()
                .WithMessage("Sum of regular frame cannot exceed 10 points. Please check your string and try again.");
        }

        [TestMethod]
        public void Validate_RegularFrameCantHaveMoreThanOneSpareSymbol_InvalidInputExceptionException()
        {
            var gameInput = "--|//|--|--|--|--|--|--|--|--||";

            Action act = () => validator.ValidateGameInputFormat(gameInput);

            act.Should().Throw<InvalidInputException>()
                .WithMessage("Frame cannot start with '/' symbol. Please check your string and try again.");
        }

        #endregion

        #region Bonus Frame Tests

        [TestMethod]
        public void Validate_BonusFrameCanHaveTwoStrikes_NoExceptionExpected()
        {
            var gameInput = "--|--|--|--|--|--|--|--|--|X||XX";

            Action act = () => validator.ValidateGameInputFormat(gameInput);

            act.Should().NotThrow<InvalidInputException>();
        }

        [TestMethod]
        public void Validate_BonusFrameCantStartWithSpareSymbol_InvalidInputExceptionExpected()
        {
            var gameInput = "--|--|--|--|--|--|--|--|--|X||//";

            Action act = () => validator.ValidateGameInputFormat(gameInput);

            act.Should().Throw<InvalidInputException>()
                .WithMessage("Bonus frame cannot start with spare symbol. Please check your string and try again.");
        }

        [TestMethod]
        public void Validate_BonusFrameCantHaveMoreThanTwoCharacters_InvalidInputExceptionExpected()
        {
            var gameInput = "--|--|--|--|--|--|--|--|--|X||342";

            Action act = () => validator.ValidateGameInputFormat(gameInput);

            act.Should().Throw<InvalidInputException>()
                .WithMessage("Bonus frame cannot have more than two chatacters. Please check your string and try again.");
        }
    }
    #endregion
}
