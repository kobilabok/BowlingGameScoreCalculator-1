using System.Collections.Generic;

namespace BowlingGameScoreCalculator.Code
{
    public class ScoreCalculator
    {
        private List<int> Pins { get; set; }
        public ScoreCalculator(List<int> pins) 
        {
            Pins = pins;
        }

        public int CalculateScore()
        {
            int gameScore = 0;
            int throwIndex = 0;
            
            for (int i = 0; i < 10; i++)
            {
                if(IsStrike(throwIndex))
                {
                    gameScore += CalculateStrikeScore(throwIndex);
                    throwIndex++;
                }
                else if(IsSpare(throwIndex))
                {
                    gameScore += CalculateSpareScore(throwIndex);
                    throwIndex += 2;
                }
                else
                {
                    gameScore += CalculateRegularScore(throwIndex);
                    throwIndex += 2;
                }
            }
            return gameScore;
        }

        // Strike - is when the first ball in a frame knocks down all ten pins
        private bool IsStrike(int throwIndex) => Pins[throwIndex] == 10;

        // Spare - is when the first and second balls in a frame knock down all ten pins
        private bool IsSpare(int throwIndex) => Pins[throwIndex] + Pins[throwIndex + 1] == 10;

        // The score for the Stike frame is ten plus the total of the pins knocked down in the next two balls.
        private int CalculateStrikeScore(int throwIndex)
        {
            return Pins[throwIndex] + Pins[throwIndex + 1] + Pins[throwIndex + 2]; 
        }

        // The score for the Spare frame is ten plus the number of pins knocked down in the next ball. 
        private int CalculateSpareScore(int throwIndex)
        {
            // When calculating spare score, I'm on the first ball of the frame, the sum of current ball and the second ball 
            // would be ten and in order to satisfy Spare I have to jump over a second ball of current frame and grab the first ball
            // of the next frame. 
            return 10 + Pins[throwIndex + 2];
        }

        // Regular score calculates pins knocked down on the first and second throws. 
        private int CalculateRegularScore(int throwIndex)
        {
            return Pins[throwIndex] + Pins[throwIndex + 1];
        }
    }
}
