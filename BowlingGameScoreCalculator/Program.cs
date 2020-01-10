using BowlingGameScoreCalculator.Code;
using BowlingGameScoreCalculator.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGameScoreCalculator
{
    /// <summary>
    /// Basic validations have been added but I assume that string will be in valid format.
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
                "X|7/|9-|X|-8|8/|-6|X|X|X||81",
                "X|X|X|X|X|X|X|X|X|X||XX",
                "9-|9-|9-|9-|9-|9-|9-|9-|9-|9-||",
                "5/|5/|5/|5/|5/|5/|5/|5/|5/|5/||5",
                "5-|7/|9-|X|-8|8/|-6|X|X|X||81",
                "X|7/|9-|X|-8|8/|-6|X|X|X||XX",
                "--|--|--|--|--|--|--|--|--|--||"
            };

            Console.WriteLine("Examples:\n");

            foreach (string item in listExamples)
                Console.WriteLine(item);

            Console.Write(Environment.NewLine);
        }

        static void CalculateTotalScore()
        {
            string consoleInput;

            try
            {
                Console.Write("List of pins: ");

                consoleInput = Console.ReadLine();
                var getScore = new ScoreCalculator(consoleInput);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Total Score: {getScore.CalculateScore()}");
            }
            catch (InvalidInputException ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"Input error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ResetColor();
            }
        }

        static bool ShouldCalculateAnotherGame()
        {
            bool quit = false;
            char selection = ' ';

            while (!selection.Equals('Y') && !selection.Equals('N'))
            {
                Console.WriteLine($"\nPlay again? Y / N");
                string userInput = Console.ReadLine().ToUpper();

                if (char.TryParse(userInput, out selection))
                {
                    if (selection.Equals('Y'))
                    {
                        return true;
                    }

                    else if (selection.Equals('N'))
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Thanks for playing! Goodbye!");
                        return false;
                    }
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid selection. Please Try again.");
                Console.ResetColor();
            }

            return quit;
        }
    }
}
