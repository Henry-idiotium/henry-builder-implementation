namespace AcademiaManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Validation
    {
        /// <summary>
        /// Prevents a default instance of the Validation class from being created.
        /// </summary>
        private Validation() { }

        /// <summary>
        /// ListIsNull check if the inputted list is null; if so then it display an error
        /// message and return a true value.
        /// </summary>
        public static bool ListIsNull(List<Academia> list, AcademicPosition position)
        {
            var foo = !list.Where(a => a.AcademiaPosition == position).Any();
            if (foo)
            {
                Console.WriteLine("\b=============================\n" +
                                  $"There are 0 {position}s in the current list!\n" +
                                  "Please add more!");
                Console.ReadKey();
            }
            return foo;
        }

        /// <summary>
        /// FieldIsNullOrWhiteSpace check if the inputted field is null or contains only
        /// whitespace; if so then it display an error message and return true value.
        /// </summary>
        public static bool FieldIsNullOrWhiteSpace(string inputField, string fieldName)
        {
            var foo = string.IsNullOrWhiteSpace(inputField);
            if (foo)
            {
                Console.WriteLine("\n=============================\n" +
                                  $"\nThe {fieldName} is required");
                Console.ReadKey();
            }
            return foo;
        }

        /// <summary>
        /// IsDuplicate check if the matchedList is null; if not so then it display
        /// an error message and return true value.
        /// </summary>
        public static bool IsDuplicate(IEnumerable<Academia> matchedList, string inputField)
        {
            var foo = matchedList.Any();
            if (foo)
            {
                Console.WriteLine("\n=============================\n" +
                                  $"This {inputField} is already existed!");
                Console.ReadKey();
            }
            return foo;
        }

        /// <summary>
        /// AcademiaIsNull check if academia the is null; if so then it display an error
        /// message and return true value.
        /// </summary>
        public static bool AcademiaIsNull(Academia academia, string name)
        {
            var foo = academia is null;
            if (foo)
            {
                Console.WriteLine("\n=============================\n" +
                                  $"ID {name} not exist!");
                Console.ReadKey();
            }
            return foo;
        }

        public static bool ChoiceIsValid(string choice, int rangeLeft, int rangeRight)
        {
            var foo = choice.Count() > 1
                      || !choice.All(char.IsDigit)
                      || string.IsNullOrWhiteSpace(choice)
                      || int.Parse(choice) < rangeLeft
                      || int.Parse(choice) > rangeRight;
            if (foo)
            {
                Console.Clear();
                Console.WriteLine("Invalid choice!");
                Console.ReadKey();
            }
            return foo;
        }
    }
}