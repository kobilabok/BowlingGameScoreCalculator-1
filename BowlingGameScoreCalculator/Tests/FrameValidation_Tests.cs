using BowlingGameScoreCalculator.Code;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BowlingGameScoreCalculator.Tests
{
    [TestClass]
    public class FrameValidation_Tests
    {
        private readonly ConsoleInputValidator consoleInput = new ConsoleInputValidator();

        [TestMethod]
        public void Validate_FrameCantStartWith_SpareSymbol()
        {
            var input = "/|19|9-|X|-8|8/|-6|X|X|X||XX";

            consoleInput.ValidateStringFormat(input).Should().Be("Entered string can't start with this character.\n");
        }

        [TestMethod]
        public void Validate_FrameCantStartWith_BarSymbol()
        {
            var input = "|19|9-|X|-8|8/|-6|X|X|X||XX";

            consoleInput.ValidateStringFormat(input).Should().Be("Entered string can't start with this character.\n");
        }

        [TestMethod]
        public void Validate_FrameCantHaveMoreThanOne_X()
        {
            var input = "X|7/|9-|XX|-8|8/|-6|X|X|9-||";

            consoleInput.ValidateStringFormat(input).Should().Be("Entered string contains two 'X' in one frame.\n");
        }

        [TestMethod]
        public void Validate_FrameScoreCantExceedTen()
        {
            var input = "999|77|93||-8|8/|-6|X|X|9-||";

            consoleInput.ValidateStringFormat(input).Should().Be("Sum of one frame can not exceed 10 points.\n");
        }

        [TestMethod]
        public void Validate_FrameCantHaveMoreThanOne_SpareSymbol()
        {
            var input = "5-|//|9-|X|-8|8/|-6|X|X|9-||";

            consoleInput.ValidateStringFormat(input).Should().Be("Entered string contains two '/' symbols in one frame.\n");
        }

        [TestMethod]
        public void Validate_BonusFrameCanHave_TwoStrikes()
        {
            var input = "X|7/|9-|X|-8|8/|-6|X|X|X||XX";

            consoleInput.ValidateStringFormat(input).Should().Be(string.Empty);
        }

        [TestMethod]
        public void Validate_BonusFrameCantHave_TwoSpare()
        {
            var input = "X|7/|9-|X|-8|8/|-6|X|X|X||//";

            consoleInput.ValidateStringFormat(input).Should().Be("Entered string contains two '/' symbols in one frame.\n");
        }
    }
}
