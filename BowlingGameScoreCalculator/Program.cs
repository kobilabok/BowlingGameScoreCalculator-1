using BowlingGameScoreCalculator.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGameScoreCalculator
{
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

            do
            {
                Console.Write("List of pins: ");
                consoleInput = Console.ReadLine();
            }
            while (!ValidateUserInput(consoleInput));

            // Option #1
            //var convertInput = new ConvertConsoleInput(consoleInput);

            //Option #2
            var convertInput = new ConsoleInputConverter();
            var convertedInput = convertInput.ConvertToPinsKnockedDown(consoleInput);

            var calculateScore = new TenPinScoreCalculator(convertedInput);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Total Score: {calculateScore.CalculateScore()}");
            Console.ResetColor();
        }

        static bool ValidateUserInput(string userInput)
        {
            var validateString = new ConsoleInputValidator();
            var output = validateString.ValidateStringInput(userInput);

            if (!string.IsNullOrEmpty(output))
            {
                Console.WriteLine(output);
                return false;
            }

            return true;
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

                Console.WriteLine("Invalid selection. Please Try again.");
            }

            return quit;
        }
    }
}
