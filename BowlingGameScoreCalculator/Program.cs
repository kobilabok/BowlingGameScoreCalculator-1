using BowlingGameScoreCalculator.Code;
using System;
using System.Collections.Generic;

namespace BowlingGameScoreCalculator
{
    /// <summary>
    /// Program flow:
    /// 1. Read game string input from the console.
    /// 2. Validate game string.
    /// 3. Convert game string.
    /// 4. Calculate Total Score.
    /// 5. Display score to the console.
    /// Note: Basic validations have been added but I assume that string will be in valid format.
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            DisplayCalculatorTitle();

            bool continuePlaying = true;

            while (continuePlaying)
            {
                CalculateTotalScore();
                continuePlaying = ShouldCalculateAnotherGame();
            }

            Console.ReadKey();
        }

        static void DisplayCalculatorTitle()
        {
            Console.WriteLine("*** Welcome to Bowling score calculator ***");
            Console.WriteLine("Please enter a string representing a bowling game.");
            Console.Write(Environment.NewLine);

            var listExamples = new List<string>
            {
                "X|X|X|X|X|X|X|X|X|X||XX",
                "9-|9-|9-|9-|9-|9-|9-|9-|9-|9-||",
                "5/|5/|5/|5/|5/|5/|5/|5/|5/|5/||5",
                "X|7/|9-|X|-8|8/|-6|X|X|X||81"
            };

            Console.WriteLine("Examples:\n");

            foreach (string example in listExamples)
                Console.WriteLine(example);

            Console.Write(Environment.NewLine);
        }

        static void CalculateTotalScore()
        {
            string gameInput;

            do
            {
                Console.Write("Bowling game: ");
                gameInput = Console.ReadLine().ToUpper();
            }
            while (!ValidateGameInput(gameInput));

            // Convert game string
            var convertedGameInput = new ConsoleInputConverter().ConvertToPinsKnockedDown(gameInput);

            // Calculate game total score
            var gameScore = new ScoreCalculator(convertedGameInput).CalculateScore();

            // Display total score
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Total Score: {gameScore}");
            Console.ResetColor();
        }

        static bool ValidateGameInput(string gameInput)
        {
            var validator = new ConsoleInputValidator();

            try
            {
                validator.ValidateGameInputFormat(gameInput);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(ex.Message);

                Console.Write(Environment.NewLine);
                Console.ResetColor();

                return false;
            }

            return true;
        }

        static bool ShouldCalculateAnotherGame()
        {
            bool quitCalculator = false;
            char selection = ' ';

            while (!selection.Equals('Y') && !selection.Equals('N'))
            {
                Console.WriteLine($"\nCalculate another game? Y / N");
                string userInput = Console.ReadLine().ToUpper();

                if (char.TryParse(userInput, out selection))
                {
                    if (selection.Equals('Y'))
                    {
                        Console.Write(Environment.NewLine);
                        return true;
                    }

                    else if (selection.Equals('N'))
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Thanks for using our calculator! Hope to see you soon!");
                        return false;
                    }
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Invalid selection. Please Try again.");
                Console.ResetColor();
            }

            return quitCalculator;
        }
    }
}
