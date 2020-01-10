using System.Collections.Generic;


namespace BowlingGameScoreCalculator.Code
{
    public class ScoreCalculator
    {
        private List<int> Pins { get; set; }

        public ScoreCalculator(string stringGameInput) 
        {
            Pins = new ConsoleInputConverter(stringGameInput).ConvertToPinsKnockedDown();
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
            // When calculating the score, I'm on the first ball of the frame, the sum of current ball and the second ball 
            // would ten and in order to satisfy Spare i have to jusp over a second ball of current frame and grab first ball
            // of next ball. 
            return 10 + Pins[throwIndex + 2];
        }


        // Standart score calculates pins knocked on first and second throws. 
        private int CalculateStandardScore(int throwIndex)
        {
            return Pins[throwIndex] + Pins[throwIndex + 1];
        }
    }
}
