using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Text.RegularExpressions;

namespace Lab9
{
    class Program
    {
        static void Main(string[] args)
        {

            // Initializing variables and arrays

            string userInput;
            string again;

            string menuSelect;

            int result = 0;
            int index = 0;

            const int indexOffset = 1;

            List<string> firstNames = new List<string> { "Peter", "Paul", "Mary" };
            List<string> lastNames = new List<string> { "Brown", "Arnold", "Gilfried" };
            List<string> foods = new List<string> { "Bratwurst", "Lentils", "Grilled Asparagus" };
            List<string> hometown = new List<string> { "Rochester", "Mt. Pleasant", "Marquette" };
            List<string> colors = new List<string> { "Marmalade", "Fuschia", "Robin's Egg Blue" };

            List<int> ages = new List<int> { 29, 41, 56 };

            // beginning of program

            Console.WriteLine("Welcome to the Student Information Aggregator!");
            Console.WriteLine();
            Console.WriteLine("=================================================");
            bool continueInput = true;
            while (continueInput)
            {

                bool studentAdded = true;
                while (studentAdded == true)
                {

                    Console.WriteLine($"Please select an option below:");
                    Console.WriteLine();
                    Console.WriteLine($"1) Find information about a student");
                    Console.WriteLine($"2) Add a new student");

                    menuSelect = Console.ReadLine();

                    if (menuSelect == "1")
                    {
                        Console.WriteLine();
                        Console.WriteLine("Which student would you like to learn more about?");
                        Console.WriteLine();

                        DisplayMenu(firstNames, lastNames);

                        Console.WriteLine();

                        bool validInput = true;
                        while (validInput)
                        {

                            userInput = Console.ReadLine();
                            try
                            {
                                result = int.Parse(userInput);
                                index = result - indexOffset;

                                Console.WriteLine();
                                Console.WriteLine($"Student {result} is {firstNames[index]} {lastNames[index]}.");
                                Console.WriteLine($"What would you like to know about {firstNames[index]}? Please select one of the following: ");
                                Console.WriteLine();
                                Console.WriteLine("Age");
                                Console.WriteLine("Hometown");
                                Console.WriteLine("Favorite food");
                                Console.WriteLine("Favorite color");
                                Console.WriteLine();

                                SelectStudent(firstNames, ages, hometown, foods, colors, index);

                                validInput = false;
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Invalid entry. You entered an invalid character. Please make sure you are entering a number.");
                            }
                            catch (IndexOutOfRangeException)
                            {
                                Console.WriteLine("Invalid entry. Please use the number corresponding to the student you would like to learn about.");
                            }
                        }
                    }
                    else if (menuSelect == "2")
                    {
                        Console.WriteLine();
                        Console.WriteLine("Enter new student's first name:");

                        string newFirstName = Console.ReadLine().Trim();
                        firstNames.Add(newFirstName);

                        Console.WriteLine();
                        Console.WriteLine("Enter new student's last name:");

                        string newLastName = Console.ReadLine().Trim();
                        lastNames.Add(newLastName);

                        Console.WriteLine();
                        Console.WriteLine("Enter new student's hometown:");

                        string newHometown = Console.ReadLine().Trim();
                        hometown.Add(newHometown);

                        Console.WriteLine();
                        Console.WriteLine("Enter new student's age:");

                        bool validInput2 = true;
                        while (validInput2 == true)
                        {
                            string ageInput = Console.ReadLine().Trim();
                            try
                            {
                                int newAge = int.Parse(ageInput);
                                ages.Add(newAge);
                                validInput2 = false;
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Invalid entry. Please enter student's age as a number.");
                            }
                        }
                        Console.WriteLine();
                        Console.WriteLine("Enter new student's favorite food:");

                        string newFood = Console.ReadLine().Trim();
                        foods.Add(newFood);

                        Console.WriteLine();
                        Console.WriteLine("Enter new student's favorite color:");

                        string newColor = Console.ReadLine().Trim();
                        colors.Add(newColor);

                        Console.WriteLine();
                        Console.WriteLine("Your new student has successfully been added. Returning to main menu.");
                        studentAdded = false;
                    }
                }

                Console.WriteLine();
                Console.WriteLine($"Would you like to continue using the Student Aggregator? (y/n)");

                bool response = true;
                while (response)
                {
                    again = Console.ReadLine();

                    if (again == "n")
                    {
                        continueInput = false;
                        Console.WriteLine();
                        Console.WriteLine("Thank you. Please remember all information is confidential."); //goodbye message
                        Console.WriteLine();
                        Console.WriteLine("*************************************************");
                        Console.WriteLine();
                        break;
                    }
                    else if (again == "y")
                    {
                        Console.WriteLine();
                        response = false;
                        break;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Error. Please enter either 'y' or 'n'."); //yes/no checker error message
                    }
                }
            }
        }

        public static void DisplayMenu(List<string> firstNames, List<string> lastNames)
        {
            for (int i = 0; i < firstNames.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {firstNames[i]} {lastNames[i]}");
            }
        }

        public static void SelectStudent(List<string> firstNames, List<int> ages, List<string> hometown, List<string> foods, List<string> colors, int index)
        {
            bool preferenceCheck = true;
            while (preferenceCheck)
            {
                string preference = Console.ReadLine().ToLower().Trim();
                Console.WriteLine();
                if (preference.Contains("age"))
                {
                    Console.WriteLine($"{firstNames[index]} is {ages[index]} years old.");
                    preferenceCheck = false;
                }
                else if (preference.Contains("home") || preference.Contains("town"))
                {
                    Console.WriteLine($"{firstNames[index]} is originally from {hometown[index]}.");
                    preferenceCheck = false;
                }
                else if (preference.Contains("food"))
                {
                    Console.WriteLine($"{firstNames[index]}'s favorite food is {foods[index]}.");
                    preferenceCheck = false;
                }
                else if (preference.Contains("color"))
                {
                    Console.WriteLine($"{firstNames[index]}'s favorite color is {colors[index]}.");
                    preferenceCheck = false;
                }
                else
                {
                    Console.WriteLine("Invalid entry. Please enter one of the following: (Age / Hometown / Favorite food/ Favorite color)");
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }

        public static bool ValidEntry(string input)
        {
            Regex regex = new Regex(@"^([A - za - z] +)$");
            return regex.IsMatch(input);
        }
    }
}
