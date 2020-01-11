using System;

namespace BowlingGameScoreCalculator.Exceptions
{
    public class InvalidInputException : Exception
    {
        public InvalidInputException(string message) : base (message)
        {

        }
    }
}
