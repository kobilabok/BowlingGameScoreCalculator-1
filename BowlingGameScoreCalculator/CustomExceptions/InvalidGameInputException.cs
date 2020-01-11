using System;

namespace BowlingGameScoreCalculator.Exceptions
{
    public class InvalidGameInputException : Exception
    {
        public InvalidGameInputException(string message) : base (message)
        {
        }
    }
}
