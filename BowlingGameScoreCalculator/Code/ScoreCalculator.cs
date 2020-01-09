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
            int score = 0;
            int throwIndex = 0;
            
            for (int i = 0; i < 10; i++)
            {
                if(IsStrike(throwIndex))
                {
                    score += CalculateStrikeScore(throwIndex);
                    throwIndex++;
                }
                else if(IsSpare(throwIndex))
                {
                    score += CalculateSpareScore(throwIndex);
                    throwIndex += 2;
                }
                else
                {
                    score += CalculateStandardScore(throwIndex);
                    throwIndex += 2;
                }
            }
            return score;
        }

        // Strike - is when the first ball in a frame knocks down all ten pins
        private bool IsStrike(int throwIndex) => Pins[throwIndex] == 10;

        // Spare - is when the second ball in a frame knocks down all ten pins
        private bool IsSpare(int throwIndex) => Pins[throwIndex] + Pins[throwIndex + 1] == 10;


        // The score for the Stike frame is ten plus the total of the pins knocked down in the next two balls.
        private int CalculateStrikeScore(int throwIndex)
        {
            return Pins[throwIndex] + Pins[throwIndex + 1] + Pins[throwIndex + 2]; 
        }


        // The score for the Spare frame is ten plus the number of pins knocked down in the next ball. 
        private int CalculateSpareScore(int throwIndex)
        {
            //I'm calling the GetStrikeScore() here because it uses the same formula to calculate score for 
            // both Strike and Spare. Spare score could be also calculated as outlined in the commented out line listed below.

            //return CalculateStrikeScore(throwIndex);

            return 10 + Pins[throwIndex + 2];
            //return knockedDownPins[throwIndex] + knockedDownPins[throwIndex + 1] + knockedDownPins[throwIndex + 2];
        }


        // Standart score calculates pins knocked on first and second throws. 
        private int CalculateStandardScore(int throwIndex)
        {
            return Pins[throwIndex] + Pins[throwIndex + 1];
        }
    }
}
