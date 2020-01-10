using BowlingGameScoreCalculator.Code;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BowlingGameScoreCalculator.Tests
{
    [TestClass]
    public class ConsoleInputValidator_Tests
    {
        private readonly ConsoleInputValidator consoleInput = new ConsoleInputValidator();

        [TestMethod]
        public void Validate_EmptyString_MessageExpected()
        {
            var input = "";

            Action act = () => consoleInput.ValidateStringFormat(input);

            act.Should().Throw<Exception>()
                .WithMessage("Cannot be blank or have white spaces. Please try again.");
        }

        [TestMethod]
        public void Validate_WhiteSpacesString_MessageExpected()
        {
            var input = "                    ";

            Action act = () => consoleInput.ValidateStringFormat(input);

            act.Should().Throw<Exception>()
                .WithMessage("Cannot be blank or have white spaces. Please try again.");
        }

        [TestMethod]
        public void Validate_StringLength_StringWithLessThan21Chars_MessageExpected()
        {
            var input = "12|34|56|78|91||";

            Action act = () => consoleInput.ValidateStringFormat(input);

            act.Should().Throw<Exception>()
                .WithMessage("Entered string is either too long or too short. Please try again.");
        }

        [TestMethod]
        public void Validate_StringLength_StringWithGreaterThan32Chars_MessageExpected()
        {
            var input = "12|34|56|78|91|12|34|56|78|91|89||";

            Action act = () => consoleInput.ValidateStringFormat(input);

            act.Should().Throw<Exception>()
                .WithMessage("Entered string is either too long or too short. Please try again.");
        }

        [TestMethod]
        public void Validate_LettersAndNumbersInAllowedRangeString_MessageExpected()
        {
            var input = "12|3|456|789|0qw|erty|uiop|1234|567";

            Action act = () => consoleInput.ValidateStringFormat(input);

            act.Should().Throw<Exception>()
                .WithMessage("Entered string is either too long or too short. Please try again.");
        }

        [TestMethod]
        public void Validate_AllowedRangeWithBarSymbolsOnlyString_MessageExpected()
        {
            var input = "||||||||||||||||||||||";

            Action act = () => consoleInput.ValidateStringFormat(input);

            act.Should().Throw<Exception>()
                .WithMessage("The number of frames is not correct.");
        }

        [TestMethod]
        public void Validate_AllowedRangeWithBarSymbolsAndInvalidCharacterString_MessageExpected()
        {
            var input = "X|X|A|A|A|A|A|A|A|A||";

            Action act = () => consoleInput.ValidateStringFormat(input);

            act.Should().Throw<Exception>()
                .WithMessage("Entered string contains invalid characters.");
        }

        [TestMethod]
        public void Validate_ValidStringInLowerCase_NoMessageExpected()
        {
            var input = "x|x|x|x|x|x|x|x|x|x||xx";

            Action act = () => consoleInput.ValidateStringFormat(input);

            act.Should().NotThrow(because: "This frame is valid but in lower case.");
        }

        [TestMethod]
        public void Validate_ValidString_NoMessageExpected()
        {
            var input = "5-|7/|9-|X|-8|8/|-6|X|X|5-||";

            Action act = () => consoleInput.ValidateStringFormat(input);

            act.Should().NotThrow(because: "String is in valid format.");
        }


        [TestMethod]
        public void Validate_FrameCantStartWith_SpareSymbol()
        {
            var input = "/|19|9-|X|-8|8/|-6|X|X|X||XX";

            Action act = () => consoleInput.ValidateStringFormat(input);

            act.Should().Throw<Exception>()
                .WithMessage("Frame cannot start with '/' symbol.");
        }

        [TestMethod]
        public void Validate_FrameCantStartWith_BarSymbol()
        {
            var input = "|19|9-|X|-8|8/|-6|X|X|X||XX";

            Action act = () => consoleInput.ValidateStringFormat(input);

            act.Should().Throw<Exception>()
                .WithMessage("Sum of regular frame cannot exceed 10 points.");
        }

        [TestMethod]
        public void Validate_FrameCantHaveMoreThanOne_X()
        {
            var input = "X|7/|9-|XX|-8|8/|-6|X|X|9-||";

            Action act = () => consoleInput.ValidateStringFormat(input);

            act.Should().Throw<Exception>()
                .WithMessage("Regular frame cannot contain an 'X' and another symbol.");
        }

        [TestMethod]
        public void Validate_FrameScoreCantExceedTen()
        {
            var input = "999|77|93||-8|8/|-6|X|X|9-||";

            Action act = () => consoleInput.ValidateStringFormat(input);

            act.Should().Throw<Exception>()
                .WithMessage("Sum of regular frame cannot exceed 10 points.");
        }

        [TestMethod]
        public void Validate_FrameCantHaveMoreThanOne_SpareSymbol()
        {
            var input = "5-|//|9-|X|-8|8/|-6|X|X|9-||";

            Action act = () => consoleInput.ValidateStringFormat(input);

            act.Should().Throw<Exception>()
                .WithMessage("Frame cannot start with '/' symbol.");
        }

        [TestMethod]
        public void Validate_BonusFrameCanHave_TwoStrikes()
        {
            var input = "X|7/|9-|X|-8|8/|-6|X|X|X||XX";

            Action act = () => consoleInput.ValidateStringFormat(input);

            act.Should().NotThrow<Exception>();
        }

        [TestMethod]
        public void Validate_BonusFrameCantStartWith_SpareSymbol()
        {
            var input = "X|7/|9-|X|-8|8/|-6|X|X|X||//";

            Action act = () => consoleInput.ValidateStringFormat(input);

            act.Should().Throw<Exception>()
                .WithMessage("Bonus frame cannot start with spare symbol.");
        }
    }
}
